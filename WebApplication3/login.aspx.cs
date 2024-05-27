using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using BCrypt.Net;
using System.Security.Cryptography;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Generators;

namespace WebApplication3
{
    public partial class login : System.Web.UI.Page
    {   //global connection text
        public static string con = "SERVER=localhost;DATABASE=webproject;UID=root;PASSWORD=;";

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void submitLogin_Click(object sender, EventArgs e)
        {
           
            string username = Username.Text;
            string password = Password.Text;
            Console.WriteLine(password);

            System.Diagnostics.Debug.WriteLine(password);

          
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                //error 
                ScriptManager.RegisterStartupScript(this, GetType(), "InvalidInput", "alert('Please enter username and password.');", true);
                return;
            }

          

            try
            {
                using (MySqlConnection connection = new MySqlConnection(con))
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
                        string storedPasswordHash = reader["password"].ToString();
                        string id = reader["id"].ToString();    
                        Console.WriteLine(id);
                        // Şifreyi hashle ve hashlenmiş şifreyi al
                        //string AhashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
                   
                        //----------------------------------
                        bool isMatch = BCrypt.Net.BCrypt.Verify(password, storedPasswordHash);
                        Console.WriteLine("Passwords match: " + isMatch);
                        //----------------------------------
                        // Check if entered password matches stored password hash
                        if (!isMatch)
                        {
                            // Password incorrect
                            ScriptManager.RegisterStartupScript(this, GetType(), "InvalidPassword", "alert('Invalid username or password.');", true); return;
                        }
                        Session["id"] = id;
                        // Password correct, redirect to home page
                        Response.Redirect("post.aspx");
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

        protected void loginButton_Click(object sender, EventArgs e)
        {

        }
        //protected void submitLogin_Click(object sender, EventArgs e)
        //{
        //    // Get username and password from the form
        //    string inputUsername = username.Text;
        //    string inputPassword = password.Text;

        //    // Validate username and password (optional, can be done on client-side too)
        //    if (string.IsNullOrEmpty(inputUsername) || string.IsNullOrEmpty(inputPassword))
        //    {
        //        // Display error message (e.g., using a Label control or alert)
        //        Console.WriteLine( "Please enter username and password.");
        //        return;
        //    }

        //    // Define your SQL connection string (replace "YourConnectionString" with your actual connection string)

        //    string connectionString = "Server=http://localhost;Database=webproject;User Id=root;Password=;";


        //    // Define your SQL query to fetch user information
        //    string query = "SELECT COUNT(*) FROM users WHERE username = @Username AND Password = @Password";

        //    // Create a new SQL connection and command
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    using (SqlCommand command = new SqlCommand(query, connection))
        //    {
        //        // Add parameters to prevent SQL injection
        //        command.Parameters.AddWithValue("@Username", inputUsername);
        //        command.Parameters.AddWithValue("@Password", inputPassword);

        //        // Open the SQL connection
        //        connection.Open();

        //        // Execute the SQL command and get the result (number of rows matching the criteria)
        //        int rowCount = (int)command.ExecuteScalar();

        //        // Check if a user with the provided credentials exists
        //        if (rowCount > 0)
        //        {
        //            // Login successful! Redirect to another page (e.g., home page)
        //            Response.Redirect("home.aspx");
        //        }
        //        else
        //        {
        //            // Login failed! Display error message
        //            errorMessage.Text = "Invalid username or password.";
        //        }
        //    }
        //}




    }
}