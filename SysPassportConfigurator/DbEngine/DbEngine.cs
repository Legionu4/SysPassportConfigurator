using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Odbc;
using System.Windows;

namespace SysPassportConfigurator.DbEngine
{
    public class FirebirdDbEngine
    {
        private string _dbPath;
        private string connectionString;
        private OdbcConnection _connection;
        //private OdbcTransaction _transaction;

        public FirebirdDbEngine(string dbPath)
        {
            _dbPath = dbPath;
            connectionString = $"Driver=Firebird/InterBase(r) driver; Uid=SYSDBA; Pwd=masterkey; DbName={_dbPath}";
            _connection = new OdbcConnection(connectionString);
        }

        //public void StartTransaction()
        //{
        //    _transaction = new OdbcTransaction(_connection);
        //}

        public DataTable ExecuteQuery(string sql)
        {
            try
            {
                OdbcDataAdapter DA = new OdbcDataAdapter(sql, _connection);
                DataTable dt = new DataTable();
                DA.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public int ExecuteNonQueryOnFirebirdDB(string sql)
        {
            OdbcCommand Command = new OdbcCommand(sql, _connection);
            try
            {
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();
                Command.ExecuteNonQuery();
                _connection.Close();
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }
    }
}
