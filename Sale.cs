using MyConnection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using ROServiceApp.Entities;
using System.Text.RegularExpressions;

namespace ROServiceApp
{
    public partial class Sale : Form
    {
        Connection con;
        OleDbDataReader dr;
        DataTable dt;
        int rowindex=0;
        //Flags
        bool productcode;
        bool itemsadded=false;

        public Sale()
        {
            InitializeComponent();
            lb_grand_total.Text = "0";
            con = new Connection();
            dt = new DataTable();
            LoadProducts();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            generateBill();
            //Generate New Bill No
            //Initialize Products in Combo Box
            //Get Current Date
            //Set Grand Total 0
            //Make Table uneditable
                //Make Table Row Selection Mode True

        }

        private void generateBill()
        {
            BillEntity bill = new BillEntity();
            lb_bill_no.Text = bill.GetMaxBillNumber();
        }

        private void LoadProducts()
        {
            con.open();
            dr = con.selectquery("select * from product");
            dt.Load(dr);
            DataRow dtrow = dt.NewRow();
            dtrow["productname"] = "Select Product";
            dt.Rows.InsertAt(dtrow, 0);
            combo_product_name.DisplayMember = "productname";
            combo_product_name.ValueMember = "productcode";
            combo_product_name.DataSource = dt;
            con.close();
        }

        private void Sale_Load(object sender, EventArgs e)
        {
            //Auto Completion For Mobile No
            AutoCompleteStringCollection col = new AutoCompleteStringCollection();
            //col.Add("Foo");
                //Get All The Mobile Numbers
                con.open();  
                 dr = con.selectquery("select * from clients");
                 while(dr.Read())
                 {
                     col.Add(dr["mobileno1"].ToString());
                     col.Add(dr["mobileno2"].ToString());
                 }
                 txt_client_mobile.AutoCompleteMode = AutoCompleteMode.Suggest;
                 txt_client_mobile.AutoCompleteSource = AutoCompleteSource.CustomSource;
                 txt_client_mobile.AutoCompleteCustomSource = col;            
            //Auto Completion for ClientId
                 col = new AutoCompleteStringCollection();
                 dr = con.selectquery("select * from clients");
                 while (dr.Read())
                 {
                     col.Add(dr["clientid"].ToString());
                 }
                 txt_client_id.AutoCompleteMode = AutoCompleteMode.Suggest;
                 txt_client_id.AutoCompleteSource = AutoCompleteSource.CustomSource;
                 txt_client_id.AutoCompleteCustomSource = col; 
            //Auto Completion For Product Code
                 col = new AutoCompleteStringCollection();
                 dr = con.selectquery("select * from product");
                 while (dr.Read())
                 {
                     Out.Print.Line(dr["productcode"].ToString());
                     col.Add(dr["productcode"].ToString());
                 }
                 txt_product_code.AutoCompleteMode = AutoCompleteMode.Suggest;
                 txt_product_code.AutoCompleteSource = AutoCompleteSource.CustomSource; 
                 txt_product_code.AutoCompleteCustomSource = col;
                 con.close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Insert New Client and Get Client ID Back From Add Client Form
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProductEntity pe = new ProductEntity();
            if (productcode == true)
            {
                if (!pe.ProductExistsByCode(txt_product_code.Text))
                {
                    MessageBox.Show("Product Code Does Not Exist");
                    return;
                }
                else
                {
                    pe = pe.FindProductByCode(txt_product_code.Text);
                    if (pe == null)
                    {
                        MessageBox.Show("Product Code Does Not Exist !!");
                        return;
                    }
                }
            }
            else
            {
                //Check If Product Combo Box Has Item Selected 
                if (combo_product_name.SelectedIndex == 0)
                {
                    MessageBox.Show("Please Select Product");
                    return;
                }
                else
                {
                    pe = pe.FindProductByCode(combo_product_name.SelectedValue.ToString());
                    if (pe == null)
                    {
                        MessageBox.Show("Product Code Does Not Exist !!");
                        return;
                    }
                }
            }
            
            string[] rows = new string[]{pe.id,pe.code,pe.name,pe.price,pe.specifications};
            dataGridView1.Rows.Add(rows);
            lb_grand_total.Text = Convert.ToString(Convert.ToInt32(lb_grand_total.Text) + Convert.ToInt32(pe.price));
            itemsadded = true;
            //Check ComboBpx Item For Product
                //if Product Name Is selected
                    //Add Product To Table
                    //UpdateGrandTotal()
                //else
                    //Check the Product Exist in Database 
                        //if Exist
                            //Add Product Row To Table
                            //UpdateGrandTotal()
                        //else
                            //Wrong Product Code Nad Notify User
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!itemsadded)
            {
                MessageBox.Show("Please Add Products Before Creating Bill");
                return;
            }
            //Check Client Detail Is Mentioned Above
            if (!checkClientExistence())
            {
                MessageBox.Show("Please Check Client fields Properly");
                return;
            }

