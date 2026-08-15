<%@ Page Language="VB"
    MasterPageFile="~/Site.master"
    AutoEventWireup="false"
    CodeFile="Register.aspx.vb"
    Inherits="Register" %>

<asp:Content
    ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <style type="text/css">

        .register-container {
            width: 100%;
            min-height: calc(100vh - 65px);
            display: flex;
            justify-content: center;
            align-items: center;
            padding: 40px 20px;
            box-sizing: border-box;
        }

        .register-box {
            width: 100%;
            max-width: 430px;
            background-color: white;
            padding: 35px;
            border-radius: 10px;
            box-shadow: 0 3px 15px rgba(0,0,0,0.12);
            box-sizing: border-box;
        }

        .register-title {
            margin: 0 0 8px 0;
            text-align: center;
            color: #17365d;
            font-size: 28px;
        }

        .register-subtitle {
            margin: 0 0 30px 0;
            text-align: center;
            color: #777;
            font-size: 14px;
        }

        .register-group {
            margin-bottom: 18px;
        }

        .register-label {
            display: block;
            margin-bottom: 7px;
            color: #333;
            font-weight: bold;
            font-size: 14px;
        }

        .register-input {
            width: 100%;
            height: 44px;
            padding: 10px 12px;
            border: 1px solid #bbb;
            border-radius: 5px;
            font-size: 14px;
            background-color: white;
            box-sizing: border-box;
        }

        .register-input:focus {
            outline: none;
            border-color: #0d6efd;
            box-shadow: 0 0 4px rgba(13,110,253,0.25);
        }

        .register-button {
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

        .register-button:hover {
            background-color: #0b5ed7;
        }

        .register-message {
            display: block;
            margin-top: 20px;
            padding: 12px;
            border-radius: 5px;
            text-align: center;
        }

        .login-link {
            display: block;
            margin-top: 18px;
            text-align: center;
            color: #0d6efd;
            text-decoration: none;
        }

        .login-link:hover {
            text-decoration: underline;
        }

        @media screen and (max-width: 600px) {

            .register-container {
                padding: 25px 15px;
            }

            .register-box {
                padding: 25px 20px;
            }

            .register-title {
                font-size: 24px;
            }

        }

    </style>


    <div class="register-container">

        <div class="register-box">

            <h2 class="register-title">
                Create Account
            </h2>

            <p class="register-subtitle">
                Create your Investor Management System account
            </p>


            <!-- NAME -->

            <div class="register-group">

                <asp:Label
                    ID="lblName"
                    runat="server"
                    Text="Name"
                    CssClass="register-label">
                </asp:Label>

                <asp:TextBox
                    ID="txtName"
                    runat="server"
                    CssClass="register-input"
                    MaxLength="150"
                    placeholder="Enter your full name">
                </asp:TextBox>

            </div>


            <!-- EMAIL -->

            <div class="register-group">

                <asp:Label
                    ID="lblEmail"
                    runat="server"
                    Text="Email"
                    CssClass="register-label">
                </asp:Label>

                <asp:TextBox
                    ID="txtEmail"
                    runat="server"
                    CssClass="register-input"
                    MaxLength="150"
                    TextMode="Email"
                    placeholder="Enter your email address">
                </asp:TextBox>

            </div>


            <!-- MOBILE -->

            <div class="register-group">

                <asp:Label
                    ID="lblMobile"
                    runat="server"
                    Text="Mobile"
                    CssClass="register-label">
                </asp:Label>

                <asp:TextBox
                    ID="txtMobile"
                    runat="server"
                    CssClass="register-input"
                    MaxLength="15"
                    placeholder="Enter your mobile number">
                </asp:TextBox>

            </div>


            <!-- PASSWORD -->

            <div class="register-group">

                <asp:Label
                    ID="lblPassword"
                    runat="server"
                    Text="Password"
                    CssClass="register-label">
                </asp:Label>

                <asp:TextBox
                    ID="txtPassword"
                    runat="server"
                    CssClass="register-input"
                    TextMode="Password"
                    MaxLength="100"
                    placeholder="Enter your password">
                </asp:TextBox>

            </div>


            <!-- CONFIRM PASSWORD -->

            <div class="register-group">

                <asp:Label
                    ID="lblConfirmPassword"
                    runat="server"
                    Text="Confirm Password"
                    CssClass="register-label">
                </asp:Label>

                <asp:TextBox
                    ID="txtConfirmPassword"
                    runat="server"
                    CssClass="register-input"
                    TextMode="Password"
                    MaxLength="100"
                    placeholder="Confirm your password">
                </asp:TextBox>

            </div>


            <!-- CREATE ACCOUNT -->

            <asp:Button
                ID="btnRegister"
                runat="server"
                Text="Create Account"
                CssClass="register-button">
            </asp:Button>


            <!-- MESSAGE -->

            <asp:Label
                ID="lblMessage"
                runat="server"
                Visible="False"
                CssClass="register-message">
            </asp:Label>


            <!-- LOGIN LINK -->

            <asp:HyperLink
                ID="lnkLogin"
                runat="server"
                NavigateUrl="~/Login.aspx"
                CssClass="login-link">

                Already have an account? Login

            </asp:HyperLink>

        </div>

    </div>

</asp:Content>