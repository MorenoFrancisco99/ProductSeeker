using Microsoft.AspNetCore.Identity;
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

    public async Task<StoreCoreModel?> CreateStoreCore(StoreCoreDTO storeDTO, string userID)
    {
        try
        {
            var storeCore = storeDTO.FromStoreCoreDTOToStoreCoreModel(userID);

            var newModel = await _storeRepository.CreateCore(storeCore);
            return newModel;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating StoreCore: {ex}");
        }
        return null;
    }


    public Task<StoreCoreModel?> CreateStoreSpec(StoreSpecDTO storeDTO)
    {
        throw new NotImplementedException();
    }


    public async Task<StoreCoreModel?> CreateStoreWSpec(StoreWSpecDTO storeDTO)
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

    public Task<IEnumerable<StoreCoreModel>> GetAllProducts()
    {
        throw new NotImplementedException();
    }



    public Task<StoreCoreModel> GetByName(string name)
    {
        throw new NotImplementedException();
    }
}
