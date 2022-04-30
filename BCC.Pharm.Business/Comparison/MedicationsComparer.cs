using System.Collections.Generic;
using System.Linq;
using BCC.Pharm.Shared;
using BCC.Pharm.Shared.Contracts;
using BCC.Pharm.Shared.Dtos;

namespace BCC.Pharm.Business
{
    public class MedicationsComparer : IObjectsComparer<MedicationDto>
    {
        public ChangeSet<MedicationDto> GetChangeSet(IReadOnlyCollection<MedicationDto> originalObjects, IReadOnlyCollection<MedicationDto> newObjects)
        {
            MedicationDto[] updatedObjects = newObjects.Intersect(originalObjects, MedicationDto.DefaultComparer).ToArray();
            MedicationDto[] added = newObjects.Except(originalObjects, MedicationDto.DefaultComparer).ToArray();
            MedicationDto[] removed = originalObjects.Except(newObjects, MedicationDto.DefaultComparer).ToArray();

            List<DifferencesObjects<MedicationDto>> differencesObjects = new List<DifferencesObjects<MedicationDto>>();
            foreach (MedicationDto updatedObject in updatedObjects)
            {
                MedicationDto originalObject = originalObjects.FirstOrDefault(x => updatedObject.Equals(x));
                if (originalObject != null && originalObject.GetDiffProps(updatedObject).Any())
                {
                    differencesObjects.Add(new DifferencesObjects<MedicationDto>(originalObject, updatedObject));
                }
            }

            return new ChangeSet<MedicationDto>(added, removed, differencesObjects);
        }
    }
}