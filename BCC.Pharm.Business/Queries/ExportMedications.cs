using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BCC.Pharm.Business.Factories;
using BCC.Pharm.Shared;
using BCC.Pharm.Shared.Dtos;
using MediatR;

namespace BCC.Pharm.Business.Queries
{
    /// <summary>
    /// Экспорт данных
    /// </summary>
    public static class ExportMedications
    {
        /// <inheritdoc />
        public sealed class Query : IRequest<string>
        {
            /// <summary>
            /// Данные, которые необходимо выгрузить.
            /// </summary>
            public IReadOnlyCollection<MedicationDto> Data { get; }
            
            /// <summary>
            /// Формат эеспортируемых данных.
            /// </summary>
            public ExportFormat Format { get; }

            public Query(IReadOnlyCollection<MedicationDto> data, ExportFormat format)
            {
                Data = data;
                Format = format;
            }
        }
        
        /// <inheritdoc />
        public sealed class Handler : IRequestHandler<Query ,string>
        {
            /// <inheritdoc />
            public Task<string> Handle(Query request, CancellationToken cancellationToken) => 
                Task.FromResult(ExporterFactory.Create(request.Format).GetExportedData(request.Data));
        }
    }
}