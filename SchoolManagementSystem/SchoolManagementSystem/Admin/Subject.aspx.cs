using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SchoolManagementSystem.Models.CommonFn;

namespace SchoolManagementSystem.Admin
{
    public partial class Subject : System.Web.UI.Page
    {

        CommonFnx fn = new CommonFnx();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                GetClass();
                GetSubject();
            }
        }

        private void GetClass()
        {
            string query = "Select * from Class";
            DataTable dt = fn.Fetch(query);
            ddlClass.DataSource = dt;
            ddlClass.DataTextField = "ClassName";
            ddlClass.DataValueField = "ClassId";
            ddlClass.DataBind();
            ddlClass.Items.Insert(0, "Select Class");
        }

        private void GetSubject()
        {
            string query = "Select Row_NUMBER() over(order by (Select 1)) as [Sr.No], s.SubjectId, c.ClassId, c.ClassName, s.SubjectName from Subject s inner join Class c on s.ClassId = c.ClassId";
            DataTable dt = fn.Fetch(query);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int classId = Convert.ToInt32(ddlClass.SelectedItem.Value);
                string checkQuery = "select * from Subject s inner join Class c on s.ClassId = c.ClassId where s.ClassId = '" + classId + "'";
                DataTable dt = fn.Fetch(checkQuery);

                if (dt.Rows.Count == 0)
                {
                    string insertQuery = "insert into subject values('" + classId + "', '" + txtSubject.Text.Trim() + "')";
                    fn.Query(insertQuery);
                    lblMessage.Text = "Subject Inserted successfully";
                    lblMessage.CssClass = "alert alert-success";
                    GetSubject();
                    ddlClass.SelectedIndex = 0;
                    txtSubject.Text = String.Empty;
                }
                else
                {
                    lblMessage.Text = "Subject with that particular class already exist";
                    lblMessage.CssClass = "alert alert-danger";

                }
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('exception with exception message : '" + ex.Message + ")</script>");
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GetSubject();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            int subjectId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
            string subjectName = (row.FindControl("editSubject") as TextBox).Text.Trim();
            string updateQuery = "Update Subject set SubjectName = '"+subjectName+"' where SubjectId = '"+subjectId+"'";
            fn.Query(updateQuery);
            lblMessage.Text = "Subject got updated";
            lblMessage.CssClass = "alert alert-success";
            GridView1.EditIndex = -1;
            GetSubject();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetSubject();
        }
    }
}