using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace WebForms
{
    public partial class Film : System.Web.UI.Page
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
                Form.Visible = false;
                ViewForm.Visible = true;
                BtnAction.Visible = true;

            }
        }

        public DataSet loadData()
        {
            SqlCommand com = new SqlCommand();
            com.Connection = con;
            com.CommandText = "SELECT * FROM Film WHERE NamaFilm  like '%' +@p1 + '%'";
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

        protected void btnTambah_Click(object sender, EventArgs e)
        {
            clear();
            Form.Visible = true;
            ViewForm.Visible = false;
            BtnAction.Visible = false;
        }

        public void clear()
        {
            TxtNamaFilm.Text = "";
            TxtDeskripsiFilm.Text = "";
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string namaFilm = TxtNamaFilm.Text;
                string deskripsiFilm = TxtDeskripsiFilm.Text;
                string tanggalTayangStr = TxtTanggalTayang.Text;
                DateTime tanggalTayang;
                byte[] posterData = null;

                // Parse the date string to DateTime
                if (!DateTime.TryParse(tanggalTayangStr, out tanggalTayang))
                {
                    // Handle the parsing error
                    // You can display an error message to the user or log the error
                    return;
                }

                if (lblId.Text != "")
                {
                    if (FileUploadPoster.HasFile)
                    {
                        try
                        {
                            using (BinaryReader reader = new BinaryReader(FileUploadPoster.PostedFile.InputStream))
                            {
                                posterData = reader.ReadBytes(FileUploadPoster.PostedFile.ContentLength);
                            }
                        }
                        catch (Exception ex)
                        {
                            // Handle the exception
                            return;
                        }
                    }

                    string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string query = "UPDATE Film SET NamaFilm = @NamaFilm, DeskripsiFilm = @DeskripsiFilm, Poster = @Poster, TanggalTayang = @TanggalTayang, Status = 'N' WHERE KodeFilm = @KodeFakultas";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@NamaFilm", namaFilm);
                            command.Parameters.AddWithValue("@DeskripsiFilm", deskripsiFilm);
                            command.Parameters.AddWithValue("@Poster", posterData ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@TanggalTayang", tanggalTayang);
                            command.Parameters.AddWithValue("@KodeFakultas", lblId.Text.ToUpper());
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }
                    Response.Redirect("Film.aspx");
                }
                else
                {
                    if (FileUploadPoster.HasFile)
                    {
                        try
                        {
                            using (BinaryReader reader = new BinaryReader(FileUploadPoster.PostedFile.InputStream))
                            {
                                posterData = reader.ReadBytes(FileUploadPoster.PostedFile.ContentLength);
                            }
                        }
                        catch (Exception ex)
                        {
                            // Handle the exception
                            return;
                        }
                    }

                    string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string query = "INSERT INTO Film (NamaFilm, DeskripsiFilm, Poster, TanggalTayang, Status) VALUES (@NamaFilm, @DeskripsiFilm, @Poster, @TanggalTayang, 'N')";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@NamaFilm", namaFilm);
                            command.Parameters.AddWithValue("@DeskripsiFilm", deskripsiFilm);
                            command.Parameters.AddWithValue("@Poster", posterData ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@TanggalTayang", tanggalTayang);
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }
                    Response.Redirect("Film.aspx");
                }
            }
        }



        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Form.Visible = false;
            ViewForm.Visible = true;
            BtnAction.Visible = true;
        }

        protected void DGView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ubah")
            {
                String id = DGView.DataKeys[Convert.ToInt32(e.CommandArgument.ToString())].Value.ToString();
                lblId.Text = id;
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "SELECT * FROM Film WHERE KodeFilm  = @p1 ";
                com.CommandType = CommandType.Text;
                com.Parameters.AddWithValue("@p1", id);
                con.Open();
                SqlDataReader dr = com.ExecuteReader();
                dr.Read();
                TxtNamaFilm.Text = dr[1].ToString();
                TxtDeskripsiFilm.Text = dr[2].ToString();
                DateTime tanggalTayang;
                if (DateTime.TryParse(dr["TanggalTayang"].ToString(), out tanggalTayang))
                {
                    // Format datetime ke string sesuai dengan format yang diharapkan oleh kontrol TextBox
                    TxtTanggalTayang.Text = tanggalTayang.ToString("yyyy-MM-ddTHH:mm");
                }

                con.Close();
                Form.Visible = true;
                ViewForm.Visible = false;
                BtnAction.Visible = false;
            }
            else if (e.CommandName == "Hapus")
            {
                String id = DGView.DataKeys[Convert.ToInt32(e.CommandArgument.ToString())].Value.ToString();
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "DELETE Film WHERE KodeFilm = @p1";
                com.CommandType = CommandType.Text;
                com.Parameters.AddWithValue("@p1", id);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
                loadData();
                clear();
                Form.Visible = false;
                ViewForm.Visible = true;
                BtnAction.Visible = true;
                Response.Write("<script>alert('Data Berhasil dihapus');</script>");
            }
        }

    }
}