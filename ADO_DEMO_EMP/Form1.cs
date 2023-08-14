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
using static ADO_DEMO_EMP.Employee;

namespace ADO_DEMO_EMP
{
    public partial class Form1 : Form
    {
        Employee_CRUD crud;
        List<Department> list;
        public Form1()
        {

            InitializeComponent();
            crud = new Employee_CRUD();
        }

        
        public void Form()
        {
            InitializeComponent();
            crud = new Employee_CRUD();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                list = crud.GetDepartments();
                cmbdept.DataSource = list;
                cmbdept.DisplayMember = "Dname";
                cmbdept.ValueMember = "Did";
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
                Employee emp = new Employee();
                emp.Ename = txtename.Text;
                emp.Salary = Convert.ToInt32(txtsalary.Text);
                emp.Did = Convert.ToInt32(cmbdept.SelectedValue);
                int res = crud.AddProduct(emp);
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

        private void btnsearch_Click(object sender, EventArgs e)
        {

            try
            {
                Employee emp = crud.GetEmployeeById(Convert.ToInt32(txteid.Text));
                if (emp.Eid > 0)
                {
                    foreach (Department item in list)
                    {
                        if (item.Did == emp.Did)
                        {
                            cmbdept.Text = item.Dname;
                            break;
                        }
                    }
                    txtename.Text = emp.Ename;
                    txtsalary.Text = emp.Salary.ToString();

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

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                Employee emp = new Employee();
                emp.Eid = Convert.ToInt32(txteid.Text);
                emp.Ename = txtename.Text;
                emp.Salary = Convert.ToInt32(txtsalary.Text);
                emp.Did = Convert.ToInt32(cmbdept.SelectedValue);
                int res = crud.UpdateProduct(emp);
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

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {

                int res = crud.DeleteEmployee(Convert.ToInt32(txteid.Text));
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
            DataTable table = crud.GetAllEmployee();
            dataGridView1.DataSource = table;

        }

        private void cmbdept_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
