namespace ExtraLibraries
{
    public class NotNull
    {
        public static string MakeNotNull(string? str)
        {
            if (str == null)
            {
                str = "";
            }

            return str;
        }
    }
}
