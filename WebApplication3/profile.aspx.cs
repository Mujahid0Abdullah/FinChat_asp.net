﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
                        userInfo2.Text = "Adı: "+name;

                        string email = reader["email"].ToString();
                        userEmail.Text = "Email: " + email;

                        
                        
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

        protected void Button_image_Upload(object sender, EventArgs e)
        {
             StartUpLoad();
        }

        private void StartUpLoad()
        {
            //get the file name of the posted image  
            string imgName = FileUpload1.FileName;
            //sets the image path  
            string imgPath = "ImageStorage/" + imgName;
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
                    Image1.ImageUrl = "~/" + imgPath;
                    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('Image saved!')", true);
                }
            }

        }

        

    }
}