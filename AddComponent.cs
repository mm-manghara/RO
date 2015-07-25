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
    public partial class AddComponent : Form
    {
        OleDbDataReader dr;
        DataTable dt;

        public AddComponent()
        {
            InitializeComponent();
            dt = new DataTable();
            LoadProducts();

        }

        private void LoadProducts()
        {
            ProductEntity product=new ProductEntity();
            dr = product.FindAllProducts();
           
                
                dt.Load(dr);
                DataRow datadr = dt.NewRow();
                datadr["productname"] = "Select Product";
                dt.Rows.InsertAt(datadr,0);
                combo_product.DisplayMember = "productname";
                combo_product.ValueMember = "id";
                combo_product.DataSource = dt;
               
           
            dr.Close();
        }

      
        private void btnadd_Click(object sender, EventArgs e)
        {
            ComponentEntity comp = new ComponentEntity();
            //Check All Fields Are not empty
            if (!FieldsEmpty())
            {
                MessageBox.Show("Fill All the Fields Properly.");
                return;
            }
            //Check Component does Not Exists Already
            if (comp.ComponentExistsByCode(txtcode.Text))
            {
                MessageBox.Show("Component Code Already In Use.");
                return;
            }

            //Check Price does not contain alphabetical characters
            if (!checkOnlyNumbers(txtprice.Text))
            {
                MessageBox.Show("Amount Field Can Only Contain Only");
                return;
            }
            //Get Date
                string date = DateTime.Today.ToString();
            //Add Component
            //Check For Any Insertion Errors
            //If Error 
                //Notify User
            //Else
                //Clear All Fields
                ComponentEntity component = new ComponentEntity(txtcode.Text, txtname.Text, txtprice.Text, txtspecfication.Text,date,combo_product.SelectedValue.ToString());
                if (component.CreateComponent())
                {
                    MessageBox.Show("New Product Created.");
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Problem In Inserting Product.");
                }
        }

        private void ClearFields()
        {
            txtcode.Text = "";
            txtname.Text = "";
            txtprice.Text = "";
            txtspecfication.Text = "";
            combo_product.SelectedIndex = 0;
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

        private bool FieldsEmpty()
        {
            //iF Fields Not Empty 
            //Return True
            if (txtcode.Text != "" && txtname.Text != "" && txtprice.Text != "" && txtspecfication.Text != "" && combo_product.SelectedItem.ToString()!="Select Product")
            {
                return true;
            }
            //Else
            //return False
            return false;
        }

        private void AddComponent_Load(object sender, EventArgs e)
        {

        }
    }
}
