using System.Collections.Generic;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BCC.Pharm.Shared.Contracts.Data;
using BCC.Pharm.Shared.Dtos;

namespace BCC.Pharm.DataAccess.DataProviders
{
    /// <inheritdoc />
    public class MedicationsDataProvider: IMedicationsDataProvider
    {
        /// <inheritdoc cref="PharmDbContext"/>
        private readonly PharmDbContext _dbContext;
        /// <inheritdoc cref="IMapper"/>
        private readonly IMapper _mapper;

        public MedicationsDataProvider(PharmDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<MedicationDto>> GetAllMedicationsAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Medications
                .Include(x => x.Substance)
                .ProjectTo<MedicationDto>(_mapper.ConfigurationProvider)
                .ToArrayAsync(cancellationToken);
        }
    }
}