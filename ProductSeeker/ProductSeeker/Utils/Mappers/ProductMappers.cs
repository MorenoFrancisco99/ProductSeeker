using ProductSeeker.Data.DTOs;
using ProductSeeker.Data.Models;
using System.Runtime.CompilerServices;

namespace ProductSeeker.Services.Mappers
{
    static class ProductMappers
    {
        /// <summary>
        /// From ProductModel to ProductDTO
        /// </summary>
        public static ProductDTO toProductDTO(this ProductModel model)
        {
            return new ProductDTO
            {
                Name = model.Name,
                StoreId = model.StoreId,
                Brand = model.Brand,
                Price = model.Price,
                Quantity = model.Quantity,
                UnitType = model.UnitType,
                SubUnitQuantity = model.SubUnitQuantity,
                SubUnitType = model.SubUnitType,
                SubUnitAmount = model.SubUnitAmount,
                ExtraInfo = model.ExtraInfo

            };
        }

        //Momentarily ProductDTO and PostProductDTO are the same
        /// <summary>
        /// From PostProductDTO to ProductModel
        /// </summary>
        /// <param name="product"></param>
        /// <param name="storeId"></param>
        /// <remarks>The storeID need to be validated beforehand</remarks>
        /// <returns></returns>
        public static ProductModel fromPostDTOtoModel(this POSTProductDTO product)   
        {
            return new ProductModel
            {
                Name = product.Name,
                StoreId = product.StoreId,
                Brand = product.Brand,
                Price = product.Price,
                Quantity = product.Quantity,
                UnitType = product.UnitType,
                SubUnitQuantity = product.SubUnitQuantity,
                SubUnitType = product.SubUnitType,
                SubUnitAmount = product.SubUnitAmount,
                ExtraInfo = product.ExtraInfo

            };
        }
    }
}
