using Microsoft.EntityFrameworkCore;
using ProductSeeker.Data.Context;
using ProductSeeker.Data.DTOs;
using ProductSeeker.Data.Interfaces;
using ProductSeeker.Data.Models;
using ProductSeeker.Services.Mappers;

namespace ProductSeeker.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AplicationDBContext _context;
        public ProductRepository(AplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<ProductModel>> DeleteAllProductsAsync()
        {
            try
            {
                var allProducts = await _context.Products.ToListAsync();

                _context.Products.RemoveRange(allProducts);

                await _context.SaveChangesAsync();

                return allProducts;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                Console.WriteLine($"Error deleting all products: {ex.Message}");
                return new List<ProductModel>(); // Return an empty list if an error occurs
            }
        }
        public async Task<ProductModel?> DeleteByIDAsync(int id)
        {
            try
            {

                var prod = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

                if (prod == null) { return null; }

                _context.Products.Remove(prod);
                await _context.SaveChangesAsync();

                return prod;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                Console.WriteLine($"Error deleting product: {ex.Message}");
                return null; // Return null if an error occurs
            }

        }

        public async Task<List<ProductModel>> GetProductsAsync()
        {
            try
            {
                return await _context.Products.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                Console.WriteLine($"Error fetching products: {ex.Message}");
                return new List<ProductModel>(); // Return an empty list if an error occurs
            }
        }

        public async Task<ProductModel?> GetByIDAsync(int id)
        {
            try
            {
                return await _context.Products.FindAsync(id);
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                Console.WriteLine($"Error fetching product by ID: {ex.Message}");
                return null; // Return null if an error occurs
            }
        }

        public async Task<ProductModel> PostProductAsync(ProductModel model)
        {
            try
            {
                _context.Products.Add(model);

                await _context.SaveChangesAsync();

                return model;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                Console.WriteLine($"Error creating product: {ex.Message}");
                return null; // Return null if an error occurs
            }

        }

        public async Task<ProductModel?> PutProductAsync(int id, PUTProductDTO productDTO)
        {
            try
            {
                var existingProduct = await _context.Products.FindAsync(id);
                if (existingProduct == null)
                {
                    return null; // Product not found. We checked beforehand but just in case
                }
                existingProduct.Name = productDTO.Name;
                existingProduct.Brand = productDTO.Brand;
                existingProduct.Price = productDTO.Price;
                existingProduct.Quantity = productDTO.Quantity;
                existingProduct.UnitType = productDTO.UnitType;
                existingProduct.SubUnitQuantity = productDTO.SubUnitQuantity;
                existingProduct.SubUnitType = productDTO.SubUnitType;
                existingProduct.SubUnitAmount = productDTO.SubUnitAmount;
                existingProduct.ExtraInfo = productDTO.ExtraInfo;
                await _context.SaveChangesAsync();
                return existingProduct; // Return the updated product
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                Console.WriteLine($"Error updating product: {ex.Message}");
                return null; // Return null if an error occurs


            }
        }


        public async Task<List<ProductHistoryDTO>?> GetAllProductHistoryAsync(int id)
        {
            try
            {
                // tomado de: https://learn.microsoft.com/en-us/ef/core/providers/sql-server/temporal-tables#querying-historical-data
                var productHistory = await _context.Products
                        .TemporalAll()
                        .Where(p => p.Id == id)
                        .OrderBy(e => EF.Property<DateTime>(e, "ValidFrom"))
                        .Select(e => new ProductHistoryDTO
                        {
                            product = e.toProductDTO(), //This shouldnt be done here but i refuse to create another DTO
                            validFrom = EF.Property<DateTime>(e, "ValidFrom"),
                            validTo = EF.Property<DateTime>(e, "ValidTo")
                        })
                        .ToListAsync();

                return productHistory;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                Console.WriteLine($"Error fetching product history: {ex.Message}");
                return null; // Return null if an error occurs
            }
        }
    }
}
