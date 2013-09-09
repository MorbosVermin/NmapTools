<?php
/**
 * Nmap Report (XML) Parsing Library
 * Copyright (c)2013 Mike Duncan <mike.duncan@waitwha.com>
 *
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
 */

/**
 * Parsing exception class.
 *
 */
class NmapParsingException extends Exception  {
  
  public function __construct($msg)  {
    parent::__construct($msg, -1);
  }
  
}

if(!class_exists("FileNotFoundException"))  {

  /**
   * FileNotFound exception class
   *
   */
  class FileNotFoundException extends Exception  {

    public function __construct($file)  {
      parent::__construct(sprintf("File '%s' not found.", $file), -404);
    }

  }

}

if(!class_exists("ArrayList"))  {
  /**
   * General-use ArrayList implementation using PHP's Iterator
   * interface. Once extended, use the $this->add() to add to the 
   * underlying collection.
   *
   */
   class ArrayList implements Iterator  {
  
     private $collection;
     private $index;
     
     /**
      * Constructor
      *
      */
     public function __construct()  {
       $this->collection = array();
       $this->index = 0;
     }
  
     /**
      * @see Iterator::current
      */
     public function current()  {
       return $this->collection[$this->index];
     }
     
     /**
      * @see Iterator::key
      */
     public function key()  {
       return $this->index;
     }
  
     /**
      * @see Iterator::next
      */
     public function next()  {
       $this->index++;
     }
  
     /**
      * @see Iterator::rewind
      */
     public function rewind()  {
       $this->index = 0;
     }
  
     /**
      * @see Iterator::valid
      */
     public function valid()  {
       return isset($this->collection[$this->index]);
     }
  
     /**
      * Adds a object to the collection.
      *
      * @param	object	$object to add.
      */
     protected function add($object)  {
       array_push($this->collection, $object);
     }
     
     /**
      * Clears the collection and resets the index pointer.
      *
      */
     public function clear()  {
       $this->collection = array();
       $this->index = 0;
     }
  
     /**
      * Returns the size of the collection.
      *
      * @return	int		Size of the collection.
      */
     public function size()  {
       return count($this->collection);
     }
  
   }
} //End ArrayList check and implementation.


/**
 * Top level class object for parsing Nmap scan reports (XML).
 *
 */
class NmapRun  {
  
  private $scanner;
  private $arguments;
  private $start;
  private $version;
  private $xmlOutputVersion;
  private $hosts;
  private $scanInfo;
  private $verbose;
  private $debugging;
  private $runStats;
  
  private function __construct($file)  {
    if(!file_exists($file))
      throw new FileNotFoundException($file);
      
    $dom = simplexml_load_file($file);
    $this->scanner = $dom["scanner"];
    $this->arguments = $dom["args"];
    $this->start = $dom["startstr"];
    $this->version = $dom["version"];
    $this->xmlOutputVersion = $dom["xmloutputversion"];
    $this->scanInfo = new ScanInfo($dom->scaninfo);
    $this->debugging = $dom->debugging["level"];
    $this->verbose = $dom->verbose["level"];
    
    $this->hosts = new Hosts();
    foreach($dom->host as $host)
      $this->hosts->addHost($host);
      
    trigger_error(sprintf("Parsed nmap report for scan performed at %s containing %d host(s).", $dom["startstr"], $this->hosts->size()), E_USER_NOTICE);
  }
  
  public function getScanner()  {
    return $this->scanner;
  }
  
  public function getArguments()  {
    return $this->arguments;
  }
  
  public function getStarted()  {
    return $this->start;
  }
  
  public function getVersion()  {
    return $this->version;
  }
  
  public function getXmlOutputVersion()  {
    return $this->xmlOutputVersion;
  }
  
  public function getScanInfo()  {
    return $this->scanInfo;
  }
  
  public function getDebugging()  {
    return $this->debugging;
  }
  
  public function getVerbosity()  {
    return $this->verbose;
  }
  
  public function getHosts()  {
    return $this->hosts;
  }
  
  public function __toString()  {
    return sprintf("Nmap report for scan performed on %s containing %d host(s).", 
                   $this->start,
                   $this->hosts->size());
  }
  
  /**
   * Returns a NmapRun object representing the given $file.
   *
   * @param	string	Path to the file to parse.
   * @return	object	NmapRun
   */
  public static function parse($file)  {
    return new NmapRun($file);
  }
  
}

/**
 * ArrayList of Host objects.
 *
 */
class Hosts extends ArrayList  {
  
  public function __construct()  {
    parent::__construct();
  }
  
  public function addHost($host)  {
    $this->add(new Host($host));
  }
  
}

/**
 * Represents a Status element within a Host element.
 *
 */
class HostStatus  {
  
  private $state;
  private $reason;
  
  public function __construct($state, $reason=null)  {
    if(is_string($state))  {
      $this->state = $state;
      $this->reason = $reason;
    }else{
      $Status = $state;
      $this->state = $Status["state"];
      $this->reason = $Status["reason"];
    }
  }
  
  public function getState()  {
    return $this->state;
  }
  
  public function getReason()  {
    return $this->reason;
  }
  
  public function __toString()  {
    return $this->state;
  }
  
}

/**
 * Hostname
 *
 */
class Hostname { 
  
  private $name;
  private $type;
  
  public function __construct($name, $type=null)  {
    if(is_string($name))  {
      $this->name = $name;
      $this->type = $type;
    }else{
      $Hostname = $name;
      $this->name = $Hostname["name"];
      $this->type = $Hostname["type"];
    }
  }
  
  public function getName()  {
    return $this->name;
  }
  
  public function getType()  {
    return $this->type;
  }
  
  public function __toString()  {
    return $this->name;
  }
  
}

