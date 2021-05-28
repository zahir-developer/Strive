using System;
namespace Greeter.Extensions
{
    public static class StringExtension
    {
        public static bool IsEmpty(this string txt) => string.IsNullOrWhiteSpace(txt);
    }
}
