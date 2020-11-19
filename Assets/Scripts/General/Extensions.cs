namespace Extensions {
    using System.Collections.Generic;
    using System;
    using UnityEngine;

    public static class IListExtensions {
        public static void Randomise<T>(this IList<T> list) {
            int n = list.Count;
            for(int i = (n - 1); i > 0; i--) {
                int j = UnityEngine.Random.Range(0, i);
                T temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }
    }
}