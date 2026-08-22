<%@ Page Language="VB"
    AutoEventWireup="false"
    CodeFile="InvestorRegister.aspx.vb"
    Inherits="InvestorRegister" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <title>Investor Registration</title>

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
           PAGE CONTAINER
           ========================================= */

        .register-container {

            min-height: 100vh;

            display: flex;

            justify-content: center;

            align-items: center;

            padding: 30px 15px;
        }


        /* =========================================
           REGISTER BOX
           ========================================= */

        .register-box {

            width: 100%;

            max-width: 700px;

            background-color: #ffffff;

            padding: 35px;

            border-radius: 10px;

            box-shadow:
                0 4px 20px
                rgba(0,0,0,0.10);
        }


        /* =========================================
           TITLE
           ========================================= */

        .register-title {

            margin: 0;

            text-align: center;

            color: #17365d;

            font-size: 28px;

            font-weight: 700;
        }


        .register-subtitle {

            margin: 8px 0 30px 0;

            text-align: center;

            color: #777777;

            font-size: 14px;
        }


        /* =========================================
           GRID
           ========================================= */

        .form-grid {

            display: grid;

            grid-template-columns:
                repeat(2, 1fr);

            gap: 20px;
        }


        /* =========================================
           FORM GROUP
           ========================================= */

        .form-group {

            width: 100%;
        }


        .form-label {

            display: block;

            margin-bottom: 7px;

            font-size: 14px;

            font-weight: bold;

            color: #333333;
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

            font-size: 14px;

            outline: none;
        }


        .form-input:focus {

            border-color: #0d6efd;

            box-shadow:
                0 0 4px
                rgba(13,110,253,0.25);
        }


        /* =========================================
           SELECT
           ========================================= */

        select.form-input {

            cursor: pointer;
        }


        /* =========================================
           FULL WIDTH
           ========================================= */

        .full-width {

            grid-column:
                1 / -1;
        }


        /* =========================================
           REGISTER BUTTON
           ========================================= */

        .register-button {

            width: 100%;

            height: 46px;

            margin-top: 25px;

            border: none;

            border-radius: 5px;

            background-color: #0d6efd;

            color: #ffffff;

            font-size: 15px;

            font-weight: bold;

            cursor: pointer;
        }


        .register-button:hover {

            background-color: #0b5ed7;
        }


        /* =========================================
           MESSAGE
           ========================================= */

        .register-message {

            display: block;

            width: 100%;

            margin-top: 20px;

            padding: 12px;

            border-radius: 5px;

            text-align: center;

            font-size: 14px;

            line-height: 1.5;
        }


        /* =========================================
           LOGIN LINK
           ========================================= */

        .login-link {

            display: block;

            margin-top: 18px;

            text-align: center;

            color: #0d6efd;

            font-size: 14px;

            font-weight: bold;

            text-decoration: none;
        }


        .login-link:hover {

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

            text-align: center;

            color: #666666;

            font-size: 12px;

            line-height: 1.5;
        }


        /* =========================================
           MOBILE
           ========================================= */

        @media screen and (max-width: 650px) {

            .register-box {

                padding: 25px 20px;
            }


            .form-grid {

                grid-template-columns: 1fr;
            }


            .full-width {

                grid-column: auto;
            }


            .register-title {

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
         REGISTER CONTAINER
         ========================================= -->

    <div class="register-container">


        <div class="register-box">


            <!-- =====================================
                 TITLE
                 ===================================== -->

            <h2 class="register-title">

                Investor Registration

            </h2>


            <p class="register-subtitle">

                Create your Investor Management System account

            </p>


            <!-- =====================================
                 FORM
                 ===================================== -->

            <div class="form-grid">


                <!-- =================================
                     NAME
                     ================================= -->

                <div class="form-group">

                    <asp:Label
                        ID="lblName"
                        runat="server"
                        Text="Investor Name"
                        CssClass="form-label">
                    </asp:Label>


                    <asp:TextBox
                        ID="txtName"
                        runat="server"
                        CssClass="form-input"
                        MaxLength="100"
                        placeholder="Enter your full name">
                    </asp:TextBox>

                </div>


                <!-- =================================
                     EMAIL
                     ================================= -->

                <div class="form-group">

                    <asp:Label
                        ID="lblEmail"
                        runat="server"
                        Text="Email"
                        CssClass="form-label">
                    </asp:Label>


                    <asp:TextBox
                        ID="txtEmail"
                        runat="server"
                        CssClass="form-input"
                        TextMode="Email"
                        MaxLength="150"
                        placeholder="Enter your email address">
                    </asp:TextBox>

                </div>


                <!-- =================================
                     MOBILE
                     ================================= -->

                <div class="form-group">

                    <asp:Label
                        ID="lblPhone"
                        runat="server"
                        Text="Mobile Number"
                        CssClass="form-label">
                    </asp:Label>


                    <asp:TextBox
                        ID="txtPhone"
                        runat="server"
                        CssClass="form-input"
                        MaxLength="20"
                        placeholder="Enter your mobile number">
                    </asp:TextBox>

                </div>


                <!-- =================================
                     DEPARTMENT
                     ================================= -->

                <div class="form-group">

                    <asp:Label
                        ID="lblDepartment"
                        runat="server"
                        Text="Department"
                        CssClass="form-label">
                    </asp:Label>


                    <asp:DropDownList
                        ID="ddlDepartment"
                        runat="server"
                        CssClass="form-input">

                        <asp:ListItem
                            Text="-- Select Department --"
                            Value="">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="Administration"
                            Value="Administration">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="Finance"
                            Value="Finance">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="Human Resources"
                            Value="Human Resources">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="IT"
                            Value="IT">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="Marketing"
                            Value="Marketing">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="Operations"
                            Value="Operations">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="Sales"
                            Value="Sales">
                        </asp:ListItem>

                    </asp:DropDownList>

                </div>


                <!-- =================================
                     DESIGNATION
                     ================================= -->

                <div class="form-group">

                    <asp:Label
                        ID="lblDesignation"
                        runat="server"
                        Text="Designation"
                        CssClass="form-label">
                    </asp:Label>


                    <asp:DropDownList
                        ID="ddlDesignation"
                        runat="server"
                        CssClass="form-input">

                        <asp:ListItem
                            Text="-- Select Designation --"
                            Value="">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="Chairman"
                            Value="Chairman">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="Managing Director"
                            Value="Managing Director">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="Director"
                            Value="Director">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="Manager"
                            Value="Manager">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="Senior Executive"
                            Value="Senior Executive">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="Executive"
                            Value="Executive">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="Officer"
                            Value="Officer">
                        </asp:ListItem>

                    </asp:DropDownList>

                </div>


                <!-- =================================
                     INVESTMENT AMOUNT
                     ================================= -->

                <div class="form-group">

                    <asp:Label
                        ID="lblInvestmentAmount"
                        runat="server"
                        Text="Investment Amount"
                        CssClass="form-label">
                    </asp:Label>


                    <asp:TextBox
                        ID="txtInvestmentAmount"
                        runat="server"
                        CssClass="form-input"
                        MaxLength="20"
                        placeholder="Enter investment amount">
                    </asp:TextBox>

                </div>


                <!-- =================================
                     PASSWORD
                     ================================= -->

                <div class="form-group">

                    <asp:Label
                        ID="lblPassword"
                        runat="server"
                        Text="Password"
                        CssClass="form-label">
                    </asp:Label>


                    <asp:TextBox
                        ID="txtPassword"
                        runat="server"
                        CssClass="form-input"
                        TextMode="Password"
                        MaxLength="100"
                        placeholder="Enter password">
                    </asp:TextBox>

                </div>


                <!-- =================================
                     CONFIRM PASSWORD
                     ================================= -->

                <div class="form-group">

                    <asp:Label
                        ID="lblConfirmPassword"
                        runat="server"
                        Text="Confirm Password"
                        CssClass="form-label">
                    </asp:Label>


                    <asp:TextBox
                        ID="txtConfirmPassword"
                        runat="server"
                        CssClass="form-input"
                        TextMode="Password"
                        MaxLength="100"
                        placeholder="Confirm password">
                    </asp:TextBox>

                </div>


            </div>


            <!-- =====================================
                 REGISTER BUTTON
                 ===================================== -->

            <asp:Button
                ID="btnRegister"
                runat="server"
                Text="Create Investor Account"
                CssClass="register-button">
            </asp:Button>


            <!-- =====================================
                 MESSAGE
                 ===================================== -->

            <asp:Label
                ID="lblMessage"
                runat="server"
                Visible="False"
                CssClass="register-message">
            </asp:Label>


            <!-- =====================================
                 LOGIN LINK
                 ===================================== -->

            <asp:HyperLink
                ID="lnkLogin"
                runat="server"
                NavigateUrl="~/Login.aspx"
                CssClass="login-link"
                Text="Already have an account? Login">
            </asp:HyperLink>


            <!-- =====================================
                 SECURITY NOTE
                 ===================================== -->

            <div class="security-note">

                Your password is securely protected.
                Please do not share your password with anyone.

            </div>


        </div>

    </div>


</form>

</body>

</html>