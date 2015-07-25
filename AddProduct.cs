using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyConnection;
using System.Text.RegularExpressions;
using ROServiceApp.Entities;

namespace ROServiceApp
{
    public partial class AddProduct : Form
    {
        public AddProduct()
        {
            InitializeComponent();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            ProductEntity p = new ProductEntity();
            //Check All Fields Are not empty
                if(!FieldsEmpty())
                {
                    MessageBox.Show("Fill All the Fields Properly.");
                    return;
                }
            //Check Product does Not Exists Already
                if(p.ProductExistsByCode(txtcode.Text))
                {
                    MessageBox.Show("Product Code Already In Use.");
                    return;
                }
            //Check Price does not contain alphabetical characters
                if (!checkOnlyNumbers(txtprice.Text))
                {
                    MessageBox.Show("Amount Field Can Only Contain Only");
                    return;
                }
            //Get Date
                string date=DateTime.Today.ToString();
                
            //Add Product
            //Check For Any Insertion Errors
            //If Error 
                //Notify User
            //Else
                //Clear All Fields  
                ProductEntity product = new ProductEntity(txtcode.Text,txtname.Text,txtprice.Text,txtspecfication.Text);
                if (product.CreateProduct())
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
        }

        private bool FieldsEmpty()
        {
            //iF Fields Not Empty 
                //Return True
            if (txtcode.Text != "" && txtname.Text != "" && txtprice.Text != "" && txtspecfication.Text != "")
            {
                return true;
            }
            //Else
                //return False
            return false;
        }


        private static bool checkOnlyNumbers(string text)
        {
            //
            string expr = @"^([0-9]+)(\.[0-9]{1,2})?$";
            MatchCollection mc = Regex.Matches(text, expr);
            foreach (Match m in mc)
            {
                return true;
            }
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
