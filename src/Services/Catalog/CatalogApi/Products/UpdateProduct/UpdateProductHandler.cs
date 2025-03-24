
namespace CatalogApi.Products.UpdateProduct;
using CatalogApi.Models;


public record UpdateProductCommand(Guid Id,string Name, List<string> Category, string Description, string ImageFile, decimal price)
    : ICommand<UpdateProductResult>;
public record UpdateProductResult(bool IsSuccess);
internal class UpdateProductCommandHandler(IDocumentSession session,ILogger<UpdateProductCommandHandler>logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateProductHandler.Handle called with {@Command}", command);

        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (product == null)
        {
            throw new ProductNotFoundException();

        }
        product.Name = command.Name;    
        product.Category = command.Category;
        product.Description = command.Description;
        product.ImageFile = command.ImageFile;
        product.Price=command.price;
        session.Update(product);

        await session.SaveChangesAsync(cancellationToken);
        return new UpdateProductResult(true);

    }
}
