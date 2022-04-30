using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using BCC.Pharm.Shared.Contracts.Business;
using BCC.Pharm.Shared.Dtos;
using Formatting = System.Xml.Formatting;

namespace BCC.Pharm.Business.Export
{
    /// <inheritdoc/>
    public class XmlMedicationsExporter : IMedicationsExporter
    {
        /// <inheritdoc/>
        public string GetExportedData(IReadOnlyCollection<MedicationDto> medications)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(RootNode));
            RootNode rootNode = new RootNode
            {
                Data = medications.ToArray()
            };
            using (StringWriter stringWriter = new StringWriter())
            {
                using (XmlTextWriter writer = new XmlTextWriter(stringWriter) { Formatting = Formatting.Indented })
                {
                    xmlSerializer.Serialize(writer, rootNode);
                    return stringWriter.ToString();
                }
            }
        }

        [XmlRoot("Export")]
        public class RootNode
        {
            [XmlArray]
            public MedicationDto[] Data { get; set; }
        }
    }
}