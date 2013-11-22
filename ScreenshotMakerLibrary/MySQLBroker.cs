using System.Collections.Generic;
using System.Linq;
using ScreenshotMakerLibrary.Domain;

namespace ScreenshotMakerLibrary
{
    public class MySQLBroker
    {
        public User Login(string username, string password)
        {
            using (var dc = new DataContext())
            {
                var q = from user in dc.GetAll<User>()
                    where user.Password == password && user.Username == username
                    select user;
                var authentificatedUser = q.FirstOrDefault();
                if (q.FirstOrDefault() != null)
                    return authentificatedUser;

                return null;
            }
        }

        public void Save(Screenshot screenshot)
        {
            using (var dc = new DataContext())
            {
                dc.Save(screenshot);
            }
        }
    }
}