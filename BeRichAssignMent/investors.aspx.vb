Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Text.RegularExpressions
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.IO
Imports System.Text

Imports iTextSharp.text
Imports iTextSharp.text.pdf


Partial Class Investors

    Inherits System.Web.UI.Page


    ' =========================================================
    ' DATABASE CONNECTION
    ' =========================================================

    Private ReadOnly conStr As String =
        ConfigurationManager.ConnectionStrings(
            "InvestorDB"
        ).ConnectionString


    ' =========================================================
    ' PAGINATION
    ' =========================================================

    Private Const PageSize As Integer = 5


    ' =========================================================
    ' SELECTED INVESTOR ID
    ' =========================================================

    Private Property SelectedInvestorID As Integer

        Get

            If ViewState("SelectedInvestorID") Is Nothing Then
                Return 0
            End If

            Return Convert.ToInt32(
                ViewState("SelectedInvestorID")
            )

        End Get

        Set(value As Integer)

            ViewState("SelectedInvestorID") = value

        End Set

    End Property


    ' =========================================================
    ' CURRENT PAGE
    ' =========================================================

    Private Property CurrentPage As Integer

        Get

            If ViewState("CurrentPage") Is Nothing Then
                Return 1
            End If

            Return Convert.ToInt32(
                ViewState("CurrentPage")
            )

        End Get

        Set(value As Integer)

            ViewState("CurrentPage") = value

        End Set

    End Property


    ' =========================================================
    ' PAGE LOAD
    ' =========================================================

    Protected Sub Page_Load(
        ByVal sender As Object,
        ByVal e As EventArgs
    ) Handles Me.Load

        Try

            ' =====================================================
            ' LOGIN CHECK
            ' =====================================================

            If Session("UserID") Is Nothing Then

                Response.Redirect(
                    "~/Login.aspx",
                    False
                )

                Context.ApplicationInstance.CompleteRequest()

                Return

            End If


            ' =====================================================
            ' ROLE CHECK
            ' =====================================================

            If Session("Role") Is Nothing Then

                Response.Redirect(
                    "~/Login.aspx",
                    False
                )

                Context.ApplicationInstance.CompleteRequest()

                Return

            End If


            Dim role As String =
                Session("Role").ToString().Trim().ToLower()


            If role <> "admin" Then

                Response.Redirect(
                    "~/Home.aspx",
                    False
                )

                Context.ApplicationInstance.CompleteRequest()

                Return

            End If


            ' =====================================================
            ' FIRST LOAD
            ' =====================================================

            If Not IsPostBack Then

                CurrentPage = 1

                SelectedInvestorID = 0

                ClearInvestorForm()

                LoadInvestors()

            End If


        Catch ex As Exception

            ShowError(
                "Page Load Error: " &
                ex.Message
            )

        End Try

    End Sub


    ' =========================================================
    ' LOAD INVESTORS
    ' =========================================================

    Private Sub LoadInvestors()

        LoadInvestors(
            txtSearch.Text.Trim(),
            ddlSearchDepartment.SelectedValue.Trim(),
            ddlSearchDesignation.SelectedValue.Trim()
        )

    End Sub


    ' =========================================================
    ' LOAD INVESTORS WITH PAGINATION
    ' =========================================================

    Private Sub LoadInvestors(
        ByVal searchText As String,
        ByVal department As String,
        ByVal designation As String
    )

        Try

            Using con As New SqlConnection(conStr)

                con.Open()


                ' =================================================
                ' COUNT
                ' =================================================

                Dim countSql As String =
                    "SELECT COUNT(*) " &
                    "FROM dbo.Investors " &
                    "WHERE 1 = 1 "


                AddFilterConditions(
                    countSql,
                    searchText,
                    department,
                    designation
                )


                Dim totalRecords As Integer


                Using cmd As New SqlCommand(
                    countSql,
                    con
                )

                    AddFilterParameters(
                        cmd,
                        searchText,
                        department,
                        designation
                    )


                    totalRecords =
                        Convert.ToInt32(
                            cmd.ExecuteScalar()
                        )

                End Using


                ' =================================================
                ' TOTAL PAGES
                ' =================================================

                Dim totalPages As Integer


                If totalRecords = 0 Then

                    totalPages = 1

                Else

                    totalPages =
                        CInt(
                            Math.Ceiling(
                                totalRecords /
                                CDbl(PageSize)
                            )
                        )

                End If


                If CurrentPage < 1 Then
                    CurrentPage = 1
                End If


                If CurrentPage > totalPages Then
                    CurrentPage = totalPages
                End If


                ' =================================================
                ' OFFSET
                ' =================================================

                Dim offset As Integer =
                    (CurrentPage - 1) * PageSize


                ' =================================================
                ' PAGINATED QUERY
                ' =================================================

                Dim sql As String =
                    "SELECT " &
                    "InvestorID, " &
                    "[Name], " &
                    "[Email], " &
                    "[Mobile], " &
                    "[Department], " &
                    "[Designation], " &
                    "[InvestmentAmount] " &
                    "FROM dbo.Investors " &
                    "WHERE 1 = 1 "


                AddFilterConditions(
                    sql,
                    searchText,
                    department,
                    designation
                )


                sql &=
                    "ORDER BY InvestorID DESC " &
                    "OFFSET @Offset ROWS " &
                    "FETCH NEXT @PageSize ROWS ONLY"


                Using cmd As New SqlCommand(
                    sql,
                    con
                )

                    AddFilterParameters(
                        cmd,
                        searchText,
                        department,
                        designation
                    )


                    cmd.Parameters.Add(
                        "@Offset",
                        SqlDbType.Int
                    ).Value = offset


                    cmd.Parameters.Add(
                        "@PageSize",
                        SqlDbType.Int
                    ).Value = PageSize


                    Dim dt As New DataTable()


                    Using adapter As New SqlDataAdapter(cmd)

                        adapter.Fill(dt)

                    End Using


                    gvInvestors.DataSource = dt

                    gvInvestors.DataBind()

                End Using


                ' =================================================
                ' PAGINATION INFO
                ' =================================================

                lblInvestorCount.Text =
                    totalRecords.ToString() &
                    " investors"


                lblPageInfo.Text =
                    "Page " &
                    CurrentPage.ToString() &
                    " of " &
                    totalPages.ToString()


                btnPrevious.Enabled =
                    CurrentPage > 1


                btnNext.Enabled =
                    CurrentPage < totalPages

            End Using


        Catch ex As Exception

            ShowError(
                "Load Investors Error: " &
                ex.Message
            )

        End Try

    End Sub


    ' =========================================================
    ' FILTER CONDITIONS
    ' =========================================================

    Private Sub AddFilterConditions(
        ByRef sql As String,
        ByVal searchText As String,
        ByVal department As String,
        ByVal designation As String
    )

        If searchText <> "" Then

            sql &=
                "AND (" &
                "[Name] LIKE @Search " &
                "OR [Email] LIKE @Search " &
                "OR [Mobile] LIKE @Search" &
                ") "

        End If


        If department <> "" Then

            sql &=
                "AND [Department] = @Department "

        End If


        If designation <> "" Then

            sql &=
                "AND [Designation] = @Designation "

        End If

    End Sub


    ' =========================================================
    ' FILTER PARAMETERS
    ' =========================================================

    Private Sub AddFilterParameters(
        ByVal cmd As SqlCommand,
        ByVal searchText As String,
        ByVal department As String,
        ByVal designation As String
    )

        If searchText <> "" Then

            cmd.Parameters.Add(
                "@Search",
                SqlDbType.NVarChar,
                250
            ).Value =
                "%" &
                searchText &
                "%"

        End If


        If department <> "" Then

            cmd.Parameters.Add(
                "@Department",
                SqlDbType.NVarChar,
                100
            ).Value =
                department

        End If


        If designation <> "" Then

            cmd.Parameters.Add(
                "@Designation",
                SqlDbType.NVarChar,
                100
            ).Value =
                designation

        End If

    End Sub


    ' =========================================================
    ' SEARCH
    ' =========================================================

    Protected Sub btnSearch_Click(
        ByVal sender As Object,
        ByVal e As EventArgs
    ) Handles btnSearch.Click

        CurrentPage = 1

        HideMessage()

        LoadInvestors()

    End Sub


    ' =========================================================
    ' CLEAR SEARCH
    ' =========================================================

    Protected Sub btnClearSearch_Click(
        ByVal sender As Object,
        ByVal e As EventArgs
    ) Handles btnClearSearch.Click

        txtSearch.Text = ""

        ddlSearchDepartment.SelectedIndex = 0

        ddlSearchDesignation.SelectedIndex = 0

        CurrentPage = 1

        HideMessage()

        LoadInvestors()

    End Sub


    ' =========================================================
    ' PREVIOUS
    ' =========================================================

    Protected Sub btnPrevious_Click(
        ByVal sender As Object,
        ByVal e As EventArgs
    ) Handles btnPrevious.Click

        If CurrentPage > 1 Then

            CurrentPage -= 1

            LoadInvestors()

        End If

    End Sub


    ' =========================================================
    ' NEXT
    ' =========================================================

    Protected Sub btnNext_Click(
        ByVal sender As Object,
        ByVal e As EventArgs
    ) Handles btnNext.Click

        CurrentPage += 1

        LoadInvestors()

    End Sub


    ' =========================================================
    ' ADD / UPDATE
    ' =========================================================

    Protected Sub btnSave_Click(
        ByVal sender As Object,
        ByVal e As EventArgs
    ) Handles btnSave.Click

        Try

            ' =================================================
            ' NAME
            ' =================================================

            Dim investorName As String =
                txtName.Text.Trim()


            If investorName = "" Then

                ShowError(
                    "Please enter investor name."
                )

                Return

            End If


            ' =================================================
            ' EMAIL
            ' =================================================

            Dim email As String =
                txtEmail.Text.Trim()


            If email = "" Then

                ShowError(
                    "Please enter email."
                )

                Return

            End If


            If Not IsValidEmail(email) Then

                ShowError(
                    "Please enter a valid email address."
                )

                Return

            End If


            ' =================================================
            ' MOBILE
            ' =================================================

            Dim mobile As String =
                txtMobile.Text.Trim()


            If mobile = "" Then

                ShowError(
                    "Please enter mobile number."
                )

                Return

            End If


            If Not IsValidBangladeshMobile(mobile) Then

                ShowError(
                    "Please enter a valid Bangladesh mobile number. Example: 01712345678"
                )

                Return

            End If


            ' =================================================
            ' DEPARTMENT
            ' =================================================

            Dim department As String =
                ddlDepartment.SelectedValue.Trim()


            If department = "" Then

                ShowError(
                    "Please select department."
                )

                Return

            End If


            ' =================================================
            ' DESIGNATION
            ' =================================================

            Dim designation As String =
                ddlDesignation.SelectedValue.Trim()


            If designation = "" Then

                ShowError(
                    "Please select designation."
                )

                Return

            End If


            ' =================================================
            ' INVESTMENT
            ' =================================================

            Dim investmentAmount As Decimal


            If txtInvestmentAmount.Text.Trim() = "" Then

                ShowError(
                    "Please enter investment amount."
                )

                Return

            End If


            If Not Decimal.TryParse(
                txtInvestmentAmount.Text.Trim(),
                investmentAmount
            ) Then

                ShowError(
                    "Investment amount must be a valid number."
                )

                Return

            End If


            If investmentAmount < 0 Then

                ShowError(
                    "Investment amount cannot be negative."
                )

                Return

            End If


            ' =================================================
            ' INVESTOR ID
            ' =================================================

            Dim investorID As Integer =
                SelectedInvestorID


            If investorID = 0 Then

                Integer.TryParse(
                    hfInvestorID.Value,
                    investorID
                )

            End If


            ' =================================================
            ' DUPLICATE EMAIL
            ' =================================================

            If IsEmailAlreadyRegistered(
                email,
                investorID
            ) Then

                ShowEmailDuplicatePopup()

                Return

            End If


            ' =================================================
            ' DUPLICATE MOBILE
            ' =================================================

            If IsMobileAlreadyRegistered(
                mobile,
                investorID
            ) Then

                ShowMobileDuplicatePopup()

                Return

            End If


            ' =================================================
            ' UPDATE
            ' =================================================

            If investorID > 0 Then

                UpdateInvestor(
                    investorID,
                    investorName,
                    email,
                    mobile,
                    department,
                    designation,
                    investmentAmount
                )


                ShowSuccess(
                    "Investor updated successfully."
                )

            Else

                ' =================================================
                ' ADD
                ' =================================================

                AddInvestor(
                    investorName,
                    email,
                    mobile,
                    department,
                    designation,
                    investmentAmount
                )


                CurrentPage = 1


                ShowSuccess(
                    "Investor added successfully."
                )

            End If


            ClearInvestorForm()

            LoadInvestors()


        Catch ex As Exception

            ShowError(
                "Save Error: " &
                ex.Message
            )

        End Try

    End Sub


    ' =========================================================
    ' ADD INVESTOR
    ' =========================================================

    Private Sub AddInvestor(
        ByVal investorName As String,
        ByVal email As String,
        ByVal mobile As String,
        ByVal department As String,
        ByVal designation As String,
        ByVal investmentAmount As Decimal
    )

        Dim sql As String =
            "INSERT INTO dbo.Investors " &
            "([Name], [Email], [Mobile], [Department], " &
            "[Designation], [InvestmentAmount]) " &
            "VALUES " &
            "(@Name, @Email, @Mobile, @Department, " &
            "@Designation, @InvestmentAmount)"


        Using con As New SqlConnection(conStr)

            Using cmd As New SqlCommand(
                sql,
                con
            )

                cmd.Parameters.Add(
                    "@Name",
                    SqlDbType.NVarChar,
                    150
                ).Value =
                    investorName


                cmd.Parameters.Add(
                    "@Email",
                    SqlDbType.NVarChar,
                    150
                ).Value =
                    email.ToLowerInvariant()


                cmd.Parameters.Add(
                    "@Mobile",
                    SqlDbType.NVarChar,
                    30
                ).Value =
                    mobile


                cmd.Parameters.Add(
                    "@Department",
                    SqlDbType.NVarChar,
                    100
                ).Value =
                    department


                cmd.Parameters.Add(
                    "@Designation",
                    SqlDbType.NVarChar,
                    100
                ).Value =
                    designation


                Dim amountParameter As SqlParameter =
                    cmd.Parameters.Add(
                        "@InvestmentAmount",
                        SqlDbType.Decimal
                    )


                amountParameter.Precision = 18

                amountParameter.Scale = 2

                amountParameter.Value =
                    investmentAmount


                con.Open()

                cmd.ExecuteNonQuery()

            End Using

        End Using

    End Sub


    ' =========================================================
    ' UPDATE INVESTOR
    ' =========================================================

    Private Sub UpdateInvestor(
        ByVal investorID As Integer,
        ByVal investorName As String,
        ByVal email As String,
        ByVal mobile As String,
        ByVal department As String,
        ByVal designation As String,
        ByVal investmentAmount As Decimal
    )

        Dim sql As String =
            "UPDATE dbo.Investors SET " &
            "[Name] = @Name, " &
            "[Email] = @Email, " &
            "[Mobile] = @Mobile, " &
            "[Department] = @Department, " &
            "[Designation] = @Designation, " &
            "[InvestmentAmount] = @InvestmentAmount " &
            "WHERE InvestorID = @InvestorID"


        Using con As New SqlConnection(conStr)

            Using cmd As New SqlCommand(
                sql,
                con
            )

                cmd.Parameters.Add(
                    "@Name",
                    SqlDbType.NVarChar,
                    150
                ).Value =
                    investorName


                cmd.Parameters.Add(
                    "@Email",
                    SqlDbType.NVarChar,
                    150
                ).Value =
                    email.ToLowerInvariant()


                cmd.Parameters.Add(
                    "@Mobile",
                    SqlDbType.NVarChar,
                    30
                ).Value =
                    mobile


                cmd.Parameters.Add(
                    "@Department",
                    SqlDbType.NVarChar,
                    100
                ).Value =
                    department


                cmd.Parameters.Add(
                    "@Designation",
                    SqlDbType.NVarChar,
                    100
                ).Value =
                    designation


                Dim amountParameter As SqlParameter =
                    cmd.Parameters.Add(
                        "@InvestmentAmount",
                        SqlDbType.Decimal
                    )


                amountParameter.Precision = 18

                amountParameter.Scale = 2

                amountParameter.Value =
                    investmentAmount


                cmd.Parameters.Add(
                    "@InvestorID",
                    SqlDbType.Int
                ).Value =
                    investorID


                con.Open()


                Dim affectedRows As Integer =
                    cmd.ExecuteNonQuery()


                If affectedRows = 0 Then

                    Throw New Exception(
                        "Investor not found."
                    )

                End If

            End Using

        End Using

    End Sub


    ' =========================================================
    ' CANCEL
    ' =========================================================

    Protected Sub btnCancel_Click(
        ByVal sender As Object,
        ByVal e As EventArgs
    ) Handles btnCancel.Click

        ClearInvestorForm()

        HideMessage()

        LoadInvestors()

    End Sub


    ' =========================================================
    ' CLEAR FORM
    ' =========================================================

    Private Sub ClearInvestorForm()

        txtName.Text = ""

        txtEmail.Text = ""

        txtMobile.Text = ""

        txtInvestmentAmount.Text = ""


        ddlDepartment.ClearSelection()

        If ddlDepartment.Items.Count > 0 Then
            ddlDepartment.SelectedIndex = 0
        End If


        ddlDesignation.ClearSelection()

        If ddlDesignation.Items.Count > 0 Then
            ddlDesignation.SelectedIndex = 0
        End If


        hfInvestorID.Value = ""

        SelectedInvestorID = 0


        btnSave.Text =
            "Add Investor"


        btnCancel.Visible =
            False

    End Sub


    ' =========================================================
    ' GRID ROW COMMAND
    ' =========================================================

    Protected Sub gvInvestors_RowCommand(
        ByVal sender As Object,
        ByVal e As GridViewCommandEventArgs
    ) Handles gvInvestors.RowCommand

        Try

            Dim investorID As Integer


            If Not Integer.TryParse(
                Convert.ToString(
                    e.CommandArgument
                ),
                investorID
            ) Then

                ShowError(
                    "Invalid Investor ID."
                )

                Return

            End If


            Select Case e.CommandName


                Case "EditInvestor"

                    LoadInvestor(
                        investorID
                    )


                Case "DeleteInvestor"

                    DeleteInvestor(
                        investorID
                    )


                    If SelectedInvestorID =
                        investorID Then

                        ClearInvestorForm()

                    End If


                    LoadInvestors()


                    ShowSuccess(
                        "Investor deleted successfully."
                    )


                Case "ExportInvestorExcel"

                    ExportSingleInvestorExcel(
                        investorID
                    )


                Case "DownloadInvestorPdf"

                    DownloadSingleInvestorPdf(
                        investorID
                    )


            End Select


        Catch ex As Exception

            ShowError(
                "Action Error: " &
                ex.Message
            )

        End Try

    End Sub


    ' =========================================================
    ' DELETE INVESTOR
    ' =========================================================

    Private Sub DeleteInvestor(
        ByVal investorID As Integer
    )

        Dim sql As String =
            "DELETE FROM dbo.Investors " &
            "WHERE InvestorID = @InvestorID"


        Using con As New SqlConnection(conStr)

            Using cmd As New SqlCommand(
                sql,
                con
            )

                cmd.Parameters.Add(
                    "@InvestorID",
                    SqlDbType.Int
                ).Value =
                    investorID


                con.Open()


                Dim affectedRows As Integer =
                    cmd.ExecuteNonQuery()


                If affectedRows = 0 Then

                    Throw New Exception(
                        "Investor not found."
                    )

                End If

            End Using

        End Using

    End Sub


    ' =========================================================
    ' GET SINGLE INVESTOR
    ' =========================================================

    Private Function GetInvestor(
        ByVal investorID As Integer
    ) As DataRow

        Dim dt As New DataTable()


        Dim sql As String =
            "SELECT " &
            "InvestorID, " &
            "[Name], " &
            "[Email], " &
            "[Mobile], " &
            "[Department], " &
            "[Designation], " &
            "[InvestmentAmount] " &
            "FROM dbo.Investors " &
            "WHERE InvestorID = @InvestorID"


        Using con As New SqlConnection(conStr)

            Using cmd As New SqlCommand(
                sql,
                con
            )

                cmd.Parameters.Add(
                    "@InvestorID",
                    SqlDbType.Int
                ).Value =
                    investorID


                Using adapter As New SqlDataAdapter(cmd)

                    adapter.Fill(dt)

                End Using

            End Using

        End Using


        If dt.Rows.Count = 0 Then

            Return Nothing

        End If


        Return dt.Rows(0)

    End Function


    ' =========================================================
    ' LOAD INVESTOR FOR UPDATE
    '
    ' NO ListItem
    ' =========================================================

    Private Sub LoadInvestor(
        ByVal investorID As Integer
    )

        Dim row As DataRow =
            GetInvestor(investorID)


        If row Is Nothing Then

            ShowError(
                "Investor not found."
            )

            Return

        End If


        SelectedInvestorID =
            investorID


        hfInvestorID.Value =
            investorID.ToString()


        ' =====================================================
        ' NAME
        ' =====================================================

        If IsDBNull(row("Name")) Then

            txtName.Text = ""

        Else

            txtName.Text =
                row("Name").ToString()

        End If


        ' =====================================================
        ' EMAIL
        ' =====================================================

        If IsDBNull(row("Email")) Then

            txtEmail.Text = ""

        Else

            txtEmail.Text =
                row("Email").ToString()

        End If


        ' =====================================================
        ' MOBILE
        ' =====================================================

        If IsDBNull(row("Mobile")) Then

            txtMobile.Text = ""

        Else

            txtMobile.Text =
                row("Mobile").ToString()

        End If


        ' =====================================================
        ' DEPARTMENT
        '
        ' IMPORTANT:
        ' NO Dim departmentItem As ListItem
        ' =====================================================

        ddlDepartment.ClearSelection()


        Dim department As String = ""


        If Not IsDBNull(row("Department")) Then

            department =
                row("Department").ToString().Trim()

        End If


        If department <> "" AndAlso
           ddlDepartment.Items.FindByValue(
               department
           ) IsNot Nothing Then

            ddlDepartment.SelectedValue =
                department

        Else

            ddlDepartment.SelectedIndex = 0

        End If


        ' =====================================================
        ' DESIGNATION
        '
        ' IMPORTANT:
        ' NO Dim designationItem As ListItem
        ' =====================================================

        ddlDesignation.ClearSelection()


        Dim designation As String = ""


        If Not IsDBNull(row("Designation")) Then

            designation =
                row("Designation").ToString().Trim()

        End If


        If designation <> "" AndAlso
           ddlDesignation.Items.FindByValue(
               designation
           ) IsNot Nothing Then

            ddlDesignation.SelectedValue =
                designation

        Else

            ddlDesignation.SelectedIndex = 0

        End If


        ' =====================================================
        ' INVESTMENT AMOUNT
        ' =====================================================

        If IsDBNull(
            row("InvestmentAmount")
        ) Then

            txtInvestmentAmount.Text = ""

        Else

            txtInvestmentAmount.Text =
                Convert.ToDecimal(
                    row("InvestmentAmount")
                ).ToString("0.00")

        End If


        ' =====================================================
        ' UPDATE MODE
        ' =====================================================

        btnSave.Text =
            "Update Investor"


        btnCancel.Visible =
            True


        ShowSuccess(
            "Investor loaded for update."
        )

    End Sub


    ' =========================================================
    ' DUPLICATE EMAIL
    ' =========================================================

    Private Function IsEmailAlreadyRegistered(
        ByVal email As String,
        ByVal currentInvestorID As Integer
    ) As Boolean

        Dim sql As String =
            "SELECT COUNT(*) " &
            "FROM dbo.Investors " &
            "WHERE LOWER(LTRIM(RTRIM([Email]))) = @Email " &
            "AND InvestorID <> @InvestorID"


        Using con As New SqlConnection(conStr)

            Using cmd As New SqlCommand(
                sql,
                con
            )

                cmd.Parameters.Add(
                    "@Email",
                    SqlDbType.NVarChar,
                    150
                ).Value =
                    email.Trim().
                    ToLowerInvariant()


                cmd.Parameters.Add(
                    "@InvestorID",
                    SqlDbType.Int
                ).Value =
                    currentInvestorID


                con.Open()


                Return Convert.ToInt32(
                    cmd.ExecuteScalar()
                ) > 0

            End Using

        End Using

    End Function


    ' =========================================================
    ' DUPLICATE MOBILE
    ' =========================================================

    Private Function IsMobileAlreadyRegistered(
        ByVal mobile As String,
        ByVal currentInvestorID As Integer
    ) As Boolean

        Dim sql As String =
            "SELECT COUNT(*) " &
            "FROM dbo.Investors " &
            "WHERE LTRIM(RTRIM([Mobile])) = @Mobile " &
            "AND InvestorID <> @InvestorID"


        Using con As New SqlConnection(conStr)

            Using cmd As New SqlCommand(
                sql,
                con
            )

                cmd.Parameters.Add(
                    "@Mobile",
                    SqlDbType.NVarChar,
                    30
                ).Value =
                    mobile.Trim()


                cmd.Parameters.Add(
                    "@InvestorID",
                    SqlDbType.Int
                ).Value =
                    currentInvestorID


                con.Open()


                Return Convert.ToInt32(
                    cmd.ExecuteScalar()
                ) > 0

            End Using

        End Using

    End Function


    ' =========================================================
    ' EMAIL VALIDATION
    ' =========================================================

    Private Function IsValidEmail(
        ByVal email As String
    ) As Boolean

        Dim pattern As String =
            "^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$"


        Return Regex.IsMatch(
            email.Trim(),
            pattern
        )

    End Function


    ' =========================================================
    ' BANGLADESH MOBILE VALIDATION
    ' =========================================================

    Private Function IsValidBangladeshMobile(
        ByVal mobile As String
    ) As Boolean

        Dim pattern As String =
            "^01[3-9][0-9]{8}$"


        Return Regex.IsMatch(
            mobile.Trim(),
            pattern
        )

    End Function


    ' =========================================================
    ' GET ALL FILTERED DATA
    '
    ' NO PAGINATION HERE
    '
    ' Export therefore exports ALL matching records.
    ' =========================================================

    Private Function GetAllFilteredInvestors() As DataTable

        Dim dt As New DataTable()


        Dim searchText As String =
            txtSearch.Text.Trim()


        Dim department As String =
            ddlSearchDepartment.SelectedValue.Trim()


        Dim designation As String =
            ddlSearchDesignation.SelectedValue.Trim()


        Dim sql As String =
            "SELECT " &
            "InvestorID, " &
            "[Name], " &
            "[Email], " &
            "[Mobile], " &
            "[Department], " &
            "[Designation], " &
            "[InvestmentAmount] " &
            "FROM dbo.Investors " &
            "WHERE 1 = 1 "


        AddFilterConditions(
            sql,
            searchText,
            department,
            designation
        )


        sql &=
            "ORDER BY InvestorID DESC"


        Using con As New SqlConnection(conStr)

            Using cmd As New SqlCommand(
                sql,
                con
            )

                AddFilterParameters(
                    cmd,
                    searchText,
                    department,
                    designation
                )


                Using adapter As New SqlDataAdapter(cmd)

                    adapter.Fill(dt)

                End Using

            End Using

        End Using


        Return dt

    End Function


    ' =========================================================
    ' EXPORT ALL EXCEL
    ' =========================================================

    Protected Sub btnExportExcel_Click(
        ByVal sender As Object,
        ByVal e As EventArgs
    ) Handles btnExportExcel.Click

        Try

            Dim dt As DataTable =
                GetAllFilteredInvestors()


            If dt.Rows.Count = 0 Then

                ShowError(
                    "No investors found."
                )

                Return

            End If


            Dim sw As New StringWriter()


            sw.WriteLine("<html>")

            sw.WriteLine("<head>")

            sw.WriteLine(
                "<meta http-equiv='Content-Type' " &
                "content='text/html; charset=utf-8'>"
            )

            sw.WriteLine("</head>")

            sw.WriteLine("<body>")


            sw.WriteLine(
                "<h2>Investor Report</h2>"
            )


            sw.WriteLine(
                "<p><b>Total Investors:</b> " &
                dt.Rows.Count.ToString() &
                "</p>"
            )


            sw.WriteLine(
                "<table border='1' " &
                "cellpadding='7' " &
                "cellspacing='0'>"
            )


            sw.WriteLine("<tr>")


            WriteExcelHeader(
                sw,
                "Investor ID"
            )


            WriteExcelHeader(
                sw,
                "Name"
            )


            WriteExcelHeader(
                sw,
                "Email"
            )


            WriteExcelHeader(
                sw,
                "Mobile"
            )


            WriteExcelHeader(
                sw,
                "Department"
            )


            WriteExcelHeader(
                sw,
                "Designation"
            )


            WriteExcelHeader(
                sw,
                "Investment Amount"
            )


            sw.WriteLine("</tr>")


            For Each row As DataRow In dt.Rows

                sw.WriteLine("<tr>")


                WriteExcelCell(
                    sw,
                    row("InvestorID").ToString()
                )


                WriteExcelCell(
                    sw,
                    row("Name").ToString()
                )


                WriteExcelCell(
                    sw,
                    row("Email").ToString()
                )


                WriteExcelCell(
                    sw,
                    row("Mobile").ToString()
                )


                WriteExcelCell(
                    sw,
                    row("Department").ToString()
                )


                WriteExcelCell(
                    sw,
                    row("Designation").ToString()
                )


                WriteExcelCell(
                    sw,
                    Convert.ToDecimal(
                        row("InvestmentAmount")
                    ).ToString("N2")
                )


                sw.WriteLine("</tr>")

            Next


            sw.WriteLine("</table>")

            sw.WriteLine("</body>")

            sw.WriteLine("</html>")


            Response.Clear()

            Response.Buffer = True

            Response.Charset = "utf-8"

            Response.ContentEncoding =
                Encoding.UTF8

            Response.ContentType =
                "application/vnd.ms-excel"


            Response.AddHeader(
                "Content-Disposition",
                "attachment;filename=Investors_" &
                DateTime.Now.ToString(
                    "yyyyMMdd_HHmmss"
                ) &
                ".xls"
            )


            Response.Write(
                sw.ToString()
            )


            Response.Flush()


            HttpContext.Current.
                ApplicationInstance.
                CompleteRequest()


        Catch ex As Exception

            ShowError(
                "Excel export error: " &
                ex.Message
            )

        End Try

    End Sub


    ' =========================================================
    ' EXCEL HEADER
    ' =========================================================

    Private Sub WriteExcelHeader(
        ByVal sw As StringWriter,
        ByVal value As String
    )

        sw.WriteLine(
            "<th>" &
            Server.HtmlEncode(value) &
            "</th>"
        )

    End Sub


    ' =========================================================
    ' EXCEL CELL
    ' =========================================================

    Private Sub WriteExcelCell(
        ByVal sw As StringWriter,
        ByVal value As String
    )

        sw.WriteLine(
            "<td>" &
            Server.HtmlEncode(value) &
            "</td>"
        )

    End Sub


    ' =========================================================
    ' SINGLE INVESTOR EXCEL
    ' =========================================================

    Private Sub ExportSingleInvestorExcel(
        ByVal investorID As Integer
    )

        Try

            Dim row As DataRow =
                GetInvestor(investorID)


            If row Is Nothing Then

                ShowError(
                    "Investor not found."
                )

                Return

            End If


            Dim sw As New StringWriter()


            sw.WriteLine("<html>")

            sw.WriteLine("<head>")

            sw.WriteLine(
                "<meta http-equiv='Content-Type' " &
                "content='text/html; charset=utf-8'>"
            )

            sw.WriteLine("</head>")

            sw.WriteLine("<body>")


            sw.WriteLine(
                "<h2>Investor Information</h2>"
            )


            sw.WriteLine(
                "<table border='1' " &
                "cellpadding='8' " &
                "cellspacing='0'>"
            )


            WriteExcelRow(
                sw,
                "Investor ID",
                row("InvestorID").ToString()
            )


            WriteExcelRow(
                sw,
                "Name",
                row("Name").ToString()
            )


            WriteExcelRow(
                sw,
                "Email",
                row("Email").ToString()
            )


            WriteExcelRow(
                sw,
                "Mobile",
                row("Mobile").ToString()
            )


            WriteExcelRow(
                sw,
                "Department",
                row("Department").ToString()
            )


            WriteExcelRow(
                sw,
                "Designation",
                row("Designation").ToString()
            )


            WriteExcelRow(
                sw,
                "Investment Amount",
                Convert.ToDecimal(
                    row("InvestmentAmount")
                ).ToString("N2")
            )


            sw.WriteLine("</table>")

            sw.WriteLine("</body>")

            sw.WriteLine("</html>")


            Response.Clear()

            Response.Buffer = True

            Response.Charset = "utf-8"

            Response.ContentEncoding =
                Encoding.UTF8

            Response.ContentType =
                "application/vnd.ms-excel"


            Response.AddHeader(
                "Content-Disposition",
                "attachment;filename=Investor_" &
                investorID.ToString() &
                ".xls"
            )


            Response.Write(
                sw.ToString()
            )


            Response.Flush()


            HttpContext.Current.
                ApplicationInstance.
                CompleteRequest()


        Catch ex As Exception

            ShowError(
                "Excel export error: " &
                ex.Message
            )

        End Try

    End Sub


    ' =========================================================
    ' EXCEL ROW
    ' =========================================================

    Private Sub WriteExcelRow(
        ByVal sw As StringWriter,
        ByVal title As String,
        ByVal value As String
    )

        sw.WriteLine("<tr>")


        sw.WriteLine(
            "<th>" &
            Server.HtmlEncode(title) &
            "</th>"
        )


        sw.WriteLine(
            "<td>" &
            Server.HtmlEncode(value) &
            "</td>"
        )


        sw.WriteLine("</tr>")

    End Sub


    ' =========================================================
    ' DOWNLOAD ALL PDF
    ' =========================================================

    Protected Sub btnDownloadAllPdf_Click(
        ByVal sender As Object,
        ByVal e As EventArgs
    ) Handles btnDownloadAllPdf.Click

        Try

            Dim dt As DataTable =
                GetAllFilteredInvestors()


            If dt.Rows.Count = 0 Then

                ShowError(
                    "No investors found."
                )

                Return

            End If


            Response.Clear()

            Response.Buffer = True

            Response.ContentType =
                "application/pdf"


            Response.AddHeader(
                "Content-Disposition",
                "attachment;filename=Investors_" &
                DateTime.Now.ToString(
                    "yyyyMMdd_HHmmss"
                ) &
                ".pdf"
            )


            Using ms As New MemoryStream()


                Dim document As New iTextSharp.text.Document(
                    iTextSharp.text.PageSize.A4.Rotate(),
                    25,
                    25,
                    25,
                    25
                )


                iTextSharp.text.pdf.PdfWriter.GetInstance(
                    document,
                    ms
                )


                document.Open()


                Dim titleFont As iTextSharp.text.Font =
                    iTextSharp.text.FontFactory.GetFont(
                        iTextSharp.text.FontFactory.HELVETICA_BOLD,
                        18
                    )


                Dim normalFont As iTextSharp.text.Font =
                    iTextSharp.text.FontFactory.GetFont(
                        iTextSharp.text.FontFactory.HELVETICA,
                        9
                    )


                document.Add(
                    New iTextSharp.text.Paragraph(
                        "Investor Report",
                        titleFont
                    )
                )


                document.Add(
                    New iTextSharp.text.Paragraph(
                        "Total Investors: " &
                        dt.Rows.Count.ToString(),
                        normalFont
                    )
                )


                document.Add(
                    New iTextSharp.text.Paragraph(" ")
                )


                Dim table As New iTextSharp.text.pdf.PdfPTable(
                    7
                )


                table.WidthPercentage = 100


                table.SetWidths(
                    New Single() {
                        0.7F,
                        2.0F,
                        2.6F,
                        1.5F,
                        1.5F,
                        1.6F,
                        1.5F
                    }
                )


                AddPdfHeader(
                    table,
                    "ID"
                )


                AddPdfHeader(
                    table,
                    "Name"
                )


                AddPdfHeader(
                    table,
                    "Email"
                )


                AddPdfHeader(
                    table,
                    "Mobile"
                )


                AddPdfHeader(
                    table,
                    "Department"
                )


                AddPdfHeader(
                    table,
                    "Designation"
                )


                AddPdfHeader(
                    table,
                    "Investment"
                )


                For Each row As DataRow In dt.Rows


                    AddPdfValue(
                        table,
                        row("InvestorID").ToString()
                    )


                    AddPdfValue(
                        table,
                        row("Name").ToString()
                    )


                    AddPdfValue(
                        table,
                        row("Email").ToString()
                    )


                    AddPdfValue(
                        table,
                        row("Mobile").ToString()
                    )


                    AddPdfValue(
                        table,
                        row("Department").ToString()
                    )


                    AddPdfValue(
                        table,
                        row("Designation").ToString()
                    )


                    AddPdfValue(
                        table,
                        Convert.ToDecimal(
                            row("InvestmentAmount")
                        ).ToString("N2")
                    )


                Next


                document.Add(table)


                document.Add(
                    New iTextSharp.text.Paragraph(" ")
                )


                document.Add(
                    New iTextSharp.text.Paragraph(
                        "Generated: " &
                        DateTime.Now.ToString(
                            "dd-MM-yyyy HH:mm"
                        ),
                        normalFont
                    )
                )


                document.Close()


                Response.BinaryWrite(
                    ms.ToArray()
                )


            End Using


            Response.Flush()


            HttpContext.Current.
                ApplicationInstance.
                CompleteRequest()


        Catch ex As Exception

            ShowError(
                "PDF export error: " &
                ex.Message
            )

        End Try

    End Sub


    ' =========================================================
    ' SINGLE INVESTOR PDF
    ' =========================================================

    Private Sub DownloadSingleInvestorPdf(
        ByVal investorID As Integer
    )

        Try

            Dim row As DataRow =
                GetInvestor(investorID)


            If row Is Nothing Then

                ShowError(
                    "Investor not found."
                )

                Return

            End If


            Response.Clear()

            Response.Buffer = True

            Response.ContentType =
                "application/pdf"


            Response.AddHeader(
                "Content-Disposition",
                "attachment;filename=Investor_" &
                investorID.ToString() &
                ".pdf"
            )


            Using ms As New MemoryStream()


                Dim document As New iTextSharp.text.Document(
                    iTextSharp.text.PageSize.A4,
                    40,
                    40,
                    40,
                    40
                )


                iTextSharp.text.pdf.PdfWriter.GetInstance(
                    document,
                    ms
                )


                document.Open()


                Dim titleFont As iTextSharp.text.Font =
                    iTextSharp.text.FontFactory.GetFont(
                        iTextSharp.text.FontFactory.HELVETICA_BOLD,
                        18
                    )


                Dim normalFont As iTextSharp.text.Font =
                    iTextSharp.text.FontFactory.GetFont(
                        iTextSharp.text.FontFactory.HELVETICA,
                        10
                    )


                document.Add(
                    New iTextSharp.text.Paragraph(
                        "Investor Information",
                        titleFont
                    )
                )


                document.Add(
                    New iTextSharp.text.Paragraph(" ")
                )


                Dim table As New iTextSharp.text.pdf.PdfPTable(
                    2
                )


                table.WidthPercentage = 100


                AddPdfCell(
                    table,
                    "Investor ID",
                    row("InvestorID").ToString()
                )


                AddPdfCell(
                    table,
                    "Name",
                    row("Name").ToString()
                )


                AddPdfCell(
                    table,
                    "Email",
                    row("Email").ToString()
                )


                AddPdfCell(
                    table,
                    "Mobile",
                    row("Mobile").ToString()
                )


                AddPdfCell(
                    table,
                    "Department",
                    row("Department").ToString()
                )


                AddPdfCell(
                    table,
                    "Designation",
                    row("Designation").ToString()
                )


                AddPdfCell(
                    table,
                    "Investment Amount",
                    Convert.ToDecimal(
                        row("InvestmentAmount")
                    ).ToString("N2")
                )


                document.Add(table)


                document.Add(
                    New iTextSharp.text.Paragraph(" ")
                )


                document.Add(
                    New iTextSharp.text.Paragraph(
                        "Generated: " &
                        DateTime.Now.ToString(
                            "dd-MM-yyyy HH:mm"
                        ),
                        normalFont
                    )
                )


                document.Close()


                Response.BinaryWrite(
                    ms.ToArray()
                )


            End Using


            Response.Flush()


            HttpContext.Current.
                ApplicationInstance.
                CompleteRequest()


        Catch ex As Exception

            ShowError(
                "PDF download error: " &
                ex.Message
            )

        End Try

    End Sub


    ' =========================================================
    ' PDF HEADER
    ' =========================================================

    Private Sub AddPdfHeader(
        ByVal table As iTextSharp.text.pdf.PdfPTable,
        ByVal text As String
    )

        Dim font As iTextSharp.text.Font =
            iTextSharp.text.FontFactory.GetFont(
                iTextSharp.text.FontFactory.HELVETICA_BOLD,
                8
            )


        Dim cell As New iTextSharp.text.pdf.PdfPCell(
            New iTextSharp.text.Phrase(
                text,
                font
            )
        )


        cell.HorizontalAlignment =
            iTextSharp.text.Element.ALIGN_CENTER


        cell.Padding = 5


        table.AddCell(cell)

    End Sub


    ' =========================================================
    ' PDF VALUE
    ' =========================================================

    Private Sub AddPdfValue(
        ByVal table As iTextSharp.text.pdf.PdfPTable,
        ByVal text As String
    )

        Dim font As iTextSharp.text.Font =
            iTextSharp.text.FontFactory.GetFont(
                iTextSharp.text.FontFactory.HELVETICA,
                8
            )


        Dim cell As New iTextSharp.text.pdf.PdfPCell(
            New iTextSharp.text.Phrase(
                text,
                font
            )
        )


        cell.Padding = 4


        table.AddCell(cell)

    End Sub


    ' =========================================================
    ' PDF LABEL + VALUE
    ' =========================================================

    Private Sub AddPdfCell(
        ByVal table As iTextSharp.text.pdf.PdfPTable,
        ByVal label As String,
        ByVal value As String
    )

        Dim labelFont As iTextSharp.text.Font =
            iTextSharp.text.FontFactory.GetFont(
                iTextSharp.text.FontFactory.HELVETICA_BOLD,
                10
            )


        Dim valueFont As iTextSharp.text.Font =
            iTextSharp.text.FontFactory.GetFont(
                iTextSharp.text.FontFactory.HELVETICA,
                10
            )


        Dim labelCell As New iTextSharp.text.pdf.PdfPCell(
            New iTextSharp.text.Phrase(
                label,
                labelFont
            )
        )


        Dim valueCell As New iTextSharp.text.pdf.PdfPCell(
            New iTextSharp.text.Phrase(
                value,
                valueFont
            )
        )


        labelCell.Padding = 7

        valueCell.Padding = 7


        table.AddCell(labelCell)

        table.AddCell(valueCell)

    End Sub


    ' =========================================================
    ' MOBILE DUPLICATE POPUP
    ' =========================================================

    Private Sub ShowMobileDuplicatePopup()

        Dim script As String =
            "alert('This mobile number is already registered by another investor.');"


        ClientScript.RegisterStartupScript(
            Me.GetType(),
            "DuplicateMobile",
            script,
            True
        )

    End Sub


    ' =========================================================
    ' EMAIL DUPLICATE POPUP
    ' =========================================================

    Private Sub ShowEmailDuplicatePopup()

        Dim script As String =
            "alert('This email address is already registered by another investor.');"


        ClientScript.RegisterStartupScript(
            Me.GetType(),
            "DuplicateEmail",
            script,
            True
        )

    End Sub


    ' =========================================================
    ' SUCCESS MESSAGE
    ' =========================================================

    Private Sub ShowSuccess(
        ByVal message As String
    )

        lblMessage.Text =
            message


        lblMessage.Visible =
            True


        lblMessage.ForeColor =
            Drawing.Color.Green


        lblMessage.BackColor =
            Drawing.ColorTranslator.FromHtml(
                "#d1e7dd"
            )


        lblMessage.BorderColor =
            Drawing.ColorTranslator.FromHtml(
                "#badbcc"
            )


        lblMessage.BorderStyle =
            BorderStyle.Solid


        lblMessage.BorderWidth =
            Unit.Pixel(1)

    End Sub


    ' =========================================================
    ' ERROR MESSAGE
    ' =========================================================

    Private Sub ShowError(
        ByVal message As String
    )

        lblMessage.Text =
            message


        lblMessage.Visible =
            True


        lblMessage.ForeColor =
            Drawing.Color.Red


        lblMessage.BackColor =
            Drawing.ColorTranslator.FromHtml(
                "#f8d7da"
            )


        lblMessage.BorderColor =
            Drawing.ColorTranslator.FromHtml(
                "#f5c2c7"
            )


        lblMessage.BorderStyle =
            BorderStyle.Solid


        lblMessage.BorderWidth =
            Unit.Pixel(1)

    End Sub


    ' =========================================================
    ' HIDE MESSAGE
    ' =========================================================

    Private Sub HideMessage()

        lblMessage.Text = ""

        lblMessage.Visible = False

    End Sub

End Class