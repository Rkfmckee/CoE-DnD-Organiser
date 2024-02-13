using FluentValidation;

namespace coe.dnd.api.ViewModels.Games;

public class CreateGameViewModel
{
    public string Details { get; set; }
    public int GameMasterId { get; set; }
    public int CampaignId { get; set; }
}

public class CreateGameValidator : AbstractValidator<CreateGameViewModel>
{
    public CreateGameValidator()
    {
        RuleFor(game => game.GameMasterId)
            .NotEmpty();
        
        RuleFor(game => game.CampaignId)
            .NotEmpty();
    }
}