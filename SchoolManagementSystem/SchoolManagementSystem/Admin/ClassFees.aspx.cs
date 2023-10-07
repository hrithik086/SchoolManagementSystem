using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.WebSockets;
using static SchoolManagementSystem.Models.CommonFn;

namespace SchoolManagementSystem.Admin
{
    public partial class ClassFees : System.Web.UI.Page
    {
        CommonFnx fn = new CommonFnx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetClass();
                GetFees();
            }
        }

        private void GetClass()
        {
            DataTable dt = fn.Fetch("Select * from Class");
            ddlClass.DataSource = dt;
            ddlClass.DataTextField = "ClassName";
            ddlClass.DataValueField = "ClassId";
            ddlClass.DataBind();
            ddlClass.Items.Insert(0, "Select Class");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string classVal=ddlClass.SelectedItem.Value;
            try
            {
                DataTable dt = fn.Fetch("Select * from fees where ClassId = '" + ddlClass.SelectedItem.Value + "' ");

                if (dt.Rows.Count == 0)
                {
                    String query = "Insert Into Fees values('"+ ddlClass.SelectedItem.Value +"','" + txtFeeAmount.Text.Trim() + "')";
                    fn.Query(query);
                    lblMessage.Text = "Inserted Successfully";
                    lblMessage.CssClass = "alert alert-success";
                    ddlClass.SelectedIndex = 0;
                    txtFeeAmount.Text = String.Empty;
                    this.GetFees();
                }
                else
                {
                    lblMessage.Text = "Entered class "+ classVal +" already exist";
                    lblMessage.CssClass = "alert alert-danger";
                }
            }
            catch (Exception ex)
            {
                Response.Write("<sript>alert('" + ex.Message + "')</script>");
            }
        }

        private void GetFees()
        {
            DataTable dt = fn.Fetch("Select Row_NUMBER() over(Order by  (Select 1)) as [Sr.No], f.FeesId, f.ClassId, c.ClassName, f.FeesAmount   from Fees f inner join Class c on c.ClassId = f.ClassId ");
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GetFees();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetFees();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int feesId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                string query = "Delete From Fees where FeesId = '" + feesId + "'";
                fn.Query(query);
                lblMessage.Text = "Fees Deleted Successfully";
                lblMessage.CssClass = "alert alert-success";
                GridView1.EditIndex = -1;
                GetFees();
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('exception occured message : " + ex.Message + "')</script>");
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GetFees();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int feesId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                string fees =(row.FindControl("feeEdit") as TextBox).Text;
                string query = "Update Fees Set FeesAmount = '" + fees.Trim() + "' where FeesId ='" + feesId + "'";
                fn.Query(query);
                lblMessage.Text = "Fees updated successfully";
                lblMessage.CssClass = "alert alert-success";
                GridView1.EditIndex = -1;
                GetFees();
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('exception occured message : "+ex.Message+"')</script>");
            }
        }
    }
}