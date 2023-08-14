using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ADO_DEMO_EMP.Employee;
using System.Data;

namespace ADO_DEMO_EMP
{
    public class Employee_CRUD
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        public Employee_CRUD()
        {

            string connstr = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
            con = new SqlConnection(connstr);
        }

        public int AddProduct(Employee emp)
        {
            //step 1 --> query
            string qry = "INSERT INTO employee VALUES(@ename,@salary,@dept_id)";

            con.Open();

            //step 2--> assign query to command
            cmd = new SqlCommand(qry, con);

            //step 3--> pass value to parameter
            cmd.Parameters.AddWithValue("@ename", emp.Ename);
            cmd.Parameters.AddWithValue("@salary", emp.Salary);
            cmd.Parameters.AddWithValue("@dept_id", emp.Did);

            // step 4--> open the connection
            // con.Open();

            //step5-->fire the query
            int result = cmd.ExecuteNonQuery();

            //Step 6--->close the connec
            con.Close();
            return result;

        }

        public int UpdateProduct(Employee emp)
        {
            // step1 -> qry
            string qry = "update employee set ename=@ename,salary=@salary,dept_id=@dept_id where eid=@eid";
            // step2- assign qry to command
            cmd = new SqlCommand(qry, con);
            // step3- pass valeu to the parameters
            cmd.Parameters.AddWithValue("@ename", emp.Ename);
            cmd.Parameters.AddWithValue("@salary", emp.Salary);
            cmd.Parameters.AddWithValue("@dept_id", emp.Did);
            cmd.Parameters.AddWithValue("@eid", emp.Eid);
            // step4- open the connection
            con.Open();
            //step5- fire the query
            int result = cmd.ExecuteNonQuery();
            //step6- close the conn
            con.Close();
            return result;
        }


        public List<Department> GetDepartments()
        {
            List<Department> List = new List<Department>();

            //step 1---> write query

            string qry = "SELECT * FROM department";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Department d = new Department();
                    d.Did = Convert.ToInt32(dr["dept_id"]);
                    d.Dname = dr["dept_name"].ToString();
                    List.Add(d);
                }
            }
            con.Close();
            return List;
        }

        public int DeleteEmployee(int Eid)
        {
            // step1 -> qry
            string qry = "delete from employee where eid=@eid";
            // step2- assign qry to command
            cmd = new SqlCommand(qry, con);
            // step3- pass valeu to the parameters
            cmd.Parameters.AddWithValue("@eid", Eid);
            // step4- open the connection
            con.Open();
            //step5- fire the query
            int result = cmd.ExecuteNonQuery();
            //step6- close the conn
            con.Close();
            return result;
        }

        public Employee GetEmployeeById(int Eid)
        {
            Employee emp = new Employee();
            string qry = "select * from employee where eid=@eid";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@eid", Eid);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                if (dr.Read())
                {
                    emp.Eid = Convert.ToInt32(dr["eid"]);
                    emp.Ename = dr["ename"].ToString();
                    emp.Salary = Convert.ToInt32(dr["salary"]);
                    emp.Did = Convert.ToInt32(dr["dept_id"]);
                }
            }
            con.Close();
            return emp;
        }


        public DataTable GetAllEmployee()
        {
            DataTable dt = new DataTable();
            string qry = "select * from employee";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dt.Load(dr);
            }
            con.Close();
            return dt;
        }






    }




}

