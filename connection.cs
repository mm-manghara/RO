using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
namespace Out
{
    class Print
    {
        public static void Line(string msg)
        {
            System.Diagnostics.Debug.WriteLine(msg);
        }

        public static void show(string msg)
        {
            System.Windows.Forms.MessageBox.Show(msg);
        }
    }
}

namespace MyConnection
{
    class Connection
    {
        OleDbConnection con;
        OleDbCommand cmd;

        public Connection()
        {
            con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\\ROServiceApp\\ROServiceApp\\ro_database.mdb;Jet OLEDB:DataBase Password=123");

        }

        public void open()
        {
            con.Open();
        }

        public int runinsertquery(string query)
        {
            try
            {
                cmd = new OleDbCommand(query, con);
                
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Out.Print.Line(ex.Message);

            }
            return -1;

        }

        public OleDbDataReader selectquery(string query)
        {
            try
            {
                cmd = new OleDbCommand(query, con);
                return cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                Out.Print.Line(ex.Message);

            }
            return null;

        }

        public void close()
        {
            con.Close();
        }
    }

}
