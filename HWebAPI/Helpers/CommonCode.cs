namespace HWebAPI.Helpers
{
    public class CommonCode
    {
        public static string GuidID()
        {
            return Guid.NewGuid().ToString().ToUpper();
        }
    }
}
