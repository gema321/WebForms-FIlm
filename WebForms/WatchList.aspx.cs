using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.EnterpriseServices;

namespace WebForms
{
    public partial class WatchList : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadData();
            }
        }

        public DataSet loadData()
        {
            SqlCommand com = new SqlCommand();
            com.Connection = con;
            com.CommandText = "SELECT * FROM Film WHERE NamaFilm  like '%' +@p1 + '%' AND status = 'Y'";
            com.CommandType = CommandType.Text;
            com.Parameters.AddWithValue("@p1", txtSearch.Text);
            SqlDataAdapter adap = new SqlDataAdapter(com);
            adap.Fill(ds);
            dt = ds.Tables[0];
            ViewState["DGView"] = dt;
            DGView.DataSource = (DataTable)ViewState["DGView"];
            DGView.DataBind();
            return ds;
        }

        protected void DGView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DGView.PageIndex = e.NewPageIndex;
            loadData();
        }

        public SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = SortDirection.Ascending;
                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }

        private void SortGridView(string sortExpression, string direction)
        {
            DataTable dt = loadData().Tables[0];

            DataView dv = new DataView(dt);
            dv.Sort = sortExpression + direction;

            DGView.DataSource = dv;
            DGView.DataBind();
        }

        protected void DGView_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;

            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridView(sortExpression, DESCENDING);
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridView(sortExpression, ASCENDING);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadData();
        }

        protected void DGView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           if (e.CommandName == "Hapus")
            {
                String id = DGView.DataKeys[Convert.ToInt32(e.CommandArgument.ToString())].Value.ToString();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "UPDATE Film SET status = 'N' WHERE KodeFilm = @p1";
                com.CommandType = CommandType.Text;
                com.Parameters.AddWithValue("@p1", id);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
                loadData();
                Response.Write("<script>alert('Data Berhasil dihapus');</script>");
            }
        }

    }
}