using coe.dnd.api.ViewModels.Campaigns;
using FluentValidation;

namespace coe.dnd.api.ViewModels.GameMasters;

public class UpdateGameMasterViewModel
{
    public string PlanningNotes { get; set; }
}

public class UpdateGameMasterValidator : AbstractValidator<UpdateGameMasterViewModel>
{
    public UpdateGameMasterValidator()
    {
        RuleFor(gameMaster => gameMaster)
            .Must(gameMaster => !string.IsNullOrEmpty(gameMaster.PlanningNotes))
            .WithMessage("At least one value required")
            .WithName("NoValue");
    }
}