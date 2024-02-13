using FluentValidation;

namespace coe.dnd.api.ViewModels.Campaigns;

public class CreateCampaignViewModel
{
    public string Name { get; set; }
    public string Theme { get; set; }
    public string Details { get; set; }
    public string Writer { get; set; }
}

public class CreateCampaignValidator : AbstractValidator<CreateCampaignViewModel>
{
    public const int NameLengthMinimumCharacters = 2;
    public const int NameLengthMaximumCharacters = 100;
    
    public CreateCampaignValidator()
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