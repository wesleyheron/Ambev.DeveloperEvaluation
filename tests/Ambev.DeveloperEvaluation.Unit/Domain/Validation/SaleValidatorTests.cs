using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation
{
    public class SaleValidatorTests
    {
        private readonly SaleValidator _validator;

        public SaleValidatorTests()
        {
            _validator = new SaleValidator();
        }
        

        /// <summary>
        /// Tests that a valid Sale instance passes validation.
        /// </summary>
        [Fact(DisplayName = "Valid Sale instance should pass validation")]
        public void Given_ValidSale_When_Validated_Then_ShouldNotHaveErrors()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            typeof(Sale).GetProperty("CreatedAt")?.SetValue(sale, DateTime.UtcNow.AddSeconds(-10));
            typeof(Sale).GetProperty("UpdatedAt")?.SetValue(sale, DateTime.UtcNow.AddSeconds(-10));

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Tests that validation fails when SaleNumber is empty.
        /// </summary>
        [Fact(DisplayName = "Empty SaleNumber should fail validation")]
        public void Given_EmptySaleNumber_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            sale.SaleNumber = string.Empty;

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(s => s.SaleNumber)
                .WithErrorMessage("Sale number is required.");
        }

        /// <summary>
        /// Tests that validation fails when SaleNumber exceeds 50 characters.
        /// </summary>
        [Fact(DisplayName = "SaleNumber exceeding maximum length should fail validation")]
        public void Given_LongSaleNumber_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            sale.SaleNumber = new string('A', 51);

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(s => s.SaleNumber)
                .WithErrorMessage("Sale number cannot exceed 50 characters.");
        }

        /// <summary>
        /// Tests that validation fails when Customer is empty.
        /// </summary>
        [Fact(DisplayName = "Empty Customer should fail validation")]
        public void Given_EmptyCustomer_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            sale.Customer = string.Empty;

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(s => s.Customer)
                .WithErrorMessage("Customer is required.");
        }

        /// <summary>
        /// Tests that validation fails when Customer exceeds 100 characters.
        /// </summary>
        [Fact(DisplayName = "Customer exceeding maximum length should fail validation")]
        public void Given_LongCustomerName_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            sale.Customer = new string('A', 101);

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(s => s.Customer)
                .WithErrorMessage("Customer name cannot exceed 100 characters.");
        }

        /// <summary>
        /// Tests that validation fails when CreatedAt is a future date.
        /// </summary>
        [Fact(DisplayName = "Future CreatedAt date should fail validation")]
        public void Given_FutureCreatedAt_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();

            typeof(Sale).GetProperty("CreatedAt")?.SetValue(sale, DateTime.UtcNow.AddDays(1));

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(s => s.CreatedAt)
                .WithErrorMessage("CreatedAt cannot be in the future.");
        }

        /// <summary>
        /// Tests that validation fails when UpdatedAt is a future date.
        /// </summary>
        [Fact(DisplayName = "Future UpdatedAt date should fail validation")]
        public void Given_FutureUpdatedAt_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();

            typeof(Sale).GetProperty("UpdatedAt")?.SetValue(sale, DateTime.UtcNow.AddDays(1));

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(s => s.UpdatedAt)
                .WithErrorMessage("UpdatedAt cannot be in the future.");
        }

        /// <summary>
        /// Tests that validation fails when Branch is empty.
        /// </summary>
        [Fact(DisplayName = "Empty Branch should fail validation")]
        public void Given_EmptyBranch_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            sale.Branch = string.Empty;

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(s => s.Branch)
                .WithErrorMessage("Branch is required.");
        }

        /// <summary>
        /// Tests that validation fails when Branch exceeds 50 characters.
        /// </summary>
        [Fact(DisplayName = "Branch exceeding maximum length should fail validation")]
        public void Given_LongBranchName_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            sale.Branch = new string('A', 51); // 51 characters

            // Act
            var result = _validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(s => s.Branch)
                .WithErrorMessage("Branch name cannot exceed 50 characters.");
        }
    }
}
