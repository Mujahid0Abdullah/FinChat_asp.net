<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="post.aspx.cs" Inherits="WebApplication3.post" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="StyleSheet1.css" type="text/css" />
    <script type="text/javascript" src="anasayfa.js"></script>
    <title></title>
</head>
<body>
    <script type="text/javascript">
        function postClicked(postId) {
            alert(postId)
            
            window.location = `./post?postId=${postId}`
        }
    </script>
     <input type="button" id="rbutton31" style='background-image: url("/user.png")' cssclass="profile-button"  onclientclick="return postClicked(12)" />
    <div id="container">
     <div id="left-region">
           <div class="user-avatar-big" style="margin:40px;">
      <div class="user-avatar" style="height: 100%; width: 100%;">
          <asp:Image ID="Image1" runat="server" ImageUrl="" />
      </div>
      <asp:Label id="userInfo2" runat="server" Text=""></asp:Label>

  </div>

   

         </div>
          <div id="middle-region">

               <div class="horizontal-middle-line">               
                    <div style="font-size: 36px;" id="finChatTitle">FinChat</div>   
            </div>


               <form ID="postForm" runat="server" DefaultButton="sendButton">
     <div class="share-post">
         <div class="left-column">
             <div class="user-avatar-big">
                 <div class="user-avatar" style="height: 100%; width: 100%;">
                     <asp:Image ID="Image2" runat="server" ImageUrl="" />
                 </div>
             </div>
             <div id="Div2" runat="server" class="user-name"></div>
         </div>
         <div class="right-column">
             <div class="upper-row">
                 <asp:TextBox ID="desc" runat="server" TextMode="MultiLine" Rows="5" Columns="50" placeholder="Paylaş..." CssClass="post-description" />
             </div>
             <div class="lower-row">
                 <%--<asp:Button ID="sendButton" runat="server" Text="" CssClass="--%>
                 <asp:Button ID="sendButton" runat="server" Text="" CssClass="send-button" OnClick="PostMessage" />
             </div>
         </div>
     </div>





 </form>

               <div id="postsList" runat="server">

 </div>
              </div>

          <div id="right-region">
              </div>

        </div>
</body>
</html>
