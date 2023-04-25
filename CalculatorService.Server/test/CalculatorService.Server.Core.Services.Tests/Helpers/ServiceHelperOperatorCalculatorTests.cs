using CalculatorService.Server.Core.Services.Helpers;

namespace CalculatorService.Server.Core.Services.Tests.Helpers
{
    public class ServiceHelperOperatorCalculatorTests
    {
        private readonly ServiceHelperOperatorCalculator _sut = new();


        /// <summary>
        /// Test case: When the correct data is sent the correct generation delegate is used and return correct result
        /// </summary>
        [Theory]
        [MemberData(nameof(Data))]
        public void WhenSendingAValidData_ReturnCorrectValue(OperationDTOOperands operands, char operation, int result)
        {
            //Arrange - Act
            var returnedValue = _sut.PerformOperation(operands, operation);

            //Assert
            returnedValue.Result.Should().Be(result);
        }


        /// <summary>
        /// Test Case: When the correct data is sent the correct generation delegate is used and return correct result for div operation
        /// </summary>
        [Fact]
        public void WhenSendingValidDataForDivOperation_ReturnCorrectValue()
        {
            //Arrange
            var divDTOOperand = new OperationDTOOperands()
            {
                Operands = new List<int> { 3, 4 }
            };

            //Act
            var returnedValue = (OperationResultDTOExtension)_sut.PerformOperation(divDTOOperand, ValidOperations.Div);

            //Assert
            returnedValue.ExtraResult.Should().Be(3);
            returnedValue.Result.Should().Be(0);
        }

        /// <summary>
        /// Test Case: When a wrong Operator is sent the method throw exception.
        /// </summary>
        [Fact]
        public void WhenWrongDataIsSent_ThrowException()
        {
            //Arrange
            var wrongOperator = '$';

            Action action = () => _sut.PerformOperation(new OperationDTOOperands(), wrongOperator);

            //Act - Assert
            action.Should().Throw<Exception>()
                .WithMessage($"Operation Not implemented for operator {wrongOperator}");
        }

        public static IEnumerable<object[]> Data()
        {
            yield return new object[] { new OperationDTOOperands() { Operands = new List<int> { 2, 3, 4 } },
                                        ValidOperations.Sum,
                                        9 };
            yield return new object[] { new OperationDTOOperands() { Operands = new List<int> { 2, 3, 4 } },
                                        ValidOperations.Mult,
                                        24};
            yield return new object[] { new OperationDTOOperands() { Operands = new List<int> { 3,4 } },
                                        ValidOperations.Diff,
                                        -1 };
            yield return new object[] { new OperationDTOOperands() { Operands = new List<int> { 4 } },
                                        ValidOperations.Sqrt,
                                       2};

        }
    }
}
