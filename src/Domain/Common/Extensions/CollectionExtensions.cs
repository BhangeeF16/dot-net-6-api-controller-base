namespace Domain.Common.Extensions
{
    public static class CollectionExtensions
    {
        public static bool ContainsDuplicate<T>(this IEnumerable<T> enumerable)
        {
            HashSet<T> knownElements = new();
            foreach (T element in enumerable)
            {
                if (!knownElements.Add(element))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
