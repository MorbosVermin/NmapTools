package com.waitwha.nmap.utils;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.util.UUID;
import java.util.logging.Logger;

import javax.xml.parsers.ParserConfigurationException;

import org.tmatesoft.sqljet.core.SqlJetException;
import org.tmatesoft.sqljet.core.SqlJetTransactionMode;
import org.tmatesoft.sqljet.core.table.ISqlJetCursor;
import org.tmatesoft.sqljet.core.table.ISqlJetTable;
import org.tmatesoft.sqljet.core.table.SqlJetDb;
import org.xml.sax.SAXException;

import com.waitwha.logging.LogManager;
import com.waitwha.nmap.Host;
import com.waitwha.nmap.NmapRun;
import com.waitwha.nmap.Port;

/**
 * <b>NmapTools</b>: Nmap2Sqlite<br/>
 * <small>Copyright (c)2014 Mike Duncan &lt;<a href="mailto:mike.duncan@waitwha.com">mike.duncan@waitwha.com</a>&gt;</small><p />
 *
 * <pre>
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 * </pre>
 *
 * Parses a given XML file and imports this into a Sqlite v3+ database file.
 *
 * @author Mike Duncan <mike.duncan@waitwha.com>
 * @version $Id$
 * @package com.waitwha.nmap.utils
 */
public class Nmap2Sqlite implements Runnable {

	private final String[] SQL_CREATE_TABLES = {
			"CREATE TABLE scans (ID VARCHAR NOT NULL PRIMARY KEY, SCANNER VARCHAR NOT NULL, ARGUMENTS VARCHAR NOT NULL, START_TIME INT NOT NULL, END_TIME INT DEFAULT 0, VERSION VARCHAR NOT NULL)",
			"CREATE TABLE hosts (IP VARCHAR NOT NULL PRIMARY KEY, HOSTNAME VARCHAR NOT NULL, OS VARCHAR NOT NULL, FIRST_SEEN INT NOT NULL, LAST_SEEN INT NOT NULL)",
			"CREATE TABLE ports (ID INT NOT NULL, PROTOCOL VARCHAR NOT NULL, IP VARCHAR NOT NULL, SERVICE_NAME VARCHAR, FIRST_SEEN INT NOT NULL, LAST_SEEN INT NOT NULL)"
	};
	
	private final String[] SQL_CREATE_INDEXES = {
			"CREATE INDEX scans_index ON scans(START_TIME)",
			"CREATE INDEX hosts_index ON hosts(FIRST_SEEN)",
			"CREATE INDEX ports_index ON ports(FIRST_SEEN)"
	};
	
	private static final Logger log = LogManager.getLogger(Nmap2Sqlite.class);
	private SqlJetDb database;
	private File nmapXml;
	
	public Nmap2Sqlite(File db, File nmapXml)  {
		try {
			boolean exists = db.exists();
			this.database = SqlJetDb.open(db, true);
			this.database.getOptions().setAutovacuum(true);
			log.info(String.format("Successfully opened database at %s.", db));
			
			if(! exists)
				create();
			
		}catch(SqlJetException e) {
			log.severe(String.format("Could not open/read database file %s", db));
			throw new RuntimeException(e);
			
		}
		
		this.nmapXml = nmapXml;
		if(!nmapXml.exists())
			throw new RuntimeException(new FileNotFoundException(String.format("File not found: %s", nmapXml)));
		
	}
	
	/**
	 * Creates the database schema.
	 * 
	 */
	private synchronized void create()  {
		try {
			database.beginTransaction(SqlJetTransactionMode.WRITE);
			
			for(String sql : SQL_CREATE_TABLES)  {
				database.createTable(sql);
				log.fine(String.format("Successfully created table using SQL: %s", sql));
			}
			
			for(String sql : SQL_CREATE_INDEXES)  {
				database.createIndex(sql);
				log.fine(String.format("Successfully created index using SQL: %s", sql));
			}
			
			log.info("Successfully created database schema.");
			
		}catch(SqlJetException e1) {
			log.warning(String.format("Could not create database schema: %s", e1.getMessage()));
			
		}finally{
			try {
				database.commit();
				
			}catch(SqlJetException e1) {
				log.warning(String.format("Could not create database schema: %s", e1.getMessage()));
				
			}
			
		}
	}
	
