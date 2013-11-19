using System.Collections.Generic;
using System.Data;
using MySQL.Domain;
using System.Linq;

namespace MySQL
{
    public class MySQLBroker
    {
        public List<AutoPost> GetAutoPosts()
        {
            using (var dc = new DataContext())
            {
                return dc.GetAll<AutoPost>().ToList();
            }
        }

        public void Save(AutoPost item)
        {
            using (var dc = new DataContext())
            {
                dc.Save(item);
            }
        }

        public List<AutoPost> GetUndoneTasks(long userId)
        {
            using (var dc = new DataContext())
            {
                var q = from task in dc.GetAll<AutoPost>()
                        where (task.UserId == userId) && (task.IsDone == false)
                        select task;
                return q.ToList();
            }
        }

        public List<BigSpin> GetBigSpins()
        {
            using (var dc = new DataContext())
            {
                return dc.GetAll<BigSpin>().ToList();
            }
        }

        public BigSpin GetBigSpinById(long id)
        {
            using (var dc = new DataContext())
            {
                var q = from bigspin in dc.GetAll<BigSpin>() where (bigspin.Id == id) select bigspin;
                return q.ToList().FirstOrDefault();
            }
        }

        public List<BigSpinSmallThemeView> GetBigSpinSmallThemeViews()
        {
            using (var dc = new DataContext())
            {
                return dc.GetAll<BigSpinSmallThemeView>().ToList();
            }
        }

        public List<Posted> GetPosteds()
        {
            using (var dc = new DataContext())
            {
                return dc.GetAll<Posted>().ToList();
            }
        }

        public List<Posted> GetLastPosteds(User user, int quantity)
        {
            using (var dc = new DataContext())
            {
                var q = from posted in dc.GetAll<Posted>() where (posted.UserId== user.Id) select  posted ;
                return q.OrderByDescending(x=> x.PostDate).Take(quantity).ToList();
            }
        }

        public void Save(Posted item)
        {
            using (var dc = new DataContext())
            {
                dc.Save(item);
            }
        }

        public List<User> GetUsers()
        {
            using (var dc = new DataContext())
            {
                return dc.GetAll<User>().ToList();
            }
        }

        public User GetAuthorizedUser(string email, string password)
        {
            using (var dc = new DataContext())
            {
                var q = from user in dc.GetAll<User>()
                        where (user.Email == email) && (user.Pass == password)
                        select user;
                return q.ToList().FirstOrDefault();
            }
        }

        public List<WordPress> GetWordPresses()
        {
            using (var dc = new DataContext())
            {
                return dc.GetAll<WordPress>().ToList();
            }
        }
    }
}