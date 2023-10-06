using System;
using System.Collections.Generic;

namespace TVMEmulator.helpers
{
    static class Extensions  //Part of JSON Helper
    {
        public static void ForEach<T>(this IEnumerable<T> ie, Action<T> action)
        {
            foreach (var i in ie)
            {
                action(i);
            }
        }
    }
}
