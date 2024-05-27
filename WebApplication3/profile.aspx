<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="profile.aspx.cs" Inherits="WebApplication3.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        function openDynamicPopup() {
            console.log("Opening dynamic popup");
            var dynamicPopup = document.getElementById("dynamic-popup");
            dynamicPopup.style.display = "block";
        }

        function closeDynamicPopup() {
            var dynamicPopup = document.getElementById("dynamic-popup");
            dynamicPopup.style.display = "none";
        }
    </script>

    <div id="dynamic-popup">
        <div id="update-user-form">
            <div id="close-btn" onclick="closeDynamicPopup()">X</div>
            <div class="user-avatar-big" style="    margin-left: auto;
    margin-right: auto;">
                <div class="user-avatar"  style="height: 100%; width: 100%;">
                <asp:Image id="currentProfilePicture" runat="server"  alt=""/>
                </div>
                <div id="editProfilePicture"></div>
            </div>
             <asp:FileUpload id="FileUpload1" style="display:block ;    margin-left: auto;
    margin-right: auto;" text="" type="file" runat="server" />
            <br>
            <asp:TextBox runat="server"  id="firstnameLastname" placeholder="First Name Last Name" Style="margin-bottom:4px"> </asp:TextBox>
            <br>
            <asp:TextBox runat="server"  type="password" id="password" placeholder="Password" Style="margin-bottom:4px"></asp:TextBox>
            <br>
            <asp:TextBox type="password" runat="server"  id="repeatPassword" placeholder="Repeat Password" Style="margin-bottom:4px"></asp:TextBox>
            <br>
            
            <input type="button" ID="Button1" cssClass="save-btn" runat="server"  onserverclick="ButtonUpdate_Click"  value="SAVE" style="  background-color: #9CC2E6;
  color: white;
  padding: 10px 20px;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  border: 2px;"/>

        </div>
    </div>


         <div id="profile-info" style=" border-radius:15px  " >
<div class="user-avatar-big" style="margin:40px; display:inline-block ;  vertical-align:middle">
      <div class="user-avatar" style="height: 100%; width: 100%;" runat="server" >
          <asp:Image ID="Image1" runat="server" ImageUrl=""  />
          
      </div>
      
              
  </div>
            <div class="user-info" style="padding-left: 9px; display: inline-block; vertical-align: middle; height: 150px">
     <asp:Label ID="Label1" runat="server" CssClass="label-user-info" Style="display: block; font-family: 'Bahnschrift Condensed'; font-weight: 700" Text="ADI "></asp:Label>

     <asp:Label ID="userInfo2" runat="server" Style="display: block" Text=" "></asp:Label>
     <asp:Label ID="Label2" CssClass="label-user-info" runat="server" Style="display: block; font-family: 'Bahnschrift Condensed'; font-weight: 700" Text="EMAIL "></asp:Label>

     <asp:Label ID="userEmail" runat="server" Text=""></asp:Label>
 </div>
         

        <div style="padding-left: 9px; display: inline-block; vertical-align: middle; height: 150px;min-width:200px">
            <label id="follower"
                style="  font-size: 18px;
  font-family: 'Bahnschrift Condensed'; font-weight: bold; text-align: center; display: block; margin: 0 auto;">Takip Edilen
            </label>
            <div runat="server" id="followerList" class="You-Follow-Follows-You-container" style="padding-left: 9px; display: inline-block; vertical-align: middle; height: 150px">
            </div>

        </div>




        <div style="padding-left: 9px; display: inline-block; vertical-align: middle; height: 150px; min-width:200px">
            <label id="followed"
                style="  font-size: 18px;
  font-family: 'Bahnschrift Condensed'; font-weight: bold; text-align: center; display: block; margin: 0 auto;">Takipçi
            </label>
            <div runat="server" id="followedList" class="You-Follow-Follows-You-container" style="padding-left: 9px; display: inline-block; vertical-align: middle; height: 150px">
            </div>

        </div>


        <br />

         <%-- ADD TO PROFILE PAGE  --%>
               <%--<form ID="FileUp"  >--%>
    
       <%-- <asp:FileUpload ID="FileUpload1" runat="server" />  --%>
<%--<input type="button" ID="Buttonimage2" runat="server" Text="Upload" onserverclick="Button_image_Upload"  value="upload"/>--%>
              <div class="popup-content" onclick="openDynamicPopup()">Edit Profile</div>
<br />  
<%--<asp:Image ID="Image3" runat="server" />--%>  
                   <%--</form>--%>
  </div>

     <div id="middle-region">
     <div id="postsList" runat="server">
         </div>
         </div>
</asp:Content>
