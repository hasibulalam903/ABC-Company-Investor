<%@ Page Language="VB"
    MasterPageFile="~/Site.master"
    AutoEventWireup="false"
    CodeFile="Home.aspx.vb"
    Inherits="Home" %>

<asp:Content
    ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">


    <style type="text/css">

        /* ==========================================
           HOME CONTAINER
           ========================================== */

        .home-container {
            width: 90%;
            max-width: 1100px;
            margin: 50px auto;
        }


        /* ==========================================
           WELCOME BOX
           ========================================== */

        .welcome-box {
            background-color: white;

            padding: 60px 40px;

            text-align: center;

            border-radius: 10px;

            box-shadow:
                0 2px 10px
                rgba(0,0,0,0.10);
        }


        .welcome-box h1 {
            margin-top: 0;

            margin-bottom: 15px;

            color: #343a40;
        }


        .welcome-box p {
            color: #666;

            font-size: 17px;

            margin-bottom: 30px;
        }


        /* ==========================================
           INVESTOR BUTTON
           ========================================== */

        .investor-button {
            display: inline-block;

            padding: 12px 25px;

            background-color: #007bff;

            color: white;

            text-decoration: none;

            border-radius: 5px;

            font-size: 15px;
        }


        .investor-button:hover {
            background-color: #0056b3;
        }


        /* ==========================================
           FOOTER
           ========================================== */

        .home-footer {
            text-align: center;

            margin-top: 40px;

            color: #777;

            font-size: 13px;
        }


        /* ==========================================
           MOBILE
           ========================================== */

        @media screen and (max-width: 600px) {

            .home-container {
                width: 95%;

                margin: 25px auto;
            }


            .welcome-box {
                padding: 40px 20px;
            }


            .welcome-box h1 {
                font-size: 26px;
            }


            .welcome-box p {
                font-size: 15px;
            }

        }

    </style>


    <!-- ==========================================
         HOME CONTENT
         ========================================== -->

    <div class="home-container">


        <!-- WELCOME BOX -->

        <div class="welcome-box">


            <h1>
                Welcome to InvestorDB
            </h1>


            <p>
                Investor Management System
            </p>


            <!-- ==========================================
                 GO TO INVESTORS
                 ========================================== -->

            <asp:HyperLink
                ID="btnInvestors"
                runat="server"
                NavigateUrl="Investors.aspx"
                Text="Go to Investors"
                CssClass="investor-button">
            </asp:HyperLink>


        </div>


        <!-- ==========================================
             FOOTER
             ========================================== -->

        <div class="home-footer">

            InvestorDB Management System

        </div>


    </div>


</asp:Content>