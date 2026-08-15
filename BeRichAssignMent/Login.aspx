<%@ Page Language="VB"
    MasterPageFile="~/Site.master"
    AutoEventWireup="false"
    CodeFile="Login.aspx.vb"
    Inherits="login" %>

<asp:Content
    ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <style type="text/css">

        .login-container {
            width: 100%;
            min-height: calc(100vh - 65px);

            display: flex;
            justify-content: center;
            align-items: center;

            padding: 40px 20px;
            box-sizing: border-box;
        }


        .login-box {
            width: 100%;
            max-width: 430px;

            background-color: white;

            padding: 35px;

            border-radius: 10px;

            box-shadow:
                0 3px 15px
                rgba(0,0,0,0.12);

            box-sizing: border-box;
        }


        .login-title {
            margin: 0 0 8px 0;

            text-align: center;

            color: #17365d;

            font-size: 28px;
        }


        .login-subtitle {
            margin: 0 0 30px 0;

            text-align: center;

            color: #777;

            font-size: 14px;
        }


        .login-group {
            margin-bottom: 20px;
        }


        .login-label {
            display: block;

            margin-bottom: 7px;

            color: #333;

            font-weight: bold;

            font-size: 14px;
        }


        .login-input {
            width: 100%;

            height: 44px;

            padding: 10px 12px;

            border: 1px solid #bbb;

            border-radius: 5px;

            font-size: 14px;

            background-color: white;

            box-sizing: border-box;
        }


        .login-input:focus {
            outline: none;

            border-color: #0d6efd;

            box-shadow:
                0 0 4px
                rgba(13,110,253,0.25);
        }


        .login-button {
            width: 100%;

            height: 44px;

            border: none;

            border-radius: 5px;

            background-color: #0d6efd;

            color: white;

            font-size: 15px;

            font-weight: bold;

            cursor: pointer;
        }


        .login-button:hover {
            background-color: #0b5ed7;
        }


        .login-message {
            display: block;

            margin-top: 20px;

            padding: 12px;

            border-radius: 5px;

            text-align: center;

            color: #842029;

            background-color: #f8d7da;

            border: 1px solid #f5c2c7;

            box-sizing: border-box;
        }


        .register-link {
            display: block;

            margin-top: 18px;

            text-align: center;

            color: #0d6efd;

            font-size: 14px;

            font-weight: bold;

            text-decoration: none;
        }


        .register-link:hover {
            text-decoration: underline;
        }


        @media screen and (max-width: 600px) {

            .login-container {
                padding: 25px 15px;
            }


            .login-box {
                padding: 25px 20px;
            }


            .login-title {
                font-size: 24px;
            }

        }

    </style>


    <!-- LOGIN CONTAINER -->

    <div class="login-container">

        <div class="login-box">


            <!-- TITLE -->

            <h2 class="login-title">
                Login
            </h2>


            <p class="login-subtitle">
                Login to Investor Management System
            </p>


            <!-- EMAIL -->

            <div class="login-group">

                <asp:Label
                    ID="lblEmail"
                    runat="server"
                    Text="Email"
                    CssClass="login-label">
                </asp:Label>


                <asp:TextBox
                    ID="txtEmail"
                    runat="server"
                    CssClass="login-input"
                    MaxLength="150"
                    TextMode="Email"
                    placeholder="Enter your email address">
                </asp:TextBox>

            </div>


            <!-- PASSWORD -->

            <div class="login-group">

                <asp:Label
                    ID="lblPassword"
                    runat="server"
                    Text="Password"
                    CssClass="login-label">
                </asp:Label>


                <asp:TextBox
                    ID="txtPassword"
                    runat="server"
                    CssClass="login-input"
                    TextMode="Password"
                    MaxLength="100"
                    placeholder="Enter your password">
                </asp:TextBox>

            </div>


            <!-- LOGIN BUTTON -->

            <asp:Button
                ID="btnLogin"
                runat="server"
                Text="Login"
                CssClass="login-button">
            </asp:Button>


            <!-- MESSAGE -->

            <asp:Label
                ID="lblMessage"
                runat="server"
                Visible="False"
                CssClass="login-message">
            </asp:Label>


            <!-- REGISTER -->

            <asp:HyperLink
                ID="lnkRegister"
                runat="server"
                NavigateUrl="~/Register.aspx"
                CssClass="register-link"
                Visible="True">

                Create your account / Register

            </asp:HyperLink>


        </div>

    </div>

</asp:Content>