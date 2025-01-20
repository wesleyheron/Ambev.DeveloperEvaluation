namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.DeleteSale
{
    /// <summary>
    /// Represents the result of a sale deleted.
    /// </summary>
    public class DeleteSaleResult
    {
        /// <summary>
        /// Indicates whether the deletion was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Optional message providing details about the result of the deletion operation.
        /// This could contain error messages or a confirmation of success.
        /// </summary>
        public string? Message { get; set; }
    }
}
