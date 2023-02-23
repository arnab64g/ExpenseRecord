namespace ExtraLibraries
{
    public class StringFormat
    {
        public static string NotNull(string? value)
        {
            if(value == null)
            {
                return "";
            }
            else
            {
                return value;
            }
        }
    }
}