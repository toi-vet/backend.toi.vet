namespace Toi.Backend.Models;

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot(ElementName = "script", Namespace = "")]
public class Script
{
    [XmlAttribute(AttributeName = "xmlns", Namespace = "")]
    public string Xmlns { get; set; }
}

[XmlRoot(ElementName = "Sender", Namespace = "http://www.gesmes.org/xml/2002-08-01")]
public class Sender
{
    [XmlElement(ElementName = "name", Namespace = "http://www.gesmes.org/xml/2002-08-01")]
    public string Name { get; set; }
}

[XmlRoot(ElementName = "Cube", Namespace = "http://www.ecb.int/vocabulary/2002-08-01/eurofxref")]
public class Cube
{
    [XmlAttribute(AttributeName = "currency", Namespace = "")]
    public string? Currency { get; set; }

    [XmlAttribute(AttributeName = "rate", Namespace = "")]
    public string? Rate { get; set; }

    [XmlElement(ElementName = "Cube", Namespace = "http://www.ecb.int/vocabulary/2002-08-01/eurofxref")]
    public List<Cube> Cubes { get; set; }

    [XmlAttribute(AttributeName = "time", Namespace = "")]
    public string? Time { get; set; }
}

[XmlRoot(ElementName = "Envelope", Namespace = "http://www.gesmes.org/xml/2002-08-01")]
public class Envelope
{
    [XmlElement(ElementName = "script", Namespace = "")]
    public Script Script { get; set; }

    [XmlElement(ElementName = "subject", Namespace = "http://www.gesmes.org/xml/2002-08-01")]
    public string Subject { get; set; }

    [XmlElement(ElementName = "Sender", Namespace = "http://www.gesmes.org/xml/2002-08-01")]
    public Sender Sender { get; set; }

    [XmlElement(ElementName = "Cube", Namespace = "http://www.ecb.int/vocabulary/2002-08-01/eurofxref")]
    public Cube Cube { get; set; }

    [XmlAttribute(AttributeName = "gesmes", Namespace = "http://www.w3.org/2000/xmlns/")]
    public string Gesmes { get; set; }

    [XmlAttribute(AttributeName = "xmlns", Namespace = "")]
    public string Xmlns { get; set; }

    [XmlText] public string Text { get; set; }
}