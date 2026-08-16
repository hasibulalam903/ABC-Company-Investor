<%@ Page Title="User Dashboard"
    Language="VB"
    MasterPageFile="~/Site.master"
    AutoEventWireup="false"
    CodeFile="UserDashboard.aspx.vb"
    Inherits="UserDashboard" %>

<asp:Content
    ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <style type="text/css">

        /* ==========================================
           USER DASHBOARD
           ========================================== */

        .user-dashboard {
            min-height: 80vh;
            padding: 40px 25px 60px 25px;
            background: #f5f7fb;
            box-sizing: border-box;
        }

        .dashboard-container {
            width: 100%;
            max-width: 1100px;
            margin: 0 auto;
        }


        /* ==========================================
           HEADER
           ========================================== */

        .dashboard-header {
            margin-bottom: 30px;
        }

        .dashboard-header h1 {
            margin: 0;
            color: #111827;
            font-size: 34px;
            font-weight: 800;
        }

        .dashboard-header p {
            margin: 8px 0 0 0;
            color: #6b7280;
            font-size: 15px;
        }


        /* ==========================================
           WELCOME CARD
           ========================================== */

        .welcome-card {
            margin-bottom: 30px;
            padding: 30px;

            border-radius: 18px;

            background:
                linear-gradient(
                    135deg,
                    #1d4ed8,
                    #2563eb,
                    #3b82f6
                );

            color: white;

            box-shadow:
                0 12px 30px
                rgba(37, 99, 235, 0.20);
        }

        .welcome-card h2 {
            margin: 0 0 8px 0;
            color: white;
            font-size: 26px;
            font-weight: 700;
        }

        .welcome-card p {
            margin: 0;
            color: rgba(255,255,255,0.85);
            font-size: 14px;
        }


        /* ==========================================
           ACCOUNT INFORMATION
           ========================================== */

        .account-grid {
            display: grid;

            grid-template-columns:
                repeat(4, 1fr);

            gap: 15px;

            margin-top: 25px;
            padding-top: 20px;

            border-top:
                1px solid
                rgba(255,255,255,0.20);
        }

        .account-item {
            padding: 15px;

            border-radius: 10px;

            background:
                rgba(255,255,255,0.10);
        }

        .account-label {
            display: block;

            margin-bottom: 6px;

            color:
                rgba(255,255,255,0.70);

            font-size: 11px;

            font-weight: 600;

            text-transform: uppercase;
        }

        .account-value {
            display: block;

            color: white;

            font-size: 14px;

            font-weight: 700;

            word-break: break-word;
        }


        /* ==========================================
           STATUS
           ========================================== */

        .status-badge {
            display: inline-block;

            padding: 5px 11px;

            border-radius: 20px;

            background:
                rgba(255,255,255,0.18);

            color: white;

            font-size: 12px;

            font-weight: 700;
        }


        /* ==========================================
           SECTION
           ========================================== */

        .section-header {
            margin-bottom: 18px;
        }

        .section-header h2 {
            margin: 0;

            color: #111827;

            font-size: 23px;

            font-weight: 750;
        }

        .section-header p {
            margin: 5px 0 0 0;

            color: #6b7280;

            font-size: 13px;
        }


        /* ==========================================
           PROFILE CARD
           ========================================== */

        .profile-card {
            background: white;

            padding: 28px;

            border-radius: 18px;

            border:
                1px solid #e5e7eb;

            box-shadow:
                0 5px 20px
                rgba(15,23,42,0.06);

            transition:
                transform 0.25s ease,
                box-shadow 0.25s ease;
        }

        .profile-card:hover {
            transform:
                translateY(-5px);

            box-shadow:
                0 15px 35px
                rgba(15,23,42,0.12);
        }


        /* ==========================================
           PROFILE ICON
           ========================================== */

        .profile-icon {
            width: 60px;
            height: 60px;

            display: flex;

            align-items: center;
            justify-content: center;

            margin-bottom: 20px;

            border-radius: 15px;

            background: #dbeafe;

            font-size: 28px;
        }


        /* ==========================================
           PROFILE CONTENT
           ========================================== */

        .profile-card h3 {
            margin: 0 0 10px 0;

            color: #111827;

            font-size: 21px;

            font-weight: 750;
        }

        .profile-card p {
            margin: 0;

            color: #6b7280;

            font-size: 14px;

            line-height: 1.7;
        }


        /* ==========================================
           BUTTON
           ========================================== */

        .profile-action {
            margin-top: 22px;
        }

        .profile-button {
            width: 100%;

            height: 44px;

            border: none;

            border-radius: 9px;

            background: #2563eb;

            color: white;

            font-size: 14px;

            font-weight: 700;

            cursor: pointer;
        }

        .profile-button:hover {
            background: #1d4ed8;
        }


        /* ==========================================
           RESPONSIVE
           ========================================== */

        @media (max-width: 850px) {

            .account-grid {
                grid-template-columns:
                    repeat(2, 1fr);
            }

        }

        @media (max-width: 600px) {

            .user-dashboard {
                padding: 25px 15px 40px 15px;
            }

            .dashboard-header h1 {
                font-size: 28px;
            }

            .welcome-card {
                padding: 23px;
            }

            .account-grid {
                grid-template-columns: 1fr;
            }

        }

    </style>


    <!-- ==========================================
         USER DASHBOARD
         ========================================== -->

    <div class="user-dashboard">

        <div class="dashboard-container">


            <!-- ======================================
                 HEADER
                 ====================================== -->

            <div class="dashboard-header">

                <h1>
                    User Dashboard
                </h1>

                <p>
                    Welcome to your account dashboard
                </p>

            </div>


            <!-- ======================================
                 WELCOME CARD
                 ====================================== -->

            <div class="welcome-card">

                <h2>

                    Welcome,

                    <asp:Label
                        ID="lblUserName"
                        runat="server"
                        Text="User">
                    </asp:Label>

                </h2>

                <p>
                    View your account information and manage your profile.
                </p>


                <!-- ACCOUNT INFORMATION -->

                <div class="account-grid">


                    <!-- EMAIL -->

                    <div class="account-item">

                        <span class="account-label">
                            Email
                        </span>

                        <span class="account-value">

                            <asp:Label
                                ID="lblEmail"
                                runat="server">
                            </asp:Label>

                        </span>

                    </div>


                    <!-- PHONE -->

                    <div class="account-item">

                        <span class="account-label">
                            Phone Number
                        </span>

                        <span class="account-value">

                            <asp:Label
                                ID="lblPhone"
                                runat="server">
                            </asp:Label>

                        </span>

                    </div>


                    <!-- ROLE -->

                    <div class="account-item">

                        <span class="account-label">
                            Account Role
                        </span>

                        <span class="account-value">

                            <asp:Label
                                ID="lblRole"
                                runat="server"
                                Text="User">
                            </asp:Label>

                        </span>

                    </div>


                    <!-- STATUS -->

                    <div class="account-item">

                        <span class="account-label">
                            Account Status
                        </span>

                        <span class="account-value">

                            <span class="status-badge">

                                <asp:Label
                                    ID="lblStatus"
                                    runat="server">
                                </asp:Label>

                            </span>

                        </span>

                    </div>


                </div>

            </div>


            <!-- ======================================
                 PROFILE SECTION
                 ====================================== -->

            <div class="section-header">

                <h2>
                    My Account
                </h2>

                <p>
                    Manage your personal account information
                </p>

            </div>


            <!-- ======================================
                 PROFILE CARD
                 ====================================== -->

            <div class="profile-card">


                <div class="profile-icon">
                    👤
                </div>


                <h3>
                    My Profile
                </h3>


                <p>
                    View and update your personal information,
                    contact details and account information.
                </p>


                <div class="profile-action">

                    <asp:Button
                        ID="btnProfile"
                        runat="server"
                        Text="View My Profile"
                        CssClass="profile-button" />

                </div>


            </div>


        </div>

    </div>

</asp:Content>