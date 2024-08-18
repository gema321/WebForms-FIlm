using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForms
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindFilmData();
            }
        }

        private void BindFilmData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT KodeFilm, NamaFilm, DeskripsiFilm, Poster, TanggalTayang, Status FROM Film";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    FilmRepeater.DataSource = dataTable;
                    FilmRepeater.DataBind();
                }
            }
        }

        public string GetImageUrl(object kodeFilm)
        {
            return $"ImageHandler.ashx?id={kodeFilm}";
        }


        protected void btnWatchlist_Click(object sender, EventArgs e)
        {
            string kodeFilm = ((Button)sender).CommandArgument;
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Film SET Status = 'Y' WHERE KodeFilm = @KodeFilm";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@KodeFilm", kodeFilm);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            BindFilmData();
        }
    }
}