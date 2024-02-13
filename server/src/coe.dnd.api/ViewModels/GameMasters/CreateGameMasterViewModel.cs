using FluentValidation;

namespace coe.dnd.api.ViewModels.GameMasters;

public class CreateGameMasterViewModel
{
    public int PlayerId { get; set; }
    public string PlanningNotes { get; set; }
}

public class CreateGameMasterValidator : AbstractValidator<CreateGameMasterViewModel>
{
    public CreateGameMasterValidator()
    {
        RuleFor(gameMaster => gameMaster.PlayerId)
            .NotEmpty();
    }
}