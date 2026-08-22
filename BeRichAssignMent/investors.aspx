<%@ Page Language="VB"
    AutoEventWireup="false"
    CodeFile="Investors.aspx.vb"
    Inherits="Investors" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <title>Investor Management</title>

    <style type="text/css">

        * {
            box-sizing: border-box;
        }

        body {
            margin: 0;
            padding: 30px;
            font-family: Arial, Helvetica, sans-serif;
            background-color: #f5f7fa;
        }

        .container {
            width: 100%;
            max-width: 1450px;
            margin: 0 auto;
        }

        h2 {
            margin: 0 0 20px 0;
            color: #222;
        }

        h3 {
            margin: 30px 0 15px 0;
            color: #222;
        }


        /* =========================================
           NAVBAR
           ========================================= */

        .navbar {
            width: 100%;
            background-color: #17365d;
            border-radius: 8px;
            margin-bottom: 25px;
            box-shadow: 0 2px 8px rgba(0,0,0,0.10);
        }

        .navbar ul {
            list-style: none;
            margin: 0;
            padding: 0;
            display: flex;
            flex-wrap: wrap;
        }

        .navbar li {
            margin: 0;
        }

        .navbar a {
            display: block;
            padding: 15px 18px;
            color: #ffffff;
            text-decoration: none;
            font-size: 14px;
            font-weight: bold;
        }

        .navbar a:hover,
        .navbar a.active {
            background-color: #0d6efd;
            color: #ffffff;
        }


        /* =========================================
           MESSAGE
           ========================================= */

        .message {
            display: block;
            padding: 12px 15px;
            margin-bottom: 20px;
            border-radius: 5px;
            border: 1px solid transparent;
        }


        /* =========================================
           FORM
           ========================================= */

        .form-box {
            background-color: #ffffff;
            padding: 25px;
            border-radius: 8px;
            box-shadow: 0 2px 8px rgba(0,0,0,0.08);
            margin-bottom: 25px;
        }

        .form-table {
            width: 100%;
            max-width: 850px;
            border-collapse: collapse;
        }

        .form-table td {
            padding: 8px;
            vertical-align: middle;
        }

        .label-cell {
            width: 190px;
            font-weight: bold;
            color: #333;
        }

        .form-control {
            width: 100%;
            height: 42px;
            padding: 8px 12px;
            border: 1px solid #bbb;
            border-radius: 5px;
            font-size: 14px;
            background-color: #fff;
        }

        .form-control:focus {
            outline: none;
            border-color: #1677c8;
            box-shadow: 0 0 3px rgba(22,119,200,0.25);
        }

        .button-area {
            margin-top: 20px;
            margin-left: 198px;
        }


        /* =========================================
           BUTTONS
           ========================================= */

        .btn {
            border: none;
            border-radius: 5px;
            padding: 11px 20px;
            font-size: 14px;
            color: #ffffff;
            cursor: pointer;
            margin-right: 8px;
        }

        .btn-save {
            background-color: #198754;
        }

        .btn-save:hover {
            background-color: #157347;
        }

        .btn-cancel {
            background-color: #dc3545;
        }

        .btn-cancel:hover {
            background-color: #bb2d3b;
        }

        .btn-search {
            background-color: #0d6efd;
        }

        .btn-search:hover {
            background-color: #0b5ed7;
        }

        .btn-clear {
            background-color: #6c757d;
        }

        .btn-clear:hover {
            background-color: #5c636a;
        }

        .btn-excel {
            background-color: #198754;
        }

        .btn-excel:hover {
            background-color: #157347;
        }

        .btn-pdf {
            background-color: #dc3545;
        }

        .btn-pdf:hover {
            background-color: #bb2d3b;
        }


        /* =========================================
           SEARCH
           ========================================= */

        .search-box {
            background-color: #ffffff;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 2px 8px rgba(0,0,0,0.08);
            margin-bottom: 20px;
        }

        .search-table {
            width: 100%;
            border-collapse: collapse;
        }

        .search-table td {
            padding: 6px;
            vertical-align: middle;
        }

        .search-label {
            font-weight: bold;
            color: #333;
            white-space: nowrap;
        }

        .search-input {
            width: 100%;
            height: 40px;
            padding: 8px 12px;
            border: 1px solid #bbb;
            border-radius: 5px;
            font-size: 14px;
        }

        .search-select {
            width: 100%;
            height: 40px;
            padding: 8px 10px;
            border: 1px solid #bbb;
            border-radius: 5px;
            font-size: 14px;
            background-color: #ffffff;
        }


        /* =========================================
           EXPORT AREA
           ========================================= */

        .export-area {
            background-color: #ffffff;
            padding: 15px;
            border-radius: 8px;
            box-shadow: 0 2px 8px rgba(0,0,0,0.08);
            margin-bottom: 20px;
        }

        .export-title {
            font-weight: bold;
            margin-right: 15px;
            color: #333;
        }


        /* =========================================
           GRID
           ========================================= */

        .grid-wrapper {
            width: 100%;
            overflow-x: auto;
        }

        .grid {
            width: 100%;
            min-width: 1200px;
            background-color: #ffffff;
            border-collapse: collapse;
            border: 1px solid #ddd;
        }

        .grid th {
            background-color: #17365d;
            color: #ffffff;
            padding: 12px 10px;
            text-align: left;
            border: 1px solid #17365d;
            white-space: nowrap;
        }

        .grid td {
            padding: 10px;
            border: 1px solid #ddd;
            vertical-align: middle;
        }

        .grid tr:nth-child(even) {
            background-color: #f8f9fa;
        }

        .grid tr:hover {
            background-color: #eef5ff;
        }


        /* =========================================
           ROW ACTION BUTTONS
           ========================================= */

        .action-button {
            display: inline-block;
            border: none;
            border-radius: 4px;
            padding: 7px 10px;
            color: #ffffff !important;
            text-decoration: none;
            cursor: pointer;
            font-size: 12px;
            margin-right: 4px;
            margin-bottom: 3px;
        }

        .row-excel {
            background-color: #198754;
        }

        .row-excel:hover {
            background-color: #157347;
        }

        .row-pdf {
            background-color: #dc3545;
        }

        .row-pdf:hover {
            background-color: #bb2d3b;
        }

        .delete-button {
            background-color: #dc3545;
        }

        .delete-button:hover {
            background-color: #bb2d3b;
        }


        /* =========================================
           PAGINATION
           ========================================= */

        .pagination-area {
            margin-top: 20px;
            background-color: #ffffff;
            padding: 15px;
            border-radius: 8px;

            display: flex;
            justify-content: space-between;
            align-items: center;

            box-shadow: 0 2px 8px rgba(0,0,0,0.08);
        }

        .pagination-info {
            color: #555;
            font-size: 14px;
            font-weight: bold;
        }

        .pagination-buttons {
            display: flex;
            gap: 8px;
        }

        .page-button {
            border: none;
            border-radius: 5px;
            padding: 9px 18px;
            background-color: #0d6efd;
            color: white;
            cursor: pointer;
            font-weight: bold;
        }

        .page-button:hover {
            background-color: #0b5ed7;
        }

        .page-button:disabled {
            background-color: #ced4da;
            color: #666;
            cursor: not-allowed;
        }


        /* =========================================
           MOBILE
           ========================================= */

        @media screen and (max-width: 900px) {

            body {
                padding: 10px;
            }

            .navbar ul {
                display: block;
            }

            .navbar a {
                border-bottom: 1px solid rgba(255,255,255,0.12);
            }

            .label-cell {
                width: 130px;
            }

            .button-area {
                margin-left: 0;
            }

            .search-table,
            .search-table tbody,
            .search-table tr,
            .search-table td {
                display: block;
                width: 100%;
            }

            .search-label {
                display: block;
                margin-top: 8px;
            }

            .pagination-area {
                flex-direction: column;
                gap: 15px;
            }

        }

    </style>

