using ProductSeeker.Data.Models;

namespace ProductSeeker;

public interface IProductService
{
    Task<IEnumerable<ProductCoreModel>> GetAllProducts();
    Task<Result<ProductCoreModel>> GetCoreByID(int CoreId, string userID);
    Task<Result<GETProductSpecDTO>>  GetSpecByID(int SpecId, string userID);
    Task<Result<GETProductCoreDTO>> GetCoreWithSpecByIDs(int CoreId, int specId, string userID);
    Task<Result<AppUserProductPriceModel>> GetPriceByID(int priceId, string userID);
    Task<Result<ProductCoreModel>> CreateProductCoreWSpec(POSTProductWCoreDTObase dto, string userID);
    Task<Result<ProductCoreModel>> CreateProductCore(POSTProductCoreDTO productDTO, string userID);
    Task<Result<ProductSpecModel>> CreateProductSpec(POSTProductSpecDTObase productDTO, string userID);
    Task<Result<AppUserProductPriceModel>> CreateProductPrice(POSTProductPriceDTO dto, string userID);
   

}
