using System;
using BCC.Pharm.Business.Export;
using BCC.Pharm.Shared;
using BCC.Pharm.Shared.Contracts.Business;

namespace BCC.Pharm.Business.Factories
{
    /// <summary>
    /// Фабрика для создания объектов типа <see cref="IMedicationsExporter"/>
    /// </summary>
    public static class ExporterFactory
    {
        public static IMedicationsExporter Create(ExportFormat format)
        {
            switch (format)
            {
                case ExportFormat.Json:
                    return new JsonMedicationsExporter();
                case ExportFormat.Xml:
                    return new XmlMedicationsExporter();
                default:
                    throw new ArgumentOutOfRangeException(nameof(format), format, null);
            }
        }
    }
}