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
    public partial class AddClass : System.Web.UI.Page
    {
        CommonFnx fn = new CommonFnx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetClass();
            }
        }

        private void GetClass()
        {
            DataTable dt = fn.Fetch("Select ROW_NUMBER() over(order by (select 1)) as [sr.no], ClassId, ClassName from Class");
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = fn.Fetch("Select * from Class where ClassName = '"+txtClass.Text.Trim()+"' ");

                if(dt.Rows.Count == 0)
                {
                    String query = "Insert Into Class values('" + txtClass.Text.Trim() + "')";
                    fn.Query(query);
                    lblMessage.Text = "Inserted Successfully";
                    lblMessage.CssClass="alert alert-success";
                    txtClass.Text = String.Empty;
                    this.GetClass();
                }
                else
                {
                    lblMessage.Text = "Entered class already exist";
                    lblMessage.CssClass = "alert alert-danger";
                }
            }
            catch(Exception ex)
            {
                Response.Write("<sript>alert('" + ex.Message + "')</script>");
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            this.GetClass();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetClass();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            this.GetClass();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int cid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                string className = (row.FindControl("txtClassEdit") as TextBox).Text;
                string query = "Update Class Set ClassName = '" + className + "' Where ClassId = " + cid;
                fn.Query(query);
                lblMessage.Text = "Class updated Successfully";
                lblMessage.CssClass = "alert alert-success";
                GridView1.EditIndex = -1;  
                this.GetClass();
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('"+ex.Message+"')</script>");
            }
        }
    }
}