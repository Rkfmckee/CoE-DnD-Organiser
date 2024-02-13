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
        RuleFor(character => character.Name)
            .NotEmpty()
            .Length(NameLengthMinimumCharacters, NameLengthMaximumCharacters);

        RuleFor(character => character.Race)
            .NotEmpty()
            .Length(RaceLengthMinimumCharacters, RaceLengthMaximumCharacters);
        
        RuleFor(character => character.ClassLevels)
            .NotEmpty()
            .MinimumLength(ClassLengthMinimumCharacters);
    }
}