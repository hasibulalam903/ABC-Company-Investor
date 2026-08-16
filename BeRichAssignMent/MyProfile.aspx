<%@ Page Language="VB"
    MasterPageFile="~/Site.master"
    AutoEventWireup="false"
    CodeFile="MyProfile.aspx.vb"
    Inherits="MyProfile" %>


<asp:Content
    ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">


    <style type="text/css">

        /* ==========================================
           PROFILE CONTAINER
           ========================================== */

        .profile-container {
            width: 100%;
            min-height: calc(100vh - 65px);

            padding: 40px 20px;

            box-sizing: border-box;
        }


        /* ==========================================
           PROFILE CARD
           ========================================== */

        .profile-card {
            width: 100%;
            max-width: 850px;

            margin: 0 auto;

            background: white;

            border-radius: 12px;

            padding: 35px;

            box-shadow:
                0 3px 15px
                rgba(0, 0, 0, 0.10);

            box-sizing: border-box;
        }


        /* ==========================================
           HEADER
           ========================================== */

        .profile-header {
            text-align: center;

            margin-bottom: 30px;
        }


        .profile-avatar {
            width: 90px;
            height: 90px;

            margin: 0 auto 15px auto;

            display: flex;

            align-items: center;
            justify-content: center;

            border-radius: 50%;

            background: #0d6efd;

            color: white;

            font-size: 36px;

            font-weight: bold;
        }


        .profile-title {
            margin: 0;

            color: #17365d;

            font-size: 28px;
        }


        .profile-subtitle {
            margin: 8px 0 0 0;

            color: #777;

            font-size: 14px;
        }


        /* ==========================================
           PROFILE INFORMATION
           ========================================== */

        .profile-grid {
            display: grid;

            grid-template-columns:
                repeat(2, minmax(0, 1fr));

            gap: 18px;
        }


        .profile-item {
            padding: 18px;

            background: #f8f9fa;

            border: 1px solid #e5e7eb;

            border-radius: 8px;
        }


        .profile-label {
            display: block;

            margin-bottom: 7px;

            color: #6b7280;

            font-size: 13px;

            font-weight: bold;

            text-transform: uppercase;
        }


        .profile-value {
            display: block;

            color: #111827;

            font-size: 16px;

            font-weight: 600;

            word-break: break-word;
        }


        /* ==========================================
           INVESTMENT
           ========================================== */

        .investment-item {
            background: #eef5ff;

            border-color: #cfe2ff;
        }


        .investment-value {
            color: #0d6efd;

            font-size: 22px;

            font-weight: bold;
        }


        /* ==========================================
           MESSAGE
           ========================================== */

        .profile-message {
            display: block;

            margin-bottom: 20px;

            padding: 12px;

            border-radius: 6px;

            text-align: center;

            box-sizing: border-box;
        }


        /* ==========================================
           BACK BUTTON
           ========================================== */

        .profile-actions {
            margin-top: 30px;

            display: flex;

            justify-content: center;

            gap: 10px;
        }


        .profile-button {
            display: inline-block;

            padding: 11px 20px;

            border-radius: 6px;

            background: #0d6efd;

            color: white;

            text-decoration: none;

            font-size: 14px;

            font-weight: bold;
        }


        .profile-button:hover {
            background: #0b5ed7;

            color: white;

            text-decoration: none;
        }


        /* ==========================================
           MOBILE
           ========================================== */

        @media screen and (max-width: 650px) {

            .profile-container {
                padding: 25px 15px;
            }


            .profile-card {
                padding: 25px 20px;
            }


            .profile-grid {
                grid-template-columns: 1fr;
            }


            .profile-title {
                font-size: 24px;
            }

        }

    </style>


    <!-- ==========================================
         PROFILE CONTAINER
         ========================================== -->

    <div class="profile-container">


        <div class="profile-card">


            <!-- ======================================
                 HEADER
                 ====================================== -->

            <div class="profile-header">


                <div class="profile-avatar">

                    <asp:Label
                        ID="lblInitial"
                        runat="server"
                        Text="I">
                    </asp:Label>

                </div>


                <h1 class="profile-title">
                    My Profile
                </h1>


                <p class="profile-subtitle">
                    Your Investor Management System profile
                </p>

            </div>


            <!-- ======================================
                 MESSAGE
                 ====================================== -->

            <asp:Label
                ID="lblMessage"
                runat="server"
                Visible="False"
                CssClass="profile-message">
            </asp:Label>


            <!-- ======================================
                 PROFILE INFORMATION
                 ====================================== -->

            <div class="profile-grid">


                <!-- NAME -->

                <div class="profile-item">

                    <span class="profile-label">
                        Investor Name
                    </span>

                    <asp:Label
                        ID="lblName"
                        runat="server"
                        CssClass="profile-value">
                    </asp:Label>

                </div>


                <!-- EMAIL -->

                <div class="profile-item">

                    <span class="profile-label">
                        Email
                    </span>

                    <asp:Label
                        ID="lblEmail"
                        runat="server"
                        CssClass="profile-value">
                    </asp:Label>

                </div>


                <!-- MOBILE -->

                <div class="profile-item">

                    <span class="profile-label">
                        Mobile
                    </span>

                    <asp:Label
                        ID="lblMobile"
                        runat="server"
                        CssClass="profile-value">
                    </asp:Label>

                </div>


                <!-- DEPARTMENT -->

                <div class="profile-item">

                    <span class="profile-label">
                        Department
                    </span>

                    <asp:Label
                        ID="lblDepartment"
                        runat="server"
                        CssClass="profile-value">
                    </asp:Label>

                </div>


                <!-- DESIGNATION -->

                <div class="profile-item">

                    <span class="profile-label">
                        Designation
                    </span>

                    <asp:Label
                        ID="lblDesignation"
                        runat="server"
                        CssClass="profile-value">
                    </asp:Label>

                </div>


                <!-- INVESTOR ID -->

                <div class="profile-item">

                    <span class="profile-label">
                        Investor ID
                    </span>

                    <asp:Label
                        ID="lblInvestorID"
                        runat="server"
                        CssClass="profile-value">
                    </asp:Label>

                </div>


                <!-- INVESTMENT -->

                <div class="profile-item investment-item">

                    <span class="profile-label">
                        Investment Amount
                    </span>

                    <asp:Label
                        ID="lblInvestmentAmount"
                        runat="server"
                        CssClass="profile-value investment-value">
                    </asp:Label>

                </div>


            </div>


            <!-- ======================================
                 ACTIONS
                 ====================================== -->

            <div class="profile-actions">


                <asp:HyperLink
                    ID="lnkDashboard"
                    runat="server"
                    NavigateUrl="~/UserDashboard.aspx"
                    CssClass="profile-button"
                    Text="Back to Dashboard">
                </asp:HyperLink>


            </div>


        </div>

    </div>

</asp:Content>