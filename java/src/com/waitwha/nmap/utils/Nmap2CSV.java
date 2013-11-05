package com.waitwha.nmap.utils;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.PrintWriter;
import java.util.Date;
import java.util.Enumeration;
import java.util.UUID;
import java.util.logging.Level;
import java.util.logging.Logger;
import java.util.zip.ZipEntry;
import java.util.zip.ZipFile;

import com.waitwha.logging.LogManager;
import com.waitwha.nmap.Host;
import com.waitwha.nmap.NmapRun;
import com.waitwha.nmap.Port;

/**
 * <b>NmapTools</b>: Nmap2CSV<br/>
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
 * Quick application to convert a NmapRun (scan results) into CSV format 
 * containing the IP, Hostname, OS, date scanned, and number of ports open. 
 *
 * @author Mike Duncan <mike.duncan@waitwha.com>
 * @version $Id$
 * @package com.waitwha.nmap.utils
 */
public class Nmap2CSV {
	
	private static final Logger log = LogManager.getLogger(Nmap2CSV.class);
	
	/**
	 * Copies the contents of File src to File dst.
	 * 
	 * @param src	File source
	 * @param dst	File destination
	 */
	public static final void copy(File src, File dst, boolean move)  {
		FileInputStream fis = null;
		FileOutputStream fos = null;
		boolean ok = false;
		try  {
			fis = new FileInputStream(src);
			fos = new FileOutputStream(dst);
			byte[] buffer = new byte[2048];
			int c = 0;
			while((c = fis.read(buffer)) != -1)  {
				fos.write(buffer, 0, c);
				fos.flush();
			}
			
			ok = true;
			log.fine(String.format("Successfully copied '%s' -> '%s'.", src, dst));
			
		}catch(IOException e)  {
			log.warning(String.format("Could not copy '%s' -> '%s': %s", src, dst, e.getMessage()));
			
		}finally{
			if(fis != null || fos != null)  {
				try  {
					fis.close();
					fos.close();
				}catch(Exception e) {}
			}
		}
		
		if(ok && move)  {
			if(src.delete())
				log.fine(String.format("Successfully deleted source file: %s", src));
			else
				log.warning(String.format("Unable to delete source file: %s", src));
			
		}
	}
	
	/**
	 * Extracts a given ZipFile to a File (directory).
	 * 
	 * @param src	ZipFile to extract
	 * @param dst	File directory to extract too.
	 * @return	boolean
	 */
	public static final boolean extract(ZipFile src, File dst)  {
		boolean ok = false;
		Enumeration<? extends ZipEntry> entries = src.entries();
		log.fine(String.format("Extracting %s", src));
		
		while(entries.hasMoreElements())  {
			ZipEntry entry = (ZipEntry)entries.nextElement();
			if(entry.isDirectory())  {
				if(new File(dst, entry.getName()).mkdirs())
					log.fine(String.format("[dir ] %s %d", entry.getName(), entry.getSize()));
				
			}else{
				InputStream in = null;
				FileOutputStream out = null;
				byte[] buffer = new byte[2048];
				int c = 0;
				try  {
					in = src.getInputStream(entry);
					out = new FileOutputStream(new File(dst, entry.getName()));
					while((c = in.read(buffer)) != -1)  {
						out.write(buffer, 0, c);
						out.flush();
					}
					
					log.fine(String.format("[file] %s %d", entry.getName(), entry.getSize()));
					ok = true;
					
				}catch(IOException e)  {
					log.warning(String.format("Failed to extract zipfile '%s': %s", src, e.getMessage()));
					
				}finally{
					if(in != null || out != null)  {
						try  {
							in.close();
							out.close();
						}catch(Exception e) {}
					}
					
				}
				
			}
		}
		
		if(ok)
			log.fine(String.format("Successfully extracted zipfile %s -> %s", src, dst));
		
		return ok;
	}
	
