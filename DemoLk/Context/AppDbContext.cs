using DemoLk.Models.Identity;
using DemoLk.Models.WorkStation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace DemoLk.Context
{
    public class AppDbContext:DbContext
    {
        public AppDbContext() : base(ConfigurationManager.ConnectionStrings[1].ConnectionString)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<WorkStation> WorkStations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        internal static TResult Run<TResult>(Func<AppDbContext, TResult> dbFunction)
        {
            using (var db = new AppDbContext())
            {
                try
                {
                    return dbFunction(db);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Data Error: {ex.Message}");
                }
            }
        }

        internal static void Run(Action<AppDbContext> dbAction)
        {
            Run(db =>
            {
                dbAction(db);
                return true;
            });
        }
    }
}