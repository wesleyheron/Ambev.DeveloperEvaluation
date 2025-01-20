using Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.Queries.ListSale;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users;

/// <summary>
/// Controller responsible for managing sales operations such as creation, retrieval, updating, deletion, and cancellation of sales.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SalesController : BaseController
{
    /// <summary>
    /// The mediator used to send commands and queries through the application's request pipeline.
    /// </summary>
    private readonly IMediator _mediator;

    /// <summary>
    /// The AutoMapper instance used for mapping between domain models and data transfer objects (DTOs).
    /// </summary>
    private readonly IMapper _mapper;


    /// <summary>
    /// Initializes a new instance of the SalesController class.
    /// </summary>
    /// <param name="mediator">The mediator instance for sending commands and queries.</param>
    /// <param name="mapper">The AutoMapper instance for object mapping.</param>
    public SalesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new sale.
    /// </summary>
    /// <param name="request">The request object containing sale creation data.</param>
    /// <param name="cancellationToken">The cancellation token for asynchronous operations.</param>
    /// <returns>A result indicating the outcome of the sale creation process.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateSaleCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateSaleResponse>
        {
            Success = true,
            Message = "Sale created successfully",
            Data = _mapper.Map<CreateSaleResponse>(response)
        });
    }

    /// <summary>
    /// Retrieves a sale by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the sale to retrieve.</param>
    /// <param name="cancellationToken">The cancellation token for asynchronous operations.</param>
    /// <returns>A result containing the sale data if found, or a not found response.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<Sales.GetSale.GetSaleResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSale([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetSaleRequest { SaleId = id };
        var validator = new GetSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var query = _mapper.Map<GetSaleQuery>(request);
        var response = await _mediator.Send(query, cancellationToken);

        return Ok(new ApiResponseWithData<Sales.GetSale.GetSaleResult>
        {
            Success = true,
            Message = "Sale retrieved successfully",
            Data = _mapper.Map<Sales.GetSale.GetSaleResult>(response)
        });
    }

    /// <summary>
    /// Retrieves a list of sales.
    /// </summary>
    /// <param name="request">The request object containing query parameters for listing sales.</param>
    /// <param name="cancellationToken">The cancellation token for asynchronous operations.</param>
    /// <returns>A result containing a list of sales.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseWithData<List<ListSaleResult>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListSales([FromQuery] ListSalesRequest request, CancellationToken cancellationToken)
    {
        var query = new ListSaleQuery();

        var response = await _mediator.Send(query, cancellationToken);

        return Ok(new ApiResponseWithData<List<ListSaleResult>>
        {
            Data = _mapper.Map<List<ListSaleResult>>(response),
            Success = true,
            Message = "Sales listed successfully"
        });
    }

    /// <summary>
    /// Updates an existing sale by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the sale to update.</param>
    /// <param name="request">The request object containing the updated sale data.</param>
    /// <param name="cancellationToken">The cancellation token for asynchronous operations.</param>
    /// <returns>A result indicating the outcome of the sale update process.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSale([FromRoute] Guid id, [FromBody] UpdateSaleRequest request, CancellationToken cancellationToken)
    {
        request.Id = id;
        var validator = new UpdateSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<UpdateSaleCommand>(request);
        command.Id = id;

        var response = await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponseWithData<UpdateSaleResponse>
        {
            Data = _mapper.Map<UpdateSaleResponse>(response),
            Success = true,
            Message = "Sale updated successfully"
        });
    }

    /// <summary>
    /// Deletes a sale by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the sale to delete.</param>
    /// <param name="cancellationToken">The cancellation token for asynchronous operations.</param>
    /// <returns>A result indicating the outcome of the sale deletion process.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSale([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteSaleRequest { SaleId = id };
        var validator = new DeleteSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<DeleteSaleCommand>(request);
        await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Sale deleted successfully"
        });
    }

    /// <summary>
    /// Cancels a sale by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the sale to cancel.</param>
    /// <param name="cancellationToken">The cancellation token for asynchronous operations.</param>
    /// <returns>A result indicating the outcome of the sale cancellation process.</returns>
    [HttpPatch("cancel/{id:guid}")]
    public async Task<IActionResult> Cancel([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new CancelSaleCommand { SaleId = id };
        await _mediator.Send(request, cancellationToken);

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Sale cancelled successfully"
        });
    }

    /// <summary>
    /// Cancels an item in a sale.
    /// </summary>
    /// <param name="saleId">The unique identifier of the sale containing the item.</param>
    /// <param name="itemId">The unique identifier of the item to cancel.</param>
    /// <param name="cancellationToken">The cancellation token for asynchronous operations.</param>
    /// <returns>A result indicating the outcome of the item cancellation process.</returns>
    [HttpPatch("cancel-item/{saleId:guid}/{itemId:guid}")]
    public async Task<IActionResult> CancelItem([FromRoute] Guid saleId, [FromRoute] Guid itemId,
        CancellationToken cancellationToken)
    {
        var request = new CancelItemCommand { SaleId = saleId, ItemId = itemId };
        await _mediator.Send(request, cancellationToken);

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Item cancelled successfully"
        });
    }
}

