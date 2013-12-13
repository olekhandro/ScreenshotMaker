using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using NHibernate.Dialect.Function;
using NHibernate.Hql.Ast.ANTLR;
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

        public List<Message> GetMessagesForUser(User receiver, User sender)
        {
            using (var dc = new DataContext())
            {
                var q = from message in dc.GetAll<Message>()
                    where message.Receiver.Id == receiver.Id && message.Sender.Id == sender.Id
                    select message;
                return q.ToList();
            }
        }

        public void Delete(Message item)
        {
            using (var dc = new DataContext())
            {
                dc.Delete(item);
            }
        }

        public void Save(Message item)
        {
            using (var dc = new DataContext())
            {
                dc.Save(item);
            }
        }
    }
}