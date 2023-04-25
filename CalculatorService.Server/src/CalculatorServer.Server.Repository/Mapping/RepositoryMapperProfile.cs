using AutoMapper;
using CalculatorService.Server.Core.Model.Entitites;
using CalculatorService.Server.Repository.Entities;

namespace CalculatorService.Server.Repository.Mapping
{
    /// <summary>
    /// As we are using an entity for the repository and class for the application, here we defined the rule for
    /// mapping from one object to another.
    /// </summary>
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