	/**
	 * Extracts a given ZipFile to a File (directory).
	 * 
	 * @param src	String path to ZipFile to extract
	 * @param dst	File directory to extract too.
	 * @return	boolean
	 */
	public static final boolean extract(String src, File dst)  {
		try {
			ZipFile file = new ZipFile(src);
			return extract(file, dst);
			
		}catch(IOException e) {
			log.warning(String.format("Could not extract zipfile %s: %s", src, e.getMessage()));
			
		}
		
		return false;
	}
	
	/**
	 * Functions much like the ole' DOS command: deltree. This will 
	 * delete a directory and all of the contents within.
	 * 
	 * @param directory	File directory to delete.
	 * @return	boolean
	 */
	public static final boolean deltree(File directory)  {
		File[] files = directory.listFiles();
		for(File f : files) {
			if(f.isDirectory())
				deltree(f);
			else
				f.delete();
			
		}
		
		return directory.delete();
	}
	
	/**
	 * Converts a Nmap result file (XML) to CSV format.
	 * 
	 * @param nmapXmlResults
	 * @param dst
	 */
	public static final synchronized void convert(File nmapXmlResults, File dst)  {
		File output = new File(dst, nmapXmlResults.getName().replace(".xml", ".csv"));
		PrintWriter out = null;
		
		try {
			out = new PrintWriter(output);
			out.println("IP,Hostname,OS,\"Ports Open\",Date");
			out.flush();
			
			NmapRun run = NmapRun.getInstance(nmapXmlResults);
			for(Host host : run.getHosts())  {
				String ip = host.getAddress().getInetAddress().getHostAddress();
				String name = host.getHostname();
				String os = host.getOS().toString();
				Date date = new Date( (long)host.getEndtime()*1000 );
				int portsOpen = 0;
				for(Port p : host.getPorts())
					if(p.getState().getState().equalsIgnoreCase("open"))
						portsOpen++;
				
				out.println(String.format("%s,%s,\"%s\",%d,\"%s\"", ip, name, os, portsOpen, date.toString()));
				out.flush();
			}
			
			log.info(String.format("%s -> %s", nmapXmlResults, output));
			
		}catch(Exception e) {
			log.warning(String.format("Could not convert Nmap results at %s to CSV format: %s", nmapXmlResults, e.getMessage()));
			
		}finally{
			if(out != null)  {
				try  {
					out.close();
					
				}catch(Exception e) {}
			}
		}
	}
	
	/**
	 * Outputs a helpful message (Console) and then exits with code of one (1).
	 * 
	 */
	public static final void help()  {
		System.out.println(String.format("Syntax: %s [-v] [-z] <nmap_results.xml|inventory.zip>", Nmap2CSV.class.getName()));
		System.exit(1);
	}
	
	/**
	 * Application entry point. 
	 * 
	 * @param args	Arguments.
	 */
	public static final void main(String[] args)  {
		String filename = "";
		boolean verbose = false;
		boolean zipfile = false;
		
		for(int i = 0; i < args.length; i++)  {
			if(args[i].equals("-z"))
				zipfile = true;
			else if(args[i].equals("-v"))
				verbose = true;
			else
				filename = args[i];
			
		}
		
		if(filename.length() == 0)
			help();
		
		if(! new File(filename).exists())  {
			System.err.println(String.format("Error: File does not exist: %s", filename));
			System.exit(1);
		}
		
		if(verbose)
			log.setLevel(Level.FINEST);
		else
			log.setLevel(Level.INFO);
		
		File cwd = new File(System.getProperty("user.dir"));
		
		if(zipfile)  {
			String uuid = UUID.randomUUID().toString();
			String tmpdir = System.getProperty("java.io.tmpdir");
			File wd = new File(tmpdir, uuid);
			
			if(wd.mkdirs())
				log.fine(String.format("Successfully created working directory: %s", wd));
			
			if(extract(filename, wd))  {
				File[] files = wd.listFiles();
				for(File f : files)
					convert(f, cwd);
				
			}
			
			if(deltree(wd))
				log.fine(String.format("Successfully deleted working directory: %s", wd));
			
		}else
			convert(new File(filename), cwd);
		
	}
	
}
