using CalculatorService.Server.Core.Model.Entitites;

namespace CalculatorService.Server.Core.Services.Helpers
{
    /// <summary>
    /// The idea for this class is summarize all the methods that should be use to generate all the possible Calculation
    /// String properties that should be used to storage the Operation information in the database
    /// </summary>
    internal class ServiceHelperOperatorCalculationStringGenerator
    {
        private delegate string StringGeneratorToBeUsed(OperationDTOOperands addOperationDTO, OperationResultDTO result, char operation);
        private readonly Dictionary<char, StringGeneratorToBeUsed> _stringGeneratorsToBeUsed;

        internal ServiceHelperOperatorCalculationStringGenerator()
        {
            _stringGeneratorsToBeUsed = new Dictionary<char, StringGeneratorToBeUsed>()
            {
                {ValidOperations.Sum, new StringGeneratorToBeUsed(SimpleStringGenerator)},
                {ValidOperations.Mult,new StringGeneratorToBeUsed(SimpleStringGenerator)},
                {ValidOperations.Diff,new StringGeneratorToBeUsed(SimpleStringGenerator)},
                {ValidOperations.Sqrt,new StringGeneratorToBeUsed(OneParameterFuntionStringGenerator)},
                {ValidOperations.Div, new StringGeneratorToBeUsed(DoubleResultStringGeneration)},
            };
        }

        internal string PerformOperation(OperationDTOOperands operands, char operation, OperationResultDTO result)
        {
            if (_stringGeneratorsToBeUsed.ContainsKey(operation))
            {
                return _stringGeneratorsToBeUsed[operation](operands, result, operation);
            }
            else
            {
                throw new Exception($"Operation Not implemented for operator {operation}");
            }
        }

        /// <summary>
        /// This method will generate the string for Sum, Mult, Sub operations
        /// </summary>
        /// <param name="operators"></param>
        /// <param name="result"></param>
        /// <param name="operation"></param>
        /// <returns>String value for the calculation property</returns>
        private string SimpleStringGenerator(OperationDTOOperands operators, OperationResultDTO result, char operation)
        {
            return $"{string.Join(operation, " "+operators.Operands)} = {result.Result}";
        }

        /// <summary>
        /// This method will generate the string for Sqrt operation
        /// </summary>
        /// <param name="operators"></param>
        /// <param name="result"></param>
        /// <param name="operation"></param>
        /// <returns>String value for the calculation property</returns>
        private string OneParameterFuntionStringGenerator(OperationDTOOperands operators, OperationResultDTO result, char operation)
        {
            return $"sqrt({operators.Operands.ElementAt(0)}) = {result.Result}";
        }

        /// <summary>
        ///  This method will generate the string for Div operation
        /// </summary>
        /// <param name="operators"></param>
        /// <param name="result"></param>
        /// <param name="operation"></param>
        /// <returns>String value for the calculation property</returns>
        private string DoubleResultStringGeneration(OperationDTOOperands operators, OperationResultDTO result, char operation)
        {
            var resultExtended = (OperationResultDTOExtension)result;

            return $"{string.Join(operation, " " + operators.Operands)} = {resultExtended.Result} - Remainder = {resultExtended.ExtraResult}";
        }
    }
}
