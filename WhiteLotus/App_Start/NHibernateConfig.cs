using System.Reflection;
using WhiteLotus.Models.Mappings;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;

namespace WhiteLotus
{
    public class NHibernateConfig
    {
        public static ISessionFactory ConfigureNHibernate()
        {
            var cfg = new Configuration();

            cfg.DataBaseIntegration(db => {
                db.ConnectionStringName = "MyConnString";
                db.Dialect<MsSql2005Dialect>();
                db.BatchSize = 500;
                db.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                if (MvcApplication.IsDebug())
                {
                    db.LogSqlInConsole = true;
                    db.LogFormattedSql = true;
                }
            });

            // Add mappings
            var mapper = new ModelMapper();
            mapper.AddMappings(Assembly.GetAssembly(typeof(BookingMap)).GetExportedTypes());
            cfg.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());

            return cfg.BuildSessionFactory();
        }
    }
}