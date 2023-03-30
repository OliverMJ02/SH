using System;
using System.Collections.Generic;
using System.Text;

namespace CurryFit.model.util
{
    public static class Extensions
    {
        public static void Swap<T>(this List<T> list, int i, int j)
        {
            T tmp = list[i];
            list[i] = list[j];
            list[j] = tmp;
        }
    }
}
