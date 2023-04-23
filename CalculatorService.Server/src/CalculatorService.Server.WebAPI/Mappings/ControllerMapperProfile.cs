using AutoMapper;
using CalculatorService.Server.Core.Model.Entitites;
using CalculatorService.Server.WebAPI.Models;

namespace CalculatorService.Server.WebAPI.Mappings
{
  /// <summary>
  /// Definition for all the custom mapping rules that are going to be used in the controller.
  /// </summary>
  public class ControllerMapperProfile : Profile
  {
    public ControllerMapperProfile()
    {
      AddRuleForAddOperation();
      AddRulesForMultiplyOperation();
    }

    /// <summary>
    /// Definition for the mapping rules for the Add operation
    /// </summary>
    private void AddRuleForAddOperation()
    {
      CreateMap<AddOperationModel, OperationDTOOperands>()
        .ForMember(dest => dest.Operands,
                   opt => opt.MapFrom(source => source.Addends));
      CreateMap<OperationResultDTO, AddOperationResultModel>();
    }

    private void AddRulesForMultiplyOperation()
    {
      CreateMap<MultiplyOperationModel, OperationDTOOperands>()
        .ForMember(dest => dest.Operands,
                   opt => opt.MapFrom(source => source.Factors));
      CreateMap<OperationResultDTO, MultiplyOperationResultModel>();
    }
  }
}
