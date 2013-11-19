namespace ScreenshotMakerLibrary.Domain
{
    public class User:Identifiable
    {
        public string Username { get; set; }
        public string Password { get; set; }

        protected User()
        {
        }
    }
}