Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Text
Imports System.Text.RegularExpressions

Partial Class ForgotPassword

    Inherits System.Web.UI.Page


    ' =========================================================
    ' PAGE LOAD
    ' =========================================================

    Protected Sub Page_Load(
        ByVal sender As Object,
        ByVal e As EventArgs
    )

        If Not IsPostBack Then

            lblMessage.Visible = False

            lblMessage.Text = ""

        End If

    End Sub


    ' =========================================================
    ' RESET PASSWORD
    ' =========================================================

    Protected Sub btnResetPassword_Click(
        ByVal sender As Object,
        ByVal e As EventArgs
    )

        Dim email As String
        Dim newPassword As String
        Dim confirmPassword As String


        ' =====================================================
        ' GET FORM VALUES
        ' =====================================================

        email = txtEmail.Text.Trim()

        newPassword = txtNewPassword.Text

        confirmPassword = txtConfirmPassword.Text


        ' =====================================================
        ' EMAIL REQUIRED
        ' =====================================================

        If email = "" Then

            ShowError(
                "Please enter your registered email."
            )

            Return

        End If


        ' =====================================================
        ' EMAIL FORMAT
        ' =====================================================

        If Not Regex.IsMatch(
            email,
            "^[^@\s]+@[^@\s]+\.[^@\s]+$"
        ) Then

            ShowError(
                "Please enter a valid email address."
            )

            Return

        End If


        ' =====================================================
        ' PASSWORD REQUIRED
        ' =====================================================

        If newPassword = "" Then

            ShowError(
                "Please enter a new password."
            )

            Return

        End If


        ' =====================================================
        ' PASSWORD LENGTH
        ' =====================================================

        If newPassword.Length < 8 Then

            ShowError(
                "Password must contain at least 8 characters."
            )

            Return

        End If


        ' =====================================================
        ' UPPERCASE
        ' =====================================================

        If Not Regex.IsMatch(
            newPassword,
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
            newPassword,
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
            newPassword,
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
            newPassword,
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
                "Please confirm your new password."
            )

            Return

        End If


        If newPassword <> confirmPassword Then

            ShowError(
                "Passwords do not match."
            )

            Return

        End If


        ' =====================================================
        ' GET CONNECTION STRING
        ' =====================================================

        Dim cs As String = ""


        Try

            If ConfigurationManager.ConnectionStrings(
                "InvestorDB"
            ) Is Nothing Then

                ShowError(
                    "InvestorDB connection string was not found."
                )

                Return

            End If


            cs =
                ConfigurationManager.ConnectionStrings(
                    "InvestorDB"
                ).ConnectionString


            If String.IsNullOrWhiteSpace(cs) Then

                ShowError(
                    "InvestorDB connection string is empty."
                )

                Return

            End If


        Catch ex As Exception

            ShowError(
                "Unable to read database connection string."
            )

            Return

        End Try


        ' =========================================================
        ' HASH PASSWORD
        ' =========================================================

        Dim passwordHash As String

        passwordHash =
            HashPassword(
                newPassword
            )


        ' =========================================================
        ' DATABASE
        ' =========================================================

        Try

            Using con As New SqlConnection(cs)

                con.Open()


                ' =================================================
                ' CHECK INVESTOR
                ' =================================================

                Dim investorId As Object

                investorId = Nothing


                Dim investorSql As String

                investorSql =
                    "SELECT TOP 1 InvestorID " &
                    "FROM dbo.Investors " &
                    "WHERE LOWER(LTRIM(RTRIM(Email))) = @Email"


                Using cmd As New SqlCommand(
                    investorSql,
                    con
                )

                    cmd.Parameters.Add(
                        "@Email",
                        SqlDbType.VarChar,
                        150
                    ).Value =
                        email.ToLowerInvariant()


                    investorId =
                        cmd.ExecuteScalar()

                End Using


                ' =================================================
                ' INVESTOR FOUND
                ' =================================================

                If investorId IsNot Nothing AndAlso
                   investorId IsNot DBNull.Value Then


                    Dim updateInvestorSql As String

                    updateInvestorSql =
                        "UPDATE dbo.Investors " &
                        "SET PasswordHash = @PasswordHash " &
                        "WHERE InvestorID = @InvestorID"


                    Using cmd As New SqlCommand(
                        updateInvestorSql,
                        con
                    )

                        cmd.Parameters.Add(
                            "@PasswordHash",
                            SqlDbType.VarChar,
                            64
                        ).Value =
                            passwordHash


                        cmd.Parameters.Add(
                            "@InvestorID",
                            SqlDbType.Int
                        ).Value =
                            Convert.ToInt32(
                                investorId
                            )


                        Dim rowsUpdated As Integer

                        rowsUpdated =
                            cmd.ExecuteNonQuery()


                        If rowsUpdated > 0 Then

                            ShowSuccess(
                                "Password changed successfully. You can now login."
                            )


                            ClearPasswordFields()

                            Return

                        End If

                    End Using

                End If


                ' =================================================
                ' CHECK NORMAL USER
                ' =================================================

                Dim userId As Object

                userId = Nothing


                Dim userSql As String

                userSql =
                    "SELECT TOP 1 UserID " &
                    "FROM dbo.Users " &
                    "WHERE LOWER(LTRIM(RTRIM(Email))) = @Email"


                Using cmd As New SqlCommand(
                    userSql,
                    con
                )

                    cmd.Parameters.Add(
                        "@Email",
                        SqlDbType.VarChar,
                        150
                    ).Value =
                        email.ToLowerInvariant()


                    userId =
                        cmd.ExecuteScalar()

                End Using


                ' =================================================
                ' NORMAL USER FOUND
                ' =================================================

                If userId IsNot Nothing AndAlso
                   userId IsNot DBNull.Value Then


                    Dim updateUserSql As String

                    updateUserSql =
                        "UPDATE dbo.Users " &
                        "SET Password = @Password " &
                        "WHERE UserID = @UserID"


                    Using cmd As New SqlCommand(
                        updateUserSql,
                        con
                    )

                        cmd.Parameters.Add(
                            "@Password",
                            SqlDbType.VarChar,
                            64
                        ).Value =
                            passwordHash


                        cmd.Parameters.Add(
                            "@UserID",
                            SqlDbType.Int
                        ).Value =
                            Convert.ToInt32(
                                userId
                            )


                        Dim rowsUpdated As Integer

                        rowsUpdated =
                            cmd.ExecuteNonQuery()


                        If rowsUpdated > 0 Then

                            ShowSuccess(
                                "Password changed successfully. You can now login."
                            )


                            ClearPasswordFields()

                            Return

                        End If

                    End Using

                End If


                ' =================================================
                ' EMAIL NOT FOUND
                ' =================================================

                ShowError(
                    "No account was found with this email address."
                )


            End Using


        Catch ex As SqlException

            ShowError(
                "Database error: " &
                ex.Message
            )


        Catch ex As Exception

            ShowError(
                "Error: " &
                ex.Message
            )

        End Try

    End Sub


    ' =========================================================
    ' SHA-256 PASSWORD HASH
    ' =========================================================

    Private Function HashPassword(
        ByVal password As String
    ) As String

        Using sha256 As SHA256 =
            SHA256.Create()


            Dim bytes As Byte()

            bytes =
                Encoding.UTF8.GetBytes(
                    password
                )


            Dim hashBytes As Byte()

            hashBytes =
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
    ' CLEAR PASSWORD FIELDS
    ' =========================================================

    Private Sub ClearPasswordFields()

        txtNewPassword.Text = ""

        txtConfirmPassword.Text = ""

    End Sub


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


        lblMessage.CssClass =
            "message message-error"

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


        lblMessage.CssClass =
            "message message-success"

    End Sub


End Class