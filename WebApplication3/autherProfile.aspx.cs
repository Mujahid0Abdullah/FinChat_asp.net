using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        private int followerUserId;
        private int followedUserId;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["id"] == null)
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                string id = Session["id"].ToString();
                
                
                
                if (Request.QueryString["userId"] != null)
                {
                    GetUserinfo(Request.QueryString["userId"]);
                    followedUserId= Int32.Parse(Request.QueryString["userId"]);
                    followerUserId = Int32.Parse(id);
                    if(followerUserId == followedUserId)
                    {
                        Response.Redirect("profile.aspx");

                    }
                    string gg = Request.QueryString["userId"];
                    GetPost(Request.QueryString["userId"]);
                    Console.WriteLine(Request.QueryString["userId"]);
                    UpdateButtonState(followerUserId, followedUserId);
                }
                else
                {
                    Response.Redirect("post.aspx");
                }
            }
        }

        protected void FollowButton_Click(object sender, EventArgs e)
        {
           
            if (IsFollowing(followerUserId, followedUserId))
            {
                deleteRelationship(followerUserId, followedUserId);
            }
            else
            {
                addRelationship(followerUserId, followedUserId);
            }
            UpdateButtonState(followerUserId, followedUserId);
        }

        private void UpdateButtonState(int followerUserId, int followedUserId)
        {
            if (IsFollowing(followerUserId, followedUserId))
            {
                FollowButton.Text = "Unfollow";
            }
            else
            {
                FollowButton.Text = "Follow";
            }
        }

        private bool IsFollowing(int followerUserId, int followedUserId)
        {
            string query = "SELECT followerUserId FROM relationships WHERE followedUserId =@followedUserId and followerUserId=@followerUserId ";

            using (MySqlConnection connection = new MySqlConnection(login.con))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@followerUserId", followerUserId);
                    command.Parameters.AddWithValue("@followedUserId", followedUserId);

                    try
                    {
                        connection.Open();
                        MySqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        { return true; }
                        else { return false; }
                            int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Following");
                        }
                        else
                        {
                            Console.WriteLine("Failed to follow");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
            // Takip durumunu kontrol etmek için gerekli sorguyu burada oluşturabilir ve çalıştırabilirsiniz
            // Örneğin:
            // string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            // string query = "SELECT COUNT(*) FROM relationships WHERE followerUserId = @followerUserId AND followedUserId = @followedUserId";
            // // SQL sorgusu için gerekli bağlantı ve parametreleri burada tanımlayabilirsiniz
            // // Ardından sorguyu çalıştırabilir ve sonucu işleyebilirsiniz
            // int count = 0; // Sorgu sonucunu almak için bir değişken
            // return count > 0; // Sorgu sonucuna göre true veya false döndürün
            // Burada örnek olarak her zaman false döndürüyoruz.
            return false;
        }
        private void addRelationship(int followerUserId, int followedUserId)
        {
            string query = "INSERT INTO relationships (followerUserId, followedUserId) VALUES (@followerUserId, @followedUserId)";

            using (MySqlConnection connection = new MySqlConnection(login.con))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@followerUserId", followerUserId);
                    command.Parameters.AddWithValue("@followedUserId", followedUserId);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                          Console.WriteLine("Following");
                        }
                        else
                        {
                            Console.WriteLine("Failed to follow");
                        }
                    }
                    catch (Exception ex)
                    {
                       Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
        }

        private void deleteRelationship(int followerUserId, int followedUserId)
        {
            string query = "DELETE FROM relationships WHERE followerUserId = @followerUserId AND followedUserId = @followedUserId";

            using (MySqlConnection connection = new MySqlConnection(login.con))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@followerUserId", followerUserId);
                    command.Parameters.AddWithValue("@followedUserId", followedUserId);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Unfollowed");
                        }
                        else
                        {
                            Console.WriteLine("Failed to unfollow");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
        }

        protected void GetUserinfo(string userId)
        {
            try
            {

                using (MySqlConnection connection = new MySqlConnection(login.con))
                {
                    Console.WriteLine(login.con);
                    connection.Open();
                    string query = "SELECT name, username, profilePic, id, email FROM users WHERE id = @UserId";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UserId", userId);
                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        string name = reader["name"].ToString();
                        userInfo2.Text =  name;

                        string email = reader["email"].ToString();
                        userEmail.Text =  email;



                        string profilePic = reader["profilePic"].ToString();
                        Image1.ImageUrl = "https://lh3.googleusercontent.com/d/" + profilePic;

                        Console.WriteLine($"{name} {profilePic}");
                        // JavaScript tarafına değişkenleri aktar
                        //string script = $"setUserInfo('{name}', '{profilePic}');";
                        // ClientScript.RegisterStartupScript(this.GetType(), "UserInfoScript", script, true);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching user info: {ex.Message}");
            }
        }


        protected void GetPost(string Id)
        {



            // postDetails değişkeni örnek olarak bir postun detaylarını içeriyor olsun
            // Bu verileri veritabanından almak için gerekli sorgu çalıştırılır
            string query = "SELECT p.*, u.id AS userId, name, profilePic FROM posts p JOIN users u ON (u.id = p.userId) WHERE u.id=@Id ORDER BY p.createdAt DESC";
            using (MySqlConnection connection = new MySqlConnection(login.con))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", Id);
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        string postsHtml = "";
                        while (reader.Read())
                        {
                            postsHtml += $@"
                        <div class='post-view' style='border-radius: 12px;
                            box-shadow: 2px 2px black; '>
                            <div class='left-column'>
                                <div class='user-avatar-big' onclick='openHisProfilePage({reader["userId"]})'>
                                    <div class='user-avatar' style='height: 100%; width: 100%;'>
                                        <img src='https://lh3.googleusercontent.com/d/{reader["profilePic"]}'>
                                    </div>
                                </div>
                                <div class='user-name'>{reader["name"]}</div>
                            </div>
                            <div class='right-column'>
                                <div class='upper-row'>
                                    <textarea readonly style='font-weight: bold; font-size: 16px;'>{reader["desc"]}</textarea>
                                </div>
                                <div class='lower-row'>
                                    <div style='font-weight: normal; font-size: 12px;' readonly>{reader["createdAt"]}</div>
                                    <button onclick='postClicked({reader["id"]})' id='commentsButton'>
                                        <img src='./photos/comment.png' alt='Button Image'>
                                    </button>
                                </div>
                            </div>
                        </div>";
                        }
                        postsList.InnerHtml = postsHtml;
                        reader.Close();
                    }



                }
            }

        }

    }
}