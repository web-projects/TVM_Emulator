using System.Reflection;

namespace System
{
    public static class ExtensionMethods
    {
        [AttributeUsage(AttributeTargets.All)]
        internal class StringValue : Attribute
        {
            public string Value { get; private set; }

            public StringValue(string value)
            {
                Value = value;
            }
        }

        public static string GetStringValue(this Enum value)
        {
            string stringValue = value?.ToString();
            Type type = value?.GetType();
            FieldInfo fieldInfo = type.GetField(value?.ToString());
            StringValue[] attrs = fieldInfo.GetCustomAttributes(typeof(StringValue), false) as StringValue[];
            if (attrs.Length > 0)
            {
                stringValue = attrs[0].Value;
            }
            return stringValue;
        }
    }
}
