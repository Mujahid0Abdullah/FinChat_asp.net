<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="post.aspx.cs" Inherits="WebApplication3.post" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head"
runat="server">
     <link rel="stylesheet" type="text/css" href="StyleSheet1.css?t=<%= DateTime.Now.Ticks %>" media="screen" />
 <script type="text/javascript" src="anasayfa.js"></script>

    <style>
        container body-content{
            margin:0 !important;
        }
    </style>

</asp:Content>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%--<style>
        
.card {
    position: relative;
    width: 600px;
    min-height: 450px;
    box-shadow: 5px 5px 5px rgba(0, 0, 0, 0.15),
        -5px -5px 5px rgba(0, 0, 0, 0.15);
    padding: 20px;
}
.card .top {
    display: flex;
    justify-content: space-between;
    align-items: center;
}
.card .top .user_details {
    display: flex;
    align-items: center;
}
.card .top .user_details .profile_img {
    position: relative;
    width: 40px;
    height: 40px;
    border-radius: 50%;
    overflow: hidden;
    margin-right: 8px;
}
.cover {
    position: absolute;
    top: 0;
    right: 0;
    width: 100%;
    height: 100%;
    object-fit: cover;
    object-position: center;
    cursor: pointer;
}
.card .top .user_details h3 {
    font-size: 18px;
    color: black;
    font-weight: 900;
    line-height: 1em;
    cursor: pointer;
}
.hour {
    color: gray;
    font-size: 15px;
    font-weight: 500;
}
.globDot {
    position: absolute;
    margin-left: 5px;
    margin-top: -4px;
    font-size: 20px;
    align-items: center;
    color: #0000004b;
}
ul {
    position: relative;
    width: 25px;
    height: 25px;
    border-radius: 50%;
    display: flex;
    justify-content: center;
    align-items: center;
}
ul li {
    position: absolute;
    top: 0;
    right: 0;
    width: 100%;
    height: 100%;
    border-radius: 50%;
    margin-top: 10px;
    margin-right: 48px;
    list-style: none;
    text-align: center;
}
ul li img {
    position: absolute;
    top: 0;
    right: 0;
    width: 100%;
    height: 100%;
    border-radius: 50%;
    margin-top: -1.5px;
}
.dot {
    transform: rotate(90deg) scale(0.6);
    cursor: pointer;
    margin-top: -16px;
}
.message{
    font-weight: 400;
    margin-top: 5px;
    color: #777;
    line-height: 1.5em;
}
.imgBg{
    position: relative;
    width: 600px;
    height: 420px;
    margin: 10px 0 15px;
    margin-left: -20px;
    overflow: hidden;
}
.coverFull{
    position: absolute;
    top: 0;
    right: 0;
    width: 100%;
    height: 100%;
    object-fit: cover;
    object-position: center;
    cursor: pointer;
}
.btns{
    display: flex;
    align-items: center;
    justify-content: space-between;
}
.btns img{
    max-width: 30px;
    cursor: pointer;
}
.btns .left img{
    margin-right: 8px;
}
.likes{
    margin-top: -28px;
    margin-left: 33px;
    font-size: 17px;
    color: #777;
    text-align: center;
    font-weight: 500;
}
.right h4{
    margin-top: 5px;
    margin-left: 33px;
    font-size: 17px;
    color: #777;
    text-align: center;
    font-weight: 500;
}
.border{
    position: relative;
    width: 100%;
    height: 0.5px;
    background: #0000004b;
    margin-top: 15px;
}
.icon{
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin: 5px 0 5px 0;
}
.icon img{
    max-width: 30px;
    cursor: pointer;
}
.like{
    display: flex;
    justify-content: space-around;
    align-items: center;
    width: 100%;
}
.like img{
    position: relative;
    max-width: 70px;
    cursor: pointer;
    margin: 0 40px;
    margin-top: -15px;
}
.like img:nth-child(1){
    width: 55px;
    margin-right: 20px;
}
.like img:nth-child(2){
    width: 33px;
    margin-right: 15px;
}
.border_bott{
    position: relative;
    width: 100%;
    height: 0.5px;
    background: #0000004b;
    margin-top: -15px;
}
.addComments{
    display: flex;
    align-items: center;
    margin-top: 20px;
}
.addComments .userimg{
    position: relative;
    min-width: 40px;
    height: 40px;
    border-radius: 50%;
    overflow: hidden;
    margin-right: 8px;
}
.text{
    background: #F0F2F5;
    width: 100%;
    height: 40px;
    border: none;
    outline: none;
    font-weight: 400;
    font-size: 16px;
    color: #262626;
    border-radius: 20px;
}
input[type="text"]{
    position: relative;
    padding: 0 25px;
}
@media (max-width: 640px){
    .card{
        width: 350px;
    }
    .imgBg{
        width: 350px;
    }
}
    </style>--%>

        <script type="text/javascript">
            function postClicked(postId) {


                window.location = `./Comments?postId=${postId}`
            }
            function openHisProfilePage(userId) {


                window.location = `./autherProfile?userId=${userId}`
            }

        </script>
     <%--<input type="button" id="rbutton31" style='background-image: url("/user.png")' cssclass="profile-button"  onclientclick="return postClicked(12)" />--%>
    <div id="container">



     <div id="left-region">
           <div class="user-avatar-big" style="margin:40px;">
      <div class="user-avatar" style="height: 100%; width: 100%;">
          <asp:Image ID="Image1" runat="server" ImageUrl="" />
      </div>
      
                   <asp:Label id="userInfo2" Style="color: #333;
