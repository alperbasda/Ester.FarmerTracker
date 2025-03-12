using System.ComponentModel;
using System.Reflection;

namespace Ester.FarmetTracker.Common.Extensions;

public static class EnumExtensions
{
    public static string GetvalueValue<T>(this T enumerationValue, string value)
           where T : struct
    {
        Type type = enumerationValue.GetType();
        if (!type.IsEnum)
        {
            return "";
        }

        MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString()!);
        if (memberInfo.Length > 0)
        {
            object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attrs.Length > 0)
            {
                return ((DescriptionAttribute[])attrs).FirstOrDefault(w => string.Equals(w.Description, value, StringComparison.OrdinalIgnoreCase))?.Description ?? enumerationValue.ToString()!;
            }
        }
        return enumerationValue.ToString()!;
    }

}
