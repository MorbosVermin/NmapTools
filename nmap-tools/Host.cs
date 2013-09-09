using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace nmap_tools
{
    public sealed class Host
    {
        [XmlAttribute("starttime")]
        public int StartTime { get; set; }

        [XmlAttribute("endtime")]
        public int EndTime { get; set; }

        [XmlElement("status")]
        public Status Status { get; set; }

        [XmlElement("address")]
        public List<Address> Addresses { get; set; }

        [XmlArray("hostnames")]
        [XmlArrayItem("hostname")]
        public List<Hostname> Hostnames { get; set; }

        [XmlArray("ports")]
        [XmlArrayItem("port")]
        public List<Port> Ports { get; set; }

        [XmlElement("os")]
        public OS OS { get; set; }

        [XmlElement("uptime")]
        public UpTime UpTime { get; set; }

        [XmlElement("distance")]
        public Distance Distance { get; set; }

        [XmlElement("tcpsequence")]
        public TcpSequence TcpSequence { get; set; }

        [XmlElement("ipidsequence")]
        public IpIdSequence IpIdSequence { get; set; }

        [XmlElement("tcptssequence")]
        public TcpTsSequence TcpTsSequence { get; set; }

        [XmlElement("times")]
        public Times Times { get; set; }

        /// <summary>
        /// Returns TRUE if the OS was detected during this scan.
        /// </summary>
        /// <returns>bool</returns>
        public bool DetectedOS()
        {
            return (OS != null && OS.Match != null);
        }

        /// <summary>
        /// Returns the IP address of this host.
        /// </summary>
        /// <returns>string; IP Address</returns>
        public override string ToString()
        {
            return Addresses[0].Name;
        }

    }

    public sealed class Status
    {
        [XmlAttribute("state")]
        public string State { get; set; }

        [XmlAttribute("reason")]
        public string Reason { get; set; }

    }

    public sealed class Address
    {
        [XmlAttribute("addr")]
        public string Name { get; set; }

        [XmlAttribute("addrtype")]
        public string Type { get; set; }

        [XmlAttribute("vendor")]
        public string Vendor { get; set; }
    }

    public sealed class Hostname
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }

    }

    public sealed class UpTime
    {
        [XmlAttribute("seconds")]
        public int Seconds { get; set; }

        [XmlAttribute("lastboot")]
        public string LastBoot { get; set; }

    }

    public sealed class Distance
    {
        [XmlAttribute("value")]
        public int Value { get; set; }
    }

    public sealed class TcpSequence
    {
        [XmlAttribute("index")]
        public int Index { get; set; }

        [XmlAttribute("difficulty")]
        public string Difficulty { get; set; }

        [XmlAttribute("values")]
        public string Values { get; set; }

    }

    public sealed class IpIdSequence
    {
        [XmlAttribute("class")]
        public string Class { get; set; }

        [XmlAttribute("values")]
        public string Values { get; set; }

    }

    public sealed class TcpTsSequence
    {
        [XmlAttribute("class")]
        public string Class { get; set; }

        [XmlAttribute("values")]
        public string Values { get; set; }

    }

    public sealed class Times
    {
        [XmlAttribute("srtt")]
        public int Srtt { get; set; }

        [XmlAttribute("rttvar")]
        public int RttVar { get; set; }

        [XmlAttribute("to")]
        public int To { get; set; }

    }

}
