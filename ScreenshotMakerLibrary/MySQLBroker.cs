using System.Linq;
using ScreenshotMakerLibrary.Domain;

namespace ScreenshotMakerLibrary
{
    public class MySQLBroker
    {
        public bool Login(string username, string password)
        {
            using (var dc = new DataContext())
            {
                var q = from user in dc.GetAll<User>()
                    where user.Password == password && user.Username == username
                    select user;
                if (q.Count() > 0)
                    return true;
                return false;
            }
        }
    }
}