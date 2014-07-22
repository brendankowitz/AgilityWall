using System.Collections.Generic;
using System.Dynamic;

namespace AgilityWall.Core.Navigation
{
    internal static class DictionaryExtensions
    {
        public static ExpandoObject ToExpando(this IDictionary<string, string> dictionary)
        {
            var expando = new ExpandoObject();
            var expandoDic = (IDictionary<string, object>)expando;
            foreach (var kvp in dictionary)
            {
                expandoDic.Add(new KeyValuePair<string, object>(kvp.Key, kvp.Value));
            }
            return expando;
        }
    }
}