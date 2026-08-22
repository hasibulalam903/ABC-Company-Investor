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

    .users-panel {
        padding: 30px;
        background: #f5f7fb;
        min-height: 80vh;
        box-sizing: border-box;
    }

    .users-header {
        margin-bottom: 25px;
    }

    .users-header h1 {
        margin: 0;
        color: #17365d;
        font-size: 30px;
    }

    .users-header p {
        margin-top: 6px;
        color: #6b7280;
    }

    /* ================================
       STATISTICS
       ================================ */

    .statistics-grid {
        display: grid;
        grid-template-columns: repeat(4, 1fr);
        gap: 18px;
        margin-bottom: 25px;
    }

    .stat-card {
        background: #ffffff;
        padding: 22px;
        border-radius: 10px;
        box-shadow: 0 3px 15px rgba(0,0,0,0.08);
    }

    .stat-title {
        color: #6b7280;
        font-size: 14px;
        margin-bottom: 8px;
    }

    .stat-number {
        color: #17365d;
        font-size: 28px;
        font-weight: bold;
    }

    /* ================================
       MESSAGE
       ================================ */

    .message {
        display: block;
        padding: 12px;
        margin-bottom: 20px;
        border-radius: 6px;
    }

    .success-message {
        color: #0f5132;
        background: #d1e7dd;
        border: 1px solid #badbcc;
    }

    .error-message {
        color: #842029;
        background: #f8d7da;
        border: 1px solid #f5c2c7;
    }

    /* ================================
       SEARCH
       ================================ */

    .search-card {
        background: #ffffff;
        padding: 22px;
        border-radius: 10px;
        box-shadow: 0 3px 15px rgba(0,0,0,0.08);
        margin-bottom: 20px;
    }

    .search-grid {
        display: grid;
        grid-template-columns: 2fr 1fr 1fr auto auto auto auto;
        gap: 12px;
        align-items: end;
    }

    .form-group {
        display: flex;
        flex-direction: column;
    }

    .form-group label {
        font-size: 13px;
        font-weight: bold;
        color: #374151;
        margin-bottom: 6px;
    }

    .form-control {
        width: 100%;
        height: 40px;
        border: 1px solid #d1d5db;
        border-radius: 6px;
        padding: 8px 10px;
        box-sizing: border-box;
    }

    .form-control:focus {
        outline: none;
        border-color: #2563eb;
    }

    /* ================================
       TOP BUTTONS
       ================================ */

    .main-button {
        height: 40px;
        padding: 0 14px;
        border: none;
        border-radius: 6px;
        cursor: pointer;
        font-weight: bold;
        white-space: nowrap;
    }

    .search-button {
        background: #0d6efd;
        color: white;
    }

    .reset-button {
        background: #6c757d;
        color: white;
    }

    .excel-all-button {
        background: #198754;
        color: white;
    }

    .pdf-all-button {
        background: #dc3545;
        color: white;
    }

    /* ================================
       TABLE
       ================================ */

    .table-card {
        background: #ffffff;
        padding: 22px;
        border-radius: 10px;
        box-shadow: 0 3px 15px rgba(0,0,0,0.08);
        overflow-x: auto;
    }

    .table-header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 15px;
    }

    .table-header h2 {
        margin: 0;
        color: #17365d;
        font-size: 20px;
    }

    .user-count {
        color: #6b7280;
        font-size: 14px;
    }

    .users-grid {
        width: 100%;
        border-collapse: collapse;
    }

    .users-grid th {
        background: #17365d;
        color: white;
        padding: 12px;
        text-align: left;
        white-space: nowrap;
    }

    .users-grid td {
        padding: 12px;
        border-bottom: 1px solid #e5e7eb;
        vertical-align: middle;
    }

    .users-grid tr:hover td {
        background: #f8fafc;
    }

    /* ================================
       STATUS
       ================================ */

    .active-status,
    .inactive-status {
        display: inline-block;
        padding: 5px 9px;
        border-radius: 20px;
        font-size: 12px;
        font-weight: bold;
    }

    .active-status {
        background: #d1e7dd;
        color: #0f5132;
    }

    .inactive-status {
        background: #f8d7da;
        color: #842029;
    }

    /* ================================
       ROW BUTTONS
       ================================ */

    .row-button {
        border: none;
        border-radius: 5px;
        padding: 7px 10px;
        margin: 2px;
        color: white;
        cursor: pointer;
        font-size: 12px;
        font-weight: bold;
        white-space: nowrap;
    }

    .activate-button {
        background: #198754;
    }

    .deactivate-button {
        background: #fd7e14;
    }

    .pdf-button {
        background: #dc3545;
    }

    .excel-button {
        background: #198754;
    }

    .delete-button {
        background: #dc3545;
    }

    /* ================================
       PAGINATION
       ================================ */

    .pagination-container {
        display: flex;
        justify-content: center;
        align-items: center;
        gap: 15px;
        margin-top: 20px;
    }

    .pagination-button {
        border: none;
        background: #17365d;
        color: white;
        padding: 9px 18px;
        border-radius: 5px;
        cursor: pointer;
        font-weight: bold;
    }

    .pagination-button:disabled {
        background: #cccccc;
        cursor: not-allowed;
    }

    .page-info {
        font-weight: bold;
        color: #374151;
    }

    /* ================================
       RESPONSIVE
       ================================ */

    @media screen and (max-width: 1100px) {

        .statistics-grid {
            grid-template-columns: repeat(2, 1fr);
        }

        .search-grid {
            grid-template-columns: 1fr 1fr;
        }

    }

    @media screen and (max-width: 600px) {

        .users-panel {
            padding: 15px;
        }

        .statistics-grid {
            grid-template-columns: 1fr;
        }

        .search-grid {
            grid-template-columns: 1fr;
        }

    }