</head>


<body>

<form
    id="form1"
    runat="server">


<div class="container">


    <!-- =====================================================
         NAVBAR
         ===================================================== -->

    <nav class="navbar">

        <ul>

            <li>
                <a href="Default.aspx">
                    Dashboard
                </a>
            </li>

            <li>
                <a
                    href="Investors.aspx"
                    class="active">
                    Investor Management
                </a>
            </li>

            <li>
                <a href="Users.aspx">
                    User Management
                </a>
            </li>

            <li>
                <a href="Projects.aspx">
                    Projects
                </a>
            </li>

            <li>
                <a href="Transactions.aspx">
                    Transactions
                </a>
            </li>

            <li>
                <a href="Reports.aspx">
                    Reports
                </a>
            </li>

            <li>
                <a href="Settings.aspx">
                    Settings
                </a>
            </li>

            <li>
                <a href="Logout.aspx">
                    Logout
                </a>
            </li>

        </ul>

    </nav>


    <!-- =====================================================
         TITLE
         ===================================================== -->

    <h2>
        Investor Management
    </h2>


    <!-- =====================================================
         MESSAGE
         ===================================================== -->

    <asp:Label
        ID="lblMessage"
        runat="server"
        Visible="False"
        CssClass="message">
    </asp:Label>


    <!-- =====================================================
         HIDDEN ID
         ===================================================== -->

    <asp:HiddenField
        ID="hfInvestorID"
        runat="server" />


    <!-- =====================================================
         ADD / UPDATE FORM
         ===================================================== -->

    <div class="form-box">

        <table class="form-table">


            <!-- NAME -->

            <tr>

                <td class="label-cell">
                    Name
                </td>

                <td>

                    <asp:TextBox
                        ID="txtName"
                        runat="server"
                        CssClass="form-control"
                        MaxLength="100"
                        placeholder="Enter your full name">
                    </asp:TextBox>

                </td>

            </tr>


            <!-- EMAIL -->

            <tr>

                <td class="label-cell">
                    Email
                </td>

                <td>

                    <asp:TextBox
                        ID="txtEmail"
                        runat="server"
                        CssClass="form-control"
                        MaxLength="150"
                        placeholder="Enter your email address">
                    </asp:TextBox>

                </td>

            </tr>


            <!-- MOBILE -->

            <tr>

                <td class="label-cell">
                    Mobile
                </td>

                <td>

                    <asp:TextBox
                        ID="txtMobile"
                        runat="server"
                        CssClass="form-control"
                        MaxLength="11"
                        placeholder="01712345678">
                    </asp:TextBox>

                </td>

            </tr>


            <!-- DEPARTMENT -->

            <tr>

                <td class="label-cell">
                    Department
                </td>

                <td>

                    <asp:DropDownList
                        ID="ddlDepartment"
                        runat="server"
                        CssClass="form-control">

                        <asp:ListItem
                            Text="Select your department"
                            Value="">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="Accounts"
                            Value="Accounts">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="HR"
                            Value="HR">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="IT"
                            Value="IT">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="Customer Care"
                            Value="Customer Care">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="Admin"
                            Value="Admin">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="Finance"
                            Value="Finance">
                        </asp:ListItem>

                    </asp:DropDownList>

                </td>

            </tr>


            <!-- DESIGNATION -->

            <tr>

                <td class="label-cell">
                    Designation
                </td>

                <td>

                    <asp:DropDownList
                        ID="ddlDesignation"
                        runat="server"
                        CssClass="form-control">

                        <asp:ListItem
                            Text="Select your designation"
                            Value="">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="Executive"
                            Value="Executive">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="Officer"
                            Value="Officer">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="Deputy Manager"
                            Value="Deputy Manager">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="Manager"
                            Value="Manager">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="Senior Manager"
                            Value="Senior Manager">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="Director"
                            Value="Director">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="CEO"
                            Value="CEO">
                        </asp:ListItem>

                    </asp:DropDownList>

                </td>

            </tr>


            <!-- INVESTMENT -->

            <tr>

                <td class="label-cell">
                    Investment Amount
                </td>

                <td>

                    <asp:TextBox
                        ID="txtInvestmentAmount"
                        runat="server"
                        CssClass="form-control"
                        placeholder="Enter investment amount">
                    </asp:TextBox>

                </td>

            </tr>


        </table>


        <!-- BUTTONS -->

        <div class="button-area">

            <asp:Button
                ID="btnSave"
                runat="server"
                Text="Add Investor"
                CssClass="btn btn-save" />


            <asp:Button
                ID="btnCancel"
                runat="server"
                Text="Cancel"
                CssClass="btn btn-cancel"
                Visible="False"
                CausesValidation="False" />

        </div>

    </div>


    <!-- =====================================================
         SEARCH
         ===================================================== -->

    <div class="search-box">

        <table class="search-table">

            <tr>


                <!-- SEARCH -->

                <td class="search-label">
                    Search
                </td>

                <td style="width:30%;">

                    <asp:TextBox
                        ID="txtSearch"
                        runat="server"
                        CssClass="search-input"
                        MaxLength="150"
                        placeholder="Name, email or mobile">
                    </asp:TextBox>

                </td>


                <!-- DEPARTMENT -->

                <td class="search-label">
                    Department
                </td>

                <td style="width:20%;">

                    <asp:DropDownList
                        ID="ddlSearchDepartment"
                        runat="server"
                        CssClass="search-select">

                        <asp:ListItem
                            Text="All Departments"
                            Value="">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="Accounts"
                            Value="Accounts">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="HR"
                            Value="HR">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="IT"
                            Value="IT">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="Customer Care"
                            Value="Customer Care">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="Admin"
                            Value="Admin">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="Finance"
                            Value="Finance">
                        </asp:ListItem>

                    </asp:DropDownList>

                </td>


                <!-- DESIGNATION -->

                <td class="search-label">
                    Designation
                </td>

                <td style="width:20%;">

                    <asp:DropDownList
                        ID="ddlSearchDesignation"
                        runat="server"
                        CssClass="search-select">

                        <asp:ListItem
                            Text="All Designations"
                            Value="">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="Executive"
                            Value="Executive">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="Officer"
                            Value="Officer">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="Deputy Manager"
                            Value="Deputy Manager">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="Manager"
                            Value="Manager">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="Senior Manager"
                            Value="Senior Manager">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="Director"
                            Value="Director">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="CEO"
                            Value="CEO">
                        </asp:ListItem>

                    </asp:DropDownList>

                </td>

            </tr>


            <tr>

                <td colspan="6">

                    <asp:Button
                        ID="btnSearch"
                        runat="server"
                        Text="Search"
                        CssClass="btn btn-search"
                        CausesValidation="False" />


                    <asp:Button
                        ID="btnClearSearch"
                        runat="server"
                        Text="Clear"
                        CssClass="btn btn-clear"
                        CausesValidation="False" />

                </td>

            </tr>

        </table>

    </div>


    <!-- =====================================================
         EXPORT ALL
         ===================================================== -->

    <div class="export-area">

        <span class="export-title">
            Export Current Filtered Data:
        </span>


        <asp:Button
            ID="btnExportExcel"
            runat="server"
            Text="Export All Excel"
            CssClass="btn btn-excel"
            CausesValidation="False" />


        <asp:Button
            ID="btnDownloadAllPdf"
            runat="server"
            Text="Download All PDF"
            CssClass="btn btn-pdf"
            CausesValidation="False" />

        <asp:Button
    ID="btnExportAllCsv"
    runat="server"
    Text="Export All CSV"
    CssClass="btn btn-excel"
    CausesValidation="False" />

    </div>


    <!-- =====================================================
         LIST
         ===================================================== -->

    <h3>
        Investor List
    </h3>


    <div class="grid-wrapper">


        <asp:GridView
            ID="gvInvestors"
            runat="server"
            AutoGenerateColumns="False"
            DataKeyNames="InvestorID"
            CssClass="grid"
            EmptyDataText="No investors found.">


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
                    HeaderText="Actions">

                    <ItemTemplate>



                        <asp:LinkButton
                            ID="btnRowExcel"
                            runat="server"
                            Text="Excel"
                            CommandName="ExportInvestorExcel"
                            CommandArgument='<%# Eval("InvestorID") %>'
                            CssClass="action-button row-excel"
                            CausesValidation="False">
                        </asp:LinkButton>



                        <asp:LinkButton
                            ID="btnRowPdf"
                            runat="server"
                            Text="PDF"
                            CommandName="DownloadInvestorPdf"
                            CommandArgument='<%# Eval("InvestorID") %>'
                            CssClass="action-button row-pdf"
                            CausesValidation="False">
                        </asp:LinkButton>
                        <asp:LinkButton
    ID="btnRowCsv"
    runat="server"
    Text="CSV"
    CommandName="ExportInvestorCsv"
    CommandArgument='<%# Eval("InvestorID") %>'
    CssClass="action-button row-excel"
    CausesValidation="False">