font-weight: bold;
position: relative;
margin: auto;" runat="server" Text=""></asp:Label>
  </div>
         
         <%-- ADD TO PROFILE PAGE  --%>
               <%--<form ID="FileUp"  >--%>
     <%--<input id="FileUpload12" type="file" runat="server" NAME="FileUpload12"/>--%>
       <%-- <asp:FileUpload ID="FileUpload1" runat="server" />  --%>
<%--<input type="button" ID="Buttonimage2" runat="server" Text="Upload" onclick="Button_image_Upload"/>--%>  
<br />  
<%--<asp:Image ID="Image3" runat="server" />--%>  
                   <%--</form>--%>
  </div>
          <div id="middle-region">

               <div ID="postForm" runat="server" DefaultButton="sendButton">
     <div class="share-post">
         <div class="left-column">
             <div class="user-avatar-big">
                 <div class="user-avatar" style="height: 100%; width: 100%;">
                     <asp:ImageButton ID="Image2" runat="server" ImageUrl="" OnClick="openProfile" />
                 </div>
             </div>
             <div id="Div2" runat="server" class="user-name"></div>
         </div>
         <div class="right-column">
             <div class="upper-row">
                 <asp:TextBox ID="desc" runat="server" TextMode="MultiLine" Rows="5" Columns="50" placeholder="Paylaş..." CssClass="post-description"  />
             </div>
             <div class="lower-row">
                 <%--<asp:Button ID="sendButton" runat="server" Text="" CssClass="--%>
                 <asp:Button ID="sendButton"  runat="server" Text="Paylaş" CssClass="send-button" OnClick="PostMessage" />
               
             </div>
         </div>
     </div>





 </div>

               <div id="postsList" runat="server">

 </div>
              </div>

          <div id="right-region">
              <div class="horizontal-line">
                <img id="dollar-icon"
                    src="https://fin-chat.onrender.com/static/dollar-icon.gif"
                    alt="Dollar Icon">
            </div>

            <h3 class="para-birimleri" id="dolar-try"></h3>
            <h3 class="para-birimleri" id="euro-try"></h3>
            <h3 class="para-birimleri" id="sterlin-try"></h3>
            <h3 class="para-birimleri" id="pln-try"></h3>
            <div class="horizontal-line">
                <img id="gold-icon"
                    src="https://fin-chat.onrender.com/static/gold-icon.gif"
                    alt="Dollar Icon">
            </div>
            <h3 id="22altin" class="altin-degerleri"></h3>
            <h3 id="Caltin" class="altin-degerleri"></h3>
            <h3 id="cumhuriyet-altini" class="altin-degerleri"></h3>
            <h3 id="gumus" class="altin-degerleri"></h3>
            <div class="horizontal-line">
                <img id="kripto-icon"
                    src="https://fin-chat.onrender.com/static/kripto-icon.gif"
                    alt="Dollar Icon">
            </div> 
            <h3 id="bitcoin" class="kripto-degerleri"></h3>
            <h3 id="Ethereum" class="kripto-degerleri"></h3>
            <h3 id="Litecoin" class="kripto-degerleri"></h3>
            <h3 id="Dogecoin" class="kripto-degerleri"></h3>
            <div class="horizontal-line"></div>


              </div>

        </div>
      <script type="text/javascript">
       
          gitCurrency();
          getExchangeRates();
          function getExchangeRates() {
              console.log('İstek başarılı bir şekilde atıldı');

              // JSON verisini çekelim
              fetch('https://finans.truncgil.com/v3/today.json')
                  .then(response => response.json())
                  .then(data => {
                      console.log(data);
                      console.log(data["14-ayar-altin"].Buying);
                      console.log(data["22-ayar-bilezik"].Buying);
                      document.getElementById('dolar-try').textContent = "USD To TR: " + data["USD"].Buying;
                      document.getElementById('euro-try').textContent = "EUR To TR: " + data["EUR"].Buying;
                      document.getElementById('sterlin-try').textContent = "GBP To TR: " + data["GBP"].Buying;
                      document.getElementById('pln-try').textContent = "PLN To TR : " + data["PLN"].Buying;

                      document.getElementById('22altin').textContent = "22 Ayar Altın: " + data["22-ayar-bilezik"].Buying;

                      document.getElementById('Caltin').textContent = "Çeyrek Altın: " + data["ceyrek-altin"].Buying;
                      document.getElementById('gumus').textContent = "Gümüş: " + data["gumus"].Buying;
                      document.getElementById('cumhuriyet-altini').textContent = "C. Altını: " + data["cumhuriyet-altini"].Buying;





                      // Bir sonraki isteği 10 saniye sonra atalım

                  })
                  .catch(error => console.error('Veri alınamadı:', error));
          }


          function gitCurrency() {



              // CoinGecko API'sinden veri çekme
              fetch('https://api.coingecko.com/api/v3/simple/price?ids=bitcoin,ethereum,litecoin,ripple,dogecoin&vs_currencies=try')
                  .then(response => {
                      if (!response.ok) {
                          throw new Error('Network response was not ok');
                      }
                      return response.json();
                  })
                  .then(data => {

                      console.log(data.bitcoin.try)
                      // Kripto paraların fiyatlarını alm
                      document.getElementById('bitcoin').textContent = "Bitcoin: " + data.bitcoin.try;
                      document.getElementById('Ethereum').textContent = "Ethereum: " + data.ethereum.try;
                      document.getElementById('Litecoin').textContent = "Litecoin: " + data.litecoin.try;
                      document.getElementById('Dogecoin').textContent = "Dogecoin: " + data.dogecoin.try;

                      // İstek başarılı olduğunda konsola mesaj yazdırma
                      console.log('İstek başarıyla yapıldı.');

                      // İstek sayısını artırma

                  })
                  .catch(error => {
                      console.error('API çağrısı sırasında bir hata oluştu:', error);
                  });
          }
      </script>

</asp:Content>


