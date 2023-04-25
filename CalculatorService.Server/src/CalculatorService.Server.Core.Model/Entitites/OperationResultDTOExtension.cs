namespace CalculatorService.Server.Core.Model.Entitites
{
    /// <summary>
    /// Entity to handle all posible scenarios for a extra value that should be returned. 
    /// The idea is to extend the OperationResultDTO class so we don´t have to change the code some much
    /// we only need to add a new class and use it when we need it.
    /// </summary>
    public record OperationResultDTOExtension : OperationResultDTO
    {
        public int ExtraResult { get; set; }
    }
}
