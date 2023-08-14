using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ADO_DEMO_EMP
{
    public partial class Product_form : Form

    {
        Product_CRUD crud;
        List<Category> list;
        public Product_form()
        {
            InitializeComponent();
            crud = new Product_CRUD();

        }

        private void Product_form_Load(object sender, EventArgs e)
        {

            try
            {
                list = crud.GetCategories();
                cmbcategory.DataSource = list;
                cmbcategory.DisplayMember = "Cname";
                cmbcategory.ValueMember = "Cid";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {

            try
            {
                Product p = new Product();
                p.Name = txtproname.Text;
                p.Price = Convert.ToInt32(txtpropri.Text);
                p.Cid = Convert.ToInt32(cmbcategory.SelectedValue);
                int res = crud.AddProduct(p);
                if (res > 0)
                {
                    MessageBox.Show("Record inserted..");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                Product p = new Product();
                p.Id = Convert.ToInt32(txtproid.Text);
                p.Name = txtproname.Text;
                p.Price = Convert.ToInt32(txtpropri.Text);
                p.Cid = Convert.ToInt32(cmbcategory.SelectedValue);
                int res = crud.UpdateProduct(p);
                if (res > 0)
                {
                    MessageBox.Show("Record updated..");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                Product prod = crud.GetProductById(Convert.ToInt32(txtproid.Text));
                if (prod.Id > 0)
                {
                    foreach (Category item in list)
                    {
                        if (item.Cid == prod.Cid)
                        {
                            cmbcategory.Text = item.Cname;
                            break;
                        }
                    }
                    txtproname.Text = prod.Name;
                    txtpropri.Text = prod.Price.ToString();

                }
                else
                {
                    MessageBox.Show("Record not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {

                int res = crud.DeleteProduct(Convert.ToInt32(txtproid.Text));
                if (res > 0)
                {
                    MessageBox.Show("Record deleted..");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnshowall_Click(object sender, EventArgs e)
        {
            DataTable table = crud.GetAllProducts();
            dataGridView1.DataSource = table;
        }

    }
}


