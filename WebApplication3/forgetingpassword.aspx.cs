using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace WebApplication3
{
    public partial class forgetingpassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
     

        protected void p() {
            // Get username and password from the form
            string username = Username.Text;
            string email = Email.Text;
            string password = Password.Text;
            string RepeatPassword = rPassword.Text;
            string k = Session["kod"].ToString();
            if (!string.IsNullOrEmpty(Kod.Text) && Kod.Text.Trim() == Session["kod"].ToString() && !string.IsNullOrEmpty(Password.Text) && password == RepeatPassword)
            {
                // Şifreleme işlemi
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(Password.Text);

                // Kullanıcı bilgilerini güncelleme işlemi

                string query = "UPDATE users SET  password=@password  WHERE username=@id";

                using (MySqlConnection connection = new MySqlConnection(login.con))
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        
                        command.Parameters.AddWithValue("@password", hashedPassword);
                        command.Parameters.AddWithValue("@id", username);
                       


                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Güncelleme başarılı
                            Response.Write("<script>alert('Your information has been updated');</script>");
                            Response.Redirect(Request.RawUrl); // Sayfayı yenile
                        }
                        else
                        {
                            // Güncelleme başarısız
                            Response.Write("<script>alert('Update failed');</script>");
                        }
                    }
                }




            }
          
        }



        protected void ema()
        {

          
         
            // Get username and password from the form
            string username = Username.Text;
            string email = Email.Text;
            Console.WriteLine(email);
            System.Diagnostics.Debug.WriteLine(email);
            // Validate username and password (optional, can be done on client-side too)
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email))
            {
                // Display error message
                ScriptManager.RegisterStartupScript(this, GetType(), "InvalidInput", "alert('Please enter username and email.');", true);
                return;
            }

            // Establish connection to the database
            string connectionString = "SERVER=localhost;DATABASE=webproject;UID=root;PASSWORD=;";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Query to select user by username
                    string query = "SELECT * FROM users WHERE username = @Username";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        MySqlDataReader reader = command.ExecuteReader();

                        if (!reader.HasRows)
                        {
                            // User not found
                            ScriptManager.RegisterStartupScript(this, GetType(), "UserNotFound", "alert('This user does not exist.');", true); return;
                        }

                        // User found, read password hash from database
                        reader.Read();
                        //string storedPasswordHash = reader["password"].ToString();
                        //string id = reader["id"].ToString();
                        //Console.WriteLine(id);
                        // Şifreyi hashle ve hashlenmiş şifreyi al
                        //string AhashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
                        if(reader["email"].ToString()== email)
                        {

                            MailMessage mesaj = new MailMessage();
                            SmtpClient istemci = new SmtpClient();
                            istemci.Credentials = new System.Net.NetworkCredential("qqwweeqweft@outlook.sa", "12345ttyyuukkoo");
                            istemci.Port = 587;
                            istemci.Host = "smtp.office365.com";
                            istemci.EnableSsl = true;
                            mesaj.To.Add(Email.Text);
                            mesaj.Subject = "Doğrulama Kodu";
                            mesaj.From = new MailAddress("qqwweeqweft@outlook.sa");
                            Random rnd = new Random();
                            String rndsifre = rnd.Next(100000000).ToString();
                            mesaj.Body = "Kod: " + rndsifre;
                            Session["kod"] = rndsifre;
                           
                            try
                            {
                                istemci.Send(mesaj);


                            
                               
                                ScriptManager.RegisterStartupScript(this, GetType(), "email inceleyin", "alert('emaile Doğrulama kodu geldi');", true);


                            }
                            catch (Exception ex)
                            {

                                ScriptManager.RegisterStartupScript(this, GetType(), "email inceleyin", "alert('"+ex.Message+"');", true);
                            }

                        }
                        //----------------------------------

                        //----------------------------------
                        // Check if entered password matches stored password hash

                    }
                }
            }
            catch (Exception ex)
            {
                // Handle database connection error
                ScriptManager.RegisterStartupScript(this, GetType(), "DatabaseError", "alert('An error occurred while processing your request.');", true);
                Console.WriteLine(ex.Message);
            }


        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            ema();
        }

        protected void loginButton_Click(object sender, EventArgs e)
        {
            p();
        }
    }
}