using MIE;
using Sirenix.OdinInspector.Editor.Validation;

[assembly: RegisterValidator(typeof(DifficultyLevelSoValidator))]

public class DifficultyLevelSoValidator : RootObjectValidator<DifficultyLevelSo> {
    protected override void Validate(ValidationResult result) {
        if (Value.dictionaryTextAsset == null) result.AddError("Dictionary Text Asset is null");
    }
}