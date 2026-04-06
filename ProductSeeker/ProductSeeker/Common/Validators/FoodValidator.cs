using FluentValidation;
using Microsoft.AspNetCore.Identity;
using ProductSeeker;
using ProductSeeker.Data.Models;

public class FoodValidator : ProductSpecValidator<FoodProductModel>
{
    public FoodValidator(IProductRepository prodrepo, UserManager<AppUser> userManager) : base(prodrepo, userManager)
    {
        RuleFor(x => x.NetContent)
            .NotNull()
            .WithMessage("Net content is required")
            //Same value aplied as constrain Model
            //Modification here should be reflected in the model and vice versa
            .InclusiveBetween(1, 10000) 
            .WithMessage("Net content must be greater than zero and less than or equal to 10000");

        RuleFor(x => x.UnitOfMeasure)
            .NotNull()
            .WithMessage("Unit of measure is required")
            .Must(unit => Enum.IsDefined(typeof(UnitOfMeasureEnum.Unit), unit))
            .WithMessage("Unit of measure must be a valid value")
            .NotEqual(UnitOfMeasureEnum.Unit.Unknown)
            .WithMessage("Unit of measure cannot be Unknown");
    }
} 