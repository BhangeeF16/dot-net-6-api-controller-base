using System.ComponentModel;

namespace Domain.Common.Utilities
{
    public static class EnumsHelper
    {
        public static T ToEnum<T>(this string enumDescription) where T : struct, IConvertible
        {
            Enum.TryParse(enumDescription, true, out T result);
            return result;
        }
        public static string GetDescription<T>(this T enumValue) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                return null;

            var description = enumValue.ToString();
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString() ?? string.Empty);

            if (fieldInfo != null)
            {
                var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    description = ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return description ?? string.Empty;
        }
    }
}
