using FluentValidation;

namespace coe.dnd.api.ViewModels.Players;

public class UpdatePlayerViewModel
{
    public string EmailAddress { get; set; }
    public string Name { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string Password { get; set; }
}

public class UpdatePlayerValidator : AbstractValidator<UpdatePlayerViewModel>
{
    private const int PasswordLengthMinimumCharacters = CreatePlayerValidator.PasswordLengthMinimumCharacters;
    private const int PasswordLengthMaximumCharacters = CreatePlayerValidator.PasswordLengthMaximumCharacters;
    
    public UpdatePlayerValidator()
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