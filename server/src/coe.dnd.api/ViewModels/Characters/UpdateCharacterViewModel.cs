using FluentValidation;

namespace coe.dnd.api.ViewModels.Characters;

public class UpdateCharacterViewModel
{
    public string Name { get; set; }
    public string Race { get; set; }
    public string ClassLevels { get; set; }
}

public class UpdateCharacterValidator : AbstractValidator<UpdateCharacterViewModel>
{
    private const int NameLengthMinimumCharacters = CreateCharacterValidator.NameLengthMinimumCharacters;
    private const int NameLengthMaximumCharacters = CreateCharacterValidator.NameLengthMaximumCharacters;
    
    private const int RaceLengthMinimumCharacters = CreateCharacterValidator.RaceLengthMinimumCharacters;
    private const int RaceLengthMaximumCharacters = CreateCharacterValidator.RaceLengthMaximumCharacters;
    
    private const int ClassLengthMinimumCharacters = CreateCharacterValidator.ClassLengthMinimumCharacters;
    
    public UpdateCharacterValidator()
    {
        RuleFor(character => character)
            .Must(character => !string.IsNullOrEmpty(character.Name) || 
                              !string.IsNullOrEmpty(character.Race) || 
                              !string.IsNullOrEmpty(character.ClassLevels))
            .WithMessage("At least one value required")
            .WithName("NoValue");
        
        RuleFor(character => character.Name)
            .Length(NameLengthMinimumCharacters, NameLengthMaximumCharacters)
            .When(character => !string.IsNullOrEmpty(character.Name));

        RuleFor(character => character.Race)
            .Length(RaceLengthMinimumCharacters, RaceLengthMaximumCharacters)
            .When(character => !string.IsNullOrEmpty(character.Race));
        
        RuleFor(character => character.ClassLevels)
            .MinimumLength(ClassLengthMinimumCharacters)
            .When(character => !string.IsNullOrEmpty(character.ClassLevels));
    }
}