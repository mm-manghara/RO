using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyConnection;
namespace ROServiceApp.Entities
{
    class ProductEntity
    {
        public string code;
        public string name;
        public string price;
        public string specifications;
        public string id;
        Connection con;
        public ProductEntity()
        {
            code = null;
            name = null;
            price = null;
            specifications = null;
            con = new Connection();
            con.open();

        }
        public ProductEntity(string c, string n, string p, string spec)
        {
            code = c;
            name = n;
            price = p;
            specifications = spec;
            con = new Connection();
            con.open();
        }

        public ProductEntity(string i,string c, string n, string p, string spec)
        {
            id = i;
            code = c;
            name = n;
            price = p;
            specifications = spec;
            con = new Connection();
            con.open();
        }

        public bool CreateProduct()
        {
            if(code==null || name==null || price==null || specifications==null)
            {
                return false;
            }
            //insert product here
            string query = "insert into product(productcode,productname,productprice,productspecification) values('" + code + "','" + name + "','" + price + "','" + specifications + "')";
            int result=con.runinsertquery(query);
            if(result==1)
            {
                return true;
            }
            return false;
        }

        public ProductEntity FindProductByCode(string code)
        {
            //check if product exists
                //if exists
                    // return entity;
            string query = "select * from product where productcode='" + code + "'";
            OleDbDataReader dr = con.selectquery(query);
            if (dr.Read())
            {
                return new ProductEntity(dr["id"].ToString(),dr["productcode"].ToString(), dr["productname"].ToString(), dr["productprice"].ToString(), dr["productspecification"].ToString());
            }
            return null;
        }

        //public ProductEntity FindProductByCode(string code)
        //{
        //    //check if product exists
        //    //if exists
        //    // return entity;
        //    string query = "select * from product where productcode='" + code + "'";
        //    OleDbDataReader dr = con.selectquery(query);
        //    if (dr.Read())
        //    {
        //        return new ProductEntity(dr["productcode"].ToString(), dr["productname"].ToString(), dr["productprice"].ToString(), dr["productspecification"].ToString());
        //    }
        //    return null;
        //}

        public bool ProductExistsByCode(string code)
        {
            //check if product exists
            //if exists
            // return True;
            string query="select * from product where productcode='"+code+"'";
            OleDbDataReader dr=con.selectquery(query);
            if(dr.Read())
            {
                return true;
            }
            return false;
        }

        public OleDbDataReader FindAllProducts()
        {
            return(con.selectquery("select * from product order by productname"));  
        }

        ~ProductEntity()
        {
            con.close();
        }
    }
}
