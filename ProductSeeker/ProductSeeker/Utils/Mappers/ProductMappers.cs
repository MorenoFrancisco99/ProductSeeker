using ProductSeeker.Data.DTOs;
using ProductSeeker.Data.Models;
using System.IO.IsolatedStorage;
using System.Runtime.CompilerServices;

namespace ProductSeeker.Services.Mappers
{
    static class ProductMappers
    {
        public static ProductCoreModel FromProductCoreDTOToModel(this ProductCoreDTO dto, string userID)
        {
            return new ProductCoreModel
            {
                ProductName = dto.ProductName,
                Brand = dto.Brand,
                IdCreator = userID,
                IsActive = true
            };
        }
        public static FoodProductModel MapSpecToModel(this FoodProductDTO dto, string userID)
        {
            return new FoodProductModel
            {
              Category = "Food",
              ProductCoreId = dto.ProductCoreId,
              NetContent = dto.NetContent,
              UnitOfMeasure = dto.UnitOfMeasure,
              TACC = dto.TACC,
              IdCreator = userID
            };
        }   

    }


    // public static ProductSpecModel FromProductSpecDTOToModel( this ProductSpecDTO dto, string userID)
    // {
    //     var spec = new ProductSpecModel
    //     {
    //         Category = dto.Category,
    //         ProductCoreId = dto.ProductCoreId,
    //         IdCreator = userID,
    //         IsActive = true
    //     };

    //     if (dto.Attributes is not null && dto.Attributes.Count > 0)
    //     {
    //         spec.Attributes = dto.Attributes
    //             .Select(psav => new ProductSpecAttributeValue
    //             {
    //                 AttributeKey = psav.Key,
    //                 AttributeValue = psav.Value,
    //                 IdCreator = userID
    //             })
    //             .ToList();
    //     }
    //     else
    //     {
    //         throw new ArgumentException("Attributes list cannot be empty");
    //     }

    //     return spec;
    // }


}
