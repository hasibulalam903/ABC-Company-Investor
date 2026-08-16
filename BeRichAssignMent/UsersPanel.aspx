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
           USERS PANEL
           ========================================== */

        .users-panel {
            padding: 35px;
            background: #f5f7fb;
            min-height: 80vh;
            box-sizing: border-box;
        }


        /* ==========================================
           HEADER
           ========================================== */

        .users-header {
            margin-bottom: 30px;
        }

        .users-header h1 {
            margin: 0;
            color: #111827;
            font-size: 32px;
            font-weight: 700;
        }

        .users-header p {
            margin-top: 8px;
            color: #6b7280;
            font-size: 15px;
        }


        /* ==========================================
           STATISTICS
           ========================================== */

        .statistics-grid {
            display: grid;
            grid-template-columns: repeat(4, 1fr);
            gap: 20px;
            margin-bottom: 30px;
        }

        .stat-card {
            background: #ffffff;
            border: 1px solid #e5e7eb;
            border-radius: 15px;
            padding: 25px;

            box-shadow:
                0 5px 20px rgba(0, 0, 0, 0.06);
        }

        .stat-title {
            color: #6b7280;
            font-size: 14px;
            margin-bottom: 10px;
        }

        .stat-number {
            color: #111827;
            font-size: 30px;
            font-weight: 700;
        }


        /* ==========================================
           SEARCH CARD
           ========================================== */

        .search-card {
            background: #ffffff;
            border: 1px solid #e5e7eb;
            border-radius: 15px;
            padding: 25px;
            margin-bottom: 25px;

            box-shadow:
                0 5px 20px rgba(0, 0, 0, 0.06);
        }

        .search-title {
            margin: 0 0 20px 0;
            color: #111827;
            font-size: 20px;
            font-weight: 700;
        }

        .search-grid {
            display: grid;
            grid-template-columns: 2fr 1fr 1fr auto auto;
            gap: 15px;
            align-items: end;
        }


        /* ==========================================
           FORM
           ========================================== */

        .form-group {
            display: flex;
            flex-direction: column;
        }

        .form-group label {
            margin-bottom: 7px;
            color: #374151;
            font-size: 13px;
            font-weight: 600;
        }

        .form-control {
            width: 100%;
            height: 42px;

            padding: 8px 12px;

            border: 1px solid #d1d5db;
            border-radius: 8px;

            background: #ffffff;

            font-size: 14px;

            box-sizing: border-box;
        }

        .form-control:focus {
            outline: none;

            border-color: #2563eb;

            box-shadow:
                0 0 0 3px rgba(37, 99, 235, 0.10);
        }


        /* ==========================================
           BUTTONS
           ========================================== */

        .btn {
            height: 42px;

            padding: 0 20px;

            border: none;
            border-radius: 8px;

            font-size: 14px;
            font-weight: 600;

            cursor: pointer;
        }

        .btn-search {
            background: #2563eb;
            color: #ffffff;
        }

        .btn-search:hover {
            background: #1d4ed8;
        }

        .btn-reset {
            background: #6b7280;
            color: #ffffff;
        }

        .btn-reset:hover {
            background: #4b5563;
        }


        /* ==========================================
           MESSAGE
           ========================================== */

        .message {
            display: block;

            padding: 12px 15px;

            margin-bottom: 20px;

            border-radius: 8px;

            border: 1px solid transparent;

            font-size: 14px;
        }

        .success-message {
            background: #d1e7dd;
            color: #146c43;
            border-color: #badbcc;
        }

        .error-message {
            background: #f8d7da;
            color: #b02a37;
            border-color: #f5c2c7;
        }


        /* ==========================================
           USERS TABLE
           ========================================== */

        .users-table-card {
            background: #ffffff;

            border: 1px solid #e5e7eb;

            border-radius: 15px;

            padding: 25px;

            box-shadow:
                0 5px 20px rgba(0, 0, 0, 0.06);

            overflow-x: auto;
        }

        .table-header {
            display: flex;

            justify-content: space-between;

            align-items: center;

            margin-bottom: 20px;
        }

        .table-header h2 {
            margin: 0;

            color: #111827;

            font-size: 20px;

            font-weight: 700;
        }

        .user-count {
            color: #6b7280;

            font-size: 14px;

            font-weight: 600;
        }


        /* ==========================================
           GRIDVIEW
           ========================================== */

        .users-grid {
            width: 100%;

            border-collapse: collapse;

            font-size: 14px;
        }

        .users-grid th {
            background: #f3f4f6;

            color: #374151;

            padding: 14px 12px;

            text-align: left;

            font-weight: 700;

            border-bottom: 1px solid #e5e7eb;

            white-space: nowrap;
        }

        .users-grid td {
            padding: 14px 12px;

            color: #4b5563;

            border-bottom: 1px solid #e5e7eb;

            vertical-align: middle;
        }

        .users-grid tr:hover td {
            background: #f9fafb;
        }


        /* ==========================================
           STATUS
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

        .status-button {
            border: none;

            border-radius: 7px;

            padding: 8px 14px;

            font-size: 12px;

            font-weight: 600;

            cursor: pointer;
        }

        .activate-button {
            background: #dcfce7;

            color: #166534;
        }

        .activate-button:hover {
            background: #bbf7d0;
        }

        .deactivate-button {
            background: #fee2e2;

            color: #991b1b;
        }

        .deactivate-button:hover {
            background: #fecaca;
        }


        /* ==========================================
           PAGINATION
           ========================================== */

        .users-grid a {
            color: #2563eb;

            text-decoration: none;

            font-weight: 600;
        }

        .users-grid a:hover {
            text-decoration: underline;
        }


        /* ==========================================
           RESPONSIVE
           ========================================== */

        @media (max-width: 1000px) {

            .statistics-grid {
                grid-template-columns: repeat(2, 1fr);
            }

            .search-grid {
                grid-template-columns: 1fr 1fr;
            }

        }


        @media (max-width: 700px) {

            .users-panel {
                padding: 20px;
            }

            .statistics-grid {
                grid-template-columns: 1fr;
            }

            .search-grid {
                grid-template-columns: 1fr;
            }

            .table-header {
                flex-direction: column;

                align-items: flex-start;

                gap: 8px;
            }

        }

    </style>


    <!-- ==========================================
         USERS PANEL
         ========================================== -->

    <div class="users-panel">


        <!-- ======================================
             HEADER
             ====================================== -->

        <div class="users-header">

            <h1>
                Users Panel
            </h1>

            <p>
                View, search and manage all registered users.
            </p>

        </div>


        <!-- ======================================
             STATISTICS
             ====================================== -->

        <div class="statistics-grid">


            <!-- TOTAL USERS -->

            <div class="stat-card">

                <div class="stat-title">
                    Total Users
                </div>

                <asp:Label
                    ID="lblTotalUsers"
                    runat="server"
                    CssClass="stat-number"
                    Text="0">
                </asp:Label>

            </div>


            <!-- ACTIVE USERS -->

            <div class="stat-card">

                <div class="stat-title">
                    Active Users
                </div>

                <asp:Label
                    ID="lblActiveUsers"
                    runat="server"
                    CssClass="stat-number"
                    Text="0">
                </asp:Label>

            </div>


            <!-- INACTIVE USERS -->

            <div class="stat-card">

                <div class="stat-title">
                    Inactive Users
                </div>

                <asp:Label
                    ID="lblInactiveUsers"
                    runat="server"
                    CssClass="stat-number"
                    Text="0">
                </asp:Label>

            </div>


            <!-- ADMIN USERS -->

            <div class="stat-card">

                <div class="stat-title">
                    Admin Users
                </div>

                <asp:Label
                    ID="lblAdminUsers"
                    runat="server"
                    CssClass="stat-number"
                    Text="0">
                </asp:Label>

            </div>


        </div>


        <!-- ======================================
             MESSAGE
             ====================================== -->

        <asp:Label
            ID="lblMessage"
            runat="server"
            CssClass="message"
            Visible="False">
        </asp:Label>


        <!-- ======================================
             SEARCH & FILTER
             ====================================== -->

        <div class="search-card">

            <h2 class="search-title">
                Search &amp; Filter Users
            </h2>


            <div class="search-grid">


                <!-- SEARCH -->

                <div class="form-group">

                    <label>
                        Search
                    </label>

                    <asp:TextBox
                        ID="txtSearch"
                        runat="server"
                        CssClass="form-control"
                        placeholder="Search by name, email or mobile">
                    </asp:TextBox>

                </div>


                <!-- ROLE -->

                <div class="form-group">

                    <label>
                        Role
                    </label>

                    <asp:DropDownList
                        ID="ddlRole"
                        runat="server"
                        CssClass="form-control">

                        <asp:ListItem
                            Text="All Roles"
                            Value="">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="Admin"
                            Value="Admin">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="User"
                            Value="User">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="Investor"
                            Value="Investor">
                        </asp:ListItem>

                    </asp:DropDownList>

                </div>


                <!-- STATUS -->

                <div class="form-group">

                    <label>
                        Status
                    </label>

                    <asp:DropDownList
                        ID="ddlStatus"
                        runat="server"
                        CssClass="form-control">

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

                </div>


                <!-- SEARCH BUTTON -->

                <div class="form-group">

                    <asp:Button
                        ID="btnSearch"
                        runat="server"
                        Text="Search"
                        CssClass="btn btn-search">
                    </asp:Button>

                </div>


                <!-- RESET BUTTON -->

                <div class="form-group">

                    <asp:Button
                        ID="btnReset"
                        runat="server"
                        Text="Reset"
                        CssClass="btn btn-reset">
                    </asp:Button>

                </div>


            </div>

        </div>


        <!-- ======================================
             USERS TABLE
             ====================================== -->

        <div class="users-table-card">


            <div class="table-header">

                <h2>
                    Registered Users
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
                PageSize="10"
                DataKeyNames="UserID"
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


                   

                    <asp:BoundField
                        DataField="Role"
                        HeaderText="Role" />


                 

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
                                ID="btnToggleStatus"
                                runat="server"
                                Text='<%# GetStatusButtonText(Eval("Status")) %>'
                                CssClass='<%# "status-button " & GetStatusButtonCss(Eval("Status")) %>'
                                CommandName="ToggleStatus"
                                CommandArgument='<%# Eval("UserID") %>'>
                            </asp:Button>

                        </ItemTemplate>

                    </asp:TemplateField>


                </Columns>


                <EmptyDataTemplate>

                    <div style="
                        padding: 30px;
                        text-align: center;
                        color: #6b7280;
                    ">

                        No users found.

                    </div>

                </EmptyDataTemplate>

            </asp:GridView>


        </div>


    </div>

</asp:Content>