	/**
	 * Imports Nmap scan information into the <i>scans</i> table.
	 * 
	 * @param nmap	NmapRun object to import.
	 */
	private synchronized void addScan(NmapRun nmap)  {
		try  {
			database.beginTransaction(SqlJetTransactionMode.WRITE);
			ISqlJetTable scans = database.getTable("scans");
			scans.insert(
					UUID.randomUUID().toString(),
					nmap.getScanner(),
					nmap.getArguments(),
					Integer.parseInt(nmap.getStart()),
					nmap.getEndTime(),
					nmap.getVersion()
			);
			log.fine("Successfully imported scan information.");
			
		}catch(SqlJetException e)  {
			log.warning(String.format("Unable to import scan information: %s", e.getMessage()));
			
		}finally{
			try  {
				database.commit();
			
			}catch(SqlJetException e)  {
				log.warning(String.format("Unable to import scan information: %s", e.getMessage()));
				
			}
			
		}
	}
	
	/**
	 * Imports the Host object given into the <i>hosts</i> table.
	 * 
	 * @param host	Host object to import.
	 */
	private synchronized void addHost(Host host)  {
		try  {
			database.beginTransaction(SqlJetTransactionMode.WRITE);
			ISqlJetTable scans = database.getTable("hosts");
			scans.insert(
					host.getAddress().getInetAddress().getHostAddress(),
					host.getHostname(),
					host.getOS().toString(),
					host.getStarttime(),
					host.getEndtime()
			);
			
			log.info(String.format("Successfully added host %s.", host));
			
		}catch(SqlJetException e)  {
			log.warning(String.format("Unable to import host '%s' information: %s", host.getAddress().getInetAddress().getHostAddress(), e.getMessage()));
			
		}finally{
			try  {
				database.commit();
			
			}catch(SqlJetException e)  {
				log.warning(String.format("Unable to import host '%s' information: %s", host.getAddress().getInetAddress().getHostAddress(), e.getMessage()));
				
			}
			
		}
	}
	
	private synchronized void updateHost(Host host)  {
		try  {
			database.beginTransaction(SqlJetTransactionMode.WRITE);
			ISqlJetTable hosts = database.getTable("hosts");
			ISqlJetCursor cursor = hosts.open();
			while(!cursor.eof())  {
				String ip = cursor.getString("IP");
				long firstSeen = cursor.getInteger("FIRST_SEEN");
				if(ip.equals(host.getAddress().getInetAddress().getHostAddress()))  {
					cursor.update(
							host.getAddress().getInetAddress().getHostAddress(),
							host.getHostname(),
							host.getOS().toString(),
							firstSeen,
							host.getEndtime()
					);
					break;
				}
			}
			
			log.info(String.format("Successfully updated host %s.", host.getAddress().getInetAddress().getHostAddress()));
			
		}catch(SqlJetException e)  {
			log.warning(String.format("Could not lookup host '%s': %s", host.getAddress().getInetAddress().getHostAddress(), e.getMessage()));
			
		}finally{
			try  {
				database.commit();
				
			}catch(SqlJetException e)  {
				log.warning(String.format("Could not lookup host '%s': %s", host.getAddress().getInetAddress().getHostAddress(), e.getMessage()));
				
			}
			
		}
	}
	
	/**
	 * Returns true if the Host object given is already within the <i>hosts</i> table.
	 * 
	 * @param host		Host object to search for.
	 * @return				boolean
	 */
	private boolean hostExists(Host host)  {
		boolean found = false;
		try  {
			database.beginTransaction(SqlJetTransactionMode.READ_ONLY);
			ISqlJetTable hosts = database.getTable("hosts");
			ISqlJetCursor cursor = hosts.open();
			while(!cursor.eof())  {
				String ip = cursor.getString("IP");
				if(ip.equals(host.getAddress().getInetAddress().getHostAddress()))  {
					found = true;
					break;
				}
			}
			
		}catch(SqlJetException e)  {
			log.warning(String.format("Could not lookup host '%s': %s", host.getAddress().getInetAddress().getHostAddress(), e.getMessage()));
			
		}finally{
			try  {
				database.commit();
				
			}catch(SqlJetException e)  {
				log.warning(String.format("Could not lookup host '%s': %s", host.getAddress().getInetAddress().getHostAddress(), e.getMessage()));
				
			}
			
		}
		
		return found;
	}
	
