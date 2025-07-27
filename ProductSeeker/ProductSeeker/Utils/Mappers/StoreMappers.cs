using ProductSeeker.Data.DTOs;
using ProductSeeker.Data.Models;

namespace ProductSeeker.Services.Mappers
{
    static class StoreMappers
    {

        /// <summary>
        /// Maps a StoreModel to a StoreDTO.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static StoreDTO ToStoreDTO(this StoreModel model) {
            return new StoreDTO
            {
                Id = model.Id,
                Name = model.Name,
                ExtraInfo = model.ExtraInfo,
                Address = model.Address,
                BussinessDays = model.BussinessDays,
                Img = model.Img,
                Location = model.Location,
                Rating = model.Rating,
                Type = model.Type,
                ProductList = model.ProductList.Select(p => p.toProductDTO()).ToList()
            };
        }
        /// <summary>
        /// Maps a PostStoreDTO to a StoreModel.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static StoreModel ToStoreModel(this PostStoreDTO dto)
        {
            return new StoreModel
            {
                Name = dto.Name,
                ExtraInfo = dto.ExtraInfo,
                Address = dto.Address,
                BussinessDays = dto.BussinessDays,
                Img = dto.Img,
                Location = dto.Location,
                Rating = dto.Rating,
                Type = dto.Type
            };
        }
    }
}
