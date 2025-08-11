using System.Collections.Generic;

namespace XTools {
    public static class XidListExtensions {
        public static bool HasPrevious<T>(this IList<T> list, T item) {
            int index = list.IndexOf(item);
            return index > 0;
        }
    
        public static bool HasNext<T>(this IList<T> list, T item) {
            int index = list.IndexOf(item);
            return index >= 0 && index < list.Count - 1;
        }
    
        public static T GetPrevious<T>(this IList<T> list, T item) {
            int index = list.IndexOf(item);
            return index > 0 ? list[index - 1] : default(T);
        }
    
        public static T GetNext<T>(this IList<T> list, T item) {
            int index = list.IndexOf(item);
            return index >= 0 && index < list.Count - 1 ? list[index + 1] : default(T);
        }

    }
}