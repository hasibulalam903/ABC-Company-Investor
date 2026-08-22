<%@ Page Language="VB"
    AutoEventWireup="false"
    CodeFile="login.aspx.vb"
    Inherits="login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <title>Login</title>

    <meta name="viewport"
          content="width=device-width, initial-scale=1" />

    <style>

        * {
            box-sizing: border-box;
        }

        body {
            margin: 0;
            padding: 0;

            font-family: Arial, Helvetica, sans-serif;

            background: #f4f7fb;
        }

        .login-page {
            min-height: 100vh;

            display: flex;

            justify-content: center;

            align-items: center;

            padding: 20px;
        }

        .login-card {
            width: 100%;
            max-width: 430px;

            background: #ffffff;

            padding: 35px;

            border-radius: 14px;

            box-shadow:
                0 8px 30px rgba(0, 0, 0, 0.10);
        }

        .login-title {
            margin: 0;

            text-align: center;

            color: #17365d;

            font-size: 30px;

            font-weight: 700;
        }

        .login-subtitle {
            text-align: center;

            color: #6b7280;

            margin-top: 8px;

            margin-bottom: 30px;

            font-size: 14px;
        }

        .form-group {
            margin-bottom: 20px;
        }

        .form-label {
            display: block;

            margin-bottom: 8px;

            color: #374151;

            font-size: 14px;

            font-weight: 600;
        }

        .form-control {
            width: 100%;

            height: 45px;

            padding: 10px 13px;

            border: 1px solid #d1d5db;

            border-radius: 8px;

            font-size: 15px;

            outline: none;
        }

        .form-control:focus {
            border-color: #2563eb;

            box-shadow:
                0 0 0 3px rgba(37, 99, 235, 0.10);
        }

        .login-type-title {
            display: block;

            margin-bottom: 10px;

            color: #374151;

            font-size: 14px;

            font-weight: 600;
        }

        .login-type {
            display: flex;

            gap: 25px;

            align-items: center;

            flex-wrap: wrap;
        }

        .login-type label {
            margin-left: 5px;

            color: #374151;

            font-size: 14px;

            cursor: pointer;
        }

        .login-button {
            width: 100%;

            height: 46px;

            border: none;

            border-radius: 8px;

            background: #2563eb;

            color: #ffffff;

            font-size: 15px;

            font-weight: 700;

            cursor: pointer;
        }

        .login-button:hover {
            background: #1d4ed8;
        }


        /* =========================================
           REGISTER BUTTONS
           ========================================= */

        .register-section {
            margin-top: 5px;

            display: flex;

            flex-direction: column;

            gap: 10px;
        }

        .register-button {
            display: block;

            width: 100%;

            padding: 11px 15px;

            border-radius: 8px;

            text-align: center;

            text-decoration: none;

            font-size: 14px;

            font-weight: 600;

            box-sizing: border-box;
        }

        .user-register {
            background: #2563eb;

            color: #ffffff;
        }

        .user-register:hover {
            background: #1d4ed8;
        }

        .investor-register {
            background: #198754;

            color: #ffffff;
        }

        .investor-register:hover {
            background: #157347;
        }


        /* =========================================
           FORGOT PASSWORD
           ========================================= */

        .forgot-link {
            display: block;

            text-align: center;

            margin-top: 18px;

            color: #2563eb;

            text-decoration: none;

            font-size: 14px;

            font-weight: 600;
        }

        .forgot-link:hover {
            text-decoration: underline;
        }


        /* =========================================
           MESSAGE
           ========================================= */

        .message {
            display: block;

            padding: 12px 14px;

            margin-bottom: 20px;

            border-radius: 8px;

            font-size: 14px;

            line-height: 1.5;
        }

        .success-message {
            color: #0f5132;

            background: #d1e7dd;

            border: 1px solid #badbcc;
        }

        .error-message {
            color: #842029;

            background: #f8d7da;

            border: 1px solid #f5c2c7;
        }


        /* =========================================
           NOTE
           ========================================= */

        .account-note {
            margin-top: 20px;

            padding: 12px;

            background: #f8f9fa;

            border-radius: 8px;

            color: #6b7280;

            font-size: 12px;

            line-height: 1.5;

            text-align: center;
        }


        /* =========================================
           MOBILE
           ========================================= */

        @media (max-width: 480px) {

            .login-card {
                padding: 25px 20px;
            }

            .login-title {
                font-size: 26px;
            }

            .login-type {
                gap: 15px;
            }

        }

    </style>

