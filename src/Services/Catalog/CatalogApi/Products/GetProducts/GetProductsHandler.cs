namespace CatalogApi.Products.GetProducts;

using System.Threading;
using System.Threading.Tasks;
using CatalogApi.Models;

public record GetProductsQuery():IQuery<GetProductsResult>;
public record GetProductsResult(IEnumerable<Product> products);

public class GetProductsQueryHandler(IDocumentSession session,ILogger<GetProductsQueryHandler> logger) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductsQueryHandler.Handle Called with{@Query}", query);
        var products= await session.Query<Product>().ToListAsync(cancellationToken);
        return new GetProductsResult(products);
    }
}
