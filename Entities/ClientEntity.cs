using MyConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
namespace ROServiceApp.Entities
{
    class ClientEntity
    {
        //fields
        string name,address,city,village, mobile1,mobile2, email,fathername,date;
        Connection con;

        public ClientEntity(string name,string address,string city,string village,string  mobile1,string mobile2,string  email,string fathername,string date)
        {
            this.name = name;
            this.fathername = fathername;
            this.address = address;
            this.city = city;
            this.village = village;
            this.mobile1 = mobile1;
            this.mobile2 = mobile2;
            this.email = email;
            this.date=date;
            con = new Connection();
            con.open();
        }

        public ClientEntity()
        {
            name = null;
            address = null;
            city = null;
            village =null;
            mobile1 = null;
            mobile2 = null;
            email = null;
            fathername=null;
            date=null;
            con = new Connection();
            con.open();
        }

        public bool CreateClient()
        {
            if (name == null || address == null  ||  city == null   ||   village ==null || mobile1 == null  ||  mobile2 == null ||  date == null)
            {
                return false;
            }
            //insert product here
            string query = "insert into clients(clientname,fathername,address,village,city,mobileno1,mobileno2,emailid,[date]) values('" + name + "','" + fathername + "','" + address + "','" + village + "','"+city+"','"+ mobile1 +"','"+mobile2+"','"+email+"',#" + date + "#)";
            int result = con.runinsertquery(query);
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        public bool ClientExistsByMobileNo(string mobile1,string mobile2)
        {
            //check if product exists
            //if exists
            // return True;
            string query = "select * from clients where mobileno1='" + mobile1 + "' or mobileno2='"+mobile2+"'";
            OleDbDataReader dr = con.selectquery(query);
            if (dr.Read())
            {
                return true;
            }
            return false;
        }

        internal bool ClientExistsById(string p)
        {
            string query = "select * from clients where clientid='" + p + "'";
            OleDbDataReader dr = con.selectquery(query);
            if (dr.Read())
            {
                return true;
            }
            return false;
        }

        public string GetClientId(string mobile1, string mobile2)
        {
            string query = "select * from clients where mobileno1='" + mobile1 + "' OR mobileno2='" + mobile2 + "'";
            OleDbDataReader dr = con.selectquery(query);
            if (dr.Read())
            {
                return dr["clientid"].ToString();
            }
            return null;
        }

        ~ClientEntity()
        {
            con.close();
        }
    }
}
