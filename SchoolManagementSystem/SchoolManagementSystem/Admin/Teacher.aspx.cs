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
    public partial class Teacher : System.Web.UI.Page
    {
        private CommonFnx fn = new CommonFnx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetTeachers();
            }
        }

        private void GetTeachers()
        {
            string query = "select ROW_NUMBER() over(order by (select 1)) as [Sr.No], [TeacherId], [Name], [DOB], [Gender], [Mobile], [Email], [Address], [Password] from Teacher";
            DataTable dt = fn.Fetch(query);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            DataTable dt;
            try
            {
                if(ddlGender.SelectedValue != "0" )
                {
                    dt = fn.Fetch("Select * from Teacher where email = '" + txtEmail.Text.Trim() + "'");
                    if(dt.Rows.Count == 0)
                    {
                        string query = "Insert into Teacher values ('"+txtName.Text.Trim()+
                            "', '"+txtDob.Text.Trim()+
                            "', '"+ddlGender.SelectedValue+
                            "', '"+txtMobile.Text.Trim()+
                            "', '"+txtEmail.Text.Trim()+
                            "', '"+txtAddress.Text.Trim()+
                            "', '"+txtPassword.Text.Trim()+"')";
                        fn.Query(query);
                        lblMessage.Text = "Teacher inserted successfully";
                        lblMessage.CssClass = "alert alert-success";
                        txtName.Text = string.Empty;
                        txtDob.Text = string.Empty;
                        ddlGender.SelectedIndex = 0;
                        txtMobile.Text = string.Empty;
                        txtEmail.Text = string.Empty ;
                        txtAddress.Text = string.Empty ;
                        txtPassword.Text = string.Empty ;
                        GetTeachers();
                    }
                    else
                    {
                        lblMessage.Text = "Teacher with the given email already exist";
                        lblMessage.CssClass = "alert alert-danger";
                    }
                }
            }
            catch(Exception ex)
            {
                Response.Write("<script>'" + ex.Message + "'</script>");
            }
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetTeachers();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string teacherId = GridView1.DataKeys[e.RowIndex].Values[0].ToString();
            string query = "Delete from Teacher where TeacherId = '" + teacherId + "'";
            try
            {
                fn.Query(query);
                lblMessage.Text = "Row deleted successfully";
                lblMessage.CssClass = "alert alert-danger";
                GetTeachers();
            }
            catch(Exception ex)
            {
                Response.Write("<script>'" + ex.Message + "'</script>");
            }
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string teacherId = GridView1.DataKeys[e.RowIndex].Values[0].ToString();
            GridViewRow row = GridView1.Rows[e.RowIndex];
            string name = ((TextBox)row.FindControl("TextBox1")).Text;
            string DOB = (row.FindControl("TextBox2") as TextBox).Text;
            string mobile = (row.FindControl("TextBox3") as TextBox).Text;
            string query = "update Teacher set name = '"+name+
                "', DOB = '"+DOB+
                "', Mobile = '"+mobile+"' where TeacherId = '"+teacherId+"'";
            fn.Query(query);
            try
            {
                lblMessage.Text = "Row updated Sucessfully";
                lblMessage.CssClass = "alert alert-success";
                GridView1.EditIndex = -1;
                GetTeachers();
            }
            catch(Exception ex)
            {
                Response.Write("<script>'" + ex.Message + "'</script>");
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GetTeachers();
        }
    }
}