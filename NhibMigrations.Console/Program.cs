using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using log4net;
using log4net.Config;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using Configuration = NHibernate.Cfg.Configuration;

namespace NhibMigrations.Console
{
    class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Program));
        private static int _mappersCount = 0;

        static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            var connectionString = ConfigurationManager.AppSettings[Constants.ConnectionString];
            var doUpdate = Convert.ToBoolean(ConfigurationManager.AppSettings[Constants.DoUpdate]);
            var doDrop = Convert.ToBoolean(ConfigurationManager.AppSettings[Constants.DoDrop]);
            var fileName = ConfigurationManager.AppSettings[Constants.Version] + ".sql";

            var mapper = BuildModelMapper(Helper.GetMappingAssemblies());
            var hbmMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            Logger.Info("Mappings");
            Logger.Info(hbmMapping.AsString());

            var cfg = GetConfiguration(connectionString);
            cfg.AddDeserializedMapping(hbmMapping, null);

            var error = string.Empty;
            var backupOut = System.Console.Out;

            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (var sw = new StreamWriter(fs))
                {
                    System.Console.SetOut(sw);
                    try
                    {
                        if (doDrop)
                        {
                            new SchemaExport(cfg).Execute(true, true, false);
                        }
                        else
                        {
                            new SchemaUpdate(cfg).Execute(true, doUpdate);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                        error = ex.ToString();
                    }

                    sw.Flush();
                    sw.Close();
                }
            }

            System.Console.SetOut(backupOut);
            System.Console.WriteLine(string.IsNullOrWhiteSpace(error) ? string.Format("Done for {0} types", _mappersCount) : error);
            System.Console.ReadLine();
        }

        private static ModelMapper BuildModelMapper(IEnumerable<string> mappingAssemblies)
        {
            var mapper = new ModelMapper();

            foreach (var mappingAssembly in mappingAssemblies)
            {
                var asm = Assembly.LoadFrom(mappingAssembly);
                var types = asm.GetTypes().Where(type => type.IsClass && type.IsAbstract == false).ToList();
                _mappersCount += types.Count();

                mapper.AddMappings(types);
            }

            return mapper;
        }

        private static Configuration GetConfiguration(string connectionString)
        {
            var cfg = new Configuration();

            cfg.DataBaseIntegration(db =>
            {
                db.Driver<SqlClientDriver>();
                db.Dialect<MsSql2008Dialect>();
                db.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                db.ConnectionString = connectionString;
                db.LogFormattedSql = true;
                db.LogSqlInConsole = true;
                db.AutoCommentSql = true;
            });

            return cfg;
        }
    }
}
