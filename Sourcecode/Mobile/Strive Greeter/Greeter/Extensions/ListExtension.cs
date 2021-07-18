using System;
using System.Collections.Generic;

namespace Greeter.Extensions
{
    public static class ListExtension
    {
        public static bool IsNullOrEmpty(this List<object> list) => list == null && list.Count == 0;
    }
}
