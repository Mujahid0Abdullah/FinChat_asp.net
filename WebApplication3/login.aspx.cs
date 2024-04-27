﻿using System;
using System.Collections.Generic;

using System.Security.Cryptography;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace WebApplication3
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void submitLogin_Click(object sender, EventArgs e)
        {
            // Get username and password from the form
            string username = Username.Text;
            string password = Password.Text;
            Console.WriteLine(password);
            System.Diagnostics.Debug.WriteLine(password);
            // Validate username and password (optional, can be done on client-side too)
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                // Display error message
                ScriptManager.RegisterStartupScript(this, GetType(), "InvalidInput", "alert('Please enter username and password.');", true);
                return;
            }

            // Establish connection to the database
            //string connectionString = "Server=http://localhost;Database=webproject;User Id=root;Password=;";
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
                        string storedPasswordHash = reader["password"].ToString();

                        // Check if entered password matches stored password hash
                        if (!VerifyPassword(password, storedPasswordHash))
                        {
                            // Password incorrect
                            ScriptManager.RegisterStartupScript(this, GetType(), "InvalidPassword", "alert('Invalid username or password.');", true); return;
                        }

                        // Password correct, redirect to home page
                        Response.Redirect("About.aspx");
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

        private bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            // Decode stored password hash and extract salt
            byte[] hashBytes = Convert.FromBase64String(storedPasswordHash);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            // Hash entered password using same salt and compare with stored hash
            using (var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, 10000))
            {
                byte[] hash = pbkdf2.GetBytes(20);
                for (int i = 0; i < 20; i++)
                {
                    if (hashBytes[i + 16] != hash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

       
    }
}