using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null)
            {
                // Kullanıcı oturum açmamışsa, örneğin giriş sayfasına yönlendirilebilir
                Response.Redirect("login.aspx");
            }

        }

        protected void gg(object sender, EventArgs e)
        {
            Label1.Text = Session["id"].ToString();
            Label2.Text = "lebal2 HERE DDFDSFDSFDSF";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Label1.Text= Session["id"].ToString();
            Label2.Text = "lebal2 HERE DDFDSFDSFDSF";

        }
    }
}