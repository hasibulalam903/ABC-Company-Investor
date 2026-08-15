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

    * {
        box-sizing: border-box;
    }

    .home-page {
        width: 94%;
        max-width: 1250px;
        margin: 35px auto 50px auto;
        font-family: Arial, Helvetica, sans-serif;
    }

    /* =========================
       HERO
       ========================= */

    .hero-section {
        background: linear-gradient(135deg, #0d47a1, #1976d2);
        color: white;
        padding: 45px 45px;
        border-radius: 14px;
        margin-bottom: 28px;
        box-shadow: 0 6px 20px rgba(0,0,0,0.12);
    }

    .hero-section h1 {
        margin: 0 0 10px 0;
        font-size: 32px;
        font-weight: 600;
    }

    .hero-section p {
        margin: 0;
        font-size: 16px;
        opacity: 0.92;
        line-height: 1.6;
    }

    /* =========================
       SECTION TITLE
       ========================= */

    .section-title {
        font-size: 21px;
        color: #263238;
        margin: 32px 0 17px 0;
        font-weight: 600;
    }

    /* =========================
       STATISTICS
       ========================= */

    .stats-grid {
        display: grid;
        grid-template-columns: repeat(4, 1fr);
        gap: 18px;
    }

    .stat-card {
        background: #ffffff;
        border-radius: 10px;
        padding: 25px;
        box-shadow: 0 2px 10px rgba(0,0,0,0.08);
        border-left: 5px solid #1976d2;
    }

    .stat-title {
        color: #777;
        font-size: 14px;
        margin-bottom: 12px;
    }

    .stat-number {
        font-size: 28px;
        color: #263238;
        font-weight: bold;
    }

    .stat-card.green {
        border-left-color: #2e7d32;
    }

    .stat-card.orange {
        border-left-color: #ef6c00;
    }

    .stat-card.purple {
        border-left-color: #6a1b9a;
    }

    /* =========================
       QUICK ACTIONS
       ========================= */

    .action-grid {
        display: grid;
        grid-template-columns: repeat(3, 1fr);
        gap: 18px;
    }

    .action-card {
        display: block;
        background: white;
        padding: 25px;
        border-radius: 10px;
        text-decoration: none;
        color: #333;
        box-shadow: 0 2px 10px rgba(0,0,0,0.08);
        transition: all 0.2s ease;
        border: 1px solid #eeeeee;
    }

    .action-card:hover {
        transform: translateY(-3px);
        box-shadow: 0 6px 18px rgba(0,0,0,0.12);
        border-color: #1976d2;
    }

    .action-icon {
        width: 48px;
        height: 48px;
        background: #e3f2fd;
        color: #1565c0;
        border-radius: 9px;
        display: flex;
        align-items: center;
        justify-content: center;
        font-size: 22px;
        margin-bottom: 15px;
        font-weight: bold;
    }

    .action-card h3 {
        margin: 0 0 8px 0;
        color: #263238;
        font-size: 17px;
    }

    .action-card p {
        margin: 0;
        color: #777;
        font-size: 14px;
        line-height: 1.5;
    }

    /* =========================
       INFORMATION PANEL
       ========================= */

    .info-panel {
        margin-top: 30px;
        display: grid;
        grid-template-columns: 2fr 1fr;
        gap: 20px;
    }

    .info-box {
        background: white;
        border-radius: 10px;
        padding: 25px;
        box-shadow: 0 2px 10px rgba(0,0,0,0.08);
    }

    .info-box h3 {
        margin-top: 0;
        margin-bottom: 18px;
        color: #263238;
        font-size: 18px;
    }

    .info-row {
        display: flex;
        justify-content: space-between;
        border-bottom: 1px solid #eeeeee;
        padding: 12px 0;
        font-size: 14px;
    }

    .info-row:last-child {
        border-bottom: none;
    }

    .info-label {
        color: #666;
    }

    .info-value {
        color: #263238;
        font-weight: 600;
    }

    /* =========================
       MAIN BUTTON
       ========================= */

    .primary-button {
        display: inline-block;
        background: #1976d2;
        color: white !important;
        text-decoration: none;
        padding: 12px 22px;
        border-radius: 6px;
        margin-top: 15px;
        font-size: 14px;
    }

    .primary-button:hover {
        background: #0d47a1;
    }

    /* =========================
       FOOTER
       ========================= */

    .home-footer {
        text-align: center;
        margin-top: 45px;
        padding-top: 20px;
        border-top: 1px solid #ddd;
        color: #888;
        font-size: 13px;
    }

    /* =========================
       TABLET
       ========================= */

    @media screen and (max-width: 950px) {

        .stats-grid {
            grid-template-columns: repeat(2, 1fr);
        }

        .action-grid {
            grid-template-columns: repeat(2, 1fr);
        }

        .info-panel {
            grid-template-columns: 1fr;
        }
    }

    /* =========================
       MOBILE
       ========================= */

    @media screen and (max-width: 600px) {

        .home-page {
            width: 95%;
            margin-top: 20px;
        }

        .hero-section {
            padding: 30px 22px;
        }

        .hero-section h1 {
            font-size: 25px;
        }

        .hero-section p {
            font-size: 14px;
        }

        .stats-grid {
            grid-template-columns: 1fr;
        }

        .action-grid {
            grid-template-columns: 1fr;
        }

        .stat-card,
        .action-card,
        .info-box {
            padding: 20px;
        }
    }

</style>


<div class="home-page">

    <!-- HERO -->

    <div class="hero-section">

        <h1>Investor Management System</h1>

        <p>
            Manage investor information, BO accounts,
            investment records and investor registration
            from one centralized system.
        </p>

        <asp:HyperLink
            ID="btnOpenInvestors"
            runat="server"
            NavigateUrl="~/Investors.aspx"
            Text="Open Investor Register"
            CssClass="primary-button">
        </asp:HyperLink>

    </div>


    <!-- STATISTICS -->

    <div class="section-title">
        Investor Overview
    </div>

    <div class="stats-grid">

        <div class="stat-card">

            <div class="stat-title">
                Total Investors
            </div>

            <div class="stat-number">
                <asp:Label
                    ID="lblTotalInvestors"
                    runat="server"
                    Text="0">
                </asp:Label>
            </div>

        </div>


        <div class="stat-card green">

            <div class="stat-title">
                Active Investors
            </div>

            <div class="stat-number">
                <asp:Label
                    ID="lblActiveInvestors"
                    runat="server"
                    Text="0">
                </asp:Label>
            </div>

        </div>


        <div class="stat-card orange">

            <div class="stat-title">
                BO Accounts
            </div>

            <div class="stat-number">
                <asp:Label
                    ID="lblBOAccounts"
                    runat="server"
                    Text="0">
                </asp:Label>
            </div>

        </div>


        <div class="stat-card purple">

            <div class="stat-title">
                Total Investment
            </div>

            <div class="stat-number">
                ৳
                <asp:Label
                    ID="lblTotalInvestment"
                    runat="server"
                    Text="0">
                </asp:Label>
            </div>

        </div>

    </div>


    <!-- QUICK ACTIONS -->

    <div class="section-title">
        Quick Actions
    </div>

    <div class="action-grid">


        <asp:HyperLink
            ID="lnkInvestorRegister"
            runat="server"
            NavigateUrl="~/Investors.aspx"
            CssClass="action-card">

            <div class="action-icon">
                IR
            </div>

            <h3>Investor Register</h3>

            <p>
                View and manage all registered investors.
            </p>

        </asp:HyperLink>


        <asp:HyperLink
            ID="lnkAddInvestor"
            runat="server"
            NavigateUrl="~/Investors.aspx"
            CssClass="action-card">

            <div class="action-icon">
                +
            </div>

            <h3>Add Investor</h3>

            <p>
                Register a new investor in the system.
            </p>

        </asp:HyperLink>


        <asp:HyperLink
            ID="lnkSearchInvestor"
            runat="server"
            NavigateUrl="~/Investors.aspx"
            CssClass="action-card">

            <div class="action-icon">
                S
            </div>

            <h3>Search Investor</h3>

            <p>
                Search investor records using investor information.
            </p>

        </asp:HyperLink>


        <asp:HyperLink
            ID="lnkBOAccounts"
            runat="server"
            NavigateUrl="~/Investors.aspx"
            CssClass="action-card">

            <div class="action-icon">
                BO
            </div>

            <h3>BO Accounts</h3>

            <p>
                View investor BO account information.
            </p>

        </asp:HyperLink>


        <asp:HyperLink
            ID="lnkReports"
            runat="server"
            NavigateUrl="~/Investors.aspx"
            CssClass="action-card">

            <div class="action-icon">
                R
            </div>

            <h3>Investor Reports</h3>

            <p>
                Review investor statistics and management reports.
            </p>

        </asp:HyperLink>


        <asp:HyperLink
            ID="lnkManagement"
            runat="server"
            NavigateUrl="~/Investors.aspx"
            CssClass="action-card">

            <div class="action-icon">
                M
            </div>

            <h3>Investor Management</h3>

            <p>
                Update, edit and maintain investor records.
            </p>

        </asp:HyperLink>

    </div>


    <!-- SYSTEM INFORMATION -->

    <div class="info-panel">

        <div class="info-box">

            <h3>Investor Management</h3>

            <p style="color:#666; line-height:1.7; margin-top:0;">

                InvestorDB provides a centralized platform for
                maintaining investor information for a stock broker
                house. Investor registration, BO account information,
                contact details and investment records can be managed
                from the system.

            </p>

            <asp:HyperLink
                ID="btnManageInvestors"
                runat="server"
                NavigateUrl="~/Investors.aspx"
                Text="Manage Investors"
                CssClass="primary-button">
            </asp:HyperLink>

        </div>


        <div class="info-box">

            <h3>System Summary</h3>

            <div class="info-row">

                <span class="info-label">
                    System
                </span>

                <span class="info-value">
                    InvestorDB
                </span>

            </div>

            <div class="info-row">

                <span class="info-label">
                    Module
                </span>

                <span class="info-value">
                    Investor Management
                </span>

            </div>

            <div class="info-row">

                <span class="info-label">
                    Market
                </span>

                <span class="info-value">
                    Bangladesh
                </span>

            </div>

            <div class="info-row">

                <span class="info-label">
                    Status
                </span>

                <span class="info-value">
                    Active
                </span>

            </div>

        </div>

    </div>


    <!-- FOOTER -->

    <div class="home-footer">

        InvestorDB &copy;
        <%= DateTime.Now.Year %>
        | Investor Management System

    </div>

</div>

</asp:Content>