using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class post : System.Web.UI.Page
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
                GetPost();
                if (Request.QueryString["postId"] != null)
                {
                    string gg = Request.QueryString["postId"];
                    Console.WriteLine(Request.QueryString["postId"]);
                }
            }
        }

        protected void GetPost() {

          

            // postDetails değişkeni örnek olarak bir postun detaylarını içeriyor olsun
            // Bu verileri veritabanından almak için gerekli sorgu çalıştırılır
            string query = "SELECT p.*, u.id AS userId, name, profilePic FROM posts p JOIN users u ON (u.id = p.userId) ORDER BY p.createdAt DESC";
            using (MySqlConnection connection = new MySqlConnection(login.con))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                   
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        string postsHtml = "";
                        while (reader.Read())
                        {
                            postsHtml += $@"
                        <div class='post-view'>
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
                        Image1.ImageUrl = "https://lh3.googleusercontent.com/d/" + profilePic;
                        Image2.ImageUrl = "https://lh3.googleusercontent.com/d/" + profilePic;
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


        

             protected void Button_image_Upload(object sender, EventArgs e)
        {
         //   StartUpLoad();
        }

        //private void StartUpLoad()
        //{
        //    //get the file name of the posted image  
        //   //// string imgName = FileUpload1.FileName;
        //    //sets the image path  
        //    //string imgPath = "ImageStorage/" + imgName;
        //    //get the size in bytes that  

        //   // int imgSize = FileUpload1.PostedFile.ContentLength;

        //    //validates the posted file before saving  
        //    if (FileUpload1.PostedFile != null && FileUpload1.PostedFile.FileName != "")
        //    {
        //        // 10240 KB means 10MB, You can change the value based on your requirement  
        //        if (FileUpload1.PostedFile.ContentLength > 10240)
        //        {
        //            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('File is too big.')", true);
        //        }
        //        else
        //        {
        //            //then save it to the Folder  
        //            FileUpload1.SaveAs(Server.MapPath(imgPath));
        //            Image1.ImageUrl = "~/" + imgPath;
        //            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('Image saved!')", true);
        //        }

        //    }
        //}


        protected void openProfile(object sender, EventArgs e)
        {
            Response.Redirect("profile.aspx");
        }

        protected void PostMessage(object sender, EventArgs e)
        {
            string Desc = desc.Text.Trim();
            Console.WriteLine($"{Desc}");
            string userId = Session["id"].ToString(); // Kullanıcı kimliğinizi buraya ekleyin

            try
            {
              
                using (MySqlConnection connection = new MySqlConnection(login.con))
                {
                    connection.Open();
                    string query = "INSERT INTO posts (`desc`, `img`, `userId`, `createdAt`) VALUES (@Desc, @Img, @UserId, @CreatedAt)";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Desc", Desc);
                    command.Parameters.AddWithValue("@Img", "1"); // Örnek resim yolu
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        // Post başarıyla eklendi
                        Response.Redirect("post.aspx", false);  // Sayfayı yenile
                        Context.ApplicationInstance.CompleteRequest();
                    }
                    else
                    {
                        // Post eklenirken bir hata oluştu
                        // Hata mesajı gösterilebilir veya uygun şekilde işlenebilir
                        Console.WriteLine("post add hata oldu");
                    }
                }
            }
            catch (Exception ex)
            {
                // Hata oluştuğunda yapılacak işlemler buraya yazılır
                Console.WriteLine($"Error adding post: {ex.Message}");
            }
        }
    
    }
}