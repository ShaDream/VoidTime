﻿using System;
using System.ComponentModel;

namespace VoidTime
{
    public static class EnumExtensions
    {
        public static string GetDescription<T>(this T enumerationValue)
            where T : struct
        {
            var type = enumerationValue.GetType();
            if (!type.IsEnum) throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");

            //Tries to find a DescriptionAttribute for a potential friendly name
            //for the enum
            var memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo == null || memberInfo.Length <= 0)
                return enumerationValue.ToString();

            var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attrs != null && attrs.Length > 0) return ((DescriptionAttribute) attrs[0]).Description;
            //If we have no description attribute, just return the ToString of the enum
            return enumerationValue.ToString();
        }
    }
}