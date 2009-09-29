using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Driver;


namespace Sharekhan.service
{
    public class NHibernateInMemoryTestFixtureBase
    {
        protected static ISessionFactory sessionFactory;
        protected static Configuration configuration;
        
        public static void InitalizeSessionFactory(params FileInfo[] hbmFiles)
        {

            if (sessionFactory != null)
                return;
            //NHibernate.Driver.SQLite20Driver driver = new NHibernate.Driver.SQLite20Driver();
            var properties = new Dictionary<string, string>();
            properties.Add("connection.driver_class", "NHibernate.Driver.SQLite20Driver");
            properties.Add("dialect", "NHibernate.Dialect.SQLiteDialect");
            properties.Add("connection.provider", "NHibernate.Connection.DriverConnectionProvider");
            properties.Add("connection.connection_string", "Data Source=:memory:;Version=3;New=True;");
            properties.Add("connection.release_mode", "on_close");
            properties.Add("show_sql", "true");

            configuration = new Configuration();
            configuration.Properties = properties;

            foreach (FileInfo mappingFile in hbmFiles)
            {
                configuration = configuration.AddFile(mappingFile);
            }
            configuration.BuildMapping();
            sessionFactory = configuration.BuildSessionFactory();
        }

        public ISession CreateSession()
        {
            ISession openSession = sessionFactory.OpenSession();
            IDbConnection connection = openSession.Connection;
            new SchemaExport(configuration).Execute(false, true, false, true, connection, null);
            return openSession;
        }

    }
}
