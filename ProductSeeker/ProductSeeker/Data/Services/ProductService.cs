using System.Collections;
using ProductSeeker.Data.Context;
using ProductSeeker.Data.Interfaces;
using ProductSeeker.Data.Models;

namespace ProductSeeker;

public class ProductService : IProductService 
{

    //context is only for saving changes. Any other operations
    // has to be done through repos
    private readonly AplicationDBContext _context;
    private readonly IGenericRepository<ProductCoreModel> _productCores;

    public ProductService(AplicationDBContext context,IGenericRepository<ProductCoreModel> productcore)
    {
        _context = context;
        _productCores = productcore;
    }


    public async Task<IEnumerable<ProductCoreModel>> GetAllProducts()
    {
        return await _productCores.GetAll();
    }

    public async Task<ProductCoreModel?> GetByID(int id)
    {
        return await _productCores.GetById(id);
    }

    public async Task<ProductCoreModel?> CreateNewProduct(ProductCoreModel productCore)
    {
        // await _productCores.Add(productCore);
        // await _context.SaveChangesAsync(); 
        // return productCore;       
        throw new NotImplementedException();
    }
}
