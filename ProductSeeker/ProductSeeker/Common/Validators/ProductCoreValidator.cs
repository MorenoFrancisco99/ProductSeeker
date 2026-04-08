using FluentValidation;
using Microsoft.AspNetCore.Identity;
using ProductSeeker.Data.Models;

public class ProductCoreValidator : BaseEntityValidator<ProductCoreModel> 
{
    public ProductCoreValidator(UserManager<AppUser> userManager) : base(userManager)
    {
        RuleFor(x => x.ProductName)
            .NotEmpty()
            .WithMessage("Product name is required")
            .MaximumLength(100)
            .WithMessage("Product name cannot exceed 100 characters");

        RuleFor(x => x.Brand)
            .NotEmpty()
            .WithMessage("Brand is required")
            .MaximumLength(50)
            .WithMessage("Brand cannot exceed 50 characters");

        RuleFor(x => x.Category)
            .NotNull()
            .Must(Category => CategoriesEnum.ProductCategories.IsDefined(typeof(CategoriesEnum.ProductCategories), Category))
            .WithMessage("Product must have a valid category")
            .NotEqual(CategoriesEnum.ProductCategories.Unknown)
            .WithMessage("Product category is unknown");    
    }
}