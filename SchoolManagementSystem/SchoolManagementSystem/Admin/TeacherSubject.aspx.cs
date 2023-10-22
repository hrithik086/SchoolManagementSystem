using SchoolManagementSystem.Models;
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
    public partial class TeacherSubject : System.Web.UI.Page
    {
        CommonFnx fn= new CommonFnx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetClass();
                GetTeacher();
                GetTeacherSubject();
            }
            
        }

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                string classId = ddlClass.SelectedValue;
                GetSubject(classId);
            }
        }

        private void GetClass()
        {
            string query = "select [ClassId], [ClassName] from Class";
            try
            {
                DataTable dt = fn.Fetch(query);
                ddlClass.DataSource = dt;
                ddlClass.DataValueField = "ClassId";
                ddlClass.DataTextField = "ClassName";
                ddlClass.DataBind();
                ddlClass.Items.Insert(0, "Select Class");
            }
            catch(Exception ex)
            {
                Response.Write("<script>'Exceptin while fetching Class Details " + ex.Message + "'</script>");
            }
        }

        private void GetSubject(string classId)
        {
            string query = "select [SubjectId], [ClassId], [SubjectName] from Subject where ClassId = '" + classId + "'";

            try
            {
                DataTable dt = fn.Fetch(query);
                ddlSubject.DataSource = dt;
                ddlSubject.DataTextField = "SubjectName";
                ddlSubject.DataValueField = "SubjectId";
                ddlSubject.DataBind();
                ddlSubject.Items.Insert(0, "Select Subject");
            }
            catch(Exception ex)
            {
                Response.Write("<script>'Error occured while fetching Student Details exception - " + ex.Message + "'</script>");
            }
        }

        private void GetTeacher()
        {
            string query = "select [TeacherId], [Name] from Teacher";

            try
            {
                DataTable dt = fn.Fetch(query);
                ddlTeacher.DataSource = dt;
                ddlTeacher.DataTextField = "Name";
                ddlTeacher.DataValueField = "TeacherId";
                ddlTeacher.DataBind();
                ddlTeacher.Items.Insert(0, "Select Teacher");
            }
            catch(Exception ex)
            {
                Response.Write("<script>'Error has occured while fetching Teacher details exception - " + ex.Message + "'</script>");
            }
        }

        private void GetTeacherSubject()
        {
            string query = "select [Id] ,[ClassId], [SubjectId], [TeacherId] from TeacherSubject";

            try
            {
                DataTable dt = fn.Fetch(query);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            catch(Exception ex)
            {
                Response.Write("<script>'Exception occured while fetching exception - " + ex.Message + "'</script>");
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string classId = ddlClass.SelectedValue;
            string subjectId = ddlSubject.SelectedValue;
            string teacherId = ddlTeacher.SelectedValue;
            string query = "Insert into TeacherSubject values('" +
                classId + "', '" +
                subjectId + "', '" +
                teacherId + "')";

            try
            {
                fn.Query(query);
                lblMessage.Text = "Teacher subject added successfully";
                lblMessage.CssClass = "alert alert-success";
                GetTeacherSubject();
            }
            catch(Exception ex)
            {
                lblMessage.Text = "exception occured while inserting";
                lblMessage.CssClass = "alert alert-danger";
                Response.Write("<script>'exception occured while insering exception -" + ex.Message + "'</script>");
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GetTeacherSubject();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            string classId = (row.FindControl("txtClass") as TextBox).Text;
            string subjectId = (row.FindControl("txtSubject") as TextBox).Text;
            string teacherId = (row.FindControl("txtTeacher") as TextBox).Text;
            string id= GridView1.DataKeys[e.RowIndex].Values[0].ToString();
            string query = "update TeacherSubject set ClassId = '" + classId + "'," +
                "SubjectId = '" + subjectId + "'," +
                "TeacherId = '" + teacherId + "' where id = '" + id + "'";
            try
            {
                fn.Query(query);
                lblMessage.Text = "Edited Successfully";
                lblMessage.CssClass = "alert alert-success";
                GridView1.EditIndex = -1;
                GetTeacherSubject();
            }
            catch(Exception ex)
            {
                Response.Write("<script>'Exception occured while updating exception - " + ex.Message + "'</script>");
            }
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetTeacherSubject();
        }
    }
}