using ProductSeeker.Data.DTOs;
using ProductSeeker.Data.Models;

namespace ProductSeeker.Services.Mappers
{
    static class StoreMappers
    {


        /// <summary>
        /// Creates a new StoreCoreModel instance from a StoreCoreDTO.
        /// </summary>
        /// <remarks>
        /// Initializes domain-controlled properties:
        /// - IdCreator is assigned from the provided userID.
        /// - IsActive is set to true by default.
        /// Assumes DTO validation has already been performed.
        /// </remarks>
        /// <param name="dto">Validated data transfer object.</param>
        /// <param name="userID">Identifier of the user creating the entity.</param>
        /// <returns>A new StoreCoreModel ready for persistence.</returns>


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

        /// <summary>
        /// Creates a new StoreSpecModel instance from a StoreSpecDTO.
        /// </summary>
        /// <remarks>
        /// Maps optional properties as provided in the DTO.
        /// Initializes domain-controlled fields:
        /// - IdCreator is assigned from the provided userID.
        /// - IsActive is set to true.
        /// - ValidFrom defaults to the current UTC time if not specified.
        /// 
        /// Requires at least one of BusinessDays or GeoLocation
        /// Is expected to be handled outside this mapper.
        /// </remarks>
        /// <param name="dto">Source data transfer object.</param>
        /// <param name="userID">Identifier of the user creating the entity.</param>
        /// <returns>A new StoreSpecModel instance ready for persistence.</returns>

        public static StoreSpecModel FromStoreSpecDTOToStoreSpecModel(this StoreSpecDTO dto, string userID)
        {
            return new StoreSpecModel
            {
                StoreCoreId = dto.StoreCoreId,
                IdCreator = userID,
                BusinessDays = dto.BusinessDays,
                GeoLocation = dto.GeoLocation,
                IsActive = true,
                ValidFrom = dto.ValidFrom ?? DateTime.UtcNow,
                ValidTo = dto.ValidTo
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
