using FluentValidation;

namespace coe.dnd.api.ViewModels.Campaigns;

public class UpdateCampaignViewModel
{
    public string Name { get; set; }
    public string Theme { get; set; }
    public string Details { get; set; }
    public string Writer { get; set; }
}

public class UpdateCampaignValidator : AbstractValidator<UpdateCampaignViewModel>
{
    private const int NameLengthMinimumCharacters = CreateCampaignValidator.NameLengthMinimumCharacters;
    private const int NameLengthMaximumCharacters = CreateCampaignValidator.NameLengthMaximumCharacters;
    
    public UpdateCampaignValidator()
    {
        RuleFor(campaign => campaign)
            .Must(campaign => campaign.Name != null || campaign.Theme != null || campaign.Details != null || campaign.Writer != null)
            .WithMessage("At least one value required")
            .WithName("NoValue");
        
        RuleFor(campaign => campaign.Name)
            .Length(NameLengthMinimumCharacters, NameLengthMaximumCharacters)
            .When(campaign => !string.IsNullOrEmpty(campaign.Name));
    }
}