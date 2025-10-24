using UnityEngine;

namespace Vhrtz
{
    public class EnableIfAttribute : PropertyAttribute
    {
        public string FieldName { get; set; } = string.Empty;

        public EnableIfAttribute(string fieldName)
        {
            FieldName = fieldName;
        }
    }
}
