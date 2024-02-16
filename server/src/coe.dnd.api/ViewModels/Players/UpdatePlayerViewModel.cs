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
        RuleFor(player => player)
            .Must(player => !string.IsNullOrEmpty(player.EmailAddress) ||
                            !string.IsNullOrEmpty(player.Name) ||
                            player.DateOfBirth != null ||
                            !string.IsNullOrEmpty(player.Password))
            .WithMessage("At least one value required")
            .WithName("NoValue");
        
        RuleFor(player => player.EmailAddress)
            .EmailAddress()
            .When(player => !string.IsNullOrEmpty(player.EmailAddress));

        RuleFor(player => player.Password)
            .Length(PasswordLengthMinimumCharacters, PasswordLengthMaximumCharacters)
            .Matches(@"[A-Z]+").WithMessage("'{PropertyName}' must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("'{PropertyName}' must contain at least one lowercase letter.")
            .Matches(@"[0-9]+").WithMessage("'{PropertyName}' must contain at least one number.")
            .When(player => !string.IsNullOrEmpty(player.Password));
    }
}