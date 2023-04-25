namespace CalculatorService.Server.Core.Services.Tests
{
    public partial class CalculatorServicesTests
    {
        /// <summary>
        /// Test cases: When the elements to be subed are sent, the result is correct, and not tracking Id is sent,
        /// the repository is not being called.
        /// </summary>
        /// <param name="elementsToBeAdded"></param>
        /// <param name="expectedResult"></param>
        /// <returns></returns>
        [Theory]
        [MemberData(nameof(DataSub))]
        public async Task SubElementsAsync_WhenElementsAreSentAndNotTrackingIdISent_ReturnsSum(List<int> elementsToBeSubed, int expectedResult)
        {
            //Arrange
            var subOperationDto = new OperationDTOOperands()
            {
                Operands = elementsToBeSubed
            };

            _sut = new CalculatorServices(_mockRepository.Object, _requestContextForNoTracking);

            //Act 
            var addOperationResult = await _sut.ExecuteOperation(subOperationDto, ValidOperations.Diff);

            //Assert
            using (new AssertionScope())
            {
                addOperationResult.Result.Should().Be(expectedResult);
                _mockRepository.Verify(mr => mr.SaveOperationToRepositoryAsync(It.IsAny<OperationDTO>()), Times.Never);
            }
        }

        /// <summary>
        /// Test case: When the tracking id is sent the data is saved in the repository.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task SubElementsAsync_WhenElementsAreSentAndTrackingIdISent_RepositoryIsCalled()
        {
            //Arrange
            var subOperationDto = new OperationDTOOperands()
            {
                Operands = new List<int>() { 2, 3 }
            };

            _sut = new CalculatorServices(_mockRepository.Object, _requestContextForTracking);

            //Act 
            var addOperationResult = await _sut.ExecuteOperation(subOperationDto, ValidOperations.Diff);

            //Assert
            _mockRepository.Verify(mr => mr.SaveOperationToRepositoryAsync(
                It.Is<OperationDTO>(x => x.Calculation == "2 - 3 = -1" && x.Operation == "Dif" && x.TrackingId == _requestContextForTracking.TrackingId)),
                Times.Once);
        }

        public static IEnumerable<object[]> DataSub()
        {
            yield return new object[] { new List<int> { -1, 3 }, -4 };
            yield return new object[] { new List<int> { 1, 1 }, 0 };
            yield return new object[] { new List<int> { 5, 4 }, 1 };
        }
    }
}