Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Security.Cryptography
Imports System.Web.UI.WebControls


Partial Class InvestorRegister

    Inherits System.Web.UI.Page


    ' =========================================================
    ' PAGE LOAD
    ' =========================================================

    Protected Sub Page_Load(
        ByVal sender As Object,
        ByVal e As EventArgs
    ) Handles Me.Load

        If Not IsPostBack Then

            lblMessage.Visible = False

            lblMessage.Text = ""

            LoadDepartments()

            LoadDesignations()

        End If

    End Sub


    ' =========================================================
    ' DEPARTMENTS
    ' =========================================================

    Private Sub LoadDepartments()

        ddlDepartment.Items.Clear()

        ddlDepartment.Items.Add(
            New ListItem(
                "-- Select Department --",
                ""
            )
        )

        ddlDepartment.Items.Add(
            New ListItem(
                "Administration",
                "Administration"
            )
        )

        ddlDepartment.Items.Add(
            New ListItem(
                "Finance",
                "Finance"
            )
        )

        ddlDepartment.Items.Add(
            New ListItem(
                "Human Resources",
                "Human Resources"
            )
        )

        ddlDepartment.Items.Add(
            New ListItem(
                "IT",
                "IT"
            )
        )

        ddlDepartment.Items.Add(
            New ListItem(
                "Marketing",
                "Marketing"
            )
        )

        ddlDepartment.Items.Add(
            New ListItem(
                "Operations",
                "Operations"
            )
        )

        ddlDepartment.Items.Add(
            New ListItem(
                "Sales",
                "Sales"
            )
        )

    End Sub


    ' =========================================================
    ' DESIGNATIONS
    ' =========================================================

    Private Sub LoadDesignations()

        ddlDesignation.Items.Clear()

        ddlDesignation.Items.Add(
            New ListItem(
                "-- Select Designation --",
                ""
            )
        )

        ddlDesignation.Items.Add(
            New ListItem(
                "Chairman",
                "Chairman"
            )
        )

        ddlDesignation.Items.Add(
            New ListItem(
                "Managing Director",
                "Managing Director"
            )
        )

        ddlDesignation.Items.Add(
            New ListItem(
                "Director",
                "Director"
            )
        )

        ddlDesignation.Items.Add(
            New ListItem(
                "Manager",
                "Manager"
            )
        )

        ddlDesignation.Items.Add(
            New ListItem(
                "Senior Executive",
                "Senior Executive"
            )
        )

        ddlDesignation.Items.Add(
            New ListItem(
                "Executive",
                "Executive"
            )
        )

        ddlDesignation.Items.Add(
            New ListItem(
                "Officer",
                "Officer"
            )
        )

    End Sub


    ' =========================================================
    ' REGISTER
    ' =========================================================

    Protected Sub btnRegister_Click(
        ByVal sender As Object,
        ByVal e As EventArgs
    ) Handles btnRegister.Click

        Try

            ' =================================================
            ' GET VALUES
            ' =================================================

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

            Dim password As String =
                txtPassword.Text

            Dim confirmPassword As String =
                txtConfirmPassword.Text


            ' =================================================
            ' NAME
            ' =================================================

            If name = "" Then

                ShowError(
                    "Please enter your name."
                )

                Return

            End If


            If name.Length < 2 Then

                ShowError(
                    "Name must contain at least 2 characters."
                )

                Return

            End If


            If name.Length > 100 Then

                ShowError(
                    "Name cannot exceed 100 characters."
                )

                Return

            End If


            ' =================================================
            ' EMAIL
            ' =================================================

            If email = "" Then

                ShowError(
                    "Please enter your email."
                )

                Return

            End If


            Dim emailPattern As String =
                "^[^@\s]+@[^@\s]+\.[^@\s]+$"


            If Not Regex.IsMatch(
                email,
                emailPattern
            ) Then

                ShowError(
                    "Please enter a valid email address."
                )

                Return

            End If


            ' =================================================
            ' MOBILE
            ' =================================================

            If mobile = "" Then

                ShowError(
                    "Please enter your mobile number."
                )

                Return

            End If


            If Not Regex.IsMatch(
                mobile,
                "^01[3-9][0-9]{8}$"
            ) Then

                ShowError(
                    "Please enter a valid Bangladesh mobile number. Example: 01712345678"
                )

                Return

            End If


            ' =================================================
            ' DEPARTMENT
            ' =================================================

            If department = "" Then

                ShowError(
                    "Please select a department."
                )

                Return

            End If


            ' =================================================
            ' DESIGNATION
            ' =================================================

            If designation = "" Then

                ShowError(
                    "Please select a designation."
                )

                Return

            End If


            ' =================================================
            ' INVESTMENT
            ' =================================================

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


            ' =================================================
            ' PASSWORD
            ' =================================================

            If password = "" Then

                ShowError(
                    "Please enter your password."
                )

                Return

            End If


            If password.Length < 8 Then

                ShowError(
                    "Password must be at least 8 characters long."
                )

                Return

            End If


            If Not Regex.IsMatch(
                password,
                "[A-Z]"
            ) Then

                ShowError(
                    "Password must contain at least one uppercase letter."
                )

                Return

            End If


            If Not Regex.IsMatch(
                password,
                "[a-z]"
            ) Then

                ShowError(
                    "Password must contain at least one lowercase letter."
                )

                Return

            End If


            If Not Regex.IsMatch(
                password,
                "[0-9]"
            ) Then

                ShowError(
                    "Password must contain at least one number."
                )

                Return

            End If


            If Not Regex.IsMatch(
                password,
                "[^a-zA-Z0-9]"
            ) Then

                ShowError(
                    "Password must contain at least one special character."
                )

                Return

            End If


            ' =================================================
            ' CONFIRM PASSWORD
            ' =================================================

            If confirmPassword = "" Then

                ShowError(
                    "Please confirm your password."
                )

                Return

            End If


            If password <> confirmPassword Then

                ShowError(
                    "Passwords do not match."
                )

                Return

            End If


            ' =================================================
            ' CONNECTION STRING
            ' =================================================

            Dim connectionString As String =
                ConfigurationManager.
                ConnectionStrings(
                    "InvestorDB"
                ).
                ConnectionString


            If String.IsNullOrWhiteSpace(
                connectionString
            ) Then

                ShowError(
                    "Connection string 'InvestorDB' was not found."
                )

                Return

            End If


            ' =================================================
            ' PASSWORD HASH
            ' =================================================

            Dim passwordHash As String =
                HashPassword(password)


            ' =================================================
            ' DATABASE
            ' =================================================

            Using con As New SqlConnection(
                connectionString
            )

                con.Open()


                ' =================================================
                ' CHECK EMAIL
                ' =================================================

                Dim checkEmailSql As String =
                    "SELECT COUNT(*) " &
                    "FROM dbo.Investors " &
                    "WHERE LOWER(LTRIM(RTRIM([Email]))) = @Email"


                Using cmdCheckEmail As New SqlCommand(
                    checkEmailSql,
                    con
                )

                    cmdCheckEmail.Parameters.Add(
                        "@Email",
                        SqlDbType.VarChar,
                        150
                    ).Value =
                        email.ToLowerInvariant()


                    Dim emailExists As Integer =
                        Convert.ToInt32(
                            cmdCheckEmail.ExecuteScalar()
                        )


                    If emailExists > 0 Then

                        ShowError(
                            "An investor with this email already exists."
                        )

                        Return

                    End If

                End Using


                ' =================================================
                ' CHECK MOBILE
                ' =================================================

                Dim checkMobileSql As String =
                    "SELECT COUNT(*) " &
                    "FROM dbo.Investors " &
                    "WHERE [Mobile] = @Mobile"


                Using cmdCheckMobile As New SqlCommand(
                    checkMobileSql,
                    con
                )

                    cmdCheckMobile.Parameters.Add(
                        "@Mobile",
                        SqlDbType.VarChar,
                        20
                    ).Value =
                        mobile


                    Dim mobileExists As Integer =
                        Convert.ToInt32(
                            cmdCheckMobile.ExecuteScalar()
                        )


                    If mobileExists > 0 Then

                        ShowError(
                            "An investor with this mobile number already exists."
                        )

                        Return

                    End If

                End Using


                ' =================================================
                ' INSERT INVESTOR
                ' =================================================

                Dim insertSql As String =
                    "INSERT INTO dbo.Investors " &
                    "([Name], [Email], [Mobile], " &
                    "[Department], [Designation], " &
                    "[InvestmentAmount], [PasswordHash]) " &
                    "VALUES " &
                    "(@Name, @Email, @Mobile, " &
                    "@Department, @Designation, " &
                    "@InvestmentAmount, @PasswordHash)"


                Using cmd As New SqlCommand(
                    insertSql,
                    con
                )

                    cmd.Parameters.Add(
                        "@Name",
                        SqlDbType.VarChar,
                        100
                    ).Value =
                        name


                    cmd.Parameters.Add(
                        "@Email",
                        SqlDbType.VarChar,
                        150
                    ).Value =
                        email.ToLowerInvariant()


                    cmd.Parameters.Add(
                        "@Mobile",
                        SqlDbType.VarChar,
                        20
                    ).Value =
                        mobile


                    cmd.Parameters.Add(
                        "@Department",
                        SqlDbType.VarChar,
                        100
                    ).Value =
                        department


                    cmd.Parameters.Add(
                        "@Designation",
                        SqlDbType.VarChar,
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
                        "@PasswordHash",
                        SqlDbType.VarChar,
                        64
                    ).Value =
                        passwordHash


                    Dim rows As Integer =
                        cmd.ExecuteNonQuery()


                    If rows <= 0 Then

                        ShowError(
                            "Investor registration failed."
                        )

                        Return

                    End If

                End Using

            End Using


            ' =================================================
            ' SUCCESS
            ' =================================================

            ShowSuccess(
                "Investor account created successfully. You can now login."
            )


            ' =================================================
            ' CLEAR FORM
            ' =================================================

            txtName.Text = ""

            txtEmail.Text = ""

            txtPhone.Text = ""

            ddlDepartment.SelectedIndex = 0

            ddlDesignation.SelectedIndex = 0

            txtInvestmentAmount.Text = ""

            txtPassword.Text = ""

            txtConfirmPassword.Text = ""


        Catch ex As SqlException

            ShowError(
                "Database error: " &
                ex.Message
            )


        Catch ex As Exception

            ShowError(
                "Registration error: " &
                ex.Message
            )

        End Try

    End Sub


    ' =========================================================
    ' PASSWORD HASH
    ' =========================================================

    Private Function HashPassword(
        ByVal password As String
    ) As String

        Using sha256 As SHA256 =
            SHA256.Create()

            Dim bytes As Byte() =
                Encoding.UTF8.GetBytes(
                    password
                )


            Dim hashBytes As Byte() =
                sha256.ComputeHash(
                    bytes
                )


            Dim builder As New StringBuilder()


            For Each b As Byte In hashBytes

                builder.Append(
                    b.ToString("x2")
                )

            Next


            Return builder.ToString()

        End Using

    End Function


    ' =========================================================
    ' SHOW ERROR
    ' =========================================================

    Private Sub ShowError(
        ByVal message As String
    )

        lblMessage.Text =
            message


        lblMessage.Visible =
            True


        lblMessage.ForeColor =
            System.Drawing.ColorTranslator.FromHtml(
                "#b02a37"
            )


        lblMessage.BackColor =
            System.Drawing.ColorTranslator.FromHtml(
                "#f8d7da"
            )


        lblMessage.BorderColor =
            System.Drawing.ColorTranslator.FromHtml(
                "#f5c2c7"
            )


        lblMessage.BorderStyle =
            BorderStyle.Solid


        lblMessage.BorderWidth =
            Unit.Pixel(1)

    End Sub


    ' =========================================================
    ' SHOW SUCCESS
    ' =========================================================

    Private Sub ShowSuccess(
        ByVal message As String
    )

        lblMessage.Text =
            message


        lblMessage.Visible =
            True


        lblMessage.ForeColor =
            System.Drawing.ColorTranslator.FromHtml(
                "#146c43"
            )


        lblMessage.BackColor =
            System.Drawing.ColorTranslator.FromHtml(
                "#d1e7dd"
            )


        lblMessage.BorderColor =
            System.Drawing.ColorTranslator.FromHtml(
                "#badbcc"
            )


        lblMessage.BorderStyle =
            BorderStyle.Solid


        lblMessage.BorderWidth =
            Unit.Pixel(1)

    End Sub


End Class