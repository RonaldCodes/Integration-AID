using System.Xml.Serialization;

namespace Routing.Common.Model
{
    [XmlType(TypeName = "CrewMember")]
    public class CrewMember
    {
        public CrewMember()
        {
            Name = string.Empty;
            Id = string.Empty;
            IdentityNo = string.Empty;
            CellNo = string.Empty;
        }

        [XmlElement(IsNullable = true)]
        public string Name { get; set; }

        [XmlElement(IsNullable = true)]
        public string Id { get; set; }

        [XmlElement(IsNullable = true)]
        public string IdentityNo { get; set; }

        [XmlElement(IsNullable = true)]
        public string CellNo { get; set; }

    }
}
