using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sql_try.Properties;
using System.Configuration;
using System.Data.SqlClient;
using MetroFramework.Forms;
namespace sql_try
{
    public partial class DataBase : MetroForm
    {
    
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\touseef.elahi\Documents\Visual Studio 2015\Projects\SQL\sql_try\sql_try\Database.mdf;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public DataBase()
        {
            InitializeComponent();
            cmd.Connection = connection;
            refreshList();
        }

        private void buttonAddData_Click(object sender, EventArgs e)
        {
            if (textBoxPhoneNumber.Text != "" && textBoxName.Text != "")
            {
                connection.Open();
                cmd.CommandText = "insert into PhoneDirectory(name,phonenumber) values('" + textBoxName.Text + "','" + textBoxPhoneNumber.Text + "') ";
                cmd.ExecuteNonQuery();
                textBoxPhoneNumber.Text = "";
                textBoxName.Text = "";
                connection.Close();
                refreshList();
            }
        }
        private void refreshList()
        {
            listViewPhoneDirectory.Items.Clear();
            connection.Open();
            cmd.CommandText = "select * from phoneDirectory";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    string[] arr = new string[2];
                    ListViewItem itm;
                    //add items to ListView
                    arr[0] = dr[1].ToString();
                    arr[1] = dr[2].ToString();
                    itm = new ListViewItem(arr);
                    listViewPhoneDirectory.Items.Add(itm);
                }
            }
            connection.Close();
        }

        private void textBoxName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                buttonAddData_Click(null,null);   
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text != "")
            {
                connection.Open();
                cmd.CommandText = "delete from phonedirectory where name='"+textBoxName.Text+"'";
                cmd.ExecuteNonQuery();
                connection.Close();
                refreshList();
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text != "")
            {
                connection.Open();
                cmd.CommandText = "update phonedirectory set phonenumber='"+textBoxPhoneNumber.Text+"' where name='"+textBoxName.Text+"'";
                cmd.ExecuteNonQuery();
                connection.Close();
                refreshList();
            }
        }

        private void DataBase_Load(object sender, EventArgs e)
        {
            this.ActiveControl = textBoxName;
        }
    }
}
