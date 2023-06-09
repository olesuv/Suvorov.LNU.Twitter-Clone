namespace Suvorov.LNU.TwitterClone.Web.Controllers
{
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message)
        {
        }
    }
}
