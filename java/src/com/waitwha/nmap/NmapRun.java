package com.waitwha.nmap;

import java.io.File;
import java.io.IOException;
import java.util.ArrayList;
import java.util.logging.Logger;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;

import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.NodeList;
import org.xml.sax.SAXException;

import com.waitwha.logging.LogManager;
import com.waitwha.xml.ElementNotFoundException;
import com.waitwha.xml.ElementUtils;

/**
 * <b>NmapTools</b>: NmapRun<br/>
 * <small>Copyright (c)2013 Mike Duncan &lt;<a href="mailto:mike.duncan@waitwha.com">mike.duncan@waitwha.com</a>&gt;</small><p />
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
 * Represents a NmapRun or scan using the nmap tool.
 *
 * @author Mike Duncan <mike.duncan@waitwha.com>
 * @version $Id$
 * @package com.waitwha.nmap
 */
public class NmapRun {
	
	private static final Logger log = LogManager.getLogger(NmapRun.class.getName());
	
	public class ScanInfo  {
		
		private String type;
		private String protocol;
		private String numServices;
		private String services;
		
		public ScanInfo(Element scanInfo)  {
			this.type = scanInfo.getAttribute("type");
			this.protocol = scanInfo.getAttribute("protocol");
			this.numServices = scanInfo.getAttribute("numservices");
			this.services = scanInfo.getAttribute("services");
		}

		/**
		 * @return the type
		 */
		public String getType() {
			return type;
		}

		/**
		 * @return the protocol
		 */
		public String getProtocol() {
			return protocol;
		}

		/**
		 * @return the numServices
		 */
		public String getNumServices() {
			return numServices;
		}

		/**
		 * @return the services
		 */
		public String getServices() {
			return services;
		}
		
	}
	
	private String scanner;
	private String arguments;
	private String start;
	private String startStr;
	private String version;
	private String xmlOutputVersion;
	private ScanInfo scanInfo;
	private int verbose;
	private int debugging;
	private ArrayList<Host> hosts;
	
	private NmapRun(Element nmaprun)  {
		this.scanner = nmaprun.getAttribute("scanner");
		this.arguments = nmaprun.getAttribute("args");
		this.start = nmaprun.getAttribute("start");
		this.startStr = nmaprun.getAttribute("startstr");
		this.version = nmaprun.getAttribute("version");
		this.xmlOutputVersion = nmaprun.getAttribute("xmloutputversion");
		
		this.hosts = new ArrayList<Host>();
		NodeList hosts = nmaprun.getElementsByTagName("host");
		for(int i = 0; i < hosts.getLength(); i++)
			this.hosts.add(new Host((Element)hosts.item(i)));
		
		log.fine("Parsed "+ this.hosts.size() +" host(s) for scan.");
		
		try {	
			this.scanInfo = new ScanInfo(ElementUtils.getFirstElementByName(nmaprun, "scaninfo"));
			Element verbose = ElementUtils.getFirstElementByName(nmaprun, "verbose");
			this.verbose = Integer.parseInt(verbose.getAttribute("level"));
			Element debugging = ElementUtils.getFirstElementByName(nmaprun, "debugging");
			this.debugging = Integer.parseInt(debugging.getAttribute("level"));
			
		}catch(ElementNotFoundException e) {
			log.fine(e.getMessage());
		}
	}
	
	/**
	 * @return the scanner
	 */
	public String getScanner() {
		return scanner;
	}

	/**
	 * @return the arguments
	 */
	public String getArguments() {
		return arguments;
	}

	/**
	 * @return the start
	 */
	public String getStart() {
		return start;
	}

	/**
	 * @return the startStr
	 */
	public String getStartStr() {
		return startStr;
	}

	/**
	 * @return the version
	 */
	public String getVersion() {
		return version;
	}

	/**
	 * @return the xmlOutputVersion
	 */
	public String getXmlOutputVersion() {
		return xmlOutputVersion;
	}

	/**
	 * @return the scanInfo
	 */
	public ScanInfo getScanInfo() {
		return scanInfo;
	}

	/**
	 * @return the verbose
	 */
	public int getVerbose() {
		return verbose;
	}

	/**
	 * @return the debugging
	 */
	public int getDebugging() {
		return debugging;
	}

	/**
	 * @return the hosts
	 */
	public ArrayList<Host> getHosts() {
		return hosts;
	}

	public static final NmapRun getInstance(File f) throws ParserConfigurationException, SAXException, IOException  {
		DocumentBuilderFactory factory = DocumentBuilderFactory.newInstance();
		DocumentBuilder builder = factory.newDocumentBuilder();
		Document document = builder.parse(f);
		return new NmapRun(document.getDocumentElement());
	}
	
	/**
	 * @param args
	 */
	public static void main(String[] args) {
		try {
			NmapRun scan = NmapRun.getInstance(new File(args[0]));
			for(Host host : scan.getHosts())  {
				System.out.println(host);
				for(Port port : host.getPorts())
					System.out.println("  -> "+ port);
				
				System.out.println();
			}
			
		}catch(ParserConfigurationException e) {
			e.printStackTrace();
		}catch(SAXException e) {
			e.printStackTrace();
		}catch(IOException e) {
			e.printStackTrace();
		}
		
	}

}
