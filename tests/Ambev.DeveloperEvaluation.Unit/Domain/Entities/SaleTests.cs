using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the Sale entity class.
/// Tests cover status changes and validation scenarios.
/// </summary>
public class SaleTests
{
    [Fact(DisplayName = "Should validate successfully when sale data is valid")]
    public void Validate_WhenSaleDataIsValid_Should_ReturnValidResult()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();

        // Act
        var result = sale.Validate();

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Fact(DisplayName = "Should calculate the total amount correctly when adding items to a valid sale")]
    public void AddItem_Should_CalculateTotalAmount()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale(0);
        var item = SaleTestData.GenerateSaleItem(6);

        // Act
        sale.AddItem(item);

        // Assert
        sale.TotalAmount.Should().Be(item.TotalAmount);
    }

    [Fact(DisplayName = "Should mark the sale as cancelled when cancelling a valid sale")]
    public void Cancel_Should_MarkSaleAsCancelled()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();

        // Act
        sale.CancelSale(true);

        // Assert
        sale.IsCancelled.Should().BeTrue();
    }

    [Fact(DisplayName = "Should cancel the sale when the last item is cancelled")]
    public void CancelItem_WhenLastItemCancelled_Should_MarkSaleAsCancelled()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale(1);
        var itemId = sale.Items[0].Id;

        // Act
        sale.CancelItem(itemId);

        // Assert
        sale.IsCancelled.Should().BeTrue();
    }

    [Fact(DisplayName = "Should throw an exception when adding a sale item with a quantity greater than 20")]
    public void AddItem_WhenExceedingMaxQuantity_Should_ThrowException()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale(0);

        var saleItem = new SaleItem
        {
            Quantity = 21,
            UnitPrice = 50m,
            Product = "Test",
            SaleId = sale.Id
        };

        // Act
        var action = () => sale.AddItem(saleItem);

        // Assert
        action.Should().Throw<InvalidOperationException>()
            .WithMessage("Cannot sell more than 20 identical items.");
    }

    [Fact(DisplayName = "Should not apply any discount when the quantity of items is less than 4")]
    public void ApplyDiscount_WhenLessThanFourItems_Should_NotApplyDiscount()
    {
        // Arrange
        var item = SaleTestData.GenerateSaleItem(3);

        // Act
        item.ApplyDiscount();

        // Assert
        item.Discount.Should().Be(0);
    }

    [Theory(DisplayName = "Should apply the correct discount for valid item quantities")]
    [InlineData(4, 0.1)]
    [InlineData(10, 0.2)]
    public void ApplyDiscount_WhenValidQuantities_Should_ApplyCorrectDiscount(int quantity, decimal expectedDiscountRate)
    {
        // Arrange
        var item = SaleTestData.GenerateSaleItem(quantity);

        // Act
        item.ApplyDiscount();

        decimal expectedDiscount = quantity * item.UnitPrice * expectedDiscountRate;

        // Assert
        item.Discount.Should().Be(expectedDiscount);
    }

    [Fact(DisplayName = "Should mark the sale item as cancelled")]
    public void Cancel_Should_SetIsCancelledToTrue()
    {
        // Arrange
        var item = SaleTestData.GenerateSaleItem(1);

        // Act
        item.Cancel();

        // Assert
        item.IsCancelled.Should().BeTrue();
    }

    [Fact(DisplayName = "Should calculate total amount correctly for non-cancelled items")]
    public void CalculateTotalAmount_Should_SumOnlyNonCancelledItems()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale(2);
        sale.Items[0].Cancel();
        var expectedTotal = sale.Items[1].TotalAmount;

        // Act
        sale.CalculateTotalAmount();

        // Assert
        sale.TotalAmount.Should().Be(expectedTotal);
    }

    [Fact(DisplayName = "Should set sale date and created at date to current UTC time")]
    public void CreateSaleDate_Should_SetDatesToCurrentUtcTime()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        var beforeExecution = DateTime.UtcNow;

        // Act
        sale.CreateSaleDate();
        var afterExecution = DateTime.UtcNow;

        // Assert
        sale.SaleDate.Should().BeOnOrAfter(beforeExecution).And.BeOnOrBefore(afterExecution);
        sale.CreatedAt.Should().BeOnOrAfter(beforeExecution).And.BeOnOrBefore(afterExecution);
    }

    [Fact(DisplayName = "Should only update SaleDate and CreatedAt without altering other fields")]
    public void CreateSaleDate_Should_OnlyUpdateSaleDateAndCreatedAt()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        var initialTotalAmount = sale.TotalAmount;

        // Act
        sale.CreateSaleDate();

        // Assert
        sale.TotalAmount.Should().Be(initialTotalAmount);
        sale.SaleDate.Should().NotBe(default);
        sale.CreatedAt.Should().NotBe(default);
    }

    [Fact(DisplayName = "Should calculate total amount as zero when all items are cancelled")]
    public void CalculateTotalAmount_WhenAllItemsCancelled_Should_BeZero()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale(3);
        sale.Items.ForEach(item => item.Cancel());

        // Act
        sale.CalculateTotalAmount();

        // Assert
        sale.TotalAmount.Should().Be(0);
    }
}