using ProductSeeker.Data.DTOs;
using ProductSeeker.Data.Models;
using System.IO.IsolatedStorage;
using System.Runtime.CompilerServices;
using ProductSeeker;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static ProductSeeker.CreationSourceEnum;
namespace ProductSeeker.Services.Mappers
{
    static class ProductMappers
    {
        public static ProductCoreModel FromProductCoreDTOToModel(this POSTProductCoreDTO dto, string userID, CreationSource userRole)
        {
            return new ProductCoreModel
            {
                Category = dto.Category,
                ProductName = dto.ProductName,
                Brand = dto.Brand,
                IdCreator = userID,
                IsActive = true,
                CreationSource = userRole
            };
        }


        public static AppUserProductPriceModel MapToModel(this POSTProductPriceDTO dto, string userID)
        {
            return new AppUserProductPriceModel
            {
                IdCreator = userID,
                Price = dto.Price,
                ProductSpecId = dto.ProductSpecId,
                StoreId = dto.StoreId,
                ValidFrom = dto.ValidFrom ?? DateTime.UtcNow,
                CreationSource = CreationSourceEnum.CreationSource.Admin

            };
        }


        public static GETProductSpecDTO FromModelToGETDTO(this ProductSpecModel model)
        {
            return model.Category switch
            {
                CategoriesEnum.ProductCategories.Food => new GETFoodProductDTO
                {
                    Id = model.Id,
                    IsActive = model.IsActive,
                    CreationDate = model.CreationDate,
                    CreationSource = model.CreationSource,
                    IdCreator = model.IdCreator,
                    Category = model.Category,
                    EAN = model.EAN,
                    ProductCoreId = model.ProductCoreId,
                    UnitOfMeasure = ((FoodProductModel)model).UnitOfMeasure,
                    TACC = ((FoodProductModel)model).TACC
                },
                _ => new GETProductSpecDTO
                {
                    Id = model.Id,
                    IsActive = model.IsActive,
                    CreationDate = model.CreationDate,
                    CreationSource = model.CreationSource,
                    IdCreator = model.IdCreator,
                    Category = model.Category,
                    EAN = model.EAN,
                    ProductCoreId = model.ProductCoreId
                }
            };
        }

        public static ProductSpecModel FromDTOToModel(this POSTProductSpecDTO dto, string userID, CreationSource creationSource)
        {
            return dto.Category switch
            {
                CategoriesEnum.ProductCategories.Food => new FoodProductModel
                {
                    IdCreator = userID,
                    ProductCoreId = dto.ProductCoreId,
                    EAN = dto.EAN,
                    Category = dto.Category,
                    UnitOfMeasure = ((POSTFoodProductDTO)dto).UnitOfMeasure,
                    NetContent = ((POSTFoodProductDTO)dto).NetContent,
                    TACC = ((POSTFoodProductDTO)dto).TACC,
                    CreationSource = creationSource,
                    IsActive = true
                },
                _ => throw new NotImplementedException("Category not implemented in mapper")
            };
        }




    }

}