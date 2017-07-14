using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Linq;
using System.Linq;

namespace NhibernateExample
{
    public class Program
    {

        public static ISessionFactory CreateSessionFactory()
        {
            var sessionFactory = Fluently
              .Configure()
              .Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromConnectionStringWithKey("YaseenSQL")))
              .Mappings(m => m.FluentMappings.AddFromAssemblyOf<TripModel>())
              .BuildSessionFactory();

            return sessionFactory;
        }
        public static void Main(string[] args)
        {

            var session = CreateSessionFactory();
            var openedSession = session.OpenSession();
            var result = openedSession.Query<TripModel>()
           .Where(x => x.RegistrationNumber != null)
           .ToList();

            var p = new Program();
            p.start();
        }

        public void start()
        {
            
        }
    }
}
