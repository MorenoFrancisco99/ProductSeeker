


using FluentValidation;
using Microsoft.AspNetCore.Identity;
using ProductSeeker;
using ProductSeeker.Data.Models;

public class BaseEntityValidator<T> : AbstractValidator<T> where T : BaseEntity
{
    private readonly UserManager<AppUser> _userManager;

   
    public BaseEntityValidator(UserManager<AppUser> userManager)
    {
        _userManager = userManager;


        RuleFor(x => x.IsActive)
            .NotNull()
            .WithMessage("IsActive status is required");

        RuleFor(x => x.CreationSource)
            .NotNull()
            .WithMessage("Creation source is required")
            .Must(source => Enum.IsDefined(typeof(CreationSourceEnum.CreationSource), source))
            .WithMessage("Creation source must be a valid value")
            .NotEqual(CreationSourceEnum.CreationSource.Unknown)
            .WithMessage("Creation source cannot be unknown");


        RuleFor(x => x.IdCreator)
            .NotNull()
            .WithMessage("Creator ID is required")
            .MustAsync(async (idCreator, cancellation) =>
            {
                var user = await _userManager.FindByIdAsync(idCreator);
                return user != null;
            })
            .WithMessage("Creator ID must correspond to an existing user");
    }

}