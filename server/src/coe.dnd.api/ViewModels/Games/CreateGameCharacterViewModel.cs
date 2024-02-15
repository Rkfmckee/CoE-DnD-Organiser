using FluentValidation;

namespace coe.dnd.api.ViewModels.Games;

public class CreateGameCharacterViewModel
{
    public int GameId { get; set; }
    public int CharacterId { get; set; }
}

public class CreateGameCharacterValidator : AbstractValidator<CreateGameCharacterViewModel>
{
    public CreateGameCharacterValidator()
    {
        RuleFor(game => game.GameId)
            .NotEmpty();
        
        RuleFor(game => game.CharacterId)
            .NotEmpty();
    }
}