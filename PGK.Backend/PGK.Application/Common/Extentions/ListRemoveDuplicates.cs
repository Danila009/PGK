namespace PGK.Application.Common.Extentions
{
    public static class ListRemoveDuplicates
    {
        public static IList<T> RemoveDuplicates<T>(this IList<T> list)
        {
            return new HashSet<T>(list).ToList();
        }

        public static void RemoveDuplicates<T>(this List<T> list)
        {
            HashSet<T> hashset = new HashSet<T>();
            list.RemoveAll(x => !hashset.Add(x));
        }

        public static IEnumerable<T> RemoveDuplicates<T>(this IEnumerable<T> list)
        {
            return new HashSet<T>(list).ToList();
        }
    }
}
