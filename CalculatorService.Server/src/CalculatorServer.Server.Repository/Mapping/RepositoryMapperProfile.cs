using AutoMapper;
using CalculatorService.Server.Core.Model.Entitites;
using CalculatorService.Server.Repository.Entities;

namespace CalculatorService.Server.Repository.Mapping
{
    public class RepositoryMapperProfile : Profile
    {
        public RepositoryMapperProfile()
        {
            ConfigureMappingJournals();
        }

        private void ConfigureMappingJournals()
        {
            CreateMap<OperationDTO, OperationEntity>()
              .ForMember(dest => dest.Id,
                          opt => opt.Ignore());

            CreateMap<OperationEntity, OperationInfoDTO>();
        }
    }
}