class Hostnames extends ArrayList  {
  
  public function __construct($Hostnames)  {
    parent::__construct();
    foreach($Hostnames->hostname as $hostname)
      $this->add(new Hostname($hostname));
    
  }
  
}

class Ports extends ArrayList  {
  
  public function __construct($Ports)  {
    parent::__construct();
    foreach($Ports->port as $Port)
      $this->add(new Port($Port));
    
  }
  
}

/**
 * State element within a Port element.
 *
 */
class PortState  {
  
  private $state;
  private $reason;
  private $reason_ttl;
  
  public function __construct($State)  {
    $this->state = $State["state"];
    $this->reason = $State["reason"];
    $this->reason_ttl = $State["reason_ttl"];
  }
  
  public function getState()  {
    return $this->state;
  }
  
  public function getReason()  {
    return $this->reason;
  }
  
  public function getReasonTtl()  {
    return $this->reason_ttl;
  }
  
  public function __toString()  {
    return $this->state;
  }
  
}

class CPEs extends ArrayList  {
  
  public function __construct()  {
    parent::__construct();
  }
  
  public function addCPE($cpe)  {
    $this->add($cpe);
  }
  
}

/**
 * Service
 *
 */
class Service  {
  
  private $name;
  private $product;
  private $version;
  private $extrainfo;
  private $ostype;
  private $method;
  private $conf;
  private $cpes;
  
  public function __construct($Service)  {
    $this->name = $Service["name"];
    $this->product = $Service["product"];
    $this->version = $Service["version"];
    $this->extrainfo = $Service["extrainfo"];
    $this->ostype = $Service["ostype"];
    $this->method = $Service["method"];
    $this->conf = $Service["conf"];
    
    $this->cpes = new CPEs();
    foreach($Service->cpe as $cpe)
      $this->cpes->addCPE($cpe);
    
  }
  
  public function getName()  {
    return $this->name;
  }
  
  public function getProduct()  {
    return $this->product;
  }
  
  public function getVersion()  {
    return $this->version;
  }
  
  public function getExtraInfo()  {
    return $this->extrainfo;
  }
  
  public function getOsType()  {
    return $this->ostype;
  }
  
  public function getMethod()  {
    return $this->method;
  }
  
  public function getConf()  {
    return $this->conf;
  }
  
  public function __toString()  {
    return $this->name;
  }
  
}

/**
 * Script
 *
 */
class Script  {
  
  private $id;
  private $output;
  
  public function __construct($Script)  {
    $this->id = $Script["id"];
    $this->output = $Script["output"];
  }
  
  public function getId()  {
    return $this->id;
  }
  
  public function getOutput()  {
    return $this->output;
  }
  
  public function __toString()  {
    return $this->id;
  }
  
}

class Scripts extends ArrayList  {
  
  public function __construct()  {
    parent::__construct();
  }
  
  public function addScript($Script)  {
    $this->add(new Script($Script));
  }
  
}

/**
 * Port class
 *
 */
class Port  {
  
  private $protocol;
  private $portid;
  private $state;
  private $service;
  private $scripts;
  
  public function __construct($Port)  {
    $this->protocol = $Port["protocol"];
    $this->portid = $Port["portid"];
    $this->state = new PortState($Port->state);
    $this->service new Service($Port->service);
    $this->scripts = new Scripts();
    foreach($Port->script as $script)
      $this->scripts->addScript($script);
   
    trigger_error(sprintf("Parsed port %s/%d (%s).", $this->protocol, $this->portid, $this->service), E_USER_NOTICE); 
  }
  
  public function getPortId()  {
    return $this->portid;
  }
  
  public function getProtocol()  {
    return $this->protocol;
  }
  
  public function getState()  {
    return $this->state;
  }
  
  public function getService()  {
    return $this->service;
  }
  
  public function getScripts()  {
    return $this->scripts;
  }
  
  public function __toString()  {
    return sprintf("%s/%d (%s)", $this->protocol, $this->portid, $this->service);
  }
  
}

/**
 * Address class
 *
 */
class Address  {
  
  private $addr;
  private $type;
  
  public function __construct($Address)  {
    $this->addr = $Address["addr"];
    $this->type = $Address["addrtype"];
  }
  
  public function getAddr()  {
    return $this->addr;
  }
  
  public function getType()  {
    return $this->type;
  }
  
  public function __toString()  {
    return $this->addr;
  }
  
}

/**
 * Host class
 *
 */
class Host  {
  
  private $hostnames;
  private $address;
  private $start;
  private $end;
  private $status;
  private $ports;
  private $scripts;
  
  public function __construct($Host)  {
    $this->hostnames = new Hostnames($Host->hostnames);
    $this->address = new Address($Host->address);
    $this->start = $Host["starttime"];
    $this->end = $Host["endtime"];
    $this->status = new HostStatus($Host->status);
    $this->ports = new Ports($Host->ports);
    $this->scripts = new Scripts($Host->hostscript);
    trigger_error(sprintf("Parsed host %s (%s) containing %d port(s).", $this, $this->address, $this->ports->size()), E_USER_NOTICE);
  }
  
  public function getHostnames()  {
    return $this->hostnames;
  }
  
  public function getAddress()  {
    return $this->address;
  }
  
  public function getStart()  {
    return $this->start;
  }
  
  public function getEnd()  {
    return $this->end;
  }
  
  public function getStatus()  {
    return $this->status;
  }
  
  public function getPorts()  {
    return $this->ports;
  }
  
  public function getHostScripts()  {
    return $this->scripts;
  }
  
  public function __toString()  {
    $o = $this->address ."";
    foreach($this->hostnames as $hostname)
      $o = $hostname;
      
    return $o;
  }
  
}

?>