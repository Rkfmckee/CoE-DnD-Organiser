using coe.dnd.api.ViewModels.Campaigns;
using FluentValidation;

namespace coe.dnd.api.ViewModels.Games;

public class UpdateGameViewModel
{
    public string Details { get; set; }
    public int? GameMasterId { get; set; }
    public int? CampaignId { get; set; }
}

public class UpdateGameValidator : AbstractValidator<UpdateGameViewModel>
{
    private const int NameLengthMinimumCharacters = CreateCampaignValidator.NameLengthMinimumCharacters;
    private const int NameLengthMaximumCharacters = CreateCampaignValidator.NameLengthMaximumCharacters;
    
    public UpdateGameValidator()
    {
        RuleFor(game => game)
            .Must(game => !string.IsNullOrEmpty(game.Details) || 
                              game.GameMasterId != null ||
                              game.CampaignId != null)
            .WithMessage("At least one value required")
            .WithName("NoValue");
        
        RuleFor(game => game.GameMasterId)
            .GreaterThan(0)
            .When(game => game.GameMasterId != null);
        
        RuleFor(game => game.CampaignId)
            .GreaterThan(0)
            .When(game => game.CampaignId != null);
    }
}