</asp:LinkButton>




                        <asp:LinkButton
                            ID="btnDelete"
                            runat="server"
                            Text="Delete"
                            CommandName="DeleteInvestor"
                            CommandArgument='<%# Eval("InvestorID") %>'
                            CssClass="action-button delete-button"
                            CausesValidation="False"
                            OnClientClick="return confirm('Are you sure you want to delete this investor?');">
                        </asp:LinkButton>


                    </ItemTemplate>

                </asp:TemplateField>


            </Columns>


        </asp:GridView>


    </div>


  
    <div class="pagination-area">


        <div class="pagination-info">

            <asp:Label
                ID="lblInvestorCount"
                runat="server"
                Text="0 investors">
            </asp:Label>

            &nbsp; | &nbsp;

            <asp:Label
                ID="lblPageInfo"
                runat="server"
                Text="Page 1 of 1">
            </asp:Label>

        </div>


        <div class="pagination-buttons">


            <asp:Button
                ID="btnPrevious"
                runat="server"
                Text="← Previous"
                CssClass="page-button"
                CausesValidation="False" />


            <asp:Button
                ID="btnNext"
                runat="server"
                Text="Next →"
                CssClass="page-button"
                CausesValidation="False" />


        </div>


    </div>


</div>


</form>

</body>

</html>