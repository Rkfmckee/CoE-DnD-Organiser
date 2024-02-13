using coe.dnd.api.ViewModels.Players;
using FluentValidation;

namespace coe.dnd.api.ViewModels.Players;

public class CreatePlayerViewModel
{
    public string EmailAddress { get; set; }
    public string Name { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string Password { get; set; }
}

public class CreatePlayerValidator : AbstractValidator<CreatePlayerViewModel>
{
    public const int PasswordLengthMinimumCharacters = 8;
    public const int PasswordLengthMaximumCharacters = 30;
    
    public CreatePlayerValidator()
    {
        RuleFor(player => player.EmailAddress)
            .EmailAddress();

        RuleFor(player => player.Password)
            .NotEmpty()
            .Length(PasswordLengthMinimumCharacters, PasswordLengthMaximumCharacters)
            .Matches(@"[A-Z]+").WithMessage("'{PropertyName}' must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("'{PropertyName}' must contain at least one lowercase letter.")
            .Matches(@"[0-9]+").WithMessage("'{PropertyName}' must contain at least one number.");
    }
}