﻿namespace Core.Extensions;

public static class EnumExtensions
{
    public static string GetEnumName<TEnum>(this TEnum enumValue) where TEnum : Enum
    {
        return Enum.GetName(typeof(TEnum), enumValue)!;
    }
}