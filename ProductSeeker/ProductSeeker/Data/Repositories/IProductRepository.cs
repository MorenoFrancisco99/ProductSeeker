using ProductSeeker.Data.Models;

namespace ProductSeeker;

public interface IProductRepository
{
    //TODO: maybe create a genetic Add(T model) instead if havin one for each table

    Task<AppUserProductPriceModel?> GetPriceByID(int priceID);

    Task<AppUserProductPriceModel> CreatePrice(AppUserProductPriceModel model);
   

    /// <summary>
    /// Retrieves a ProductCore by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the ProductCore.</param>
    /// <returns>
    /// The corresponding ProductCore entity if found; otherwise, <c>null</c>.
    /// </returns>
    Task<ProductCoreModel?> GetCoreByID(int CoreId);

    /// <summary>
    /// Retrieves a ProductSpec by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the ProductSpec.</param>
    /// <returns>
    /// The corresponding ProductSpec entity if found; otherwise, <c>null</c>.
    /// </returns>
    Task<ProductSpecModel?> GetSpecByID(int SpecId);

    /// <summary>
    /// Creates a new ProductCore entity in the database.
    /// </summary>
    /// <param name="productCore">The ProductCore entity to persist.</param>
    /// <returns>
    /// The created ProductCore entity with updated values (including generated keys),
    /// or <c>null</c> if the operation fails.
    /// </returns>
    /// <remarks>
    /// The entity is added to the context and persisted using <see cref="DbContext.SaveChangesAsync"/>.
    /// </remarks>
    Task<ProductCoreModel?> CreateCore(ProductCoreModel productCore);


    /// <summary>
    /// Creates a new ProductSpec entity associated with a ProductCore.
    /// </summary>
    /// <param name="productSpec">The ProductSpec entity to persist.</param>
    /// <returns>
    /// The created ProductSpec entity with updated values,
    /// or <c>null</c> if the operation fails.
    /// </returns>
    /// <remarks>
    /// The entity is added to the context and persisted using <see cref="DbContext.SaveChangesAsync"/>.
    /// </remarks>
    Task<ProductSpecModel?> CreateSpec(ProductSpecModel productSpec);


    /// <summary>
    /// Retrieves ProductCore entity based on the provided product name and brand.
    /// </summary>
    /// <param name="productName"></param>
    /// <param name="brand"></param>
    /// <returns>The corresponding ProductCore entity if found; otherwise, <c>null</c>.</returns>
    Task<ProductCoreModel?> FindCore(string productName, string brand);

    
  
    /// <summary>
    /// Retrieves a ProductSpec entity based on the provided coreId and a list of values that uniquely identify the spec.
    /// </summary>
    /// <remarks>
    /// Inteded to be used for finding a spec before creating it, to avoid duplicates. 
    /// The specIdentifier parameter should contain the values of the attributes that, in combination, uniquely identify a spec within a product core. 
    /// The implementation should determine how to use these values to query the database for an existing ProductSpec that matches the given criteria.
    /// 
    /// Another alternative is finding the spec by EAN, but not all products have EANs, and for some categories, 
    /// the combination of attributes is a more natural unique identifier.
    /// </remarks>
    /// <param name="coreId"></param>
    /// <param name="specIdentifier"></param>
    /// <returns>The corresponding ProductSpec entity if found; otherwise, <c>null</c>.</returns>
    Task<ProductSpecModel?> FindSpecByIdentifiers(int coreId, List<object> specIdentifier);


    Task<ProductSpecModel?> GetSpecByEAN(string? ean);

}
