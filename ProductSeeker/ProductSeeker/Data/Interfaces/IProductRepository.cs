using ProductSeeker.Data.Models;

namespace ProductSeeker;

public interface IProductRepository
{
    //TODO: maybe create a genetic Add(T model) instead if havin one for each table

    Task<Result<AppUserProductPriceModel>> GetPriceByID(int priceID, string userID);

    Task<AppUserProductPriceModel> CreatePrice(AppUserProductPriceModel model);
   

    /// <summary>
    /// Retrieves a ProductCore by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the ProductCore.</param>
    /// <returns>
    /// The corresponding ProductCore entity if found; otherwise, <c>null</c>.
    /// </returns>
    Task<Result<ProductCoreModel>?> GetCoreByID(int CoreId, string userID);

    /// <summary>
    /// Retrieves a ProductSpec by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the ProductSpec.</param>
    /// <returns>
    /// The corresponding ProductSpec entity if found; otherwise, <c>null</c>.
    /// </returns>
    Task<Result<ProductSpecModel>?> GetSpecByID(int SpecId, string userID);

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
    /// Determines whether the specified ProductCore belongs to the given user.
    /// </summary>
    /// <param name="CoreId">The identifier of the ProductCore.</param>
    /// <param name="UserId">The identifier of the user to verify ownership against.</param>
    /// <returns>
    /// <c>true</c> if the ProductCore exists and its IdCreator matches the provided UserId;
    /// otherwise, <c>false</c>.
    /// </returns>
    Task<Result> IsCoreOwner(int CoreId, string UserId);


    /// <summary>
    /// Determines whether the specified ProductSpec belongs to the given user.
    /// </summary>
    /// <param name="SpecId">The identifier of the ProductSpec.</param>
    /// <param name="UserId">The identifier of the user to verify ownership against.</param>
    /// <returns>
    /// <c>true</c> if the ProductSpec exists and its IdCreator matches the provided UserId;
    /// otherwise, <c>false</c>.
    /// </returns>
    Task<bool> IsSpecOwner(int SpecId, string UserId);

    /// <summary>
    /// Asynchronously determines whether a ProductCore with the specified identifier exists.
    /// </summary>
    /// <param name="coreID">The unique identifier of the ProductCore to search for.</param>
    /// <returns>
    /// <c>true</c> if a ProductCore with the given Id exists; otherwise, <c>false</c>.
    /// </returns>
    Task<bool> CoreExist(int coreID);


    /// <summary>
    /// Asynchronously determines whether a ProductSpec with the specified identifier exists.
    /// </summary>
    /// <param name="specID">The unique identifier of the ProductSpec to search for.</param>
    /// <returns>
    /// A task representing the asynchronous operation. 
    /// The task result contains <c>true</c> if the entity exists; otherwise, <c>false</c>.
    /// </returns>
    Task<bool> SpecExist(int specID);


}
