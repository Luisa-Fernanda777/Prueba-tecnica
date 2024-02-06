using Application.Products.Create;
using Application.Products.Delete;
using Application.Products.GetAll;
using Application.Products.GetById;
using Application.Products.Update;
using ErrorOr;
using Microsoft.AspNetCore.Http.HttpResults;
using Prueba.API.Controllers;

namespace Prueba.API.Controllers;

[Route("api/products")]
[Controller]
public class ProductController : ApiController{
    private readonly ISender _mediator;

    public ProductController(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

 [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var productsResult = await _mediator.Send(new GetAllProductsQuery());

        return productsResult.Match(
            products => Ok(products),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var productResult = await _mediator.Send(new GetProductByIdQuery(id));

        return productResult.Match(
            product => Ok(product),
            errors => Problem(errors)
        );
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command){
        var createProductResult = await _mediator.Send(command);

        return createProductResult.Match(
            product => Ok(),
            errors => Problem(errors)
        );
    }

     [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductCommand command)
    {
        if (command.Id != id)
        {
            List<Error> errors = new()
            {
                Error.Validation("Product.UpdateInvalid", "The request Id does not match with the url Id.")
            };
            return Problem(errors);
        }

        var updateResult = await _mediator.Send(command);

        return updateResult.Match(
            productId => NoContent(),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleteResult = await _mediator.Send(new DeleteProductCommand(id));

        return deleteResult.Match(
            productId => NoContent(),
            errors => Problem(errors)
        );
    }
}
