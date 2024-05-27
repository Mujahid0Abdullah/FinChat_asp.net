<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="autherProfile.aspx.cs" Inherits="WebApplication3.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div id="profile-info" style="border-radius: 15px">
        <div class="user-avatar-big" style="margin: 40px; display: inline-block; vertical-align: middle">
            <div class="user-avatar" style="height: 100%; width: 100%;" runat="server" onserverclick="Button_image_Upload">
                <asp:Image ID="Image1" runat="server" ImageUrl="" />

            </div>


        </div>
        <div class="user-info" style="padding-left: 9px;padding-right: 14px; display: inline-block; vertical-align: middle; height: 150px">
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

        <asp:Button runat="server" OnClick="FollowButton_Click" ID="FollowButton" CssClass="follow-unfollowButton" Style="display: block"></asp:Button>



    </div>


    <div id="middle-region">
        <div id="postsList" runat="server">
        </div>
    </div>

</asp:Content>
