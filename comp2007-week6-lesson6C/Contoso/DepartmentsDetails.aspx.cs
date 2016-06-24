using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// using statements required for EF DB access
using comp2007_week6_lesson6C.Models;
using System.Web.ModelBinding;

namespace comp2007_week6_lesson6C
{
    public partial class DepartmentDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!IsPostBack) && (Request.QueryString.Count > 0))
            {
                this.GetDepartment();
            }
        }
        protected void GetDepartment()
        {
            // populate the form with existing department data from the db
            int DepartmentID = Convert.ToInt32(Request.QueryString["DepartmentID"]);

            // connect to the EF DB
            using (ContosoConnection db = new ContosoConnection())
            {
                // populate a department instance with the DepartmentID from the URL parameter
                Department updatedDepartment = (from department in db.Departments
                                                where department.DepartmentID == DepartmentID
                                                select department).FirstOrDefault();

                // map the department properties to the form controls
                if (updatedDepartment != null)
                {
                    NameTextBox.Text = updatedDepartment.Name;
                    BudgetTextBox.Text = updatedDepartment.Budget.ToString();
                }
            }
        }
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            //Redirect  back to Departments page
            Response.Redirect("~/Contoso/Departments.aspx");
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            // Use EF to connect to the server
            using (ContosoConnection db = new ContosoConnection())
            {
                // use the Department model to create a new department object and
                // save a new record
                Department newDepartment = new Department();

                int DepartmentID = 0;

                if (Request.QueryString.Count > 0)
                {
                    // get the id from url
                    DepartmentID = Convert.ToInt32(Request.QueryString["DepartmentID"]);

                    // get the current department from EF DB
                    newDepartment = (from department in db.Departments
                                     where department.DepartmentID == DepartmentID
                                     select department).FirstOrDefault();
                }

                // add form data to the new department record
                newDepartment.Name = NameTextBox.Text;
                newDepartment.Budget = Convert.ToDecimal(BudgetTextBox.Text);

                // use LINQ to ADO.NET to add / insert new department into the database
                // check to see if a new department is being added
                if (DepartmentID == 0)
                {
                    db.Departments.Add(newDepartment);
                }

                // save our changes
                db.SaveChanges();

                // Redirect back to the updated Departments page
                Response.Redirect("~/Contoso/Departments.aspx");
            }
        }
    }
}