<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Comments.aspx.cs" Inherits="WebApplication3.Comments" MasterPageFile="~/Site.Master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head"
runat="server">

     <link rel="stylesheet" href="StyleSheet1.css" type="text/css" />
 <script type="text/javascript" src="anasayfa.js"></script>
 <title>POST</title>



</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent"
runat="server">

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
   

    <div id="postsList" runat="server"></div>
 <div ID="postForm" runat="server" >
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
                <asp:Button ID="sendButton" runat="server" Text="" style="height: 30px;
width: 30px;
padding: 0;
/* Padding'i sıfırlayarak butonun boyutlarına tam olarak sığmasını sağlıyoruz */
border: none;
/* Kenarlık olmasını istemiyorsak sınırı kaldırabiliriz */
background: url(./photos/send.png);
background-repeat: round;
background-size: auto;
/* Arkaplan rengini kaldırıyoruz */
cursor: pointer; background-repeat: round" OnClick="sendButton_Click" UseSubmitBehavior="false" />
            </div>
        </div>
    </div>
     </div>
    <div id="commentsList" runat="server"></div>
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




