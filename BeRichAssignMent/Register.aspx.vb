Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Text.RegularExpressions
Imports System.Drawing
Imports System.Web.UI.WebControls


Partial Class Register

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

        End If

    End Sub


    ' =========================================================
    ' REGISTER BUTTON
    ' =========================================================

    Protected Sub btnRegister_Click(
        ByVal sender As Object,
        ByVal e As EventArgs
    ) Handles btnRegister.Click


        Try

            ' =====================================================
            ' GET VALUES
            ' =====================================================

            Dim name As String =
                txtName.Text.Trim()


            Dim email As String =
                txtEmail.Text.Trim()


            Dim mobile As String =
                txtMobile.Text.Trim()


            Dim password As String =
                txtPassword.Text


            Dim confirmPassword As String =
                txtConfirmPassword.Text


            ' =====================================================
            ' NAME VALIDATION
            ' =====================================================

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


            If name.Length > 150 Then

                ShowError(
                    "Name cannot exceed 150 characters."
                )

                Return

            End If


            ' =====================================================
            ' EMAIL VALIDATION
            ' =====================================================

            If email = "" Then

                ShowError(
                    "Please enter your email."
                )

                Return

            End If


            If email.Length > 150 Then

                ShowError(
                    "Email cannot exceed 150 characters."
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


            ' =====================================================
            ' MOBILE VALIDATION
            ' =====================================================

            If mobile = "" Then

                ShowError(
                    "Please enter your mobile number."
                )

                Return

            End If


            If Not IsValidBangladeshMobile(
                mobile
            ) Then

                ShowError(
                    "Please enter a valid Bangladesh mobile number. Example: 01712345678"
                )

                Return

            End If


            ' =====================================================
            ' PASSWORD EMPTY
            ' =====================================================

            If password = "" Then

                ShowError(
                    "Please enter your password."
                )

                Return

            End If


            ' =====================================================
            ' PASSWORD LENGTH
            ' =====================================================

            If password.Length < 8 Then

                ShowError(
                    "Password must be at least 8 characters long."
                )

                Return

            End If


            If password.Length > 100 Then

                ShowError(
                    "Password cannot exceed 100 characters."
                )

                Return

            End If


            ' =====================================================
            ' UPPERCASE
            ' =====================================================

            If Not Regex.IsMatch(
                password,
                "[A-Z]"
            ) Then

                ShowError(
                    "Password must contain at least one uppercase letter."
                )

                Return

            End If


            ' =====================================================
            ' LOWERCASE
            ' =====================================================

            If Not Regex.IsMatch(
                password,
                "[a-z]"
            ) Then

                ShowError(
                    "Password must contain at least one lowercase letter."
                )

                Return

            End If


            ' =====================================================
            ' NUMBER
            ' =====================================================

            If Not Regex.IsMatch(
                password,
                "[0-9]"
            ) Then

                ShowError(
                    "Password must contain at least one number."
                )

                Return

            End If


            ' =====================================================
            ' SPECIAL CHARACTER
            ' =====================================================

            If Not Regex.IsMatch(
                password,
                "[^a-zA-Z0-9]"
            ) Then

                ShowError(
                    "Password must contain at least one special character."
                )

                Return

            End If


            ' =====================================================
            ' CONFIRM PASSWORD
            ' =====================================================

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


            ' =====================================================
            ' DATABASE CONNECTION
            ' =====================================================

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
                    "Database connection string is not configured."
                )

                Return

            End If


            ' =====================================================
            ' DATABASE
            ' =====================================================

            Using con As New SqlConnection(
                connectionString
            )


                con.Open()


                ' =================================================
                ' CHECK EMAIL
                ' =================================================

                Dim checkEmailSql As String =
                    "SELECT COUNT(*) " &
                    "FROM dbo.Users " &
                    "WHERE [Email] = @Email"


                Using checkEmailCmd As New SqlCommand(
                    checkEmailSql,
                    con
                )


                    checkEmailCmd.Parameters.Add(
                        "@Email",
                        SqlDbType.VarChar,
                        150
                    ).Value =
                        email


                    Dim existingEmail As Integer =
                        Convert.ToInt32(
                            checkEmailCmd.ExecuteScalar()
                        )


                    If existingEmail > 0 Then

                        ShowError(
                            "An account with this email already exists."
                        )

                        Return

                    End If


                End Using


                ' =================================================
                ' CHECK MOBILE
                ' =================================================

                Dim checkMobileSql As String =
                    "SELECT COUNT(*) " &
                    "FROM dbo.Users " &
                    "WHERE [Mobile] = @Mobile"


                Using checkMobileCmd As New SqlCommand(
                    checkMobileSql,
                    con
                )


                    checkMobileCmd.Parameters.Add(
                        "@Mobile",
                        SqlDbType.VarChar,
                        15
                    ).Value =
                        mobile


                    Dim existingMobile As Integer =
                        Convert.ToInt32(
                            checkMobileCmd.ExecuteScalar()
                        )


                    If existingMobile > 0 Then

                        ShowError(
                            "An account with this mobile number already exists."
                        )

                        Return

                    End If


                End Using


                ' =================================================
                ' INSERT USER
                ' =================================================
                '
                ' NORMAL USER:
                '
                ' Role   = User
                ' Status = Active
                '
                ' Therefore user can login immediately.
                ' =================================================

                Dim insertSql As String =
                    "INSERT INTO dbo.Users " &
                    "([Name], [Email], [Mobile], [Password], [Role], [Status]) " &
                    "VALUES " &
                    "(@Name, @Email, @Mobile, @Password, 'User', 'Active')"


                Using cmd As New SqlCommand(
                    insertSql,
                    con
                )


                    ' =================================================
                    ' NAME
                    ' =================================================

                    cmd.Parameters.Add(
                        "@Name",
                        SqlDbType.VarChar,
                        150
                    ).Value =
                        name


                    ' =================================================
                    ' EMAIL
                    ' =================================================

                    cmd.Parameters.Add(
                        "@Email",
                        SqlDbType.VarChar,
                        150
                    ).Value =
                        email


                    ' =================================================
                    ' MOBILE
                    ' =================================================

                    cmd.Parameters.Add(
                        "@Mobile",
                        SqlDbType.VarChar,
                        15
                    ).Value =
                        mobile


                    ' =================================================
                    ' PASSWORD
                    ' =================================================

                    cmd.Parameters.Add(
                        "@Password",
                        SqlDbType.VarChar,
                        100
                    ).Value =
                        password


                    ' =================================================
                    ' INSERT
                    ' =================================================

                    Dim rowsInserted As Integer =
                        cmd.ExecuteNonQuery()


                    If rowsInserted <= 0 Then

                        ShowError(
                            "Account could not be created."
                        )

                        Return

                    End If


                End Using


            End Using


            ' =====================================================
            ' SUCCESS
            ' =====================================================

            ShowSuccess(
                "Account created successfully. You can now login."
            )


            ' =====================================================
            ' CLEAR FORM
            ' =====================================================

            txtName.Text = ""

            txtEmail.Text = ""

            txtMobile.Text = ""

            txtPassword.Text = ""

            txtConfirmPassword.Text = ""


        Catch ex As SqlException


            ShowError(
                "Database error during registration: " &
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
    ' SHOW ERROR
    ' =========================================================

    Private Sub ShowError(
        ByVal message As String
    )


        lblMessage.Text =
            message


        lblMessage.Visible =
            True


        ' IMPORTANT:
        ' Do NOT use:
        '
        ' Color.FromHtml()
        '
        ' Use ColorTranslator.FromHtml()
        ' =====================================================

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