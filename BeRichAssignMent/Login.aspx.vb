
Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Text

Partial Class login

    Inherits System.Web.UI.Page


    '========================================================
    ' PAGE LOAD
    '========================================================

    Protected Sub Page_Load(
        ByVal sender As Object,
        ByVal e As EventArgs
    ) Handles Me.Load

        If Not IsPostBack Then

            lblMessage.Visible = False
            lblMessage.Text = ""

            ' Default account type
            rblLoginType.SelectedValue = "User"

        End If

    End Sub


    '========================================================
    ' LOGIN BUTTON
    '========================================================

    Protected Sub btnLogin_Click(
        ByVal sender As Object,
        ByVal e As EventArgs
    ) Handles btnLogin.Click


        '====================================================
        ' GET INPUT
        '====================================================

        Dim email As String =
            txtEmail.Text.Trim()

        Dim password As String =
            txtPassword.Text

        Dim loginType As String =
            rblLoginType.SelectedValue


        '====================================================
        ' EMAIL VALIDATION
        '====================================================

        If String.IsNullOrWhiteSpace(email) Then

            ShowError(
                "Please enter your email."
            )

            Return

        End If


        '====================================================
        ' PASSWORD VALIDATION
        '====================================================

        If String.IsNullOrEmpty(password) Then

            ShowError(
                "Please enter your password."
            )

            Return

        End If


        '====================================================
        ' LOGIN TYPE VALIDATION
        '====================================================

        If String.IsNullOrWhiteSpace(loginType) Then

            ShowError(
                "Please select Normal User or Investor."
            )

            Return

        End If


        '====================================================
        ' CONNECTION STRING
        '====================================================

        Dim conStr As String = ""


        Try

            Dim connectionSetting =
                ConfigurationManager.ConnectionStrings(
                    "InvestorDB"
                )


            If connectionSetting Is Nothing Then

                ShowError(
                    "InvestorDB connection string was not found."
                )

                Return

            End If


            conStr =
                connectionSetting.ConnectionString


        Catch ex As Exception

            ShowError(
                "Database connection configuration error."
            )

            Return

        End Try


        If String.IsNullOrWhiteSpace(conStr) Then

            ShowError(
                "InvestorDB connection string is empty."
            )

            Return

        End If


        '====================================================
        ' NORMAL USER LOGIN
        '====================================================

        If loginType.Equals(
            "User",
            StringComparison.OrdinalIgnoreCase
        ) Then

            '------------------------------------------------
            ' IMPORTANT:
            ' Check Admin first.
            '------------------------------------------------

            If LoginAdminIfExists(
                email,
                password,
                conStr
            ) Then

                Return

            End If


            '------------------------------------------------
            ' If not Admin, continue as Normal User.
            '------------------------------------------------

            LoginUser(
                email,
                password,
                conStr
            )

            Return

        End If


        '====================================================
        ' INVESTOR LOGIN
        '====================================================

        If loginType.Equals(
            "Investor",
            StringComparison.OrdinalIgnoreCase
        ) Then

            LoginInvestor(
                email,
                password,
                conStr
            )

            Return

        End If


        '====================================================
        ' INVALID LOGIN TYPE
        '====================================================

        ShowError(
            "Invalid account type selected."
        )

    End Sub


    '========================================================
    ' AUTOMATIC ADMIN LOGIN
    '
    ' Admin does NOT appear on Login.aspx.
    '
    ' Admin is detected using:
    '
    ' Email
    ' Password
    ' Role = Admin
    ' Status = Active
    '
    ' Returns True if an Admin login was handled.
    '========================================================

    Private Function LoginAdminIfExists(
        ByVal email As String,
        ByVal password As String,
        ByVal conStr As String
    ) As Boolean


        Try

            Using con As New SqlConnection(
                conStr
            )

                con.Open()


                '================================================
                ' FIND ADMIN
                '================================================

                Dim sql As String =
                    "SELECT TOP 1 " &
                    "UserID, " &
                    "[Name], " &
                    "[Email], " &
                    "[Password], " &
                    "[Role], " &
                    "[Status] " &
                    "FROM dbo.Users " &
                    "WHERE [Email] = @Email " &
                    "AND [Role] = 'Admin'"


                Using cmd As New SqlCommand(
                    sql,
                    con
                )


                    cmd.Parameters.Add(
                        "@Email",
                        SqlDbType.VarChar,
                        150
                    ).Value = email


                    Using reader As SqlDataReader =
                        cmd.ExecuteReader()


                        '================================================
                        ' EMAIL IS NOT ADMIN
                        '
                        ' Return False so normal User login continues.
                        '================================================

                        If Not reader.Read() Then

                            Return False

                        End If


                        '================================================
                        ' ADMIN ID
                        '================================================

                        Dim adminId As Integer =
                            Convert.ToInt32(
                                reader("UserID")
                            )


                        '================================================
                        ' ADMIN EMAIL
                        '================================================

                        Dim adminEmail As String =
                            reader("Email").
                            ToString().
                            Trim()


                        '================================================
                        ' ADMIN PASSWORD
                        '================================================

                        Dim dbPassword As String =
                            ""


                        If Not IsDBNull(
                            reader("Password")
                        ) Then

                            dbPassword =
                                reader("Password").
                                ToString()

                        End If


                        '================================================
                        ' ADMIN ROLE
                        '================================================

                        Dim role As String =
                            ""


                        If Not IsDBNull(
                            reader("Role")
                        ) Then

                            role =
                                reader("Role").
                                ToString().
                                Trim()

                        End If


                        '================================================
                        ' ADMIN STATUS
                        '================================================

                        Dim status As String =
                            ""


                        If Not IsDBNull(
                            reader("Status")
                        ) Then

                            status =
                                reader("Status").
                                ToString().
                                Trim()

                        End If


                        '================================================
                        ' PASSWORD CHECK
                        '================================================

                        If Not String.Equals(
                            dbPassword,
                            password,
                            StringComparison.Ordinal
                        ) Then

                            reader.Close()

                            ShowError(
                                "Invalid email or password."
                            )

                            Return True

                        End If


                        '================================================
                        ' STATUS CHECK
                        '================================================

                        If Not status.Equals(
                            "Active",
                            StringComparison.OrdinalIgnoreCase
                        ) Then

                            reader.Close()

                            ShowError(
                                "Admin account is inactive."
                            )

                            Return True

                        End If


                        '================================================
                        ' ROLE CHECK
                        '================================================

                        If Not role.Equals(
                            "Admin",
                            StringComparison.OrdinalIgnoreCase
                        ) Then

                            reader.Close()

                            ShowError(
                                "You do not have administrator permission."
                            )

                            Return True

                        End If


                        reader.Close()


                        '================================================
                        ' CREATE ADMIN SESSION
                        '================================================

                        Session("UserID") =
                            adminId


                        Session("Email") =
                            adminEmail


                        Session("Role") =
                            "Admin"


                        Session("Status") =
                            "Active"


                        Session("LoginType") =
                            "Admin"


                        '================================================
                        ' ADMIN REDIRECT
                        '================================================

                        Response.Redirect(
                            "~/AdminDashboard.aspx",
                            False
                        )


                        Context.ApplicationInstance.
                            CompleteRequest()


                        Return True

                    End Using

                End Using

            End Using


        Catch ex As SqlException

            ShowError(
                "Database Error: " &
                ex.Message
            )

            Return True


        Catch ex As Exception

            ShowError(
                "Admin login error: " &
                ex.Message
            )

            Return True

        End Try

    End Function


    '========================================================
    ' NORMAL USER LOGIN
    '========================================================

    Private Sub LoginUser(
        ByVal email As String,
        ByVal password As String,
        ByVal conStr As String
    )


        Try

            Using con As New SqlConnection(
                conStr
            )

                con.Open()


                '================================================
                ' GET NORMAL USER
                '================================================

                Dim sql As String =
                    "SELECT TOP 1 " &
                    "UserID, " &
                    "[Name], " &
                    "[Email], " &
                    "[Password], " &
                    "[Role], " &
                    "[Status] " &
                    "FROM dbo.Users " &
                    "WHERE [Email] = @Email " &
                    "AND [Role] = 'User'"


                Using cmd As New SqlCommand(
                    sql,
                    con
                )


                    cmd.Parameters.Add(
                        "@Email",
                        SqlDbType.VarChar,
                        150
                    ).Value = email


                    Using reader As SqlDataReader =
                        cmd.ExecuteReader()


                        '================================================
                        ' USER NOT FOUND
                        '================================================

                        If Not reader.Read() Then

                            ShowError(
                                "Invalid email or password."
                            )

                            Return

                        End If


                        '================================================
                        ' USER ID
                        '================================================

                        Dim userId As Integer =
                            Convert.ToInt32(
                                reader("UserID")
                            )


                        '================================================
                        ' EMAIL
                        '================================================

                        Dim dbEmail As String =
                            reader("Email").
                            ToString().
                            Trim()


                        '================================================
                        ' PASSWORD
                        '================================================

                        Dim dbPassword As String =
                            ""


                        If Not IsDBNull(
                            reader("Password")
                        ) Then

                            dbPassword =
                                reader("Password").
                                ToString()

                        End If


                        '================================================
                        ' ROLE
                        '================================================

                        Dim role As String =
                            ""


                        If Not IsDBNull(
                            reader("Role")
                        ) Then

                            role =
                                reader("Role").
                                ToString().
                                Trim()

                        End If


                        '================================================
                        ' STATUS
                        '================================================

                        Dim status As String =
                            "Active"


                        If Not IsDBNull(
                            reader("Status")
                        ) Then

                            status =
                                reader("Status").
                                ToString().
                                Trim()

                        End If


                        '================================================
                        ' PASSWORD CHECK
                        '================================================

                        If Not String.Equals(
                            dbPassword,
                            password,
                            StringComparison.Ordinal
                        ) Then

                            reader.Close()

                            ShowError(
                                "Invalid email or password."
                            )

                            Return

                        End If


                        '================================================
                        ' STATUS CHECK
                        '================================================

                        If Not status.Equals(
                            "Active",
                            StringComparison.OrdinalIgnoreCase
                        ) Then

                            reader.Close()

                            ShowError(
                                "Your account is inactive. Please contact the administrator."
                            )

                            Return

                        End If


                        reader.Close()


                        '================================================
                        ' USER SESSION
                        '================================================

                        Session("UserID") =
                            userId


                        Session("Email") =
                            dbEmail


                        Session("Role") =
                            role


                        Session("Status") =
                            status


                        Session("LoginType") =
                            "User"


                        '================================================
                        ' USER REDIRECT
                        '================================================

                        Response.Redirect(
                            "~/UserDashboard.aspx",
                            False
                        )


                        Context.ApplicationInstance.
                            CompleteRequest()

                    End Using

                End Using

            End Using


        Catch ex As SqlException

            ShowError(
                "Database Error: " &
                ex.Message
            )


        Catch ex As Exception

            ShowError(
                "Login error: " &
                ex.Message
            )

        End Try

    End Sub


    '========================================================
    ' INVESTOR LOGIN
    '========================================================

    Private Sub LoginInvestor(
        ByVal email As String,
        ByVal password As String,
        ByVal conStr As String
    )


        Try

            Using con As New SqlConnection(
                conStr
            )

                con.Open()


                '================================================
                ' GET INVESTOR
                '================================================

                Dim sql As String =
                    "SELECT TOP 1 " &
                    "InvestorID, " &
                    "[Name], " &
                    "[Email], " &
                    "[PasswordHash] " &
                    "FROM dbo.Investors " &
                    "WHERE [Email] = @Email"


                Using cmd As New SqlCommand(
                    sql,
                    con
                )


                    cmd.Parameters.Add(
                        "@Email",
                        SqlDbType.NVarChar,
                        150
                    ).Value = email


                    Using reader As SqlDataReader =
                        cmd.ExecuteReader()


                        '================================================
                        ' INVESTOR NOT FOUND
                        '================================================

                        If Not reader.Read() Then

                            ShowError(
                                "Invalid email or password."
                            )

                            Return

                        End If


                        '================================================
                        ' INVESTOR ID
                        '================================================

                        Dim investorId As Integer =
                            Convert.ToInt32(
                                reader("InvestorID")
                            )


                        '================================================
                        ' INVESTOR EMAIL
                        '================================================

                        Dim investorEmail As String =
                            reader("Email").
                            ToString().
                            Trim()


                        '================================================
                        ' STORED PASSWORD HASH
                        '================================================

                        Dim storedHash As String =
                            ""


                        If Not IsDBNull(
                            reader("PasswordHash")
                        ) Then

                            storedHash =
                                reader("PasswordHash").
                                ToString().
                                Trim()

                        End If


                        '================================================
                        ' HASH NOT FOUND
                        '================================================

                        If String.IsNullOrWhiteSpace(
                            storedHash
                        ) Then

                            reader.Close()

                            ShowError(
                                "Invalid email or password."
                            )

                            Return

                        End If


                        '================================================
                        ' HASH ENTERED PASSWORD
                        '================================================

                        Dim enteredHash As String =
                            HashPassword(
                                password
                            )


                        '================================================
                        ' PASSWORD HASH CHECK
                        '================================================

                        If Not String.Equals(
                            storedHash,
                            enteredHash,
                            StringComparison.OrdinalIgnoreCase
                        ) Then

                            reader.Close()

                            ShowError(
                                "Invalid email or password."
                            )

                            Return

                        End If


                        reader.Close()


                        '================================================
                        ' INVESTOR SESSION
                        '================================================

                        Session("UserID") =
                            investorId


                        Session("Email") =
                            investorEmail


                        Session("Role") =
                            "Investor"


                        Session("Status") =
                            "Active"


                        Session("LoginType") =
                            "Investor"


                        '================================================
                        ' INVESTOR REDIRECT
                        '================================================

                        Response.Redirect(
                            "~/MyProfile.aspx",
                            False
                        )


                        Context.ApplicationInstance.
                            CompleteRequest()

                    End Using

                End Using

            End Using


        Catch ex As SqlException

            ShowError(
                "Database Error: " &
                ex.Message
            )


        Catch ex As Exception

            ShowError(
                "Login error: " &
                ex.Message
            )

        End Try

    End Sub


    '========================================================
    ' SHA-256 HASH
    '========================================================

    Private Function HashPassword(
        ByVal password As String
    ) As String


        Using sha256 As SHA256 =
            sha256.Create()


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


    '========================================================
    ' SHOW ERROR
    '========================================================

    Private Sub ShowError(
        ByVal message As String
    )

        lblMessage.Text =
            message


        lblMessage.CssClass =
            "message error-message"


        lblMessage.Visible =
            True

    End Sub


    '========================================================
    ' SHOW SUCCESS
    '========================================================

    Private Sub ShowSuccess(
        ByVal message As String
    )

        lblMessage.Text =
            message


        lblMessage.CssClass =
            "message success-message"


        lblMessage.Visible =
            True

    End Sub

End Class

