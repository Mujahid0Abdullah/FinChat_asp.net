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

           
           
            string username = Username.Text;
            string password = Password.Text;
            string email = Email.Text;
            string name = Name.Text;
            Console.WriteLine(password);
            
            // username and password boş değil 
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
               
                ScriptManager.RegisterStartupScript(this, GetType(), "InvalidInput", "alert('username ve password doldurun.');", true);
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



                        // yeni kullanıcı kayıt 
                        string queryInsertUser = "INSERT INTO users (username, email, password, name) VALUES (@Username, @Email, @Password, @Name)";
                        MySqlCommand commandInsertUser = new MySqlCommand(queryInsertUser, connection);
                        commandInsertUser.Parameters.AddWithValue("@Username", username);
                        commandInsertUser.Parameters.AddWithValue("@Email", email);
                        commandInsertUser.Parameters.AddWithValue("@Password", hashedPassword);
                        commandInsertUser.Parameters.AddWithValue("@Name", name);
                        commandInsertUser.ExecuteNonQuery();

                        // login sayfasına yollandırmak 
                        Response.Write("<script>alert('kullanıcı oluşturuldu..');window.location = 'login.aspx';</script>");
                       

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