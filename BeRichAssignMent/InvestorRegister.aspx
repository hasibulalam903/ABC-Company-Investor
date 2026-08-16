<%@ Page Language="VB"
    MasterPageFile="~/Site.master"
    AutoEventWireup="false"
    CodeFile="InvestorRegister.aspx.vb"
    Inherits="InvestorRegister" %>


<asp:Content
    ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">


    <style type="text/css">

        /* ==========================================
           REGISTER CONTAINER
           ========================================== */

        .register-container {
            width: 100%;
            min-height: calc(100vh - 65px);

            display: flex;
            justify-content: center;
            align-items: center;

            padding: 40px 20px;

            box-sizing: border-box;
        }


        /* ==========================================
           REGISTER BOX
           ========================================== */

        .register-box {
            width: 100%;
            max-width: 650px;

            background-color: white;

            padding: 35px;

            border-radius: 10px;

            box-shadow:
                0 3px 15px
                rgba(0, 0, 0, 0.12);

            box-sizing: border-box;
        }


        /* ==========================================
           TITLE
           ========================================== */

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


        /* ==========================================
           FORM GRID
           ========================================== */

        .register-grid {
            display: grid;

            grid-template-columns:
                repeat(2, minmax(0, 1fr));

            gap: 20px;
        }


        /* ==========================================
           FORM GROUP
           ========================================== */

        .register-group {
            margin-bottom: 2px;
        }


        .register-label {
            display: block;

            margin-bottom: 7px;

            color: #333;

            font-weight: bold;

            font-size: 14px;
        }


        /* ==========================================
           INPUT
           ========================================== */

        .register-input {
            width: 100%;

            height: 44px;

            padding: 10px 12px;

            border:
                1px solid #bbb;

            border-radius: 5px;

            font-size: 14px;

            background-color: white;

            box-sizing: border-box;
        }


        .register-input:focus {
            outline: none;

            border-color: #0d6efd;

            box-shadow:
                0 0 4px
                rgba(13, 110, 253, 0.25);
        }


        /* ==========================================
           FULL WIDTH FIELD
           ========================================== */

        .full-width {
            grid-column: 1 / -1;
        }


        /* ==========================================
           REGISTER BUTTON
           ========================================== */

        .register-button {
            width: 100%;

            height: 46px;

            margin-top: 25px;

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


        /* ==========================================
           MESSAGE
           ========================================== */

        .register-message {
            display: block;

            width: 100%;

            margin-top: 20px;

            padding: 12px;

            border-radius: 5px;

            text-align: center;

            font-size: 14px;

            box-sizing: border-box;
        }


        /* ==========================================
           LOGIN LINK
           ========================================== */

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


        /* ==========================================
           NOTE
           ========================================== */

        .register-note {
            margin-top: 18px;

            padding: 12px;

            background-color: #f5f7fa;

            border-radius: 5px;

            color: #666;

            font-size: 12px;

            line-height: 1.5;

            text-align: center;
        }


        /* ==========================================
           MOBILE
           ========================================== */

        @media screen and (max-width: 650px) {

            .register-container {
                padding: 25px 15px;
            }


            .register-box {
                padding: 25px 20px;
            }


            .register-grid {
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


    <!-- ==========================================
         REGISTER CONTAINER
         ========================================== -->

    <div class="register-container">


        <div class="register-box">


            <!-- ======================================
                 TITLE
                 ====================================== -->

            <h2 class="register-title">
                Investor Registration
            </h2>


            <p class="register-subtitle">
                Create your Investor Management System account
            </p>


            <!-- ======================================
                 REGISTRATION FORM
                 ====================================== -->

            <div class="register-grid">


                <!-- ==================================
                     NAME
                     ================================== -->

                <div class="register-group">

                    <asp:Label
                        ID="lblName"
                        runat="server"
                        Text="Investor Name"
                        CssClass="register-label">
                    </asp:Label>


                    <asp:TextBox
                        ID="txtName"
                        runat="server"
                        CssClass="register-input"
                        MaxLength="100"
                        placeholder="Enter your full name">
                    </asp:TextBox>

                </div>


                <!-- ==================================
                     EMAIL
                     ================================== -->

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
                        placeholder="Enter your email">
                    </asp:TextBox>

                </div>


                <!-- ==================================
                     MOBILE
                     ================================== -->

                <div class="register-group">

                    <asp:Label
                        ID="lblPhone"
                        runat="server"
                        Text="Mobile Number"
                        CssClass="register-label">
                    </asp:Label>


                    <asp:TextBox
                        ID="txtPhone"
                        runat="server"
                        CssClass="register-input"
                        MaxLength="20"
                        placeholder="Enter your mobile number">
                    </asp:TextBox>

                </div>


                <!-- ==================================
                     DEPARTMENT
                     ================================== -->

                <div class="register-group">

                    <asp:Label
                        ID="lblDepartment"
                        runat="server"
                        Text="Department"
                        CssClass="register-label">
                    </asp:Label>


                    <asp:DropDownList
                        ID="ddlDepartment"
                        runat="server"
                        CssClass="register-input">

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


                <!-- ==================================
                     DESIGNATION
                     ================================== -->

                <div class="register-group">

                    <asp:Label
                        ID="lblDesignation"
                        runat="server"
                        Text="Designation"
                        CssClass="register-label">
                    </asp:Label>


                    <asp:DropDownList
                        ID="ddlDesignation"
                        runat="server"
                        CssClass="register-input">

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


                <!-- ==================================
                     INVESTMENT AMOUNT
                     ================================== -->

                <div class="register-group">

                    <asp:Label
                        ID="lblInvestmentAmount"
                        runat="server"
                        Text="Investment Amount"
                        CssClass="register-label">
                    </asp:Label>


                    <asp:TextBox
                        ID="txtInvestmentAmount"
                        runat="server"
                        CssClass="register-input"
                        MaxLength="20"
                        placeholder="Enter investment amount">
                    </asp:TextBox>

                </div>


                <!-- ==================================
                     PASSWORD
                     ================================== -->

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
                        placeholder="Enter password">
                    </asp:TextBox>

                </div>


                <!-- ==================================
                     CONFIRM PASSWORD
                     ================================== -->

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
                        placeholder="Confirm password">
                    </asp:TextBox>

                </div>


            </div>


            <!-- ======================================
                 REGISTER BUTTON
                 ====================================== -->

            <asp:Button
                ID="btnRegister"
                runat="server"
                Text="Create Investor Account"
                CssClass="register-button">
            </asp:Button>


            <!-- ======================================
                 MESSAGE
                 ====================================== -->

            <asp:Label
                ID="lblMessage"
                runat="server"
                Visible="False"
                CssClass="register-message">
            </asp:Label>


            <!-- ======================================
                 LOGIN
                 ====================================== -->

            <asp:HyperLink
                ID="lnkLogin"
                runat="server"
                NavigateUrl="~/Login.aspx"
                CssClass="login-link"
                Text="Already have an account? Login">
            </asp:HyperLink>


            <!-- ======================================
                 NOTE
                 ====================================== -->

            <div class="register-note">

                Your password is stored securely as a
                password hash. Never share your password
                with anyone.

            </div>


        </div>

    </div>

</asp:Content>