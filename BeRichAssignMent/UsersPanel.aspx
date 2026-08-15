<%@ Page Language="VB"
    MasterPageFile="~/Site.master"
    AutoEventWireup="false"
    CodeFile="UsersPanel.aspx.vb"
    Inherits="UsersPanel" %>

<asp:Content
    ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <style type="text/css">

        /* ==========================================
           USERS PAGE
           ========================================== */

        .users-page {
            padding: 35px;
            background: #f5f7fb;
            min-height: 80vh;
        }


        /* ==========================================
           HEADER
           ========================================== */

        .users-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 30px;
        }

        .users-header h1 {
            margin: 0;
            font-size: 30px;
            color: #111827;
        }

        .users-header p {
            margin: 7px 0 0 0;
            color: #6b7280;
            font-size: 14px;
        }

        .back-button {
            display: inline-block;
            text-decoration: none;
            background: #111827;
            color: white;
            padding: 11px 18px;
            border-radius: 7px;
            font-size: 14px;
            font-weight: 600;
        }

        .back-button:hover {
            background: #374151;
        }


        /* ==========================================
           STATISTICS
           ========================================== */

        .statistics {
            display: grid;
            grid-template-columns: repeat(4, 1fr);
            gap: 18px;
            margin-bottom: 25px;
        }

        .stat-card {
            background: white;
            border-radius: 12px;
            padding: 22px;
            border: 1px solid #e5e7eb;
            box-shadow: 0 4px 15px rgba(0,0,0,0.06);
        }

        .stat-title {
            color: #6b7280;
            font-size: 14px;
            margin-bottom: 10px;
        }

        .stat-value {
            color: #111827;
            font-size: 30px;
            font-weight: 700;
        }

        .total-card {
            border-left: 5px solid #2563eb;
        }

        .active-card {
            border-left: 5px solid #16a34a;
        }

        .inactive-card {
            border-left: 5px solid #dc2626;
        }

        .admin-card {
            border-left: 5px solid #9333ea;
        }


        /* ==========================================
           MESSAGE
           ========================================== */

        .message {
            display: block;
            padding: 12px 15px;
            border-radius: 7px;
            margin-bottom: 20px;
            font-size: 14px;
            font-weight: 600;
        }

        .success-message {
            background: #dcfce7;
            color: #166534;
        }

        .error-message {
            background: #fee2e2;
            color: #991b1b;
        }


        /* ==========================================
           SEARCH PANEL
           ========================================== */

        .search-panel {
            background: white;
            border-radius: 12px;
            padding: 20px;
            margin-bottom: 25px;
            border: 1px solid #e5e7eb;
            box-shadow: 0 4px 15px rgba(0,0,0,0.06);
        }

        .search-title {
            margin: 0 0 15px 0;
            color: #111827;
            font-size: 18px;
        }

        .search-row {
            display: flex;
            gap: 10px;
            flex-wrap: wrap;
        }

        .search-input {
            flex: 1;
            min-width: 250px;
            height: 42px;
            padding: 0 12px;
            border: 1px solid #d1d5db;
            border-radius: 7px;
            font-size: 14px;
            box-sizing: border-box;
        }

        .filter-select {
            min-width: 140px;
            height: 42px;
            padding: 0 10px;
            border: 1px solid #d1d5db;
            border-radius: 7px;
            background: white;
            font-size: 14px;
        }

        .search-button {
            height: 42px;
            padding: 0 20px;
            border: none;
            border-radius: 7px;
            background: #2563eb;
            color: white;
            font-weight: 600;
            cursor: pointer;
        }

        .search-button:hover {
            background: #1d4ed8;
        }

        .reset-button {
            height: 42px;
            padding: 0 20px;
            border: none;
            border-radius: 7px;
            background: #6b7280;
            color: white;
            font-weight: 600;
            cursor: pointer;
        }

        .reset-button:hover {
            background: #4b5563;
        }


        /* ==========================================
           USERS TABLE
           ========================================== */

        .users-table-panel {
            background: white;
            border-radius: 12px;
            padding: 20px;
            border: 1px solid #e5e7eb;
            box-shadow: 0 4px 15px rgba(0,0,0,0.06);
            overflow-x: auto;
        }

        .table-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 18px;
        }

        .table-header h2 {
            margin: 0;
            font-size: 21px;
            color: #111827;
        }

        .user-count {
            color: #6b7280;
            font-size: 14px;
        }

        .users-grid {
            width: 100%;
            border-collapse: collapse;
            min-width: 850px;
        }

        .users-grid th {
            background: #111827;
            color: white;
            padding: 13px 10px;
            text-align: left;
            font-size: 13px;
        }

        .users-grid td {
            padding: 13px 10px;
            border-bottom: 1px solid #e5e7eb;
            color: #374151;
            font-size: 14px;
        }

        .users-grid tr:hover td {
            background: #f9fafb;
        }


        /* ==========================================
           ROLE BADGE
           ========================================== */

        .role-badge {
            display: inline-block;
            padding: 5px 10px;
            border-radius: 20px;
            background: #dbeafe;
            color: #1e40af;
            font-size: 12px;
            font-weight: 600;
        }


        /* ==========================================
           STATUS BADGE
           ========================================== */

        .active-status {
            display: inline-block;
            padding: 5px 10px;
            border-radius: 20px;
            background: #dcfce7;
            color: #166534;
            font-size: 12px;
            font-weight: 600;
        }

        .inactive-status {
            display: inline-block;
            padding: 5px 10px;
            border-radius: 20px;
            background: #fee2e2;
            color: #991b1b;
            font-size: 12px;
            font-weight: 600;
        }


        /* ==========================================
           STATUS BUTTON
           ========================================== */

        .activate-button {
            border: none;
            background: #dcfce7;
            color: #166534;
            padding: 7px 12px;
            border-radius: 6px;
            cursor: pointer;
            font-size: 12px;
            font-weight: 600;
        }

        .deactivate-button {
            border: none;
            background: #fee2e2;
            color: #991b1b;
            padding: 7px 12px;
            border-radius: 6px;
            cursor: pointer;
            font-size: 12px;
            font-weight: 600;
        }


        /* ==========================================
           PAGING
           ========================================== */

        .users-grid a,
        .users-grid span {
            padding: 6px 10px;
            margin: 2px;
        }


        /* ==========================================
           RESPONSIVE
           ========================================== */

        @media (max-width: 900px) {

            .statistics {
                grid-template-columns: repeat(2, 1fr);
            }

        }

        @media (max-width: 600px) {

            .users-page {
                padding: 20px;
            }

            .users-header {
                flex-direction: column;
                align-items: flex-start;
                gap: 15px;
            }

            .statistics {
                grid-template-columns: 1fr;
            }

        }

    </style>


    <div class="users-page">


        <!-- ==========================================
             HEADER
             ========================================== -->

        <div class="users-header">

            <div>

                <h1>
                    👥 Users Panel
                </h1>

                <p>
                    View and manage all registered users
                </p>

            </div>

            <a
                href="AdminDashboard.aspx"
                class="back-button">

                ← Admin Dashboard

            </a>

        </div>


        <!-- ==========================================
             MESSAGE
             ========================================== -->

        <asp:Label
            ID="lblMessage"
            runat="server"
            Visible="false">
        </asp:Label>


        <!-- ==========================================
             STATISTICS
             ========================================== -->

        <div class="statistics">


            <!-- TOTAL -->

            <div class="stat-card total-card">

                <div class="stat-title">
                    Total Users
                </div>

                <div class="stat-value">

                    <asp:Label
                        ID="lblTotalUsers"
                        runat="server"
                        Text="0">
                    </asp:Label>

                </div>

            </div>


            <!-- ACTIVE -->

            <div class="stat-card active-card">

                <div class="stat-title">
                    Active Users
                </div>

                <div class="stat-value">

                    <asp:Label
                        ID="lblActiveUsers"
                        runat="server"
                        Text="0">
                    </asp:Label>

                </div>

            </div>


            <!-- INACTIVE -->

            <div class="stat-card inactive-card">

                <div class="stat-title">
                    Inactive Users
                </div>

                <div class="stat-value">

                    <asp:Label
                        ID="lblInactiveUsers"
                        runat="server"
                        Text="0">
                    </asp:Label>

                </div>

            </div>


            <!-- ADMIN -->

            <div class="stat-card admin-card">

                <div class="stat-title">
                    Admin Users
                </div>

                <div class="stat-value">

                    <asp:Label
                        ID="lblAdminUsers"
                        runat="server"
                        Text="0">
                    </asp:Label>

                </div>

            </div>

        </div>


        <!-- ==========================================
             SEARCH / FILTER
             ========================================== -->

        <div class="search-panel">

            <h2 class="search-title">
                Search Users
            </h2>

            <div class="search-row">


                <asp:TextBox
                    ID="txtSearch"
                    runat="server"
                    CssClass="search-input"
                    placeholder="Search by name, email or mobile">
                </asp:TextBox>


                <asp:DropDownList
                    ID="ddlRole"
                    runat="server"
                    CssClass="filter-select">

                    <asp:ListItem
                        Text="All Roles"
                        Value="">
                    </asp:ListItem>

                    <asp:ListItem
                        Text="User"
                        Value="User">
                    </asp:ListItem>

                    <asp:ListItem
                        Text="Admin"
                        Value="Admin">
                    </asp:ListItem>

                    <asp:ListItem
                        Text="Investor"
                        Value="Investor">
                    </asp:ListItem>

                </asp:DropDownList>


                <asp:DropDownList
                    ID="ddlStatus"
                    runat="server"
                    CssClass="filter-select">

                    <asp:ListItem
                        Text="All Status"
                        Value="">
                    </asp:ListItem>

                    <asp:ListItem
                        Text="Active"
                        Value="Active">
                    </asp:ListItem>

                    <asp:ListItem
                        Text="Inactive"
                        Value="Inactive">
                    </asp:ListItem>

                </asp:DropDownList>


                <asp:Button
                    ID="btnSearch"
                    runat="server"
                    Text="Search"
                    CssClass="search-button"
                    OnClick="btnSearch_Click" />


                <asp:Button
                    ID="btnReset"
                    runat="server"
                    Text="Reset"
                    CssClass="reset-button"
                    OnClick="btnReset_Click" />

            </div>

        </div>


        <!-- ==========================================
             USERS TABLE
             ========================================== -->

        <div class="users-table-panel">


            <div class="table-header">

                <h2>
                    All Users
                </h2>

                <asp:Label
                    ID="lblUserCount"
                    runat="server"
                    CssClass="user-count"
                    Text="0 users">
                </asp:Label>

            </div>


            <asp:GridView
                ID="gvUsers"
                runat="server"
                AutoGenerateColumns="False"
                CssClass="users-grid"
                GridLines="None"
                AllowPaging="True"
                PageSize="15"
                DataKeyNames="UserID"
                EmptyDataText="No users found."
                OnPageIndexChanging="gvUsers_PageIndexChanging"
                OnRowCommand="gvUsers_RowCommand">


                <Columns>


                  

                    <asp:BoundField
                        DataField="UserID"
                        HeaderText="ID" />


                   

                    <asp:BoundField
                        DataField="Name"
                        HeaderText="Name" />


                   

                    <asp:BoundField
                        DataField="Email"
                        HeaderText="Email" />


               

                    <asp:BoundField
                        DataField="Mobile"
                        HeaderText="Mobile" />


                   

                    <asp:TemplateField
                        HeaderText="Role">

                        <ItemTemplate>

                            <span class="role-badge">
                                <%# Eval("Role") %>
                            </span>

                        </ItemTemplate>

                    </asp:TemplateField>


                 

                    <asp:TemplateField
                        HeaderText="Status">

                        <ItemTemplate>

                            <asp:Label
                                ID="lblStatus"
                                runat="server"
                                Text='<%# Eval("Status") %>'
                                CssClass='<%# GetStatusCss(Eval("Status")) %>'>
                            </asp:Label>

                        </ItemTemplate>

                    </asp:TemplateField>


                   

                    <asp:TemplateField
                        HeaderText="Action">

                        <ItemTemplate>

                            <asp:Button
                                ID="btnStatus"
                                runat="server"
                                CommandName="ToggleStatus"
                                CommandArgument='<%# Eval("UserID") %>'
                                Text='<%# GetStatusButtonText(Eval("Status")) %>'
                                CssClass='<%# GetStatusButtonCss(Eval("Status")) %>'
                                OnClientClick="return confirm('Are you sure you want to change this user status?');" />

                        </ItemTemplate>

                    </asp:TemplateField>


                </Columns>

            </asp:GridView>

        </div>


    </div>

</asp:Content>

