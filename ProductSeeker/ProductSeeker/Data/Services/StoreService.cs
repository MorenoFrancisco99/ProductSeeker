using ProductSeeker.Data.DTOs;
using ProductSeeker.Data.Interfaces;
using ProductSeeker.Data.Models;
using ProductSeeker.Services.Mappers;

namespace ProductSeeker.Data.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IAppUserStoreRepository _appUserStoreRepository;
        public StoreService(IStoreRepository storeRepository , IAppUserStoreRepository appUserStoreRepository)
        {
            _storeRepository = storeRepository;
            _appUserStoreRepository = appUserStoreRepository;
        }


        public async Task<List<StoreDTO>> GetAllStoresAsync()
        {
            var stores = await _storeRepository.GetStoresAsync();
            var storeDTOs = stores.Select(store => store.ToStoreDTO()).ToList();

            return storeDTOs;
        }
        public async Task<StoreModel?> CreateStoreAsync(AppUser user, PostStoreDTO userStore)
        {
            var storeModel = userStore.ToStoreModel();
            var store = await _storeRepository.PostStoreAsync(storeModel);
            if (store == null)
            {
                return null;
            }

            // creates the junction between the user and the store and adds it to the database
            var appUserStore = await _appUserStoreRepository.PostUserStoreAsync(user, store);
            if (appUserStore == null)
            {
                await _storeRepository.DeleteStoreByIDAsync(store.Id);
                return null;
            }
            return store; 

        }

        public async Task<List<StoreDTO>> GetUserStoresAsync(AppUser user)
        {
            var userStores = await _appUserStoreRepository.GetUserStoresAsync(user);

            var storeDTOs = userStores.Select(store => store.ToStoreDTO()).ToList();
            return storeDTOs;   
        }
    }
}