            //Check Product is Exist in Table 
            //Check Amount Field
            if (txt_payment_amount.Text == "")
            {
                MessageBox.Show("Please Enter Payment Amount");
                return;
            }
            
                //Check Amount does not contain alphabetical characters
            if (!checkOnlyNumbers(txt_payment_amount.Text))
            {
                MessageBox.Show("Amount Field Can Only Contain Numbers");
                return;
            }
                    //Check Amount not Greater Than Grand Total
            if(Convert.ToInt32(txt_payment_amount.Text) > Convert.ToInt32(lb_grand_total.Text))
            {
                MessageBox.Show("Payment Amount Cannot Be Greater Than Bill Amount");
                return;
            }
                       
            string[] ids = new string[]{};
            //Generate Array OF Prooduct Ids
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                ids[i] = dataGridView1.Rows[i].Cells[0].Value.ToString();
            }

            //Get Client Id
            //Get Bill Number
            //Get Payment And Total Bill Amount
            //Get Date

            //Add Sale to Table
            //If Error in Inserting
                //Notify User
            //Else
                //Clear Fields

        }


        private bool checkOnlyNumbers(string p)
        {
            string expr = @"^([0-9]+)(\.[0-9]{1,2})?$";
            MatchCollection mc = Regex.Matches(p, expr);
            foreach (Match m in mc)
            {
                return true;
            }
            return false;

        }

        private bool checkClientExistence()
        {
           
           if (txt_client_id.Text == "" && txt_client_mobile.Text == "")
           {
               return false;
           }
           ClientEntity ce = new ClientEntity();
           if (txt_client_mobile.Text != "")
           {
               return ce.ClientExistsByMobileNo(txt_client_mobile.Text, txt_client_mobile.Text);
           }
           else
           {
               return ce.ClientExistsById(txt_client_id.Text);
           }
           
        }

        private void combo_product_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combo_product_name.SelectedIndex != 0)
            {
                txt_product_code.Text = "";
                productcode = false;
            }
        }

        private void txt_product_code_Leave(object sender, EventArgs e)
        {
            //if (txt_product_code.Text != "")
            //{
            //    combo_product_name.SelectedIndex = 0;
            //    productcode = true;
            //}
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;
                rowindex = e.RowIndex;
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void contextMenuStrip1_MouseClick(object sender, MouseEventArgs e)
        {
            contextMenuStrip1.Hide();
            lb_grand_total.Text = Convert.ToString(Convert.ToInt32(lb_grand_total.Text) - Convert.ToInt32(dataGridView1.Rows[rowindex].Cells[3].Value.ToString()));
            dataGridView1.Rows.RemoveAt(rowindex);
            if (lb_grand_total.Text == "0")
            {
                itemsadded = false;
            }
        }

        private void txt_product_code_KeyUp(object sender, KeyEventArgs e)
        {
            if (combo_product_name.SelectedIndex != 0)
            {
                combo_product_name.SelectedIndex = 0;
            }
            if (txt_product_code.Text == "")
            {
                productcode = false;
            }
            else
            {
                productcode = true;
            }
        }

        private void txt_client_mobile_KeyUp(object sender, KeyEventArgs e)
        {
            if (txt_client_id.Text != "")
            {
                txt_client_id.Text = "";
            }
        }

        private void txt_client_id_KeyUp(object sender, KeyEventArgs e)
        {
            if (txt_client_mobile.Text != "")
            {
                txt_client_mobile.Text = "";
            }
        }




    }
}
