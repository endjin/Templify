namespace Endjin.Templify.Domain.Framework
{
    #region Using Directives

    using System;
    using System.ComponentModel;
    using System.Reflection;

    #endregion

    public static class EnumExtensions
    {
            public static string GetDescription(this Enum en)
            {
                Type type = en.GetType();

                MemberInfo[] memInfo = type.GetMember(en.ToString());

                if (memInfo.Length > 0)
                {
                    object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                    if (attrs.Length > 0)
                    {
                        return ((DescriptionAttribute)attrs[0]).Description;
                    }
                }

                return en.ToString();
            }
    }
}