using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BCC.Pharm.Business.Factories;
using BCC.Pharm.Shared;
using BCC.Pharm.Shared.Dtos;
using MediatR;

namespace BCC.Pharm.Business.Queries
{
    public static class ExportMedications
    {
        public sealed class Query : IRequest<string>
        {
            public IReadOnlyCollection<MedicationDto> Data { get; }
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
            public Task<string> Handle(Query request, CancellationToken cancellationToken) => 
                Task.FromResult(ExporterFactory.Create(request.Format).GetExportedData(request.Data));
        }
    }
}