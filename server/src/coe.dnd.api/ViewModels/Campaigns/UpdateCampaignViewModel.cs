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
        RuleFor(campaign => campaign.Name)
            .NotEmpty()
            .Length(NameLengthMinimumCharacters, NameLengthMaximumCharacters);

        RuleFor(campaign => campaign.Theme)
            .NotEmpty();
        
        RuleFor(campaign => campaign.Writer)
            .NotEmpty();
    }
}