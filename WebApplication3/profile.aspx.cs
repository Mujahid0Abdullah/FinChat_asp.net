using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace WebApplication3
{
    public partial class WebForm1 : System.Web.UI.Page
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
                GetPost(id);
                Getfollowed(id);
                Getfollower(id);
                if (Request.QueryString["userId"] != null)
                {
                    string gg = Request.QueryString["userId"];
                    Console.WriteLine(Request.QueryString["userId"]);
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

                        string email = reader["email"].ToString();
                        userEmail.Text =  email;

                        
                        
                        string profilePic = reader["profilePic"].ToString();
                        Image1.ImageUrl =  profilePic;
                        
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

        protected void postDeleteClicked(object sender, EventArgs e) {
            string id = ((Button)sender).Attributes["data-post-id"];
            if (id != null)
            {
                postDelete(id);
            }
            else
            {
                // Handle case where data-post-id is not set
                Response.Write("<script>alert('Error: Post ID not found');</script>");
            }
     
        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            string prfilepic="";
            if (FileUpload1.HasFile)
            {
                prfilepic= StartUpLoad();
            }

            string name = firstnameLastname.Text;
            string Password = password.Text;
            string RepeatPassword = repeatPassword.Text;

            if (Password == RepeatPassword && !string.IsNullOrEmpty(name))
            {
                // Şifreleme işlemi
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(Password);

                // Kullanıcı bilgilerini güncelleme işlemi
                
                string query = "UPDATE users SET name=@name, password=@password , profilePic=@img WHERE id=@id";

                using (MySqlConnection connection = new MySqlConnection(login.con))
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@password", hashedPassword);
                        command.Parameters.AddWithValue("@id", Session["id"]);
                        command.Parameters.AddWithValue("@img", prfilepic);


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
            else
            {
                // Şifreler uyuşmuyor veya isim boş
                Response.Write("<script>alert('Passwords don't match or name is empty');</script>");
            }
        }

        protected void Getfollower(string Id)
        {



            string query = "SELECT r.* ,u.id, name, profilePic  FROM relationships r JOIN users u ON (u.id = r.followedUserId) WHERE r.followerUserId = @id";
            using (MySqlConnection connection = new MySqlConnection(login.con))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        string followHtml = "";
                        while (reader.Read())
                        {
                            followHtml += $@"  <div class=""follow-view-profiles"">
            <div class=""left-column-vp"">
              <div class=""user-avatar-vp"" onclick=""openHisProfilePage({reader["id"]})"">
                <div class=""user-avatar""  style=""height: 100%; width: 100%;"">
                <img src='{reader["profilePic"]}'>
                </div>
              </div>
            </div>
            <div class=""right-column-vp"">
              {reader["name"]}
            </div>
          </div>";
                        }
                        followerList.InnerHtml = followHtml;
                        reader.Close();
                    }



                }
            }

        }

        protected void Getfollowed(string Id)
        {



            // postDetails değişkeni örnek olarak bir postun detaylarını içeriyor olsun
            // Bu verileri veritabanından almak için gerekli sorgu çalıştırılır
            string query = "SELECT r.* ,u.id, name, profilePic  FROM relationships r JOIN users u ON (u.id = r.followerUserId) WHERE r.followedUserId = @id";
            using (MySqlConnection connection = new MySqlConnection(login.con))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        string followHtml = "";
                        while (reader.Read())
                        {
                            followHtml += $@"  <div class=""follow-view-profiles"">
            <div class=""left-column-vp"">
              <div class=""user-avatar-vp"" onclick=""openHisProfilePage({reader["id"]})"">
                <div class=""user-avatar""  style=""height: 100%; width: 100%;"">
                <img src='{reader["profilePic"]}'>
                </div>
              </div>
            </div>
            <div class=""right-column-vp"">
              {reader["name"]}
            </div>
          </div>";
                        }
                        followedList.InnerHtml = followHtml;
                        reader.Close();
                    }



                }
            }

        }
        protected void postDelete(string Id)
        {
            string q = "DELETE FROM posts WHERE id=@Id";
            using (MySqlConnection connection = new MySqlConnection(login.con))
            {
                using (MySqlCommand command = new MySqlCommand(q, connection))
                {
                    command.Parameters.AddWithValue("@Id", Id);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        //  başarılı
                        Response.Write("<script>alert('post silindi');</script>");
                        Response.Redirect(Request.RawUrl); // Sayfayı yenile
                    }
                    else
                    {
                        //  başarısız
                        Response.Write("<script>alert('silme işlemi başarlı değil');</script>");
                    }
                }
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
                                        <img src='{reader["profilePic"]}'>
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
                                    <div runat=server onclick='postClicked({reader["id"]})' id='commentsButton'>
                                        <img src='./photos/comment.png' alt='Button Image'>
                                    </div>

                                    <div runat=server  id='commentsButton'>
                                        <img src='./photos/delete.png' alt='Button Image'>
                                    </div>
<asp:Button ID='btnDeletePost' runat='server' Text='Delete Post' 
    onclick='postDeleteClicked(this)' 
    CssClass='delete-button' 
    data-post-id='{reader["id"]}' />
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

        protected void Button_image_Upload(object sender, EventArgs e)
        {
             StartUpLoad();
        }

        private string StartUpLoad()
        {
            //get the file name of the posted image  
            string imgName = FileUpload1.FileName;
            //sets the image path  
            string imgPath = "ImageStorage/" + "img"+DateTime.Now.Second.ToString() + imgName ;
            //get the size in bytes that  

            int imgSize = FileUpload1.PostedFile.ContentLength;

            //validates the posted file before saving  
            if (FileUpload1.PostedFile != null && FileUpload1.PostedFile.FileName != "")
            {
                // 10240 KB means 10MB, You can change the value based on your requirement  
                if (FileUpload1.PostedFile.ContentLength > 10000240)
                {
                    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('File is too big.')", true);
                }
                else
                {
                
                        //try
                        //{
                        //    string filename = Path.GetFileName(FileUpload1.FileName);
                        //FileUpload1.SaveAs(Server.MapPath("~/") + filename);
                           
                        //}
                        //catch (Exception ex)
                        //{
                        //    Console.WriteLine("Upload status: The file could not be uploaded. The following error occured: " + ex.Message);
                        //}
                   
                    //then save it to the Folder  
                    FileUpload1.SaveAs(Server.MapPath(imgPath));
                   
                    currentProfilePicture.ImageUrl = imgPath;
                }
            }
            return imgPath;

        }

        

    }
}