﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebApplication3.Register" %>

<!DOCTYPE html>
<html lang="tr">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.8">
    <title>Register Page</title>
    <style>
        body {
            display: flex;
            align-items: center;
            justify-content: center;
            height: 66.67vh;
            margin: 0;
            background-color: #f2f2f2;
        }

        .login-container {
            margin-top: 100px;
            text-align: center;
        }

        .login-title,
        .username-input,
        .password-input {
            font-family: 'Calibri', sans-serif;
            font-size: 38.4px;
            color: #000000;
            margin-bottom: 1.2px;
        }

        .username-input input,
        .password-input input {
            padding: 18px;
            border: 1.2px solid #000000;
            border-radius: 36px;
            background-color: #bfbfbf;
            width: 300px;
            box-sizing: border-box;
            text-align: center;
            margin-bottom: 1.2px;
            transition: background-color 0.3s;
        }

        .username-input input:focus,
        .password-input input:focus,
        .username-input input:hover,
        .password-input input:hover {
            background-color: #d9d9d9;
            border: 2px solid #000000;
        }

        .login-button {
            padding: 24px 36px;
            border: 1.2px solid #000000;
            border-radius: 36px;
            background-color: #9CC2E6;
            color: #000000;
            width: 360px;
            box-sizing: border-box;
            cursor: pointer;
            margin-top: 36px;
            transition: background-color 0.3s;
            font-size: 24px;
            font-weight: bold;
        }

        .login-button:hover {
            background-color: #6b96c7;
        }

        .register-link {
            color: #000080;
            font-size: 14px;
            margin-top: 10px;
        }

        .register-link a {
            color: #000080;
            text-decoration: none;
            font-weight: bold;
            display: block;
            margin-top: 5px;
        }
    </style>
</head>
<body>
    <form id="loginForm" runat="server" onsubmit="submitLogin">
        <div class="login-container">
            <div class="login-title">
                <h2>Login</h2>
            </div>
            <div class="username-input">
                <asp:TextBox ID="Username" runat="server" Placeholder="Username" Required="true"></asp:TextBox>
            </div>
            <div class="password-input">
                <asp:TextBox ID="Password" runat="server" TextMode="Password" Placeholder="Password" Required="true"></asp:TextBox>
            </div>
            <div class="username-input">
    <asp:TextBox ID="Email" runat="server" Placeholder="Email" Required="true"></asp:TextBox>
</div>
<div class="username-input">
    <asp:TextBox ID="Name" runat="server"  Placeholder="Name" Required="true"></asp:TextBox>
</div>
            <asp:Button ID="loginButton" runat="server" Text="Kayıt Ol" CssClass="login-button" OnClick="submitLogin_Click" />
            <p class="register-link">Already have an account?  <a href="./login">Login!</a>
            </p>
        </div>
    </form>
</body>
</html>


