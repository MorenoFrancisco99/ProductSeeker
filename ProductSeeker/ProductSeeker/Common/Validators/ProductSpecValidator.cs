using ProductSeeker.Data.Models;
using FluentValidation;
using Microsoft.Extensions.Options;
using System.Data;
using Microsoft.AspNetCore.Identity;
namespace ProductSeeker;

public class ProductSpecValidator<T> : BaseEntityValidator<T> where T : ProductSpecModel
{
    private readonly IProductRepository _prodRepo;
    public ProductSpecValidator(IProductRepository prodrepo, UserManager<AppUser> userManager, bool IsJointCreation) : base(userManager)
    {

        _prodRepo = prodrepo;

        RuleFor(x => x.Category)
        .NotNull()
        .Must(Category => CategoriesEnum.ProductCategories.IsDefined(typeof(CategoriesEnum.ProductCategories), Category))
        .WithMessage("Product must have a valid category")
        .NotEqual(CategoriesEnum.ProductCategories.Unknown)
        .WithMessage("Product category is unknown");

        if(!IsJointCreation)
        {
            RuleFor(x => x.ProductCoreId)
                .NotNull()
                .MustAsync(async (product, prodCoreId, context, cancellation) =>
                {
                    var core = await _prodRepo.GetCoreByID((int)prodCoreId!);

                    if (core == null)
                    {
                        context.MessageFormatter.AppendArgument("ErrorMessage", "Target product does not exist");
                        return false;
                    }

                    if (core.Category != product.Category)
                    {
                        context.MessageFormatter.AppendArgument("ErrorMessage",
                            $"The category of the spec must match the category of the core. " +
                            $"Core: {core.Category}, Spec: {product.Category}");
                        return false;
                    }

                    return true;
                })
                .WithMessage("{ErrorMessage}");
        }
        



        RuleFor(x => x.EAN)
        .NotNull()
        .MustAsync(async (ean, cancellation) =>
           {
               var exist = await _prodRepo.GetSpecByEAN(ean);
               if (exist != null)
               {
                   return false;
               }
               return true;
           }).WithMessage("Another product with the same EAN already exists");


        // _rules = options.Value;
        // _prodRepo = prodrepo;

        // string _msgError = "";

        // RuleFor(x => x.Category)
        // .NotNull()
        // .Must(Category => _rules.Categories.ContainsKey(Category))
        // .WithMessage("Product must have a valid category");




        //1. Each attribute must have a valid key, that means, existing for the given category
        //2. Every attribute flaged as "required" must be present
        //3. The value of the attribute must be valid
        // RuleFor(x => x.Attributes)
        // .Must((model, attributes) =>
        // {
        //     //rule 2

        //     //Get attributes of the given category 
        //     _rules.Categories.TryGetValue(model.Category, out var catConfig);

        //     //get keys of flagged as required
        //     var requiredAtt = catConfig.Attributes
        //     .Where(x => x.Value.IsRequired == true)
        //     .Select(x => x.Key);

        //     //keys of received attributes
        //     var recievedAttKey = attributes.Select(a => a.AttributeKey);

        //     //We get the required attributes thar are not present in recieved attributes
        //     var requiredMissing = requiredAtt.Except(recievedAttKey);

        //     if (requiredMissing.Any())
        //     {
        //         _msgError = $"Missing required fields: {string.Join(",", requiredMissing)}";
        //         return false;
        //     }


        //     //Rule 1
        //     //we get the received attributes that are not pressent on the category attributes
        //     var invalidAtt = recievedAttKey.Except(catConfig.Attributes.Select(x => x.Key));
        //     if(invalidAtt.Any())
        //     {
        //         _msgError =$"Invalid attributes for the category received: {string.Join(",", invalidAtt)}";
        //         return false;
        //     }





        //     return true;
        // })
        // .WithMessage(_msgError);








    }



}
