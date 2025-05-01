using Ambev.DeveloperEvaluation.Application.Sales.Commands.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class DeleteSaleHandlerTests
    {
        private readonly DeleteSaleHandler _handler;
        private readonly ISaleRepository _saleRepository;

        public DeleteSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _handler = new DeleteSaleHandler(_saleRepository);
        }

        [Fact(DisplayName = "Given invalid delete sale command When handling Then throws ValidationException")]
        public async Task Handle_InvalidCommand_ThrowsValidationException()
        {
            // Arrange
            var command = new DeleteSaleCommand(Guid.Empty);

            var validator = Substitute.For<IValidator<DeleteSaleCommand>>();
            validator.ValidateAsync(command, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(new ValidationResult(new List<ValidationFailure> { new("Id", "Sale ID is required.") })));

            // Act
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            var exception = await act.Should().ThrowAsync<ValidationException>();
            exception.Which.Message.Should().Contain("Sale ID is required.");
        }

        [Fact(DisplayName = "Given sale not found When deleting Then returns failure result")]
        public async Task Handle_SaleNotFound_ReturnsFailureResult()
        {
            // Arrange
            var command = new DeleteSaleCommand(Guid.NewGuid());

            _saleRepository.DeleteAsync(command.Id, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(false));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Be($"Sale with ID {command.Id} not found.");
        }

        [Fact(DisplayName = "Given sale found When deleting Then returns success result")]
        public async Task Handle_SaleFound_ReturnsSuccessResult()
        {
            // Arrange
            var command = new DeleteSaleCommand(Guid.NewGuid());

            _saleRepository.DeleteAsync(command.Id, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(true));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Message.Should().Be("Sale deleted successfully.");
        }
    }

}
