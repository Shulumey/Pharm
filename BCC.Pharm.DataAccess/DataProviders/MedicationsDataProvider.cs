using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BCC.Pharm.DataAccess.Entities;
using BCC.Pharm.Shared;
using BCC.Pharm.Shared.Contracts.Data;
using BCC.Pharm.Shared.Dtos;

namespace BCC.Pharm.DataAccess.DataProviders
{
    /// <inheritdoc />
    public class MedicationsDataProvider : IMedicationsDataProvider
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

        /// <inheritdoc />
        public async Task UpdateAsync(IReadOnlyCollection<DifferencesObjects<MedicationDto>> differences, SourceChange sourceChange, CancellationToken cancellationToken)
        {
            foreach (DifferencesObjects<MedicationDto> diff in differences)
            {
                Medication originalMedication = await _dbContext.Medications.FirstOrDefaultAsync(x => x.Id == diff.OriginalObject.Id, cancellationToken);
                if (originalMedication != null)
                {
                    Medication replaceMedication = _mapper.Map<Medication>(diff.NewObject);
                    replaceMedication.Id = originalMedication.Id;
                    replaceMedication.SubstanceId = originalMedication.SubstanceId;
                    _dbContext.Entry(originalMedication).CurrentValues.SetValues(replaceMedication);
                    IReadOnlyCollection<ChangeHistory> history = _mapper.Map<IReadOnlyCollection<ChangeHistory>>(diff.Differences);
                    
                    foreach (ChangeHistory historyItem in history)
                    {
                        historyItem.SourceChange = sourceChange;
                        historyItem.Created = DateTime.Now;
                        originalMedication.ChangesHistory.Add(historyItem);
                    }
                }
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task AddAsync(IReadOnlyCollection<MedicationDto> medications, CancellationToken cancellationToken)
        {
            List<Substance> needToAddSubstances = new List<Substance>();
            
            string[] originalSubstances = await _dbContext.Substances
                .Select(x => x.Name)
                .ToArrayAsync(cancellationToken);

            originalSubstances = originalSubstances.Select(x => x.Normilize()).ToArray();
            
            foreach (MedicationDto medication in medications)
            {
                if(originalSubstances.Contains(medication.ActiveSubstance.Normilize()))
                {
                    string currentSubstanceName = medication.ActiveSubstance.Normilize();
                    
                    Substance substance = await _dbContext.Substances
                        .FirstOrDefaultAsync(x => x.Name.Trim().ToUpper().Replace(" ", string.Empty) == currentSubstanceName, cancellationToken);
                    
                    if (substance != null)
                    {
                        Medication[] medicationsEntities = FindMedications(medications, medication.ActiveSubstance);

                        foreach (Medication medicationsEntity in medicationsEntities)
                        {
                            substance.Medications.Add(medicationsEntity);
                        }
                    }
                }
                else
                {
                    if (needToAddSubstances.All(x => x.Name != medication.ActiveSubstance))
                    {
                        Substance newSubstance = _dbContext.Substances.Create();
                        newSubstance.Name = medication.ActiveSubstance;
                    
                        Medication[] medicationsEntities = FindMedications(medications, medication.ActiveSubstance);

                        foreach (Medication medicationEntity in medicationsEntities)
                        {
                            newSubstance.Medications.Add(medicationEntity);
                        }

                        needToAddSubstances.Add(newSubstance);
                    }
                }
            }

            _dbContext.Substances.AddRange(needToAddSubstances);
            
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(IReadOnlyCollection<MedicationDto> medications, CancellationToken cancellationToken)
        {
            IReadOnlyCollection<Medication> deleteEntities = _mapper.Map<IReadOnlyCollection<Medication>>(medications);
            
            foreach (Medication deleteEntity in deleteEntities)
            {
                _dbContext.Medications.Attach(deleteEntity);
            }

            _dbContext.Medications.RemoveRange(deleteEntities);
            
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private Medication[] FindMedications(IReadOnlyCollection<MedicationDto> medications, string activeSubstance) => medications
            .Where(x => x.ActiveSubstance.Normilize() == activeSubstance.Normilize())
            .Select(x => _mapper.Map<Medication>(x))
            .ToArray();

        /// <inheritdoc />
        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}