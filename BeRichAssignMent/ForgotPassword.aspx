<%@ Page Language="VB"
    AutoEventWireup="false"
    CodeFile="ForgotPassword.aspx.vb"
    Inherits="ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <title>Forgot Password</title>

    <meta
        name="viewport"
        content="width=device-width, initial-scale=1" />

    <style type="text/css">

        * {
            box-sizing: border-box;
        }

        body {
            margin: 0;
            padding: 0;

            font-family:
                Arial,
                Helvetica,
                sans-serif;

            background-color: #f5f7fa;

            color: #333333;
        }


        /* =========================================
           CONTAINER
           ========================================= */

        .forgot-container {

            min-height: 100vh;

            display: flex;

            justify-content: center;

            align-items: center;

            padding: 30px 15px;
        }


        /* =========================================
           BOX
           ========================================= */

        .forgot-box {

            width: 100%;

            max-width: 460px;

            background-color: #ffffff;

            padding: 35px;

            border-radius: 10px;

            box-shadow:
                0 4px 20px
                rgba(0, 0, 0, 0.10);
        }


        /* =========================================
           TITLE
           ========================================= */

        .forgot-title {

            margin: 0;

            text-align: center;

            color: #17365d;

            font-size: 28px;

            font-weight: 700;
        }


        .forgot-subtitle {

            margin:
                8px 0 30px 0;

            text-align: center;

            color: #777777;

            font-size: 14px;

            line-height: 1.5;
        }


        /* =========================================
           FORM GROUP
           ========================================= */

        .form-group {

            margin-bottom: 20px;
        }


        /* =========================================
           LABEL
           ========================================= */

        .form-label {

            display: block;

            margin-bottom: 7px;

            color: #333333;

            font-size: 14px;

            font-weight: bold;
        }


        /* =========================================
           INPUT
           ========================================= */

        .form-input {

            width: 100%;

            height: 44px;

            padding:
                10px 12px;

            border:
                1px solid #bbbbbb;

            border-radius: 5px;

            background-color: #ffffff;

            color: #333333;

            font-size: 14px;

            outline: none;
        }


        .form-input:focus {

            border-color: #0d6efd;

            box-shadow:
                0 0 4px
                rgba(13, 110, 253, 0.25);
        }


        /* =========================================
           HELP TEXT
           ========================================= */

        .password-help {

            margin-top: 7px;

            color: #777777;

            font-size: 12px;

            line-height: 1.5;
        }


        /* =========================================
           BUTTON
           ========================================= */

        .main-button {

            width: 100%;

            height: 46px;

            margin-top: 5px;

            border: none;

            border-radius: 5px;

            background-color: #0d6efd;

            color: #ffffff;

            font-size: 15px;

            font-weight: bold;

            cursor: pointer;
        }


        .main-button:hover {

            background-color: #0b5ed7;
        }


        /* =========================================
           MESSAGE
           ========================================= */

        .message {

            display: block;

            width: 100%;

            margin-top: 20px;

            padding: 12px;

            border-radius: 5px;

            text-align: center;

            font-size: 14px;

            line-height: 1.5;
        }


        .message-error {

            color: #842029;

            background-color: #f8d7da;

            border:
                1px solid #f5c2c7;
        }


        .message-success {

            color: #0f5132;

            background-color: #d1e7dd;

            border:
                1px solid #badbcc;
        }


        /* =========================================
           LOGIN LINK
           ========================================= */

        .back-login {

            display: block;

            margin-top: 20px;

            text-align: center;

            color: #0d6efd;

            font-size: 14px;

            font-weight: bold;

            text-decoration: none;
        }


        .back-login:hover {

            text-decoration: underline;
        }


        /* =========================================
           SECURITY NOTE
           ========================================= */

        .security-note {

            margin-top: 20px;

            padding: 12px;

            background-color: #f5f7fa;

            border-radius: 5px;

            color: #666666;

            font-size: 12px;

            line-height: 1.5;

            text-align: center;
        }


        /* =========================================
           MOBILE
           ========================================= */

        @media screen and (max-width: 600px) {

            .forgot-box {

                padding:
                    25px 20px;
            }


            .forgot-title {

                font-size: 24px;
            }

        }

    </style>

</head>


<body>

<form
    id="form1"
    runat="server">


    <!-- =========================================
         MAIN CONTAINER
         ========================================= -->

    <div class="forgot-container">


        <div class="forgot-box">


            <!-- =====================================
                 TITLE
                 ===================================== -->

            <h2 class="forgot-title">

                Forgot Password

            </h2>


            <p class="forgot-subtitle">

                Change your Investor Management System password

            </p>


            <!-- =====================================
                 EMAIL
                 ===================================== -->

            <div class="form-group">

                <asp:Label
                    ID="lblEmail"
                    runat="server"
                    Text="Registered Email Address"
                    CssClass="form-label">
                </asp:Label>


                <asp:TextBox
                    ID="txtEmail"
                    runat="server"
                    CssClass="form-input"
                    MaxLength="150"
                    TextMode="Email"
                    placeholder="Enter your registered email">
                </asp:TextBox>

            </div>


            <!-- =====================================
                 NEW PASSWORD
                 ===================================== -->

            <div class="form-group">

                <asp:Label
                    ID="lblNewPassword"
                    runat="server"
                    Text="New Password"
                    CssClass="form-label">
                </asp:Label>


                <asp:TextBox
                    ID="txtNewPassword"
                    runat="server"
                    CssClass="form-input"
                    TextMode="Password"
                    MaxLength="100"
                    placeholder="Enter new password">
                </asp:TextBox>


                <div class="password-help">

                    Minimum 8 characters:
                    uppercase, lowercase,
                    number and special character.

                </div>

            </div>


            <!-- =====================================
                 CONFIRM PASSWORD
                 ===================================== -->

            <div class="form-group">

                <asp:Label
                    ID="lblConfirmPassword"
                    runat="server"
                    Text="Confirm New Password"
                    CssClass="form-label">
                </asp:Label>


                <asp:TextBox
                    ID="txtConfirmPassword"
                    runat="server"
                    CssClass="form-input"
                    TextMode="Password"
                    MaxLength="100"
                    placeholder="Confirm new password">
                </asp:TextBox>

            </div>


            <!-- =====================================
                 RESET BUTTON
                 ===================================== -->

            <asp:Button
                ID="btnResetPassword"
                runat="server"
                Text="Change Password"
                CssClass="main-button"
                CausesValidation="False">
            </asp:Button>


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
                 BACK TO LOGIN
                 ===================================== -->

            <asp:HyperLink
                ID="lnkLogin"
                runat="server"
                NavigateUrl="~/Login.aspx"
                CssClass="back-login"
                Text="Back to Login">
            </asp:HyperLink>


            <!-- =====================================
                 SECURITY NOTE
                 ===================================== -->

            <div class="security-note">

                Your new password is securely hashed
                before it is saved.

            </div>


        </div>

    </div>


</form>

</body>

</html>