<%@ Page Language="VB"
    MasterPageFile="~/Site.master"
    AutoEventWireup="false"
    CodeFile="Investors.aspx.vb"
    Inherits="Investors" %>

<asp:Content
    ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <style type="text/css">

        /* ==========================================
           MAIN CONTAINER
           ========================================== */

        .investors-container {
            width: 100%;
            max-width: 1450px;
            margin: 0 auto;
            padding: 30px 20px 50px 20px;
            box-sizing: border-box;
        }


        /* ==========================================
           PAGE HEADER
           ========================================== */

        .page-header {
            margin-bottom: 25px;
        }

        .page-header h1 {
            margin: 0;
            font-size: 30px;
            color: #111827;
        }

        .page-header p {
            margin: 8px 0 0 0;
            color: #6b7280;
            font-size: 15px;
        }


        /* ==========================================
           MESSAGE
           ========================================== */

        .message {
            display: block;
            padding: 12px 15px;
            margin-bottom: 20px;

            border: 1px solid;
            border-radius: 6px;
        }


        /* ==========================================
           STATISTICS CARDS
           ========================================== */

        .stats-cards {
            display: grid;

            grid-template-columns:
                repeat(2, minmax(250px, 1fr));

            gap: 20px;

            margin-bottom: 25px;
        }


        .stat-card {
            background: white;

            border-radius: 10px;

            padding: 24px;

            box-shadow:
                0 2px 10px rgba(0, 0, 0, 0.07);

            border-left: 5px solid #2563eb;
        }


        .stat-card h3 {
            margin: 0;

            color: #6b7280;

            font-size: 15px;

            font-weight: normal;
        }


        .stat-number {
            margin-top: 10px;

            color: #111827;

            font-size: 32px;

            font-weight: bold;
        }


        /* ==========================================
           STATISTICS TABLES
           ========================================== */

        .statistics-grid {
            display: grid;

            grid-template-columns:
                repeat(2, minmax(300px, 1fr));

            gap: 20px;

            margin-bottom: 25px;
        }


        .statistics-card {
            background: white;

            border-radius: 10px;

            padding: 22px;

            box-shadow:
                0 2px 10px rgba(0, 0, 0, 0.07);
        }


        .statistics-card h2 {
            margin: 0 0 18px 0;

            font-size: 20px;

            color: #111827;
        }


        .statistics-table {
            width: 100%;

            border-collapse: collapse;
        }


        .statistics-table th {
            background: #1f2937;

            color: white;

            padding: 11px;

            text-align: left;

            font-size: 14px;
        }


        .statistics-table td {
            padding: 11px;

            border-bottom:
                1px solid #e5e7eb;

            font-size: 14px;
        }


        .statistics-table tr:hover {
            background: #f9fafb;
        }


        .count-cell {
            font-weight: bold;

            color: #2563eb;

            text-align: center;
        }


        /* ==========================================
           GENERAL CARD
           ========================================== */

        .card {
            background: white;

            border-radius: 10px;

            padding: 25px;

            margin-bottom: 25px;

            box-shadow:
                0 2px 10px rgba(0, 0, 0, 0.07);
        }


        .card-title {
            margin: 0 0 20px 0;

            font-size: 21px;

            color: #111827;
        }


        /* ==========================================
           FORM
           ========================================== */

        .form-grid {
            display: grid;

            grid-template-columns:
                repeat(3, minmax(200px, 1fr));

            gap: 18px;
        }


        .form-group {
            display: flex;

            flex-direction: column;
        }


        .form-group label {
            margin-bottom: 7px;

            font-size: 14px;

            font-weight: bold;

            color: #374151;
        }


        .form-control {
            width: 100%;

            height: 42px;

            padding: 8px 12px;

            border:
                1px solid #d1d5db;

            border-radius: 6px;

            background: white;

            font-size: 14px;

            box-sizing: border-box;
        }


        .form-control:focus {
            outline: none;

            border-color: #2563eb;

            box-shadow:
                0 0 0 2px rgba(37, 99, 235, 0.10);
        }


        /* ==========================================
           BUTTONS
           ========================================== */

        .button-row {
            display: flex;

            gap: 10px;

            margin-top: 20px;
        }


        .btn {
            border: none;

            border-radius: 6px;

            padding: 10px 18px;

            cursor: pointer;

            font-size: 14px;

            font-weight: bold;
        }


        .btn-primary {
            background: #2563eb;

            color: white;
        }


        .btn-primary:hover {
            background: #1d4ed8;
        }


        .btn-secondary {
            background: #6b7280;

            color: white;
        }


        .btn-secondary:hover {
            background: #4b5563;
        }


        .btn-danger {
            background: #dc2626;

            color: white;
        }


        .btn-danger:hover {
            background: #b91c1c;
        }


        /* ==========================================
           SEARCH
           ========================================== */

        .search-grid {
            display: grid;

            grid-template-columns:
                2fr 1fr 1fr auto auto;

            gap: 10px;

            align-items: center;
        }


        /* ==========================================
           INVESTOR TABLE
           ========================================== */

        .table-wrapper {
            width: 100%;

            overflow-x: auto;
        }


        .investor-table {
            width: 100%;

            min-width: 900px;

            border-collapse: collapse;
        }


        .investor-table th {
            background: #111827;

            color: white;

            padding: 12px;

            text-align: left;

            font-size: 14px;

            white-space: nowrap;
        }


        .investor-table td {
            padding: 11px;

            border-bottom:
                1px solid #e5e7eb;

            font-size: 14px;

            white-space: nowrap;
        }


        .investor-table tr:hover {
            background: #f9fafb;
        }


        .investor-table .select-button {
            background: #2563eb;

            color: white;

            border: none;

            border-radius: 5px;

            padding: 7px 12px;

            cursor: pointer;
        }


        .investor-table .select-button:hover {
            background: #1d4ed8;
        }


        .delete-button {
            background: #dc2626;

            color: white;

            border: none;

            border-radius: 5px;

            padding: 7px 12px;

            cursor: pointer;
        }


        .delete-button:hover {
            background: #b91c1c;
        }


        /* ==========================================
           RESPONSIVE
           ========================================== */

        @media (max-width: 1000px) {

            .form-grid {
                grid-template-columns: 1fr 1fr;
            }

            .search-grid {
                grid-template-columns: 1fr 1fr;
            }

        }


        @media (max-width: 750px) {

            .investors-container {
                padding: 20px;
            }


            .stats-cards {
                grid-template-columns: 1fr;
            }


            .statistics-grid {
                grid-template-columns: 1fr;
            }


            .form-grid {
                grid-template-columns: 1fr;
            }


            .search-grid {
                grid-template-columns: 1fr;
            }


            .button-row {
                flex-direction: column;
            }


            .btn {
                width: 100%;
            }

        }

    </style>


    <!-- ==========================================
         INVESTORS PAGE
         ========================================== -->

    <div class="investors-container">


        <!-- ==========================================
             PAGE HEADER
             ========================================== -->

        <div class="page-header">

            <h1>
                Investor Management
            </h1>

            <p>
                Manage investors, investments,
                departments and designations.
            </p>

        </div>


        <!-- ==========================================
             MESSAGE
             ========================================== -->

        <asp:Label
            ID="lblMessage"
            runat="server"
            CssClass="message"
            Visible="false">
        </asp:Label>


        <!-- ==========================================
             STATISTICS CARDS
             ========================================== -->

        <div class="stats-cards">


            <!-- TOTAL INVESTORS -->

            <div class="stat-card">

                <h3>
                    Total Investors
                </h3>

                <div class="stat-number">

                    <asp:Label
                        ID="lblTotalInvestors"
                        runat="server"
                        Text="0">
                    </asp:Label>

                </div>

            </div>


            <!-- TOTAL INVESTMENT -->

            <div class="stat-card">

                <h3>
                    Total Investment
                </h3>

                <div class="stat-number">

                    <asp:Label
                        ID="lblTotalInvestment"
                        runat="server"
                        Text="0.00">
                    </asp:Label>

                </div>

            </div>


        </div>


        <!-- ==========================================
             DEPARTMENT / DESIGNATION STATISTICS
             ========================================== -->

        <div class="statistics-grid">


            <!-- DEPARTMENT-WISE -->

            <div class="statistics-card">

                <h2>
                    Department-wise Investor Count
                </h2>

                <div class="table-wrapper">

                    <asp:GridView
                        ID="gvDepartmentStats"
                        runat="server"
                        AutoGenerateColumns="False"
                        CssClass="statistics-table"
                        GridLines="None">

                        <Columns>

                            <asp:BoundField
                                DataField="Department"
                                HeaderText="Department" />

                            <asp:BoundField
                                DataField="InvestorCount"
                                HeaderText="Investor Count"
                                ItemStyle-CssClass="count-cell" />

                        </Columns>

                    </asp:GridView>

                </div>

            </div>


            <!-- DESIGNATION-WISE -->

            <div class="statistics-card">

                <h2>
                    Designation-wise Investor Count
                </h2>

                <div class="table-wrapper">

                    <asp:GridView
                        ID="gvDesignationStats"
                        runat="server"
                        AutoGenerateColumns="False"
                        CssClass="statistics-table"
                        GridLines="None">

                        <Columns>

                            <asp:BoundField
                                DataField="Designation"
                                HeaderText="Designation" />

                            <asp:BoundField
                                DataField="InvestorCount"
                                HeaderText="Investor Count"
                                ItemStyle-CssClass="count-cell" />

                        </Columns>

                    </asp:GridView>

                </div>

            </div>


        </div>


        <!-- ==========================================
             ADD / UPDATE INVESTOR
             ========================================== -->

        <div class="card">

            <h2 class="card-title">
                Add / Update Investor
            </h2>


            <asp:HiddenField
                ID="hfInvestorID"
                runat="server" />


            <div class="form-grid">


                <!-- NAME -->

                <div class="form-group">

                    <label for="txtName">
                        Investor Name
                    </label>

                    <asp:TextBox
                        ID="txtName"
                        runat="server"
                        CssClass="form-control">
                    </asp:TextBox>

                </div>


                <!-- EMAIL -->

                <div class="form-group">

                    <label for="txtEmail">
                        Email
                    </label>

                    <asp:TextBox
                        ID="txtEmail"
                        runat="server"
                        CssClass="form-control"
                        TextMode="Email">
                    </asp:TextBox>

                </div>


                <!-- MOBILE -->

                <div class="form-group">

                    <label for="txtPhone">
                        Mobile
                    </label>

                    <asp:TextBox
                        ID="txtPhone"
                        runat="server"
                        CssClass="form-control">
                    </asp:TextBox>

                </div>


                <!-- DEPARTMENT -->

                <div class="form-group">

                    <label for="ddlDepartment">
                        Department
                    </label>

                    <asp:DropDownList
                        ID="ddlDepartment"
                        runat="server"
                        CssClass="form-control">

                        <asp:ListItem
                            Text="Select Department"
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


                <!-- DESIGNATION -->

                <div class="form-group">

                    <label for="ddlDesignation">
                        Designation
                    </label>

                    <asp:DropDownList
                        ID="ddlDesignation"
                        runat="server"
                        CssClass="form-control">

                        <asp:ListItem
                            Text="Select Designation"
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


                <!-- INVESTMENT -->

                <div class="form-group">

                    <label for="txtInvestmentAmount">
                        Investment Amount
                    </label>

                    <asp:TextBox
                        ID="txtInvestmentAmount"
                        runat="server"
                        CssClass="form-control">
                    </asp:TextBox>

                </div>


            </div>


            <!-- BUTTONS -->

            <div class="button-row">

                <asp:Button
                    ID="btnSave"
                    runat="server"
                    Text="Add Investor"
                    CssClass="btn btn-primary" />


                <asp:Button
                    ID="btnCancel"
                    runat="server"
                    Text="Cancel"
                    CssClass="btn btn-secondary"
                    Visible="false" />

            </div>


        </div>


        <!-- ==========================================
             SEARCH
             ========================================== -->

        <div class="card">

            <h2 class="card-title">
                Search Investors
            </h2>


            <div class="search-grid">


                <!-- SEARCH TEXT -->

                <asp:TextBox
                    ID="txtSearch"
                    runat="server"
                    CssClass="form-control"
                    placeholder="Search name, email or mobile">
                </asp:TextBox>


                <!-- SEARCH DEPARTMENT -->

                <asp:DropDownList
                    ID="ddlSearchDepartment"
                    runat="server"
                    CssClass="form-control">

                    <asp:ListItem
                        Text="All Departments"
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


                <!-- SEARCH DESIGNATION -->

                <asp:DropDownList
                    ID="ddlSearchDesignation"
                    runat="server"
                    CssClass="form-control">

                    <asp:ListItem
                        Text="All Designations"
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


                <!-- SEARCH -->

                <asp:Button
                    ID="btnSearch"
                    runat="server"
                    Text="Search"
                    CssClass="btn btn-primary" />


                <!-- CLEAR -->

                <asp:Button
                    ID="btnClearSearch"
                    runat="server"
                    Text="Clear"
                    CssClass="btn btn-secondary" />

            </div>

        </div>



        <div class="card">

            <h2 class="card-title">
                All Investors
            </h2>


            <div class="table-wrapper">

                <asp:GridView
                    ID="gvInvestors"
                    runat="server"
                    AutoGenerateColumns="False"
                    AutoGenerateSelectButton="True"
                    DataKeyNames="InvestorID"
                    CssClass="investor-table"
                    GridLines="None">

                    <Columns>



                        <asp:BoundField
                            DataField="InvestorID"
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
                            DataField="Department"
                            HeaderText="Department" />



                        <asp:BoundField
                            DataField="Designation"
                            HeaderText="Designation" />



                        <asp:BoundField
                            DataField="InvestmentAmount"
                            HeaderText="Investment Amount"
                            DataFormatString="{0:N2}" />



                        <asp:TemplateField
                            HeaderText="Action">

                            <ItemTemplate>

                                <asp:Button
                                    ID="btnDelete"
                                    runat="server"
                                    Text="Delete"
                                    CssClass="delete-button"
                                    CommandName="DeleteInvestor"
                                    CommandArgument='<%# Eval("InvestorID") %>'
                                    OnClientClick="return confirm('Are you sure you want to delete this investor?');" />

                            </ItemTemplate>

                        </asp:TemplateField>


                    </Columns>

                </asp:GridView>

            </div>

        </div>


    </div>

</asp:Content>