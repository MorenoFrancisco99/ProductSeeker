using System.Collections;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using ProductSeeker.Data.Context;
using ProductSeeker.Data.Interfaces;
using ProductSeeker.Data.Models;
using ProductSeeker.Services.Mappers;
using static CategoriesEnum;
using static ProductSeeker.CreationSourceEnum;

namespace ProductSeeker;

public class ProductService : IProductService
{

    private readonly IProductRepository _productRepo;
    private readonly IStoreRepository _storeRepo;
    private readonly IServiceProvider _serviceProvider;
    private readonly IValidator<ProductCoreModel> _coreValidator;
    public ProductService(IProductRepository prodRepo, IStoreRepository storeRepo,
                          IServiceProvider serviceProvider, IValidator<ProductCoreModel> coreValidator)
    {
        _productRepo = prodRepo;
        _storeRepo = storeRepo;
        _serviceProvider = serviceProvider;
        _coreValidator = coreValidator;

    }
    public async Task<Result<ProductCoreModel>> CreateProductCore(POSTProductCoreDTO productDTO, string userID)
    {
        //Hardcode CreationSource to user
        //Any other source should be used only by admins or system, and that will be handled in a different method
        var model = productDTO.FromPOSTCoreDTOToModel(userID, CreationSource.User);

        var validationResult = await _coreValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            return validationResult.ToResult<ProductCoreModel>();
        }
        return await _productRepo.CreateCore(model);
    }

    public async Task<Result<ProductCoreModel>> CreateProductCoreWSpec(POSTProductWCoreDTObase dto, string userId)
    {
        var existingCore = await _productRepo.FindCore(dto.ProductName, dto.Brand);
        var coreModel = existingCore ?? dto.FromPOSTWCoreDTOtoCoreModel(userId, CreationSource.User);


        ProductSpecModel? specExists = null;
        if (existingCore is not null) //if the core exist in the DB we check if the spec exists for this Core
        {
            specExists = dto.Category switch
            {
                ProductCategories.Food => await _productRepo.GetSpecByPredicate(
                    existingCore.Id,
                    ((POSTFoodProductWCoreDTO)dto).MatchPredicate),
                _ => throw new NotSupportedException($"Categoría no soportada: {dto.Category}")
            };
        }
        var specModel = specExists ?? dto.FromPOSTCoreWSpecDTOToSpecModel(userId, CreationSource.User, null);


        //Spec and Core already exists
        if (existingCore is not null && specExists is not null)
        {
            existingCore.AddSpec(specExists);
            return Errors.Duplicate.WithMetadata("Resource", existingCore);
        }

        var coreValidation = existingCore is not null
            ? new ValidationResult()
            : await _coreValidator.ValidateAsync(coreModel);

        var specValidation = specExists is not null
            ? new ValidationResult()
            : await GetValidationForSpec(specModel, true);


        if (!coreValidation.IsValid || !specValidation.IsValid)
        {
            var coreResult = coreValidation.ToResult(coreModel);
            var specResult = specValidation.ToResult(specModel);

            if (!coreResult.IsSuccess && !specResult.IsSuccess)
                return coreResult.Error.AppendValidationMetadata(specResult.Error);

            return !coreResult.IsSuccess ? coreResult.Error : specResult.Error;
        }

        //Core exists but not Spec
        if (existingCore is not null && specExists is null)
        {
            specModel.Id = existingCore.Id;
            await _productRepo.CreateSpec(specModel);
            existingCore.AddSpec(specModel);

            Result<ProductCoreModel> result = existingCore;
            return result.WithMetadata("Message", "Core ya existente, spec agregado al core existente");
        }

        coreModel.AddSpec(specModel);
        return await _productRepo.CreateCore(coreModel);
    }

    public async Task<Result<ProductSpecModel>> CreateProductSpec(POSTProductSpecDTObase dto, string userID)
    {

        //Hardcode CreationSource to user
        //Any other source should be used only by admins, and that will be handled in a different method
        var model = dto.FromPOSTSpecDTOToModel(userID, CreationSource.User);

        //spc exist

        var specValidation = await GetValidationForSpec(model);

        if (!specValidation.IsValid)
            return specValidation.ToResult<ProductSpecModel>();

        return await _productRepo.CreateSpec(model);
    }

    public async Task<Result<AppUserProductPriceModel>> CreateProductPrice(POSTProductPriceDTO dto, string userID)
    {
        var specExists = await _productRepo.GetSpecByID(dto.ProductSpecId);
        if (specExists == null)
            return Errors.ProductSpecNotFound;

        var coreExists = await _storeRepo.GetCoreByID(dto.StoreId);
        if (coreExists == null)
            return Errors.StoreCoreNotFound;

        var model = dto.MapToModel(userID);
        return await _productRepo.CreatePrice(model);

    }



    public Task<IEnumerable<ProductCoreModel>> GetAllProducts()
    {
        throw new NotImplementedException();
    }

    public async Task<Result<ProductCoreModel>> GetCoreByID(int CoreId, string userID)
    {
        var result = await _productRepo.GetCoreByID(CoreId);
        if (result == null)
            return Errors.ProductCoreNotFound;
        return result;
    }

    public async Task<Result<AppUserProductPriceModel>> GetPriceByID(int priceId, string userID)
    {
        var result = await _productRepo.GetPriceByID(priceId);
        if (result == null)
            return Errors.UserPriceNotFound;
        return result;
    }

    public async Task<Result<GETProductSpecDTO>> GetSpecByID(int SpecId, string userID)
    {
        var result = await _productRepo.GetSpecByID(SpecId);
        if (result == null)
            return Errors.ProductSpecNotFound;
        return result.FromModelToGETDTO();
    }






    //     public async Task<Result<ProductSpecModel>> DEPRECATEDADMINCreateProductWCore(POSTProductWCoreDTO dto, string userID)
    //     {

    //         var existingCore = await _productRepo.FindCore(dto.ProductName, dto.Brand);


    //         ProductCoreModel core;
    //         if (existingCore != null)
    //         {
    //             core = existingCore;
    //         }
    //         else
    //         {
    //             var newCore = new ProductCoreModel
    //             {
    //                 Category = dto.Category,
    //                 ProductName = dto.ProductName,
    //                 Brand = dto.Brand,
    //                 IdCreator = userID,
    //                 CreationSource = CreationSourceEnum.CreationSource.Scrapped,
    //                 IsActive = true
    //             };

    //             var coreValidationResult = await _coreValidator.ValidateAsync(newCore);
    //             if (!coreValidationResult.IsValid)
    //             //  WRONG. MANAGE ERROR METADATA IN VALIDATRIO ERROR WITH ToResult()
    //                 return Errors.ValidationError.WithMetadata("ValidationErrors", coreValidationResult.Errors.Select(e => e.ErrorMessage));


    //             var createdCore = await _productRepo.CreateCore(newCore);
    //             if (createdCore == null) return Errors.ProductCoreNotFound;
    //             core = createdCore;
    //         }


    //         //Check if the specs exists
    //         //Sometimes specs may have the same Identifiers but not the same EA
    //         //var specByIdentifiers = await _productRepo.FindSpecByIdentifiers(core.Id, dto.GetSpecIdentifier());
    //         //if (specByIdentifiers != null) //Exists by identifiers
    //         //{
    //         //    var specByEAN = await _productRepo.GetSpecByEAN(dto?.EAN);
    //         //    if (specByEAN != null) //Exist by EAN
    //         //        return Errors.Duplicate.WithMetadata("Spec Product Already Exists, ID: ", specByIdentifiers.Id);
    //         //}
    // //
    //         //var spec = dto.FromPOSTCoreWSpecDTOToSpecModel(userID, CreationSource.Scrapped, core.Id);




    //        // var specValidation = await GetValidationForSpec(spec);
    // //
    //        // if (!specValidation.IsValid)
    //        //     return Errors.ValidationError.WithMetadata("ValidationErrors", specValidation.Errors.Select(e => e.ErrorMessage));
    // //
    // //
    // //
    // //
    //        // return await _productRepo.CreateSpec(spec);

    //     }









    /// <summary>
    /// Resolves and executes the appropriate <see cref="IValidator{T}"/> for the runtime type of <paramref name="spec"/> using reflection,
    /// since the concrete spec type is only known at runtime and cannot be resolved via a generic type parameter.
    /// The validator must be registered in the DI container for the spec's concrete type; validators are not created manually.
    /// </summary>
    /// <remarks>
    /// <see cref="FluentValidation.ValidatorFactory"/> is deprecated; resolving <see cref="IValidator{T}"/> instances via
    /// <see cref="IServiceProvider"/> with reflection is the approach currently recommended by the library's author
    /// (see https://github.com/FluentValidation/FluentValidation/issues/1961).
    /// </remarks>
    /// <param name="spec">The product spec model to validate. Its runtime type determines which validator is resolved.</param>
    /// <param name="isJointCreation">
    /// Indicates that the spec is being validated alongside the creation of another entity (e.g. a Core),
    /// meaning some fields such as <c>CoreId</c> may not yet exist or are not suitable for validation.
    /// Propagated to the validator via <see cref="ValidationContext{T}.RootContextData"/> under the key <c>"IsJointCreation"</c>,
    /// so rulesets can conditionally skip or adjust rules that depend on entities not yet created.
    /// </param>
    /// <returns>The <see cref="ValidationResult"/> produced by running the resolved validator against <paramref name="spec"/>.</returns>
    /// <exception cref="Exception">Thrown when no <see cref="IValidator{T}"/> is registered for the runtime type of <paramref name="spec"/>.</exception>
    private async Task<ValidationResult> GetValidationForSpec(ProductSpecModel spec, bool isJointCreation = false)
    {
        //Dynamic validations used to be done with ValidatorFactory but that has now been deprecated
        //Using reflection with IServiceProvider is now the recommended way by the author
        //Taken from https://github.com/FluentValidation/FluentValidation/issues/1961

        var validatorType = typeof(IValidator<>).MakeGenericType(spec.GetType());
        var validator = (IValidator)_serviceProvider.GetService(validatorType)!;
        if (validator == null)
            throw new Exception($"No validator found for type {spec.GetType()}");


        /// Note that for validator to work, the argument need to be wrapped in a ValidationContext<object>( ), so the validation can be executed with the correct ruleset. 
        /// The IsJointCreation flag is passed via RootContextData to allow validators to conditionally apply rules when the spec is being created alongside another entity.
        var context = new ValidationContext<object>(spec);
        context.RootContextData["IsJointCreation"] = isJointCreation;

        return await validator.ValidateAsync(context);
    }



}


