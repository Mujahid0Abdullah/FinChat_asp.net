using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class Comments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null)
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                string id = Session["id"].ToString();
                Console.WriteLine(id);
                GetUserinfo(id);

                if (Request.QueryString["postId"] != null)
                {
                    string gg = Request.QueryString["postId"];
                    Console.WriteLine(Request.QueryString["postId"]);
                    GetPost(gg);
                    GetComments(gg);
                }
            }
        }




        protected void GetPost(string postId)
        {



            // postDetails değişkeni örnek olarak bir postun detaylarını içeriyor olsun
            // Bu verileri veritabanından almak için gerekli sorgu çalıştırılır
            string query = "SELECT p.*, u.id AS userId, name, profilePic FROM posts p JOIN users u ON (u.id = p.userId) WHERE p.id=@PostId ORDER BY p.createdAt DESC";
            using (MySqlConnection connection = new MySqlConnection(login.con))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PostId", postId);

                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            // Veritabanından alınan post detayları işlenir
                            string postContent = @"
                            <div class='post-view'>
                                <div class='left-column'>
                                    <div class='user-avatar-big' onclick='openHisProfilePage(" + reader["userId"] + @")'>
                                        <div class='user-avatar' style='height: 100%; width: 100%;'>
                                            <img src='" + reader["profilePic"] + @"'>
                                        </div>
                                    </div>
                                    <div class='user-name'>" + reader["name"] + @"</div>
                                </div>
                                <div class='right-column'>
                                    <div class='upper-row'>
                                        <textarea readonly style='font-weight: bold; font-size: 16px;'>" + reader["desc"] + @"</textarea>
                                    </div>
                                    <div class='lower-row'>
                                        <div style='font-weight: normal; font-size: 12px;' readonly>" + reader["createdAt"] + @"</div>
                                        <button onclick='postClicked(" + reader["id"] + @")' id='commentsButton'>
                                            <img src='https://fin-chat.onrender.com/static/comment.png' alt='Button Image'>
                                        </button>
                                    </div>
                                </div>
                            </div>";

                            // Post içeriği sayfaya eklenir
                            postsList.InnerHtml = postContent;
                        }
                        else
                        {
                            // Post bulunamadıysa hata mesajı gösterilir
                            postsList.InnerHtml = "<p>Post not found.</p>";
                        }

                    }
                }

            }



        }


        protected void GetComments(string postId)
        {



            // postDetails değişkeni örnek olarak bir postun detaylarını içeriyor olsun
            // Bu verileri veritabanından almak için gerekli sorgu çalıştırılır
            string query = "SELECT c.* ,u.id AS userId , name , profilePic FROM comments c JOIN users u ON (u.id= c.userId) WHERE c.postId = ? ORDER BY c.createdAt DESC ";
            using (MySqlConnection connection = new MySqlConnection(login.con))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PostId", postId);

                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {

                        string commentsHtml = "";
                        while (reader.Read())
                        {
                            commentsHtml += $@"
                             <div class='shared-comment'>
                <div class='left-column'>
                    <div class='user-avatar-small'>
                        <div class='user-avatar' style='height: 100%; width: 100%;'>
                        <img src='{reader["profilePic"]}'>
                        </div>
                    </div>
                    <div class='user-name'>{reader["name"]}</div>
                </div>
                <div class='right-column'>
                    <div class='upper-row'>
                        <textarea id='comment{reader["id"]}' readonly style='font-weight: bold; font-size: 16px;'>{reader["desc"]}</textarea>
                    </div>

                  
                    
                    <div class='lower-row'>
                        <div style='font-weight: normal; font-size: 12px;' readonly> {reader["createdAt"]}</div>
                    </div>
                </div>
            </div>

                         ";
                        }
                        commentsList.InnerHtml = commentsHtml;
                        reader.Close();
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
                        userInfo2.Text = name;

                        string profilePic = reader["profilePic"].ToString();
                        Image1.ImageUrl = profilePic;
                        Image2.ImageUrl = profilePic;
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














        protected void sendButton_Click(object sender, EventArgs e)
        {
          
                string Desc = desc.Text.Trim();
                Console.WriteLine($"{Desc}");
                string userId = Session["id"].ToString(); // Kullanıcı kimliğinizi buraya ekleyin
            string PostId = Request.QueryString["postId"];

                try
                {

                    using (MySqlConnection connection = new MySqlConnection(login.con))
                    {
                        connection.Open();
                        string query = "INSERT INTO comments (`desc`, `postId`, `userId`, `createdAt`) VALUES (@Desc, @PostId, @UserId, @CreatedAt)";
                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Desc", Desc);
                        command.Parameters.AddWithValue("@PostId", PostId); 
                        command.Parameters.AddWithValue("@UserId", userId);
                        command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                        // Post başarıyla eklendi
                        GetComments(PostId);
                        //Response.Redirect(Request.RawUrl);// Sayfayı yenile
                        //    Context.ApplicationInstance.CompleteRequest();
                        }
                        else
                        {
                            // Post eklenirken bir hata oluştu
                            // Hata mesajı gösterilebilir veya uygun şekilde işlenebilir
                            Console.WriteLine("comment add hata oldu");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Hata oluştuğunda yapılacak işlemler buraya yazılır
                    Console.WriteLine($"Error adding comment: {ex.Message}");
                }
            
        }
    }
}