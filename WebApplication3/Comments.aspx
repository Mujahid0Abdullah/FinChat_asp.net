<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Comments.aspx.cs" Inherits="WebApplication3.Comments" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="StyleSheet1.css" type="text/css" />
    <script type="text/javascript" src="anasayfa.js"></script>
    <title>POST</title>
</head>
<body>
   
     <%--<input type="button" id="rbutton31" style='background-image: url("/user.png")' cssclass="profile-button"  onclientclick="return postClicked(12)" />--%>
    <div id="container">
     <div id="left-region">
           <div class="user-avatar-big" style="margin:40px;">
      <div class="user-avatar" style="height: 100%; width: 100%;">
          <asp:Image ID="Image1" runat="server" ImageUrl="" />
      </div>
      <asp:Label id="userInfo2" runat="server" Text=""></asp:Label>

  </div>

   

         </div>
         <div id="middle-region" >
    <div class="horizontal-middle-line">
        <span style="font-size: 36px;">
            <div id="finChatTitle">FinChat</div>
        </span>
    </div>

    <div id="postsList" runat="server"></div>
 <form ID="postForm" runat="server" DefaultButton="sendButton">
    <div class="share-comment">
        <div class="left-column">
            <div class="user-avatar-small">
                <div class="user-avatar" style="height: 100%; width: 100%;">
                  <asp:Image ID="Image2" runat="server" ImageUrl="" />
                </div>
            </div>
            <div id="userInfo" class="user-name" runat="server"></div>
        </div>
        <div class="right-column">
            <div class="upper-row">
                <asp:TextBox ID="desc" runat="server" TextMode="MultiLine" Rows="5" Columns="50" placeholder="Your text here..."></asp:TextBox>
            </div>

            <div class="lower-row">
                <asp:Button ID="sendButton" runat="server" Text="" style="background-image: url('./photos/send.png'); background-repeat: round" OnClick="sendButton_Click" UseSubmitBehavior="false" />
            </div>
        </div>
    </div>
     </form>
    <div id="commentsList" runat="server"></div>
</div>


          <div id="right-region">

              </div>

        </div>
</body>
</html>

