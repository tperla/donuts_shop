namespace BO
{
    public static class Tools
    {
        public static string? ToStringProperty<T>(this T t)
        {
            string? str = "";
            foreach (var item in t?.GetType()?.GetProperties()!)
            {
                str += item.Name + ": ";
                if (item.GetValue(t, null) is IEnumerable<object?>)
                {
                    IEnumerable<object?> list = (IEnumerable<object?>)item?.GetValue(obj: t, null)!;
                    string s = string.Join(" ", list);
                    str += s;
                }
                else
                {
                    str +=  item.GetValue(t, null)+ "\n";
                }
            }
            return str+="\n";
        }
    }
}