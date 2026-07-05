using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProductSeeker.Data.Context;
using ProductSeeker.Data.Interfaces;
using ProductSeeker.Data.Models;
using ProductSeeker.Services.Mappers;
using ProductSeeker.Utils.NetTopologySuite;
using static ProductSeeker.CreationSourceEnum;


namespace ProductSeeker;

public class StoreService : IStoreService
{

    private readonly IStoreRepository _storeRepository;
    public StoreService(IStoreRepository storeRepo)
    {
        _storeRepository = storeRepo;
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
        if(string.IsNullOrWhiteSpace(storeDTO.Name) || string.IsNullOrWhiteSpace(storeDTO.Field))
            return Errors.FieldsRequired.WithMetadata("EmptyFields", "Name and Field are required fields and cannot be empty.");

        if(storeDTO.Name.Length > 50 || storeDTO.Field.Length > 50)
            return Errors.ValidationError.WithMetadata("ValidationErrors", "Name and Field must be at most 50 characters long.");
        
        
        
        var storeCore = storeDTO.FromStoreCoreDTOToStoreCoreModel(userID);

        return await _storeRepository.CreateCore(storeCore);

    }


    public async Task<Result<StoreSpecModel>> CreateStoreSpec(StoreSpecDTO storeDTO, string userID)
    {
        var core = await _storeRepository.GetCoreByID(storeDTO.StoreCoreId);

        if (core == null)
            return Errors.StoreCoreNotFound;

        if ((storeDTO.Latitude == null || storeDTO.Longitude == null) && string.IsNullOrWhiteSpace(storeDTO.BusinessDays))
            return Errors.FieldsRequired.WithMetadata("EmptyFields", "Must provide either geolocation (latitude and longitude) or business days information.");



        var storeModel = storeDTO.FromStoreSpecDTOToStoreSpecModel(userID);

        return await _storeRepository.CreateSpec(storeModel);

    }


    public async Task<bool> IsCoreOwner(int id, string UserId)
    {
        return await _storeRepository.IsCoreOwner(id, UserId);
    }


    public async Task<Result<StoreCoreModel>> CreateStoreWSpec(StoreWSpecDTO storeDTO, string userID)
    {
        var existingStore = await _storeRepository.GetByName(storeDTO.Name);

        //If the name is the same but the geolocation is different, we can assume it's a different store and allow the creation, otherwise we return a duplicate error
        if (storeDTO.Name == existingStore.Name && 
            LocationUtils.AreLocationsClose(LocationUtils.ConvertToPoint(storeDTO.Latitude, storeDTO.Longitude)!, existingStore.StoreSpecs.LastOrDefault()?.GeoLocation!))
            return Errors.Duplicate.WithMetadata("ExistingStoreId", existingStore.Id).WithMetadata("ExistingStoreName", existingStore.Name);


        //Spec is mapped along core and POSTed together
        //Hardcoded user role. Any other method of creation is handled separatly
        var storeCore = storeDTO.FromStoreWSpecDTOToStoreCoreModel(userID, CreationSource.User);
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
