<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="anasayfa.aspx.cs" Inherits="WebApplication3.anasayfa" %>


<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <title>Anasayfa</title>
    <link rel="stylesheet" type="text/css" href="style.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.29.1/moment.min.js"></script>
</head>
<body>
    <div id="container">
        <div id="left-region">
            <asp:Button ID="ProfileButton" runat="server" Text=""  CssClass="profile-button" ClientOnClick="OpenProfilePage()" />
            <div id="Div1" runat="server" onmouseover="openPopup()" onmouseleave="closePopupWithDelay()" onclick="togglePopup(event)">
                <span></span>
                <span></span>
                <span></span>
            </div>
            <div class="user-avatar-big" style="margin:40px;">
                <div class="user-avatar" style="height: 100%; width: 100%;">
                    <asp:Image ID="Image1" runat="server" ImageUrl="" />
                </div>
            </div>
            <div id="userInfo2" runat="server" Text=""></div>
            <%--<asp:PopupExtender ID="popupExtender" runat="server" TargetControlID="clickable-lines" PopupControlID="popup">
                <asp:Trigger OnMouseOver="true" />
            </asp:PopupExtender>--%>
            <div id="popup" runat="server" onmouseover="cancelClosePopup()" onmouseleave="closePopupWithDelay()" onclick="closePopup(event)">
                <asp:LinkButton ID="editProfileLink" runat="server" Text="Edit Profile" ClientOnClick="OpenDynamicPopup" CssClass="popup-content" />
                <asp:LinkButton ID="myProfileLink" runat="server" Text="My Profile" ClientOnClick="OpenProfilePage" CssClass="popup-content" />
                <asp:LinkButton ID="logoutLink" runat="server" Text="Log Out" ClientOnClick="LogoutUser" CssClass="popup-content" />
            </div>
            <label for="search" runat="server" Text="Search People" Font-Size="25px" Font-Bold="true" Style="text-align: center; display: block; margin: 0 auto;">
            </label>
            <div style="text-align: center;">
                <asp:TextBox ID="search" runat="server" TextMode="Search" CssClass="search-box" placeholder="Search..." />
            </div>
            <div id="usersList" runat="server" class="view-profiles-container">

            </div>
        </div>

        <div id="middle-region">
            <div class="horizontal-middle-line">
                <span style="font-size: 36px;">
                    <asp:Label ID="finChatTitle" runat="server" Text="FinChat" />
                </span>
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
                            <asp:Button ID="Button1" runat="server" Text="" CssClass="send-button" ClientOnClick="PostMessage" />
                        </div>
                    </div>
                </div>
            </form>
            <div id="postsList" runat="server">

            </div>
        </div>

        <div id="right-region">
            <div class="horizontal-line">
                <img id="Img1" runat="server" src="dollar-icon.gif" alt="Dollar Icon" />
            </div>
            <asp:Label ID="Label1" runat="server" Text="" CssClass="para-birimleri" />
            <asp:Label ID="Label2" runat="server" Text="" CssClass="para-birimleri" />
            <asp:Label ID="Label3" runat="server" Text="" CssClass="para-birimleri" />
            <asp:Label ID="Label4" runat="server" Text="" CssClass="para-birimleri" />
            <div class="horizontal-line">
                <img id="Img2" runat="server" src="gold-icon.gif" alt="Dollar Icon" />
            </div>
            <asp:Label ID="Label5" runat="server" Text="" CssClass="altin-degerleri" />
            <asp:Label ID="Caltin" runat="server" Text="" CssClass="altin-degerleri" />
            <asp:Label ID="Label6" runat="server" Text="" CssClass="altin-degerleri" />
            <asp:Label ID="gumus" runat="server" Text="" CssClass="altin-degerleri" />
            <div class="horizontal-line">
                <img id="Img3" runat="server" src="kripto-icon.gif" alt="Dollar Icon" />
            </div>
            <asp:Label ID="bitcoin" runat="server" Text="" CssClass="kripto-degerleri" />
            <asp:Label ID="Ethereum" runat="server" Text="" CssClass="kripto-degerleri" />
            <asp:Label ID="Litecoin" runat="server" Text="" CssClass="kripto-degerleri" />
            <asp:Label ID="Dogecoin" runat="server" Text="" CssClass="kripto-degerleri" />
            <div class="horizontal-line"></div>
        </div>
    </div>

    <div id="Div3" runat="server">
        <form ID="Form1" runat="server" DefaultButton="save-btn">
            <div id="Div4" runat="server" onclick="closeDynamicPopup()">X</div>
            <div class="user-avatar-big" onclick="openProfilePictureInput()">
                <div class="user-avatar" style="height: 100%; width: 100%;">
                    <asp:Image ID="currentProfilePicture" runat="server" ImageUrl="" />
                </div>
            </div>
            <asp:FileUpload ID="profileImage" runat="server" AllowMultiple="false" Accept="image/*" OnChange="handleProfilePictureChange" />
            <br />
            <asp:TextBox ID="firstnameLastname" runat="server" Text="" CssClass="input-field" placeholder="First Name Last Name" />
            <br />
            <asp:TextBox ID="password" runat="server" Text="" CssClass="input-field" TextMode="Password" placeholder="Password" />
            <br />
            <asp:TextBox ID="repeatPassword" runat="server" Text="" CssClass="input-field" TextMode="Password" placeholder="Repeat Password" />
            <br />
            <asp:Button ID="Button2" runat="server" Text="Save" CssClass="save-button" ClientOnClick="UpdateUser" />
        </form>
    </div>
    <script src="./popups.js"></script>
</body>
</html>
