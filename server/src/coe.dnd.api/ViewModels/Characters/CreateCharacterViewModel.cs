using FluentValidation;

namespace coe.dnd.api.ViewModels.Characters;

public class CreateCharacterViewModel
{
    public string Name { get; set; }
    public string Race { get; set; }
    public string ClassLevels { get; set; }
    public int PlayerId { get; set; }
}

public class CreateCharacterValidator : AbstractValidator<CreateCharacterViewModel>
{
    public const int NameLengthMinimumCharacters = 2;
    public const int NameLengthMaximumCharacters = 100;
    
    public const int RaceLengthMinimumCharacters = 2;
    public const int RaceLengthMaximumCharacters = 100;
    
    public const int ClassLengthMinimumCharacters = 2;
    
    public CreateCharacterValidator()
    {
        RuleFor(character => character.Name)
            .NotEmpty()
            .Length(NameLengthMinimumCharacters, NameLengthMaximumCharacters);

        RuleFor(character => character.Race)
            .NotEmpty()
            .Length(RaceLengthMinimumCharacters, RaceLengthMaximumCharacters);
        
        RuleFor(character => character.ClassLevels)
            .NotEmpty()
            .MinimumLength(ClassLengthMinimumCharacters);

        RuleFor(character => character.PlayerId)
            .NotEmpty();
    }
}