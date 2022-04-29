using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using BCC.Pharm.Shared.Contracts.Business;
using BCC.Pharm.Shared.Dtos;

namespace BCC.Pharm.Business.Import
{
    public class XmlImportDataFile : IImportDataFile
    {
        public Task<IReadOnlyCollection<MedicationDto>> LoadAsync(TextReader fileReader)
        {
            return Task.Run(() =>
            {
                XDocument document = XDocument.Load(fileReader);
                List<MedicationDto> result = new List<MedicationDto>();

                foreach (XElement lsElement in document.Descendants("LS"))
                {
                    XElement mnnElement = lsElement.DescendantsAndSelf("MNN").FirstOrDefault();
                    string substance = null;
                    if (mnnElement != null)
                    {
                        substance = mnnElement.Value;
                    }

                    foreach (XElement dataElement in lsElement.Descendants("DATA"))
                    {
                        MedicationDto medicationDto = new MedicationDto
                        {
                            Name = dataElement.Element("NAME")?.Value
                        };

                        decimal.TryParse(dataElement.Element("PRICE")?.Value, NumberStyles.Currency, CultureInfo.InvariantCulture, out var price);
                        medicationDto.Price = price;

                        if (decimal.TryParse(dataElement.Element("COUNT")?.Value, NumberStyles.Number, CultureInfo.InvariantCulture, out var quantity))
                        {
                            medicationDto.Quantity = (int) quantity;
                        }

                        medicationDto.ActiveSubstance = substance;

                        result.Add(medicationDto);
                    }
                }

                return (IReadOnlyCollection<MedicationDto>) result.ToArray();
            });
        }
    }
}