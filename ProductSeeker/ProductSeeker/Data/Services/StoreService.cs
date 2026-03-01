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
    private readonly AplicationDBContext _context;
    public StoreService(IStoreRepository storeRepo,
                        UserManager<AppUser> userManager,
                        AplicationDBContext context)
    {
        _storeRepository = storeRepo;
        _userManager = userManager;
        _context = context;
    }

    public async Task<StoreCoreModel?> GetCoreByID(int id)
    {
        return await _storeRepository.GetCoreByID(id);
    }
    
    public async Task<StoreSpecModel?> GetSpecByID(int id)
    {
        return await _storeRepository.GetSpecByID(id);
    }

    public async Task<StoreCoreModel?> CreateStoreCore(StoreCoreDTO storeDTO, string userID)
    {
        try
        {
            var storeCore = storeDTO.FromStoreCoreDTOToStoreCoreModel(userID);

            return await _storeRepository.CreateCore(storeCore);
           
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating StoreCore: {ex}");
        }
        return null;
    }


    public async Task<StoreSpecModel?> CreateStoreSpec(StoreSpecDTO storeDTO, string userID)
    {
        if (string.IsNullOrWhiteSpace(storeDTO.BusinessDays) && string.IsNullOrWhiteSpace(storeDTO.GeoLocation))
        {
            throw new ArgumentNullException(
                "At least BusinessDays or GeoLocation must be provided."
            );
        }
        var storeModel = storeDTO.FromStoreSpecDTOToStoreSpecModel(userID);
        
        return await _storeRepository.CreateSpec(storeModel);
        
    }


    public async Task<bool> IsCoreOwner(int id, string UserId)
    {
        return await _storeRepository.IsCoreOwner(id, UserId);
    }


    public async Task<StoreCoreModel?> CreateStoreWSpec(StoreWSpecDTO storeDTO, string userID)
    {
        throw new NotImplementedException();
        //NOTE: hard to implement bc storecore has to be inserted first in order to retrieve the ID
        // Then a failure in storeSpec can leed to childless cores
        // not exactly an issue but potentially undesired. Its safer to ask user for core first and specs later


        // try
        // {
        //     var (storeCore, storeSpec) = storeDTO.FromStoreWSpecDTOToModels();
        //     await _storeCoreRepo.Add()

        // }catch(Exception ex)
        // {
        //     Console.WriteLine($"Error craeting StoreWSpec: {ex}");
        // }


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
