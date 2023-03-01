namespace ExtraLibraries
{
    public class ImageForViewOrSave
    {
        public static string ByteArrayToImageUrl(byte[] image)
        {
            return "data:image;base64," + Convert.ToBase64String(image);
        }
    }
}
