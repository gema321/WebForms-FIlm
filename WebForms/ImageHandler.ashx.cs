using System;
using System.Data.SqlClient;
using System.Web;

public class ImageHandler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        string id = context.Request.QueryString["id"];

        if (int.TryParse(id, out int filmId))
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Poster FROM Film WHERE KodeFilm = @KodeFilm";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@KodeFilm", filmId);
                    connection.Open();

                    byte[] imageData = command.ExecuteScalar() as byte[];

                    if (imageData != null)
                    {
                        context.Response.ContentType = "image/jpeg"; // Adjust MIME type if needed
                        context.Response.OutputStream.Write(imageData, 0, imageData.Length);
                    }
                    else
                    {
                        context.Response.ContentType = "text/plain";
                        context.Response.Write("Image not found.");
                    }
                }
            }
        }
        else
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Invalid ID.");
        }
    }

    public bool IsReusable
    {
        get { return false; }
    }
}
