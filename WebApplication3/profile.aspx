<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="profile.aspx.cs" Inherits="WebApplication3.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

         <div id="profile-info" style="margin:0; border-radius:15px  " >
<div class="user-avatar-big" style="margin:40px; display:inline-block ;  vertical-align:middle">
      <div class="user-avatar" style="height: 100%; width: 100%;" runat="server" onserverclick="Button_image_Upload">
          <asp:Image ID="Image1" runat="server" ImageUrl=""  />
          
      </div>
      
              
  </div>
             <div class="user-info" style="display:inline-block ;  vertical-align:middle ; height:150px">       
                 <asp:Label id="userInfo2" runat="server" style="display: block"  Text="Adı: "></asp:Label>
                 <asp:Label id="userEmail" runat="server"  Text="Adı: "></asp:Label>
<h3 runat="server" id="userName"> </h3>

             </div>
            
         <%-- ADD TO PROFILE PAGE  --%>
               <%--<form ID="FileUp"  >--%>
     <asp:FileUpload id="FileUpload1" style="display:block " type="file" runat="server" />
       <%-- <asp:FileUpload ID="FileUpload1" runat="server" />  --%>
<input type="button" ID="Buttonimage2" runat="server" Text="Upload" onserverclick="Button_image_Upload"  value="upload"/>
<br />  
<%--<asp:Image ID="Image3" runat="server" />--%>  
                   <%--</form>--%>
  </div>

     <div id="middle-region">
     <div id="postsList" runat="server">
         </div>
         </div>
</asp:Content>
