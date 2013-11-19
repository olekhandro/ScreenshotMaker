using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;
using ScreenshotMakerLibrary.Domain;
using Environment = NHibernate.Cfg.Environment;


namespace ScreenshotMakerLibrary
{
    /// <summary>
    ///     Дата контекст. Уровень работы с базой данных.
    /// </summary>
    public class DataContext : EmptyInterceptor, IDisposable
    {
        private static readonly ISessionFactory SessionFactory;

        private static readonly string _ConnectionString;


        private static int _DCCounter;

        [ThreadStatic] protected static IDbConnection _Connection;
        protected ISession Session;
        private Dictionary<object, object> _MergeItems;

        static DataContext()
        {
            // активация NHibernate лога
            //log4net.Config.XmlConfigurator.Configure();

            var nhConfig = new Configuration();

            nhConfig.SetProperty(Environment.Dialect, "NHibernate.Dialect.MySQLDialect");
            nhConfig.SetProperty(Environment.ConnectionDriver, "NHibernate.Driver.MySqlDataDriver, NHibernate");
            nhConfig.SetProperty(Environment.ConnectionProvider, "NHibernate.Connection.DriverConnectionProvider");
            nhConfig.SetProperty(Environment.Hbm2ddlKeyWords, "none");
            nhConfig.AddAssembly("ScreenshotMakerLibrary");

            // ReSharper disable LocalizableElement
            nhConfig.SetProperty(Environment.ConnectionString, _ConnectionString =
                string.Format("Server = {0}; Database={1}; {2};Convert Zero Datetime=true; CharSet=cp1251;",
                    ConnectionOptions.ServerName,
                    ConnectionOptions.DatabaseName,
                    string.Format("Uid={0}; Pwd={1};",
                        ConnectionOptions.Login,
                        ConnectionOptions.Pwd)));
            // ReSharper restore LocalizableElement
            SessionFactory = nhConfig.BuildSessionFactory();
        }

        public DataContext()
        {
            Session = SessionFactory.OpenSession();
            Session.FlushMode = FlushMode.Never;
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(false);
        }

        #endregion

        ~DataContext()
        {
#if DEBUG
            // ReSharper disable LocalizableElement
            //Debug.WriteLine(string.Format("!Delete DC {0} {1}", Session.GetSessionImplementation().SessionId, --_DCCounter));
            // ReSharper restore LocalizableElement
#endif
            Dispose(true);
        }

        public void Dispose(bool force)
        {
            if (force)
            {
                if (Session != null)
                {
                    Session.Close();
                    Session.Dispose();
                }
                Session = null;
            }
        }

        /// <summary>
        ///     Load нужно использовать только для обновления уже существующего объекта предметной области (когда требуется
        ///     перчитать данные из БД в моменте).
        ///     Для получения новых объектов нужно использовать методы Get.
        ///     Внимание! В случае если такого объекта нет - будет брошен exception!
        ///     Отличия Get от Load:
        ///     Get - перечитает данные (или возьмет из кеша) и вернет уже загруженный объект
        ///     Load - вернет прокси. Перечитывание из БД произойдет в момент обращения к объекту
        /// </summary>
        /// <param name="obj">Собственно объект</param>
        public void Load(Identifiable obj)
        {
            if (obj.Id == null)
                return;

            Session.Evict(obj);
            Session.Load(obj, obj.Id);
        }

        public IQueryable<T> GetAll<T>() where T : Identifiable
        {
            return Session.Query<T>();
        }


        public void DeleteAll(IEnumerable items)
        {
            foreach (object item in items)
                Delete(item);
        }

        public T GetById<T>(long id) where T : Identifiable
        {
            return Session.Get<T>(id);
        }


        public void Delete(object item)
        {
            ITransaction tr = Session.BeginTransaction();
            Session.Delete(item);
            tr.Commit();
            Session.Flush();
        }


        /// <summary>
        ///     Сохранение объекта
        /// </summary>
        /// <param name="item">объект</param>
        public void Save(Identifiable item)
        {
            Save(item, false);
        }

        /// <summary>
        ///     Сохранение объекта
        /// </summary>
        /// <param name="item">объект</param>
        /// <param name="quick">если true, не объекты не кешируются, использовать для импорта</param>
        public void Save(Identifiable item, bool quick)
        {
            ITransaction tr = Session.BeginTransaction();
            Session.SaveOrUpdate(item);
            tr.Commit();
            Session.Flush();
        }


        /// <summary>
        ///     Сохранение объектов
        /// </summary>
        /// <param name="items">объекты</param>
        public void SaveAll(IEnumerable items)
        {
            SaveAll(items, false);
        }

        /// <summary>
        ///     Сохранение объектов
        /// </summary>
        /// <param name="items">объекты</param>
        /// <param name="quick">если true, не объекты не кешируются, использовать для импорта</param>
        public void SaveAll(IEnumerable items, bool quick)
        {
            foreach (object item in items)
                Save((Identifiable) item, quick);
        }
    }
}