using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
       

        }


        //protected void FindUser(string searchQuery)
        //{
        //    // Veritabanından kullanıcıları ara

        //    string query = "SELECT * FROM users WHERE name LIKE @name";

        //    using (SqlConnection connection = new SqlConnection(login.con))
        //    {
        //        using (SqlCommand command = new SqlCommand(query, connection))
        //        {
        //            command.Parameters.AddWithValue("@name", "%" + searchQuery + "%");

        //            try
        //            {
        //                connection.Open();
        //                SqlDataReader reader = command.ExecuteReader();
        //                var userList = new System.Collections.Generic.List<object>();

        //                while (reader.Read())
        //                {
        //                    // Her bir kullanıcıyı JSON nesnesine ekle
        //                    var user = new
        //                    {
        //                        id = reader["id"],
        //                        name = reader["name"],
        //                        profilePic = reader["profilePic"]
        //                    };
        //                    userList.Add(user);
        //                }
        //                JavaScriptSerializer serializer = new JavaScriptSerializer();
        //                // JSON formatında kullanıcı listesini döndür
        //                Response.ContentType = "application/json";
        //                Response.Write(serializer.Serialize(userList));
        //            }
        //            catch (Exception ex)
        //            {
        //                Response.StatusCode = 500;
        //                Response.Write("Error: " + ex.Message);
        //            }
        //        }
        //    }
        //}


        protected void FindUser(string searchQuery)
        {

            if (searchQuery == null || searchQuery == "") { usersList.Visible = false; }
            else
            {
                usersList.Visible = true;
                string query = "SELECT * FROM users WHERE name LIKE @name";

                using (MySqlConnection connection = new MySqlConnection(login.con))
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", "%" + searchQuery + "%");

                        try
                        {
                            connection.Open();
                            MySqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                string searchHtml = "";
                                while (reader.Read())
                                {
                                    // Verileri işleyebilir ve ekrana yazdırabilirsiniz
                                    //ResultLabel.Text += $"Name: {reader["name"]}, Email: {reader["email"]}<br/>";

                                    searchHtml += $@"
<div class='view-profiles'>
    <div class='left-column-vp'>
        <div class='user-avatar-vp' onclick='openHisProfilePage({reader["id"]})'>
            <div class='user-avatar' style='height: 100%; width: 100%;'>
                <img src='{reader["profilePic"]}'>
            </div>
        </div>
    </div>
    <div class='right-column-vp'>
        {reader["name"]}   
    </div>
</div>  ";
                                }
                                usersList.InnerHtml = searchHtml;
                            }
                            else
                            {
                                usersList.InnerHtml = "No users found.";
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                    }
                }
            }
        }

        protected void search_TextChanged(object sender, EventArgs e)
        {
            FindUser(search.Text);
        }

        protected void logout(object sender, ImageClickEventArgs e)
        {
            Session.Clear(); // Oturum değişkenlerini temizler
            Session.Abandon(); // Oturumu sonlandırır

            // Kullanıcıyı giriş sayfasına yönlendir
            Response.Redirect("Login.aspx");
        }
    }
}