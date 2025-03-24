﻿namespace CatalogApi.Products.GetProductByCategory;
using CatalogApi.Models;
using CatalogApi.Products.GetProductById;

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Product> Products);
internal class GetProductByCategoryHandler(IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByCategoryQueryHandler.Handle called with {@Query}", query);
       
        var products = await session.Query<Product>().Where(p=>p.Category.Contains(query.Category)).ToListAsync();
       
        return new GetProductByCategoryResult(products);

    }
}
