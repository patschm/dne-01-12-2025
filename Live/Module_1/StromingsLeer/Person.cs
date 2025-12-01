using System.Xml.Serialization;

namespace StromingsLeer;

[XmlRoot("person")]
public class Person
{
    [XmlAttribute("id")]
    public int Id { get; set; }
    [XmlElement("first-name")]
    public string? FirstName { get; set; }
    [XmlElement("last-name")]
    public string? LastName { get; set; }
    public Address Address { get; set; } = new Address { Street = "XXX", Number = "23" };
}
