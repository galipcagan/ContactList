using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient; //used to do connections
using System.Data;

public partial class Contact : System.Web.UI.Page
{
    SqlConnection sqlCon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\derya.DESKTOP-BHDIT4P\Source\Repos\ConctactList2_3\ConctactList2_3\App_Data\Database.mdf;Integrated Security=True");
    protected void Page_Load(object sender, EventArgs e) //generic loading page
    {
        if (!IsPostBack) //if rendered first time or not 
        {
            btnDelete.Enabled = false;
            FillGridView();
        }
    }

    protected void btnClear_Click(object sender, EventArgs e) //button that clears fields
    {
        clear(); 
    }
    public void clear() { // as it will be called again use it.
        hf.Value = "";
        txtFirst.Text = txtLast.Text = txtPhone.Text = "";
        lblErrorMessage.Text = lblSuccessMessage.Text = "";
        btnSave.Text = "Save";
        btnDelete.Enabled = false;

    }

    protected void btnSave_Click(object sender, EventArgs e) //function used to create or update db
    {
        if (sqlCon.State == ConnectionState.Closed) {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("ContactCreateOrUpdate", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@Id",hf.Value==""?0:Convert.ToInt32(hf.Value));
            sqlCmd.Parameters.AddWithValue("@First",txtFirst.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@Last", txtLast.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@Phone", txtPhone.Text.Trim());
            sqlCmd.ExecuteNonQuery();  //executes the transcation that wants to be done
            sqlCon.Close();  //close connection
            string iD = hf.Value; //savind id value
            clear();   //clear the fields
            if (iD == "")  // this the case when there is a new entry
            {
                lblSuccessMessage.Text = "Saved Succesfuly";
                FillGridView();
            }
            else   // this is the case when entry is udate
            {
                lblSuccessMessage.Text = "Updated Successfully";
                FillGridView();
            }
        }
    }

    void FillGridView()   // shows data in gridview
    {
        if (sqlCon.State == ConnectionState.Closed)
        {
            sqlCon.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("ContactViewAll", sqlCon);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dtb = new DataTable();
            sqlDa.Fill(dtb);
            sqlCon.Close();
            gvCotact.DataSource = dtb;
            gvCotact.DataBind();

        }
    }
    protected void lnkOnClick(object sender, EventArgs e) //for viewing data in the fields in
    {
        int Id = Convert.ToInt32((sender as LinkButton).CommandArgument);
        if (sqlCon.State == ConnectionState.Closed)
        {
            sqlCon.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("ContactViewById", sqlCon);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("@Id", Id);
            DataTable dtb = new DataTable();
            sqlDa.Fill(dtb);
            sqlCon.Close();
            hf.Value = Id.ToString();
            txtFirst.Text = dtb.Rows[0]["First"].ToString();
            txtLast.Text = dtb.Rows[0]["Last"].ToString();
            txtPhone.Text = dtb.Rows[0]["Phone"].ToString();

            btnSave.Text = "Update";
            btnDelete.Enabled = true;

        }
    }

    protected void btnDelete_Click(object sender, EventArgs e) //deleting entry from db
    {
        if (sqlCon.State == ConnectionState.Closed)
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("ContactDeleteById", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@Id", Convert.ToInt32(hf.Value));
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            clear();
            FillGridView();
            lblSuccessMessage.Text = "Deleted Successfully";
        }
    }
}