</style>


<div class="users-panel">


    <!-- ================================================
         HEADER
         ================================================ -->

    <div class="users-header">

        <h1>
            Users Management
        </h1>

        <p>
            Manage registered users from the administration dashboard.
        </p>

    </div>


    <!-- ================================================
         STATISTICS
         ================================================ -->

    <div class="statistics-grid">


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


    <!-- ================================================
         MESSAGE
         ================================================ -->

    <asp:Label
        ID="lblMessage"
        runat="server"
        Visible="False"
        CssClass="message">
    </asp:Label>


    <!-- ================================================
         SEARCH / FILTER
         ================================================ -->

    <div class="search-card">

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
                    placeholder="Name, Email or Mobile">
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


            <!-- SEARCH -->

            <asp:Button
                ID="btnSearch"
                runat="server"
                Text="Search"
                CssClass="main-button search-button">
            </asp:Button>


            <!-- RESET -->

            <asp:Button
                ID="btnReset"
                runat="server"
                Text="Reset"
                CssClass="main-button reset-button">
            </asp:Button>


            <!-- ALL EXCEL -->

            <asp:Button
                ID="btnExportExcel"
                runat="server"
                Text="Export All Excel"
                CssClass="main-button excel-all-button">
            </asp:Button>


            <!-- ALL PDF -->

            <asp:Button
                ID="btnDownloadAll"
                runat="server"
                Text="Download All PDF"
                CssClass="main-button pdf-all-button">
            </asp:Button>


        </div>

    </div>


    <!-- ================================================
         USERS TABLE
         ================================================ -->

    <div class="table-card">


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
            DataKeyNames="UserID"
            CssClass="users-grid"
            GridLines="None"
            OnRowCommand="gvUsers_RowCommand">


            <Columns>



                <asp:BoundField
                    DataField="UserID"
                    HeaderText="ID">
                </asp:BoundField>


              

                <asp:BoundField
                    DataField="Name"
                    HeaderText="Name">
                </asp:BoundField>


           

                <asp:BoundField
                    DataField="Email"
                    HeaderText="Email">
                </asp:BoundField>


              

                <asp:BoundField
                    DataField="Mobile"
                    HeaderText="Mobile">
                </asp:BoundField>


            

                <asp:BoundField
                    DataField="Role"
                    HeaderText="Role">
                </asp:BoundField>


             

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
                            CssClass='<%# "row-button " & GetStatusButtonCss(Eval("Status")) %>'
                            CommandName="ToggleStatus"
                            CommandArgument='<%# Eval("UserID") %>'>
                        </asp:Button>



                        <asp:Button
                            ID="btnDownloadUser"
                            runat="server"
                            Text="Download PDF"
                            CssClass="row-button pdf-button"
                            CommandName="DownloadUser"
                            CommandArgument='<%# Eval("UserID") %>'>
                        </asp:Button>



                        <asp:Button
                            ID="btnExportUserExcel"
                            runat="server"
                            Text="Excel"
                            CssClass="row-button excel-button"
                            CommandName="ExportUserExcel"
                            CommandArgument='<%# Eval("UserID") %>'>
                        </asp:Button>


                    

                        <asp:Button
                            ID="btnDeleteUser"
                            runat="server"
                            Text="Delete"
                            CssClass="row-button delete-button"
                            CommandName="DeleteUser"
                            CommandArgument='<%# Eval("UserID") %>'
                            OnClientClick="return confirm('Are you sure you want to delete this user?');">
                        </asp:Button>


                    </ItemTemplate>

                </asp:TemplateField>


            </Columns>


            <EmptyDataTemplate>

                <div style="
                    text-align:center;
                    padding:30px;
                    color:#777;
                ">

                    No users found.

                </div>

            </EmptyDataTemplate>


        </asp:GridView>


   

        <div class="pagination-container">


            <asp:Button
                ID="btnPrevious"
                runat="server"
                Text="← Previous"
                CssClass="pagination-button">
            </asp:Button>


            <asp:Label
                ID="lblPageInfo"
                runat="server"
                Text="Page 1 of 1"
                CssClass="page-info">
            </asp:Label>


            <asp:Button
                ID="btnNext"
                runat="server"
                Text="Next →"
                CssClass="pagination-button">
            </asp:Button>


        </div>


    </div>


</div>

</asp:Content>