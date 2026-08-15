    <%@ Page Language="VB"
    MasterPageFile="~/Site.master"
    AutoEventWireup="false"
    CodeFile="AdminDashboard.aspx.vb"
    Inherits="AdminDashboard" %>

<asp:Content
    ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <style type="text/css">

        /* ==========================================
           ADMIN DASHBOARD
           ========================================== */

        .admin-dashboard {
            padding: 35px;
            background: #f5f7fb;
            min-height: 80vh;
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
            font-size: 32px;
            font-weight: 700;
        }

        .dashboard-header p {
            margin-top: 8px;
            color: #6b7280;
            font-size: 15px;
        }


        /* ==========================================
           PANEL GRID
           ========================================== */

        .panel-grid {
            display: grid;
            grid-template-columns: repeat(2, 1fr);
            gap: 25px;
            max-width: 1000px;
        }


        /* ==========================================
           DASHBOARD CARD
           ========================================== */

        .dashboard-card {
            display: block;
            text-decoration: none;
            background: white;
            border-radius: 15px;
            padding: 30px;
            min-height: 190px;

            box-shadow:
                0 5px 20px rgba(0, 0, 0, 0.08);

            border: 1px solid #e5e7eb;

            transition:
                transform 0.2s ease,
                box-shadow 0.2s ease;

            cursor: pointer;
        }

        .dashboard-card:hover {
            transform: translateY(-5px);

            box-shadow:
                0 12px 30px rgba(0, 0, 0, 0.13);
        }


        /* ==========================================
           ICON
           ========================================== */

        .card-icon {
            width: 65px;
            height: 65px;

            display: flex;
            align-items: center;
            justify-content: center;

            border-radius: 14px;

            font-size: 32px;

            margin-bottom: 20px;
        }


        /* USER CARD */

        .user-card .card-icon {
            background: #dbeafe;
        }


        /* INVESTOR CARD */

        .investor-card .card-icon {
            background: #dcfce7;
        }


        /* ==========================================
           CARD TEXT
           ========================================== */

        .dashboard-card h2 {
            margin: 0 0 8px 0;

            color: #111827;

            font-size: 23px;

            font-weight: 700;
        }

        .dashboard-card p {
            margin: 0;

            color: #6b7280;

            font-size: 14px;

            line-height: 1.6;
        }


        /* ==========================================
           ARROW
           ========================================== */

        .card-arrow {
            margin-top: 20px;

            color: #2563eb;

            font-size: 14px;

            font-weight: 600;
        }

        .investor-card .card-arrow {
            color: #16a34a;
        }


        /* ==========================================
           RESPONSIVE
           ========================================== */

        @media (max-width: 800px) {

            .panel-grid {
                grid-template-columns: 1fr;
            }

        }


        @media (max-width: 500px) {

            .admin-dashboard {
                padding: 20px;
            }

            .dashboard-header h1 {
                font-size: 26px;
            }

            .dashboard-card {
                padding: 25px;
            }

        }

    </style>


    <!-- ==========================================
         ADMIN DASHBOARD
         ========================================== -->

    <div class="admin-dashboard">


        <!-- ======================================
             HEADER
             ====================================== -->

        <div class="dashboard-header">

            <h1>
                Admin Dashboard
            </h1>

            <p>
                Manage users and investors from one place.
            </p>

        </div>


        <!-- ======================================
             PANELS
             ====================================== -->

        <div class="panel-grid">


            <!-- ==================================
                 USER PANEL
                 ================================== -->

            <asp:LinkButton
                ID="lnkUserPanel"
                runat="server"
                CssClass="dashboard-card user-card"
                PostBackUrl="~/UsersPanel.aspx">

                <div class="card-icon">
                    👥
                </div>

                <h2>
                    User Panel
                </h2>

                <p>
                    View, search and manage all registered
                    users in the system.
                </p>

                <div class="card-arrow">
                    View All Users →
                </div>

            </asp:LinkButton>



            <!-- ==================================
                 INVESTOR PANEL
                 ================================== -->

            <asp:LinkButton
                ID="lnkInvestorPanel"
                runat="server"
                CssClass="dashboard-card investor-card"
                PostBackUrl="~/Investors.aspx">

                <div class="card-icon">
                    📈
                </div>

                <h2>
                    Investor Panel
                </h2>

                <p>
                    View and manage investors, investments,
                    departments and designations.
                </p>

                <div class="card-arrow">
                    View All Investors →
                </div>

            </asp:LinkButton>


        </div>


    </div>

</asp:Content>