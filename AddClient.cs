using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ROServiceApp.Entities;
namespace ROServiceApp
{
    public partial class AddClient : Form
    {
        public AddClient()
        {
            InitializeComponent();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            ClientEntity cl = new ClientEntity();
            //Required Fields-- Name, Address, City, Village, Mobile No 
            if (!FieldsEmpty())
            {
                MessageBox.Show("Fill All the Fields Properly.");
                return;
            }
            
            //Check Client does Not Exists Already on basis of Mobile No
            if (cl.ClientExistsByMobileNo(txtmobilenumber.Text,txtmobilenumber2.Text))
            {
                MessageBox.Show("Client Already exits with this Mobile No.");
                return;
            }

            //Check Mobile no does not contain alphabetical characters
            if (!checkOnlyNumbers(txtmobilenumber.Text))
            {
                MessageBox.Show("Mobile No.1 Field Can Only Contain Number");
                return;
            }
            if (!checkOnlyNumbers(txtmobilenumber.Text))
            {
                MessageBox.Show("Mobile No.2 Field Can Only Contain Number");
                return;
            }
            //Get Date
            string date = DateTime.Today.ToString();

            //Add Client
            //Check For Any Insertion Errors
            //If Error 
                //Notify User
            //Else
                //Clear All Fields 
            ClientEntity client = new ClientEntity(txtcustomername.Text, txtaddress.Text, txtcity.Text, txtvillage.Text,txtmobilenumber.Text,txtmobilenumber2.Text,txtemail.Text,txtfathername.Text, date);
            if (client.CreateClient())
            {
                MessageBox.Show("New Client Created.");
                ClearFields();
            }
            else
            {
                MessageBox.Show("Problem In Inserting Product.");
            }
        }

        private void ClearFields()
        {
            txtcustomername.Text = "";
            txtfathername.Text = "";
            txtaddress.Text = "";
            txtcity.Text = "";
            txtvillage.Text = "";
            txtmobilenumber.Text = "";
            txtmobilenumber2.Text = "";
            txtemail.Text = "";
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
            if (txtcustomername.Text != "" && txtaddress.Text != "" && txtcity.Text != "" && txtvillage.Text != "" && txtmobilenumber.Text != "")
            {
                return true;
            }
            //Else
            //return False
            return false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
    }
}
