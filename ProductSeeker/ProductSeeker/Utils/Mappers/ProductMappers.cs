using ProductSeeker.Data.DTOs;
using ProductSeeker.Data.Models;
using System.IO.IsolatedStorage;
using System.Runtime.CompilerServices;
using ProductSeeker;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static ProductSeeker.CreationSourceEnum;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using static CategoriesEnum;
namespace ProductSeeker.Services.Mappers
{
    static class ProductMappers
    {
        public static ProductCoreModel FromPOSTCoreDTOToModel(this POSTProductCoreDTO dto, string userID, CreationSource userRole)
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


        public static ProductSpecModel FromPOSTSpecDTOToModel(this POSTProductSpecDTObase dto, string userID, CreationSource creationSource)
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


        public static ProductCoreModel FromPOSTWCoreDTOtoCoreModel(this POSTProductWCoreDTObase dto, string userID, CreationSource userRole)
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


        public static GETProductCoreDTO FromModelToGETDTO(this ProductCoreModel model)
        {
            return new GETProductCoreDTO
            {
                Id = model.Id,
                ProductName = model.ProductName,
                Brand = model.Brand,
                Category = model.Category,
                IsActive = model.IsActive,
                CreationDate = model.CreationDate,
                CreationSource = model.CreationSource,
                IdCreator = model.IdCreator,
                Specs = model.Specs.Select(x => x.FromModelToGETDTO()).ToList()
            };
        }

        public static GETProductSpecDTO FromModelToGETDTO(this ProductSpecModel model)
        {
            return model.Category switch
            {
                ProductCategories.Food => new GETFoodProductDTO
                {
                    Id = model.Id,
                    IsActive = model.IsActive,
                    CreationDate = model.CreationDate,
                    CreationSource = model.CreationSource,
                    IdCreator = model.IdCreator,
                    Category = model.Category,
                    EAN = model.EAN,
                    ProductCoreId = model.ProductCoreId!,
                    UnitOfMeasure = ((FoodProductModel)model).UnitOfMeasure,
                    TACC = ((FoodProductModel)model).TACC
                },
                _ => throw new NotImplementedException("Category not implemented in mapper")

            };
        }


        /// <summary>
        /// Maps a POSTCoreWSpecDTO to SpecModel
        /// </summary>
        /// <remarks>
        /// The resulting Spec lacks coreId reference unless explicitly given.
        /// It may be necessary for POSTing along with a CORE and let the engine manage the relationship
        /// </remarks>
        /// <param name="dto"></param>
        /// <param name="userId"></param>
        /// <param name="creationSource"></param>
        /// <param name="coreID"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static ProductSpecModel FromPOSTCoreWSpecDTOToSpecModel(this POSTProductWCoreDTObase dto, string userId, CreationSource creationSource,
                                                                       int? coreID = null)
        {
            return dto.Category switch
            {
                ProductCategories.Food => new FoodProductModel
                {
                    IdCreator = userId,
                    ProductCoreId = coreID,
                    EAN = ((POSTFoodProductWCoreDTO)dto).EAN,
                    Category = dto.Category,
                    UnitOfMeasure = ((POSTFoodProductWCoreDTO)dto).UnitOfMeasure,
                    NetContent = ((POSTFoodProductWCoreDTO)dto).NetContent,
                    TACC = ((POSTFoodProductWCoreDTO)dto).TACC,
                    CreationSource = creationSource,
                    IsActive = true
                },
                _ => throw new NotImplementedException("Category not implemented in mapper")

            };
        }


    }

}