using System;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector.Editor.Validation;
using UnityEngine;

// namespace MIE {

[assembly: RegisterValidator(typeof(SerializedFieldsValidator))]

public class SerializedFieldsValidator : ValueValidator<IValidatableSerializedFields> {

    public static FieldInfo[] GetSerializeFields(Type type) {
        return type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(field => field.GetCustomAttribute<SerializeField>() != null)
            .ToArray();
    }

    public static FieldInfo[] GetSerializeFields<T>() => GetSerializeFields(typeof(T));


    protected override void Validate(ValidationResult result) {
        var fields = GetSerializeFields(Value.GetType());

        foreach (var field in fields) {
            var value = field.GetValue(Value);

            if (value == null)
                result.AddError($"Serialized Field: {field.Name} in Type: {field.FieldType.Name} is null");
        }
    }


}

public interface IValidatableSerializedFields { }
// }