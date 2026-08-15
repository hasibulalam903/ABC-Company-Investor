Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Partial Class Investors
    Inherits System.Web.UI.Page


    ' ==========================================
    ' CONNECTION STRING
    ' ==========================================

    Private ReadOnly conStr As String =
        ConfigurationManager.ConnectionStrings(
            "InvestorDB"
        ).ConnectionString


    ' ==========================================
    ' PAGE LOAD
    ' ==========================================

    Protected Sub Page_Load(
        sender As Object,
        e As EventArgs
    ) Handles Me.Load


        ' ==========================================
        ' ADMIN ACCESS ONLY
        ' ==========================================

        If Session("UserID") Is Nothing Then

            Response.Redirect(
                "~/Login.aspx",
                False
            )

            Context.ApplicationInstance.CompleteRequest()

            Return

        End If


        ' ==========================================
        ' ADMIN ROLE ONLY
        ' ==========================================

        If Session("UserRole") Is Nothing OrElse
           Not Session("UserRole").ToString().Trim().Equals(
               "admin",
               StringComparison.OrdinalIgnoreCase
           ) Then

            Response.Redirect(
                "~/Home.aspx",
                False
            )

            Context.ApplicationInstance.CompleteRequest()

            Return

        End If


        ' ==========================================
        ' FIRST PAGE LOAD
        ' ==========================================

        If Not IsPostBack Then

            lblMessage.Visible = False

            hfInvestorID.Value = ""

            btnSave.Text = "Add Investor"

            btnCancel.Visible = False

            LoadInvestors()

            LoadStatistics()

        End If

    End Sub


    ' ==========================================
    ' LOAD ALL INVESTORS
    ' ==========================================

    Private Sub LoadInvestors()

        Try

            Using con As New SqlConnection(conStr)

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
                    "ORDER BY InvestorID DESC"


                Using cmd As New SqlCommand(
                    sql,
                    con
                )

                    Using da As New SqlDataAdapter(cmd)

                        Dim dt As New DataTable()

                        da.Fill(dt)

                        gvInvestors.DataSource = dt

                        gvInvestors.DataBind()

                    End Using

                End Using

            End Using


        Catch ex As Exception

            ShowError(
                "Error loading investors: " &
                ex.Message
            )

        End Try

    End Sub


    ' ==========================================
    ' LOAD STATISTICS
    '
    ' Total Investors
    ' Total Investment
    ' Department-wise Count
    ' Designation-wise Count
    ' ==========================================

    Private Sub LoadStatistics()

        Try

            Using con As New SqlConnection(conStr)

                con.Open()


                ' ==========================================
                ' TOTAL INVESTORS
                ' ==========================================

                Dim totalSql As String =
                    "SELECT COUNT(*) " &
                    "FROM dbo.Investors"


                Using cmdTotal As New SqlCommand(
                    totalSql,
                    con
                )

                    Dim totalInvestors As Integer =
                        Convert.ToInt32(
                            cmdTotal.ExecuteScalar()
                        )

                    lblTotalInvestors.Text =
                        totalInvestors.ToString()

                End Using


                ' ==========================================
                ' TOTAL INVESTMENT
                ' ==========================================

                Dim investmentSql As String =
                    "SELECT ISNULL(SUM(InvestmentAmount), 0) " &
                    "FROM dbo.Investors"


                Using cmdInvestment As New SqlCommand(
                    investmentSql,
                    con
                )

                    Dim totalInvestment As Decimal =
                        Convert.ToDecimal(
                            cmdInvestment.ExecuteScalar()
                        )

                    lblTotalInvestment.Text =
                        totalInvestment.ToString("N2")

                End Using


                ' ==========================================
                ' DEPARTMENT-WISE COUNT
                ' ==========================================

                Dim departmentSql As String =
                    "SELECT " &
                    "ISNULL(NULLIF(LTRIM(RTRIM([Department])), ''), 'Not Specified') AS Department, " &
                    "COUNT(*) AS InvestorCount " &
                    "FROM dbo.Investors " &
                    "GROUP BY " &
                    "ISNULL(NULLIF(LTRIM(RTRIM([Department])), ''), 'Not Specified') " &
                    "ORDER BY InvestorCount DESC, Department"


                Using cmdDepartment As New SqlCommand(
                    departmentSql,
                    con
                )

                    Using da As New SqlDataAdapter(
                        cmdDepartment
                    )

                        Dim dtDepartment As New DataTable()

                        da.Fill(dtDepartment)

                        gvDepartmentStats.DataSource =
                            dtDepartment

                        gvDepartmentStats.DataBind()

                    End Using

                End Using


                ' ==========================================
                ' DESIGNATION-WISE COUNT
                ' ==========================================

                Dim designationSql As String =
                    "SELECT " &
                    "ISNULL(NULLIF(LTRIM(RTRIM([Designation])), ''), 'Not Specified') AS Designation, " &
                    "COUNT(*) AS InvestorCount " &
                    "FROM dbo.Investors " &
                    "GROUP BY " &
                    "ISNULL(NULLIF(LTRIM(RTRIM([Designation])), ''), 'Not Specified') " &
                    "ORDER BY InvestorCount DESC, Designation"


                Using cmdDesignation As New SqlCommand(
                    designationSql,
                    con
                )

                    Using da As New SqlDataAdapter(
                        cmdDesignation
                    )

                        Dim dtDesignation As New DataTable()

                        da.Fill(dtDesignation)

                        gvDesignationStats.DataSource =
                            dtDesignation

                        gvDesignationStats.DataBind()

                    End Using

                End Using

            End Using


        Catch ex As Exception

            ShowError(
                "Error loading statistics: " &
                ex.Message
            )

        End Try

    End Sub


    ' ==========================================
    ' SEARCH BUTTON
    ' ==========================================

    Protected Sub btnSearch_Click(
        sender As Object,
        e As EventArgs
    ) Handles btnSearch.Click

        SearchInvestors()

    End Sub


    ' ==========================================
    ' SEARCH INVESTORS
    ' ==========================================

    Private Sub SearchInvestors()

        Dim searchText As String =
            txtSearch.Text.Trim()

        Dim department As String =
            ddlSearchDepartment.SelectedValue.Trim()

        Dim designation As String =
            ddlSearchDesignation.SelectedValue.Trim()


        Try

            Using con As New SqlConnection(conStr)

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


                ' ==========================================
                ' SEARCH NAME / EMAIL / MOBILE
                ' ==========================================

                If searchText <> "" Then

                    sql &=
                        "AND (" &
                        "[Name] LIKE @Search " &
                        "OR [Email] LIKE @Search " &
                        "OR [Mobile] LIKE @Search" &
                        ") "

                End If


                ' ==========================================
                ' DEPARTMENT FILTER
                ' ==========================================

                If department <> "" Then

                    sql &=
                        "AND [Department] = @Department "

                End If


                ' ==========================================
                ' DESIGNATION FILTER
                ' ==========================================

                If designation <> "" Then

                    sql &=
                        "AND [Designation] = @Designation "

                End If


                sql &=
                    "ORDER BY InvestorID DESC"


                Using cmd As New SqlCommand(
                    sql,
                    con
                )


                    ' ==========================================
                    ' SEARCH PARAMETER
                    ' ==========================================

                    If searchText <> "" Then

                        cmd.Parameters.Add(
                            "@Search",
                            SqlDbType.NVarChar,
                            150
                        ).Value =
                            "%" & searchText & "%"

                    End If


                    ' ==========================================
                    ' DEPARTMENT PARAMETER
                    ' ==========================================

                    If department <> "" Then

                        cmd.Parameters.Add(
                            "@Department",
                            SqlDbType.NVarChar,
                            100
                        ).Value =
                            department

                    End If


                    ' ==========================================
                    ' DESIGNATION PARAMETER
                    ' ==========================================

                    If designation <> "" Then

                        cmd.Parameters.Add(
                            "@Designation",
                            SqlDbType.NVarChar,
                            100
                        ).Value =
                            designation

                    End If


                    Using da As New SqlDataAdapter(cmd)

                        Dim dt As New DataTable()

                        da.Fill(dt)

                        gvInvestors.DataSource =
                            dt

                        gvInvestors.DataBind()


                        If dt.Rows.Count = 0 Then

                            ShowError(
                                "No investors found."
                            )

                        Else

                            ShowSuccess(
                                dt.Rows.Count.ToString() &
                                " investor(s) found."
                            )

                        End If

                    End Using

                End Using

            End Using


        Catch ex As Exception

            ShowError(
                "Search error: " &
                ex.Message
            )

        End Try

    End Sub


    ' ==========================================
    ' CLEAR SEARCH
    ' ==========================================

    Protected Sub btnClearSearch_Click(
        sender As Object,
        e As EventArgs
    ) Handles btnClearSearch.Click

        txtSearch.Text = ""

        ddlSearchDepartment.SelectedIndex = 0

        ddlSearchDesignation.SelectedIndex = 0

        lblMessage.Visible = False

        LoadInvestors()

        LoadStatistics()

    End Sub


    ' ==========================================
    ' ADD / UPDATE INVESTOR
    ' ==========================================

    Protected Sub btnSave_Click(
        sender As Object,
        e As EventArgs
    ) Handles btnSave.Click


        Dim name As String =
            txtName.Text.Trim()

        Dim email As String =
            txtEmail.Text.Trim()

        Dim mobile As String =
            txtPhone.Text.Trim()

        Dim department As String =
            ddlDepartment.SelectedValue.Trim()

        Dim designation As String =
            ddlDesignation.SelectedValue.Trim()

        Dim investmentText As String =
            txtInvestmentAmount.Text.Trim()


        ' ==========================================
        ' VALIDATION
        ' ==========================================

        If name = "" Then

            ShowError(
                "Please enter investor name."
            )

            Return

        End If


        If email = "" Then

            ShowError(
                "Please enter email."
            )

            Return

        End If


        If mobile = "" Then

            ShowError(
                "Please enter mobile number."
            )

            Return

        End If


        If investmentText = "" Then

            ShowError(
                "Please enter investment amount."
            )

            Return

        End If


        Dim investmentAmount As Decimal


        If Not Decimal.TryParse(
            investmentText,
            investmentAmount
        ) Then

            ShowError(
                "Please enter a valid investment amount."
            )

            Return

        End If


        If investmentAmount < 0 Then

            ShowError(
                "Investment amount cannot be negative."
            )

            Return

        End If


        Try

            Using con As New SqlConnection(conStr)

                con.Open()


                ' ==========================================
                ' UPDATE
                ' ==========================================

                If hfInvestorID.Value <> "" Then

                    Dim investorId As Integer


                    If Not Integer.TryParse(
                        hfInvestorID.Value,
                        investorId
                    ) Then

                        ShowError(
                            "Invalid investor ID."
                        )

                        Return

                    End If


                    Dim updateSql As String =
                        "UPDATE dbo.Investors SET " &
                        "[Name] = @Name, " &
                        "[Email] = @Email, " &
                        "[Mobile] = @Mobile, " &
                        "[Department] = @Department, " &
                        "[Designation] = @Designation, " &
                        "[InvestmentAmount] = @InvestmentAmount " &
                        "WHERE InvestorID = @InvestorID"


                    Using cmd As New SqlCommand(
                        updateSql,
                        con
                    )


                        cmd.Parameters.Add(
                            "@Name",
                            SqlDbType.NVarChar,
                            100
                        ).Value = name


                        cmd.Parameters.Add(
                            "@Email",
                            SqlDbType.NVarChar,
                            150
                        ).Value = email


                        cmd.Parameters.Add(
                            "@Mobile",
                            SqlDbType.NVarChar,
                            20
                        ).Value = mobile


                        cmd.Parameters.Add(
                            "@Department",
                            SqlDbType.NVarChar,
                            100
                        ).Value = department


                        cmd.Parameters.Add(
                            "@Designation",
                            SqlDbType.NVarChar,
                            100
                        ).Value = designation


                        Dim pInvestment =
                            cmd.Parameters.Add(
                                "@InvestmentAmount",
                                SqlDbType.Decimal
                            )


                        pInvestment.Precision = 18

                        pInvestment.Scale = 2

                        pInvestment.Value =
                            investmentAmount


                        cmd.Parameters.Add(
                            "@InvestorID",
                            SqlDbType.Int
                        ).Value = investorId


                        cmd.ExecuteNonQuery()

                    End Using


                    ShowSuccess(
                        "Investor updated successfully."
                    )


                    ClearForm()

                    hfInvestorID.Value = ""

                    btnSave.Text =
                        "Add Investor"

                    btnCancel.Visible =
                        False


                    LoadInvestors()

                    LoadStatistics()

                    Return

                End If


                ' ==========================================
                ' CHECK DUPLICATE EMAIL
                ' ==========================================

                Dim emailCheckSql As String =
                    "SELECT COUNT(*) " &
                    "FROM dbo.Investors " &
                    "WHERE Email = @Email"


                Using checkEmail As New SqlCommand(
                    emailCheckSql,
                    con
                )

                    checkEmail.Parameters.Add(
                        "@Email",
                        SqlDbType.NVarChar,
                        150
                    ).Value =
                        email


                    Dim count As Integer =
                        Convert.ToInt32(
                            checkEmail.ExecuteScalar()
                        )


                    If count > 0 Then

                        ShowError(
                            "This email already exists."
                        )

                        Return

                    End If

                End Using


                ' ==========================================
                ' CHECK DUPLICATE MOBILE
                ' ==========================================

                Dim mobileCheckSql As String =
                    "SELECT COUNT(*) " &
                    "FROM dbo.Investors " &
                    "WHERE Mobile = @Mobile"


                Using checkMobile As New SqlCommand(
                    mobileCheckSql,
                    con
                )

                    checkMobile.Parameters.Add(
                        "@Mobile",
                        SqlDbType.NVarChar,
                        20
                    ).Value =
                        mobile


                    Dim count As Integer =
                        Convert.ToInt32(
                            checkMobile.ExecuteScalar()
                        )


                    If count > 0 Then

                        ShowError(
                            "This mobile number already exists."
                        )

                        Return

                    End If

                End Using


                ' ==========================================
                ' INSERT
                ' ==========================================

                Dim insertSql As String =
                    "INSERT INTO dbo.Investors " &
                    "([Name], [Email], [Mobile], " &
                    "[Department], [Designation], " &
                    "[InvestmentAmount]) " &
                    "VALUES " &
                    "(@Name, @Email, @Mobile, " &
                    "@Department, @Designation, " &
                    "@InvestmentAmount)"


                Using cmd As New SqlCommand(
                    insertSql,
                    con
                )


                    cmd.Parameters.Add(
                        "@Name",
                        SqlDbType.NVarChar,
                        100
                    ).Value =
                        name


                    cmd.Parameters.Add(
                        "@Email",
                        SqlDbType.NVarChar,
                        150
                    ).Value =
                        email


                    cmd.Parameters.Add(
                        "@Mobile",
                        SqlDbType.NVarChar,
                        20
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


                    Dim pInvestment =
                        cmd.Parameters.Add(
                            "@InvestmentAmount",
                            SqlDbType.Decimal
                        )


                    pInvestment.Precision = 18

                    pInvestment.Scale = 2

                    pInvestment.Value =
                        investmentAmount


                    cmd.ExecuteNonQuery()

                End Using

            End Using


            ShowSuccess(
                "Investor added successfully."
            )


            ClearForm()

            LoadInvestors()

            LoadStatistics()


        Catch ex As Exception

            ShowError(
                "Save error: " &
                ex.Message
            )

        End Try

    End Sub


    ' ==========================================
    ' GRID SELECT
    ' ==========================================

    Protected Sub gvInvestors_SelectedIndexChanged(
        sender As Object,
        e As EventArgs
    ) Handles gvInvestors.SelectedIndexChanged


        If gvInvestors.SelectedIndex < 0 Then

            Return

        End If


        Dim investorId As Integer =
            Convert.ToInt32(
                gvInvestors.DataKeys(
                    gvInvestors.SelectedIndex
                ).Value
            )


        LoadInvestor(investorId)

    End Sub


    ' ==========================================
    ' LOAD INVESTOR FOR EDIT
    ' ==========================================

    Private Sub LoadInvestor(
        ByVal investorId As Integer
    )

        Try

            Using con As New SqlConnection(conStr)


                Dim sql As String =
                    "SELECT " &
                    "[Name], " &
                    "[Email], " &
                    "[Mobile], " &
                    "[Department], " &
                    "[Designation], " &
                    "[InvestmentAmount] " &
                    "FROM dbo.Investors " &
                    "WHERE InvestorID = @InvestorID"


                Using cmd As New SqlCommand(
                    sql,
                    con
                )


                    cmd.Parameters.Add(
                        "@InvestorID",
                        SqlDbType.Int
                    ).Value =
                        investorId


                    con.Open()


                    Using reader As SqlDataReader =
                        cmd.ExecuteReader()


                        If reader.Read() Then


                            txtName.Text =
                                reader("Name").ToString()


                            txtEmail.Text =
                                reader("Email").ToString()


                            txtPhone.Text =
                                reader("Mobile").ToString()


                            If ddlDepartment.Items.FindByValue(
                                reader("Department").ToString()
                            ) IsNot Nothing Then

                                ddlDepartment.SelectedValue =
                                    reader("Department").ToString()

                            End If


                            If ddlDesignation.Items.FindByValue(
                                reader("Designation").ToString()
                            ) IsNot Nothing Then

                                ddlDesignation.SelectedValue =
                                    reader("Designation").ToString()

                            End If


                            txtInvestmentAmount.Text =
                                reader(
                                    "InvestmentAmount"
                                ).ToString()


                            hfInvestorID.Value =
                                investorId.ToString()


                            btnSave.Text =
                                "Update Investor"


                            btnCancel.Visible =
                                True


                            ShowSuccess(
                                "Investor selected. You can now update the information."
                            )

                        End If

                    End Using

                End Using

            End Using


        Catch ex As Exception

            ShowError(
                "Error loading investor: " &
                ex.Message
            )

        End Try

    End Sub


    ' ==========================================
    ' DELETE INVESTOR
    ' ==========================================

    Protected Sub gvInvestors_RowCommand(
        sender As Object,
        e As GridViewCommandEventArgs
    ) Handles gvInvestors.RowCommand


        If e.CommandName <> "DeleteInvestor" Then

            Return

        End If


        Try


            Dim investorId As Integer =
                Convert.ToInt32(
                    e.CommandArgument
                )


            Using con As New SqlConnection(conStr)


                Dim sql As String =
                    "DELETE FROM dbo.Investors " &
                    "WHERE InvestorID = @InvestorID"


                Using cmd As New SqlCommand(
                    sql,
                    con
                )


                    cmd.Parameters.Add(
                        "@InvestorID",
                        SqlDbType.Int
                    ).Value =
                        investorId


                    con.Open()

                    cmd.ExecuteNonQuery()

                End Using

            End Using


            ShowSuccess(
                "Investor deleted successfully."
            )


            ClearForm()

            hfInvestorID.Value = ""

            btnSave.Text =
                "Add Investor"

            btnCancel.Visible =
                False


            If txtSearch.Text.Trim() <> "" OrElse
               ddlSearchDepartment.SelectedValue <> "" OrElse
               ddlSearchDesignation.SelectedValue <> "" Then

                SearchInvestors()

            Else

                LoadInvestors()

            End If


            LoadStatistics()


        Catch ex As Exception

            ShowError(
                "Delete error: " &
                ex.Message
            )

        End Try

    End Sub


    ' ==========================================
    ' CANCEL UPDATE
    ' ==========================================

    Protected Sub btnCancel_Click(
        sender As Object,
        e As EventArgs
    ) Handles btnCancel.Click


        ClearForm()

        hfInvestorID.Value = ""

        btnSave.Text =
            "Add Investor"

        btnCancel.Visible =
            False

        gvInvestors.SelectedIndex = -1

        lblMessage.Visible =
            False

    End Sub


    ' ==========================================
    ' CLEAR FORM
    ' ==========================================

    Private Sub ClearForm()

        txtName.Text = ""

        txtEmail.Text = ""

        txtPhone.Text = ""

        ddlDepartment.SelectedIndex = 0

        ddlDesignation.SelectedIndex = 0

        txtInvestmentAmount.Text = ""

    End Sub


    ' ==========================================
    ' SUCCESS MESSAGE
    ' ==========================================

    Private Sub ShowSuccess(
        ByVal message As String
    )

        lblMessage.Text =
            message

        lblMessage.Visible =
            True

        lblMessage.ForeColor =
            Drawing.Color.DarkGreen

        lblMessage.BackColor =
            Drawing.ColorTranslator.FromHtml(
                "#d1e7dd"
            )

        lblMessage.BorderColor =
            Drawing.ColorTranslator.FromHtml(
                "#badbcc"
            )

    End Sub


    ' ==========================================
    ' ERROR MESSAGE
    ' ==========================================

    Private Sub ShowError(
        ByVal message As String
    )

        lblMessage.Text =
            message

        lblMessage.Visible =
            True

        lblMessage.ForeColor =
            Drawing.Color.DarkRed

        lblMessage.BackColor =
            Drawing.ColorTranslator.FromHtml(
                "#f8d7da"
            )

        lblMessage.BorderColor =
            Drawing.ColorTranslator.FromHtml(
                "#f5c2c7"
            )

    End Sub

End Class