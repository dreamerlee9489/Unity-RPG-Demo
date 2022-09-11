using System.Collections.Generic;

namespace Control.BT.Composite
{
    public static class Utils
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            System.Random random = new System.Random();
            int n = list.Count;
            while (n > 1)
            {
                int k = random.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
                n--;
            }
        }
    }
}