	private synchronized void addPort(Host host, Port port)  {
		try  {
			database.beginTransaction(SqlJetTransactionMode.WRITE);
			ISqlJetTable scans = database.getTable("ports");
			scans.insert(
					port.getPortId(),
					port.getProtocol(),
					host.getAddress().getInetAddress().getHostAddress(),
					port.getService().getName(),
					host.getEndtime(),
					host.getEndtime()
			);
			
			log.info(String.format("Successfully imported port %s for host %s information.", port, host));
			
		}catch(SqlJetException e)  {
			log.warning(String.format("Unable to import port information: %s", e.getMessage()));
			
		}finally{
			try  {
				database.commit();
			
			}catch(SqlJetException e)  {
				log.warning(String.format("Unable to import port information: %s", e.getMessage()));
				
			}
			
		}
	}
	
	
	private synchronized void updatePort(Host host, Port port)  {
		try  {
			database.beginTransaction(SqlJetTransactionMode.WRITE);
			ISqlJetTable hosts = database.getTable("ports");
			ISqlJetCursor cursor = hosts.open();
			while(!cursor.eof())  {
				String ip = cursor.getString("IP");
				int id = (int)cursor.getInteger("ID");
				String protocol = cursor.getString("PROTOCOL");
				long firstSeen = cursor.getInteger("FIRST_SEEN");
				if(port.getPortId() == id && port.getProtocol().equalsIgnoreCase(protocol) && host.getAddress().getInetAddress().getHostAddress().equals(ip))  {
					cursor.update(
							port.getPortId(),
							port.getProtocol(),
							host.getAddress().getInetAddress().getHostAddress(),
							port.getService().getName(),
							firstSeen,
							host.getEndtime()
					);
					break;
				}
			}
			
			log.info(String.format("Successfully updated port %s for host %s.", port, host));
			
		}catch(SqlJetException e)  {
			log.warning(String.format("Could not lookup port '%s' for host %s: %s", port, host.getAddress().getInetAddress().getHostAddress(), e.getMessage()));
			
		}finally{
			try  {
				database.commit();
				
			}catch(SqlJetException e)  {
				log.warning(String.format("Could not lookup port '%s' for host %s: %s", port, host.getAddress().getInetAddress().getHostAddress(), e.getMessage()));
				
			}
			
		}
	}
	
	private boolean portExists(Host host, Port port)  {
		boolean found = false;
		try  {
			database.beginTransaction(SqlJetTransactionMode.READ_ONLY);
			ISqlJetTable hosts = database.getTable("ports");
			ISqlJetCursor cursor = hosts.open();
			while(!cursor.eof())  {
				String ip = cursor.getString("IP");
				int id = (int)cursor.getInteger("ID");
				String protocol = cursor.getString("PROTOCOL");
				if(port.getPortId() == id && port.getProtocol().equalsIgnoreCase(protocol) && host.getAddress().getInetAddress().getHostAddress().equals(ip))  {
					found = true;
					break;
				}
			}
			
		}catch(SqlJetException e)  {
			log.warning(String.format("Could not lookup port '%s' for host %s: %s", port, host.getAddress().getInetAddress().getHostAddress(), e.getMessage()));
			
		}finally{
			try  {
				database.commit();
				
			}catch(SqlJetException e)  {
				log.warning(String.format("Could not lookup port '%s' for host %s: %s", port, host.getAddress().getInetAddress().getHostAddress(), e.getMessage()));
				
			}
			
		}
		
		return found;
	}
	
	/**
	 * @see java.lang.Runnable#run()
	 */
	@Override
	public void run() {
		try  {
			NmapRun nmap = NmapRun.getInstance(nmapXml);
			log.info(String.format("Successfully parsed XML file %s. (%d hosts)", nmapXml, nmap.getHosts().size()));
			
			addScan(nmap);
			
			for(Host host : nmap.getHosts())  {
				if(hostExists(host))
					updateHost(host);
				else
					addHost(host);
				
				for(Port port : host.getPorts())  {
					if(portExists(host, port))
						updatePort(host, port);
					else
						addPort(host, port);
					
				}
			}
			
		}catch(IOException e)  {
			log.severe(String.format("Could not read XML file: %s", e.getMessage()));
			
		}catch(ParserConfigurationException e) {
			log.severe(String.format("Could not parse XML file: %s", e.getMessage()));
			
		}catch(SAXException e) {
			log.severe(String.format("Could not parse XML file: %s", e.getMessage()));
			
		}finally{
			//TODO Close the database here?
			
		}
	}
	
	public static final void main(String[] args)  {
		File db = new File(System.getProperty("user.dir"), "scans.db");
		File nmapXml = new File(args[0]);
		new Thread(new Nmap2Sqlite(db, nmapXml)).start();
	}

}
