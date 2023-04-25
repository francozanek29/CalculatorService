using CalculatorService.Server.Core.Services.Helpers;

namespace CalculatorService.Server.Core.Services.Tests.Helpers
{
    public class ServiceHelperOperatorCalculationStringGeneratorTests
    {
        private readonly ServiceHelperOperatorCalculationStringGenerator _sut = new();


        /// <summary>
        /// Test case: When the correct data is sent the correct generation delegate is used and return correct string.
        /// </summary>
        [Theory]
        [MemberData(nameof(Data))]
        public void WhenSendingAValidData_ReturnCorrectString(OperationDTOOperands operands, char operation, OperationResultDTO result, string expectedString)
        {
            //Arrange - Act
            var returnedString = _sut.PerformOperation(operands, operation, result);

            //Assert
            returnedString.Should().Be(expectedString);
        }

        /// <summary>
        /// Test Case: When a wrong Operator is sent the method throw exception.
        /// </summary>
        [Fact]
        public void WhenWrongDataIsSent_ThrowException()
        {
            //Arrange
            var wrongOperator = '$';

            Action action = () => _sut.PerformOperation(new OperationDTOOperands(), wrongOperator,new OperationResultDTO());

            //Act - Assert
            action.Should().Throw<Exception>()
                .WithMessage($"Operation Not implemented for operator {wrongOperator}");
        }

        public static IEnumerable<object[]> Data()
        {
            yield return new object[] { new OperationDTOOperands() { Operands = new List<int> { 2, 3, 4 } }, 
                                        ValidOperations.Sum, 
                                        new OperationResultDTO() { Result = 9 }, 
                                        "2 + 3 + 4 = 9" };
            yield return new object[] { new OperationDTOOperands() { Operands = new List<int> { 2, 3, 4 } }, 
                                        ValidOperations.Mult, 
                                        new OperationResultDTO(){ Result = 24}, 
                                        "2 * 3 * 4 = 24" };
            yield return new object[] { new OperationDTOOperands() { Operands = new List<int> { 3,4 } }, 
                                        ValidOperations.Diff, 
                                        new OperationResultDTO() { Result = -1 },
                                        "3 - 4 = -1" };
            yield return new object[] { new OperationDTOOperands() { Operands = new List<int> { 3,4 } }, 
                                        ValidOperations.Div, 
                                        new OperationResultDTOExtension(){ ExtraResult = 3, Result = 0},
                                        "3 / 4 = 0 - Remainder = 3" };
            yield return new object[] { new OperationDTOOperands() { Operands = new List<int> { 4 } }, 
                                        ValidOperations.Sqrt, 
                                        new OperationResultDTO() { Result = 2},
                                        "sqrt(4) = 2" };
           
        }
    }
}
