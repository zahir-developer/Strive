using Strive.Common;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Strive.Repository
{
    public class BaseRepository : IDisposable
    {
        public IDbConnection dbcon;
        public BaseRepository()
        {
            dbcon = new SqlConnection(AppDbConnection.AppDbConnectionInstance.ConnectionString);
            dbcon.Open();
        }

        public void Dispose()
        {
            dbcon.Close();
            dbcon.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
