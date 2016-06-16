using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// using statements that are required to connect to EF DB
using comp2007_week6_lesson6C.Models;
using System.Web.ModelBinding;
using System.Linq.Dynamic;

namespace comp2007_week6_lesson6C
{
    public partial class Departments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if  loading page for the first time populate the department grid
            if (!IsPostBack)
            {
                Session["SortColumn"] = "DepartmentID";
                Session["SortDirection"] = "ASC";
                // Get the department data
                this.GetDepartments();
            }
        }
        protected void GetDepartments()
        {
            string sortString = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();

            // connect to EF
            using (DefaultConnection db = new DefaultConnection())
            {
                // query the departments Table using EF and LINQ
                var Departments = (from allDepartments in db.Departments
                                   select allDepartments);

                // bind the result to the GridView
                DepartmentsGridView.DataSource = Departments.AsQueryable().OrderBy(sortString).ToList();
                DepartmentsGridView.DataBind();
            }
        }
        protected void DepartmentsGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // store which row was clicked
            int selectedRow = e.RowIndex;

            // get the selected DepartmentID using the Grid's DataKey Collection
            int DepartmentID = Convert.ToInt32(DepartmentsGridView.DataKeys[selectedRow].Values["DepartmentID"]);

            // use EF to find the selected department from DB and remove it
            using (DefaultConnection db = new DefaultConnection())
            {
                Department deletedDepartment = (from departmentsRecords in db.Departments
                                                where departmentsRecords.DepartmentID == DepartmentID
                                                select departmentsRecords).FirstOrDefault();

                // perform the removal in the DB
                db.Departments.Remove(deletedDepartment);
                db.SaveChanges();

                // refresh the grid
                this.GetDepartments();
            }
        }

        protected void DepartmentsGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            //get the coloumb to sort by
            Session["SortColumn"] = e.SortExpression;

            //refresh the grid
            this.GetDepartments();

            //toggle the direction
            Session["SortDirection"] = Session["SortDirection"].ToString() == "ASC" ? "DESC" : "ASC";
        }
        protected void DepartmentsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.Row.RowType == DataControlRowType.Header)//check to see if the click is on the header row
                {
                    LinkButton linkbutton = new LinkButton();

                    for (int i = 0; i < DepartmentsGridView.Columns.Count; i++)
                    {
                        if (DepartmentsGridView.Columns[i].SortExpression == Session["SortColumn"].ToString())
                        {
                            if (Session["SortDirection"].ToString() == "ASC")
                            {
                                linkbutton.Text = "<i class='fa fa-caret-down fa-lg'></i>";
                            }
                            else
                            {
                                linkbutton.Text = "<i class='fa fa-caret-up fa-lg'></i>";
                            }
                            e.Row.Cells[i].Controls.Add(linkbutton);
                        }
                    }
                }
            }
        }
    }
}