</head>


<body>

<form
    id="form1"
    runat="server">


    <div class="login-page">


        <div class="login-card">


            <!-- =====================================
                 TITLE
                 ===================================== -->

            <h1 class="login-title">
                Login
            </h1>


            <p class="login-subtitle">
                Select the account you want to access.
            </p>


            <!-- =====================================
                 MESSAGE
                 ===================================== -->

            <asp:Label
                ID="lblMessage"
                runat="server"
                Visible="False"
                CssClass="message">
            </asp:Label>


            <!-- =====================================
                 EMAIL
                 ===================================== -->

            <div class="form-group">

                <asp:Label
                    ID="lblEmail"
                    runat="server"
                    Text="Email"
                    AssociatedControlID="txtEmail"
                    CssClass="form-label">
                </asp:Label>


                <asp:TextBox
                    ID="txtEmail"
                    runat="server"
                    CssClass="form-control"
                    TextMode="Email"
                    MaxLength="150"
                    placeholder="Enter your email">
                </asp:TextBox>

            </div>


            <!-- =====================================
                 PASSWORD
                 ===================================== -->

            <div class="form-group">

                <asp:Label
                    ID="lblPassword"
                    runat="server"
                    Text="Password"
                    AssociatedControlID="txtPassword"
                    CssClass="form-label">
                </asp:Label>


                <asp:TextBox
                    ID="txtPassword"
                    runat="server"
                    CssClass="form-control"
                    TextMode="Password"
                    MaxLength="255"
                    placeholder="Enter your password">
                </asp:TextBox>

            </div>


            <!-- =====================================
                 LOGIN TYPE
                 ===================================== -->

            <div class="form-group">

                <span class="login-type-title">
                    Login as:
                </span>


                <asp:RadioButtonList
                    ID="rblLoginType"
                    runat="server"
                    RepeatDirection="Horizontal"
                    RepeatLayout="Flow"
                    CssClass="login-type">

                    <asp:ListItem
                        Text="Normal User"
                        Value="User"
                        Selected="True">
                    </asp:ListItem>

                    <asp:ListItem
                        Text="Investor"
                        Value="Investor">
                    </asp:ListItem>

                </asp:RadioButtonList>

            </div>


            <!-- =====================================
                 LOGIN BUTTON
                 ===================================== -->

            <div class="form-group">

                <asp:Button
                    ID="btnLogin"
                    runat="server"
                    Text="Login"
                    CssClass="login-button">
                </asp:Button>

            </div>


            <!-- =====================================
                 REGISTER BUTTONS
                 DIRECTLY UNDER LOGIN
                 ===================================== -->

            <div class="register-section">


                <!-- NORMAL USER REGISTER -->

                <asp:HyperLink
                    ID="lnkUserRegister"
                    runat="server"
                    NavigateUrl="~/Register.aspx"
                    CssClass="register-button user-register">

                    Register as Normal User

                </asp:HyperLink>


                <!-- INVESTOR REGISTER -->

                <asp:HyperLink
                    ID="lnkInvestorRegister"
                    runat="server"
                    NavigateUrl="~/InvestorRegister.aspx"
                    CssClass="register-button investor-register">

                    Register as Investor

                </asp:HyperLink>


            </div>


            <!-- =====================================
                 FORGOT PASSWORD
                 ===================================== -->

            <asp:HyperLink
                ID="lnkForgotPassword"
                runat="server"
                NavigateUrl="~/ForgotPassword.aspx"
                CssClass="forgot-link">

                Forgot Password?

            </asp:HyperLink>


            <!-- =====================================
                 NOTE
                 ===================================== -->

            <div class="account-note">

                If you have both a Normal User account
                and an Investor account, select the
                account type you want to access before
                logging in.

            </div>


        </div>

    </div>


</form>

</body>

</html>