using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyConnection;
using System.Data.OleDb;
namespace ROServiceApp.Entities
{
    class ComponentEntity
    {
        string code;
        string name;
        string price;
        string specifications;
        string date;
        string productid;
        Connection con;

        public ComponentEntity()
        {
            code = null;
            name = null;
            price = null;
            date = null;
            specifications = "";
            productid = "";
            con = new Connection();
            con.open();

        }
        public ComponentEntity(string c, string n, string p, string spec, string d, string pid)
        {
            code = c;
            name = n;
            price = p;
            specifications = spec;
            date = d;
            productid = pid;
            con = new Connection();
            con.open();
        }
        public bool CreateComponent()
        {
            if (code == null || name == null || price == null || specifications == null || date==null)
            {
                return false;
            }
            //insert product here
            string query = "insert into component(componentcode,componentname,componentprice,specification,[date],productid) values('" + code + "','" + name + "','" + price + "','" + specifications + "',#"+date+"#,"+productid+")";
            int result = con.runinsertquery(query);
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        public bool ComponentExistsByCode(string code)
        {
            //check if product exists
            //if exists
            // return True;
            string query = "select * from component where componentcode='" + code + "'";
            OleDbDataReader dr = con.selectquery(query);
            if (dr.Read())
            {
                return true;
            }
            return false;
        }


        ~ComponentEntity()
        {
            con.close();
        }
    }
}
