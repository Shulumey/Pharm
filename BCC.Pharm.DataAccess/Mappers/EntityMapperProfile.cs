using AutoMapper;
using BCC.Pharm.DataAccess.Entities;
using BCC.Pharm.Shared.Dtos;

namespace BCC.Pharm.DataAccess.Mappers
{
    /// <summary>
    /// Класс регистрации настроек автомэппера.
    /// </summary>
    public class EntityMapperProfile : Profile
    {
        /// <summary>
        /// Регистрация настроек автомэппера.
        /// </summary>
        public EntityMapperProfile()
        {
            CreateMap<Medication, MedicationDto>()
                .ForMember(dest => dest.ActiveSubstance, opt => opt.MapFrom(s => s.Substance.Name))
                .ReverseMap()
                .ForMember(dest => dest.SubstanceId, opt => opt.Ignore())
                .ForMember(dest => dest.ChangesHistory, opt => opt.Ignore())
                .ForMember(dest => dest.Substance, opt => opt.Ignore());
        }
    }
}