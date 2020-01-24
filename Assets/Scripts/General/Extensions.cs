namespace Extensions {
    using System.Collections.Generic;
    using System;

    public static class IListExtensions {
        private static Random rnd = new Random();
        public static void Randomise<T>(this IList<T> list) {
            int n = list.Count;
            for(int i = (n - 1); i > 0; i--) {
                int j = rnd.Next(0, n);
                T temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }
    }
}