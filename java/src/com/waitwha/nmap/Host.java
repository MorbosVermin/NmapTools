package com.waitwha.nmap;

import java.net.InetAddress;
import java.net.UnknownHostException;
import java.util.ArrayList;

import org.w3c.dom.Element;
import org.w3c.dom.NodeList;

import com.waitwha.xml.ElementNotFoundException;
import com.waitwha.xml.ElementUtils;

/**
 * <b>NmapTools</b>: Host<br/>
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
 * Represents a host scanned.
 *
 * @author Mike Duncan <mike.duncan@waitwha.com>
 * @version $Id$
 * @package com.waitwha.nmap
 */
public class Host {

	/**
	 * Represents the status of the host scanned.
	 * 
	 */
	public class Status {
		
		private String state;
		private String reason;
		
		public Status(Element status)  {
			this.state = status.getAttribute("state");
			this.reason = status.getAttribute("reason");
		}

		/**
		 * @return the state
		 */
		public String getState() {
			return state;
		}

		/**
		 * @return the reason
		 */
		public String getReason() {
			return reason;
		}
		
		@Override
		public String toString()  {
			return this.getState();
		}
		
	}
	
	/**
	 * Represents a hostname for a Host.
	 * 
	 */
	public class Hostname  {
		
		private String name;
		private String type;
		
		public Hostname(Element hostname)  {
			this.name = hostname.getAttribute("name");
			this.type = hostname.getAttribute("type");
		}

		/**
		 * @return the name
		 */
		public String getName() {
			return name;
		}

		/**
		 * @return the type
		 */
		public String getType() {
			return type;
		}
		
		@Override
		public String toString()  {
			return this.getName();
		}
		
	}
	
	/**
	 * Represents a Address element within a Host element. 
	 * 
	 */
	public class Address  {
		
		private InetAddress inetAddress;
		private String type;
		
		public Address(Element address) throws UnknownHostException  {
			this.inetAddress = InetAddress.getByName(address.getAttribute("addr"));
			this.type = address.getAttribute("type");
		}

		/**
		 * @return the inetAddress
		 */
		public InetAddress getInetAddress() {
			return inetAddress;
		}

		/**
		 * @return the type
		 */
		public String getType() {
			return type;
		}
		
		@Override
		public String toString()  {
			return this.inetAddress.getHostAddress();
		}
		
	}
	
	private int starttime;
	private int endtime;
	private Status status;
	private Address address;
	private ArrayList<Hostname> hostnames;
	private ArrayList<Port> ports;
	
	public Host(Element host)  {
		try  {
			this.starttime = Integer.parseInt(host.getAttribute("starttime"));
			this.endtime = Integer.parseInt(host.getAttribute("endtime"));
		
		}catch(NumberFormatException e)  {
			
		}
		
		try  {
			this.address = new Address(ElementUtils.getFirstElementByName(host, "address"));
			
		}catch(ElementNotFoundException e)  {
			
		}catch(UnknownHostException e)  {
			
		}
		
		this.hostnames = new ArrayList<Hostname>();
		this.ports = new ArrayList<Port>();
		try  {
			this.status = new Status(ElementUtils.getFirstElementByName(host, "status"));
			
			NodeList hostnames = 
					ElementUtils.getFirstElementByName(host, "hostnames").getElementsByTagName("hostname");
			for(int i = 0; i < hostnames.getLength(); i++)
				this.hostnames.add(new Hostname((Element)hostnames.item(i)));
			
			NodeList ports = 
					ElementUtils.getFirstElementByName(host, "ports").getElementsByTagName("port");
			for(int i = 0; i < ports.getLength(); i++)
				this.ports.add(new Port((Element)ports.item(i)));
		
		}catch(ElementNotFoundException e)  {
			
		}
	}

	/**
	 * @return the starttime
	 */
	public int getStarttime() {
		return starttime;
	}

	/**
	 * @return the endtime
	 */
	public int getEndtime() {
		return endtime;
	}

	/**
	 * @return the status
	 */
	public Status getStatus() {
		return status;
	}

	/**
	 * @return the address
	 */
	public Address getAddress() {
		return address;
	}

	/**
	 * @return the hostnames
	 */
	public ArrayList<Hostname> getHostnames() {
		return hostnames;
	}

	/**
	 * @return the ports
	 */
	public ArrayList<Port> getPorts() {
		return ports;
	}
	
	@Override
	public String toString()  {
		String hostname = this.address.toString();
		if(this.hostnames.size() > 0)
			hostname = this.hostnames.get(0).toString();
		
		return String.format("%s (%s; %d port(s) scanned)", hostname, this.status, this.ports.size());
	}
	
}
