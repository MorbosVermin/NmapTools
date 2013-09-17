package com.waitwha.nmap;

import java.util.ArrayList;

import org.w3c.dom.Element;
import org.w3c.dom.NodeList;

import com.waitwha.xml.ElementNotFoundException;
import com.waitwha.xml.ElementUtils;

/**
 * <b>NmapTools</b>: Port<br/>
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
 * Represents a port scanned using nmap.
 *
 * @author Mike Duncan <mike.duncan@waitwha.com>
 * @version $Id$
 * @package com.waitwha.nmap
 */
public class Port {
	
	/**
	 * State of the port scanned.
	 * 
	 */
	public class State  {
		
		private String state;
		private String reason;
		private String reason_ttl;
		
		public State(Element state)  {
			this.state = state.getAttribute("state");
			this.reason = state.getAttribute("reason");
			this.reason_ttl = state.getAttribute("reason_ttl");
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

		/**
		 * @return the reason_ttl
		 */
		public String getReason_ttl() {
			return reason_ttl;
		}
		
		@Override
		public String toString()  {
			return this.getState();
		}
		
	}
	
	/**
	 * Service information regarding the port scanned.
	 * 
	 */
	public class Service extends ArrayList<String>  {
		
		private static final long serialVersionUID = 1L;
		private String name;
		private String version;
		private String extraInfo;
		private String osType;
		private String method;
		private String conf;
		
		public Service(Element service)  {
			super();
			this.conf = service.getAttribute("conf");
			this.extraInfo = service.getAttribute("extrainfo");
			this.method = service.getAttribute("method");
			this.name = service.getAttribute("name");
			this.version = service.getAttribute("version");
			this.osType = service.getAttribute("ostype");
			
			NodeList cpes = service.getElementsByTagName("cpe");
			for(int i = 0; i < cpes.getLength(); i++)
				this.add( ((Element)cpes.item(i)).getTextContent() );
			
		}

		/**
		 * @return the name
		 */
		public String getName() {
			return name;
		}

		/**
		 * @return the version
		 */
		public String getVersion() {
			return version;
		}

		/**
		 * @return the extraInfo
		 */
		public String getExtraInfo() {
			return extraInfo;
		}

		/**
		 * @return the osType
		 */
		public String getOsType() {
			return osType;
		}

		/**
		 * @return the method
		 */
		public String getMethod() {
			return method;
		}

		/**
		 * @return the conf
		 */
		public String getConf() {
			return conf;
		}
		
		@Override
		public String toString()  {
			return this.getName();
		}
		
	}
	
	private String protocol;
	private int portId;
	private Service service;
	private State state;
	//TODO handle Script elements.
	
	public Port(Element port)  {
		this.protocol = port.getAttribute("protocol");
		this.portId = Integer.parseInt(port.getAttribute("portid"));
		
		try {
			this.service = new Service(ElementUtils.getFirstElementByName(port, "service"));
			this.state = new State(ElementUtils.getFirstElementByName(port, "state"));
			
		}catch(ElementNotFoundException e) {
			
		}
	}

	/**
	 * @return the protocol
	 */
	public String getProtocol() {
		return protocol;
	}

	/**
	 * @return the portId
	 */
	public int getPortId() {
		return portId;
	}

	/**
	 * @return the service
	 */
	public Service getService() {
		return service;
	}

	/**
	 * @return the state
	 */
	public State getState() {
		return state;
	}
	
	@Override
	public String toString()  {
		return String.format("%s/%d (%s; %s)", this.protocol, this.portId, this.service, this.state);
	}
	
}
