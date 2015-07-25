using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyConnection;
using System.Windows.Forms;
namespace ROServiceApp.Entities
{
    class BillEntity
    {
        public string billnumber;
        public string billtype;
        public string billdate;
        public string billamount;
        public string[] productids;
        public string grandtotal;
        public string clientid;
        
        Connection con;


        public BillEntity(string billnumber, string billtype, string billdate, string billamount,  string[] productids, string grandtotal,  string clientid)
        {
            this.billnumber = billnumber;
            this.billtype = billtype;
            this.billdate = billdate;
            this.billamount = billamount;
            this.productids = productids;
            this.grandtotal = grandtotal;
            this.clientid = clientid;
            con.open();
        }

        public BillEntity()
        {
            billnumber = null;
            billtype = null;
            billdate = null;
            billamount = null;
            con = new Connection();
            con.open();
        }

        public string GetMaxBillNumber()
        {
            string query = "select max(id) as id from bill";
            OleDbDataReader dr = con.selectquery(query);
            if (dr.Read())
            {
                MessageBox.Show(dr["id"].ToString());
                if (dr["id"].ToString() != "")
                {
                    return Convert.ToString(Convert.ToInt32(dr["id"].ToString()) + 1);
                    
                }
                else
                {
                    return "1";
                }
            }
            return null;
        }

        public int CreateBill()
        {
            string query = "insert into bill() values()";
            int result = con.runinsertquery(query);
            return -1;
        }

        ~BillEntity()
        {
            con.close();
        }
    }
}
