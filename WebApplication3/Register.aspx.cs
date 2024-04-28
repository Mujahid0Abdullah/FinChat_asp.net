using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace WebApplication3
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void submitLogin_Click(object sender, EventArgs e)
        {

           
            // Get username and password from the form
            string username = Username.Text;
            string password = Password.Text;
            string email = Email.Text;
            string name = Name.Text;
            Console.WriteLine(password);
            
            // Validate username and password (optional, can be done on client-side too)
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                // Display error message
                ScriptManager.RegisterStartupScript(this, GetType(), "InvalidInput", "alert('Please enter username and password.');", true);
                return;
            }

            // Establish connection to the database
  

            try
            {
                using (MySqlConnection connection = new MySqlConnection(login.con))
                {
                    connection.Open();

                    string queryCheckUser = "SELECT * FROM users WHERE username = @Username";
                    MySqlCommand commandCheckUser = new MySqlCommand(queryCheckUser, connection);
                    commandCheckUser.Parameters.AddWithValue("@Username", username);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(commandCheckUser);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        // "bu kullanıcı kayıtlı";,
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('bu kullanıcı kayıtlı')", true);
                    }


                    else
                    {

                        // Hash password
                        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);



                        // Insert new user
                        string queryInsertUser = "INSERT INTO users (username, email, password, name) VALUES (@Username, @Email, @Password, @Name)";
                        MySqlCommand commandInsertUser = new MySqlCommand(queryInsertUser, connection);
                        commandInsertUser.Parameters.AddWithValue("@Username", username);
                        commandInsertUser.Parameters.AddWithValue("@Email", email);
                        commandInsertUser.Parameters.AddWithValue("@Password", hashedPassword);
                        commandInsertUser.Parameters.AddWithValue("@Name", name);
                        commandInsertUser.ExecuteNonQuery();
                        // Password correct, redirect to home page
                        Response.Write("<script>alert('kullanıcı oluşturuldu..');window.location = 'login.aspx';</script>");
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('kullanıcı oluşturuldu')", true);
                        //Response.Redirect("login.aspx");

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
    }
}