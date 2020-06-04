using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Common
{
    public sealed class AppDbConnection
    {
        private AppDbConnection()
        {
        }
        private static readonly Lazy<AppDbConnection> lazy = new Lazy<AppDbConnection>(() => new AppDbConnection());
        public static AppDbConnection AppDbConnectionInstance
        {
            get
            {
                return lazy.Value;
            }
        }

        public string ConnectionString { get; set; }
    }

}
