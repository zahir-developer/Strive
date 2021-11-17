using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Greeter.Extensions
{
    public static class ListExtension
    {
        public static bool IsNullOrEmpty<T>(this List<T> list) => list == null || list.Count == 0;

        //public static async List<object> Search(this List<object> list, string keyword) =>
        //     await Task.Run(() => list.Where(item => item.Name.Contains(keyword)).ToList());
    }
}
