using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProductSeeker.Data.Context;
using ProductSeeker.Data.Interfaces;
using ProductSeeker.Data.Models;
using ProductSeeker.Services.Mappers;

namespace ProductSeeker;

public class StoreService : IStoreService
{

    private readonly IStoreRepository _storeRepository;
    private readonly UserManager<AppUser> _userManager;
    public StoreService(IStoreRepository storeRepo,
                        UserManager<AppUser> userManager)
    {
        _storeRepository = storeRepo;
        _userManager = userManager;
    }

    public async Task<Result<StoreCoreModel>> GetCoreByID(int CoreId, string userID)
    {
        var result = await _storeRepository.GetCoreByID(CoreId);
        if (result == null)
            return Errors.StoreCoreNotFound;
        if (result.IdCreator != userID)
            return Errors.UnauthorizedAccess;

        return result;
    }


    public async Task<Result<StoreSpecModel>> GetSpecByID(int SpecId, string userID)
    {
        var result = await _storeRepository.GetSpecByID(SpecId);
        if (result == null)
            return Errors.StoreSpecNotFound;
        if (result.IdCreator != userID)
            return Errors.UnauthorizedAccess;

        return result;
    }

    public async Task<Result<StoreCoreModel>> CreateStoreCore(StoreCoreDTO storeDTO, string userID)
    {
        var storeCore = storeDTO.FromStoreCoreDTOToStoreCoreModel(userID);

        return await _storeRepository.CreateCore(storeCore);

    }


    public async Task<Result<StoreSpecModel>> CreateStoreSpec(StoreSpecDTO storeDTO, string userID)
    {
        var core = await _storeRepository.GetCoreByID(storeDTO.StoreCoreId);


        if (core == null)
            return Errors.StoreCoreNotFound;
        if (string.IsNullOrWhiteSpace(storeDTO.BusinessDays) && string.IsNullOrWhiteSpace(storeDTO.GeoLocation))
        {
            return Errors.FieldsRequired.WithMetadata("MissingFields", ("BussinesDays", "GeoLocation"));
        }
        var storeModel = storeDTO.FromStoreSpecDTOToStoreSpecModel(userID);

        return await _storeRepository.CreateSpec(storeModel);

    }


    public async Task<bool> IsCoreOwner(int id, string UserId)
    {
        return await _storeRepository.IsCoreOwner(id, UserId);
    }


    public async Task<Result<StoreCoreModel>> CreateStoreWSpec(StoreWSpecDTO storeDTO, string userID)
    {
        //TODO search and return existing store if name already exists, to avoid duplicates
        //Check geolocation to confirm if it's the same store or a different one with the same name
        //For the time being, we will assume that the combination of name and geolocation is unique

        var storeCore = storeDTO.FromStoreWSpecDTOToStoreCoreModel(userID);
        return await _storeRepository.CreateCore(storeCore);

    }

    public Task<StoreCoreModel> GetByName(string name)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsSpecOwner(int SpecId, string UserID)
    {
        throw new NotImplementedException();
    }
}
