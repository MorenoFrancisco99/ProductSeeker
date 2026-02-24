using ProductSeeker.Data.DTOs;
using ProductSeeker.Data.Models;

namespace ProductSeeker.Services.Mappers
{
    static class StoreMappers
    {
        /// <summary>
        /// Maps a StoreWSpecDTO to a StoreCoreModel and StoreSpecModel
        /// </summary>
        /// <remarks>
        /// StoreSpecModel it's initialized with no StoreCoreModelId asigned
        /// </remarks>
        /// <param name="dto"></param>
        /// <returns>A tuple (StoreCoreModel, StoreSpecModel)</returns>
        public static (StoreCoreModel, StoreSpecModel) FromStoreWSpecDTOToModels(this StoreWSpecDTO dto)
        {
            var storeCore = new StoreCoreModel
            {
                Name = dto.Name,
                Field = dto.Field,
                IdCreator = dto.IdCreator
            };

            var storeSpec = new StoreSpecModel
            {
                GeoLocation = dto.GeoLocation,
                BusinessDays = dto.BusinessDays,
                IdCreator = dto.IdCreator
            };

            return (storeCore, storeSpec);
        }


        
        public static StoreCoreModel FromStoreCoreDTOToStoreCoreModel(this StoreCoreDTO dto, string userID)
        {
            return new StoreCoreModel
            {
                Name = dto.Name,
                Field = dto.Field,
                IdCreator = userID,
                IsActive = true,
            };
        }
        //         /// <summary>
        //         /// Maps a StoreModel to a StoreDTO.
        //         /// </summary>
        //         /// <param name="model"></param>
        //         /// <returns></returns>
        //         public static StoreDTO ToStoreDTO(this StoreModel model) {
        //             return new StoreDTO
        //             {
        //                 Id = model.Id,
        //                 Name = model.Name,
        //                 ExtraInfo = model.ExtraInfo,
        //                 Address = model.Address,
        //                 BussinessDays = model.BussinessDays,
        //                 Img = model.Img,
        //                 Location = model.Location,
        //                 Rating = model.Rating,
        //                 Type = model.Type,
        //                 ProductList = model.ProductList.Select(p => p.toProductDTO()).ToList()
        //             };
        //         }
        //         /// <summary>
        //         /// Maps a PostStoreDTO to a StoreModel.
        //         /// </summary>
        //         /// <param name="dto"></param>
        //         /// <returns></returns>
        //         public static StoreModel ToStoreModel(this PostStoreDTO dto)
        //         {
        //             return new StoreModel
        //             {
        //                 Name = dto.Name,
        //                 ExtraInfo = dto.ExtraInfo,
        //                 Address = dto.Address,
        //                 BussinessDays = dto.BussinessDays,
        //                 Img = dto.Img,
        //                 Location = dto.Location,
        //                 Rating = dto.Rating,
        //                 Type = dto.Type
        //             };
        //         }
    }
}
