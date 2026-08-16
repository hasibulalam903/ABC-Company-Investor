Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Security.Cryptography
Imports System.Text


Partial Class login
    Inherits System.Web.UI.Page


    '==================================================
    ' PAGE LOAD
    '==================================================

    Protected Sub Page_Load(
        ByVal sender As Object,
        ByVal e As EventArgs
    ) Handles Me.Load


        If Not IsPostBack Then

            lblMessage.Visible = False

            lblMessage.Text = ""

        End If

    End Sub


    '==================================================
    ' LOGIN BUTTON
    '==================================================

    Protected Sub btnLogin_Click(
        ByVal sender As Object,
        ByVal e As EventArgs
    ) Handles btnLogin.Click


        '==================================================
        ' GET INPUT
        '==================================================

        Dim email As String =
            txtEmail.Text.Trim()

        Dim password As String =
            txtPassword.Text


        '==================================================
        ' VALIDATION
        '==================================================

        If email = "" Then

            ShowMessage(
                "Please enter your email."
            )

            Return

        End If


        If password = "" Then

            ShowMessage(
                "Please enter your password."
            )

            Return

        End If


        '==================================================
        ' CONNECTION STRING
        '==================================================

        Dim conStr As String =
            ConfigurationManager.ConnectionStrings(
                "InvestorDB"
            ).ConnectionString


        Try

            '==================================================
            ' FIRST CHECK NORMAL USER / ADMIN
            '==================================================

            Using con As New SqlConnection(conStr)

                con.Open()


                Dim sql As String =
                    "SELECT UserID, Email, Password, Role, Status " &
                    "FROM dbo.Users " &
                    "WHERE Email = @Email"


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


                        If reader.Read() Then


                            '==================================================
                            ' USER INFORMATION
                            '==================================================

                            Dim userId As Integer =
                                Convert.ToInt32(
                                    reader("UserID")
                                )


                            Dim dbEmail As String =
                                reader("Email").ToString().Trim()


                            Dim dbPassword As String =
                                reader("Password").ToString()


                            Dim role As String =
                                reader("Role").ToString().Trim()


                            Dim status As String =
                                ""


                            If Not IsDBNull(
                                reader("Status")
                            ) Then

                                status =
                                    reader("Status").ToString().Trim()

                            End If


                            '==================================================
                            ' CHECK NORMAL USER PASSWORD
                            '
                            ' Your current Users table stores Password
                            '==================================================

                            If dbPassword <> password Then

                                ShowMessage(
                                    "Invalid email or password."
                                )

                                Return

                            End If


                            '==================================================
                            ' CLOSE READER
                            '==================================================

                            reader.Close()


                            '==================================================
                            ' UPDATE USER STATUS
                            '==================================================

                            Dim updateSql As String =
                                "UPDATE dbo.Users " &
                                "SET Status = 'Active' " &
                                "WHERE UserID = @UserID"


                            Using updateCmd As New SqlCommand(
                                updateSql,
                                con
                            )


                                updateCmd.Parameters.Add(
                                    "@UserID",
                                    SqlDbType.Int
                                ).Value = userId


                                updateCmd.ExecuteNonQuery()

                            End Using


                            '==================================================
                            ' SET SESSION
                            '
                            ' IMPORTANT:
                            ' Use Role, not UserRole
                            '==================================================

                            Session("UserID") =
                                userId

                            Session("Email") =
                                dbEmail

                            Session("Role") =
                                role

                            Session("Status") =
                                "Active"


                            '==================================================
                            ' LOGIN SUCCESS
                            '==================================================

                            lblMessage.Visible = False


                            '==================================================
                            ' ADMIN
                            '==================================================

                            If role.Equals(
                                "Admin",
                                StringComparison.OrdinalIgnoreCase
                            ) Then


                                Response.Redirect(
                                    "~/AdminDashboard.aspx",
                                    False
                                )

                                Context.ApplicationInstance.CompleteRequest()

                                Return

                            End If


                            '==================================================
                            ' NORMAL USER
                            '==================================================

                            If role.Equals(
                                "User",
                                StringComparison.OrdinalIgnoreCase
                            ) Then


                                Response.Redirect(
                                    "~/UserDashboard.aspx",
                                    False
                                )

                                Context.ApplicationInstance.CompleteRequest()

                                Return

                            End If


                            '==================================================
                            ' UNKNOWN USER ROLE
                            '==================================================

                            ShowMessage(
                                "Your account role is not configured correctly."
                            )

                            Return


                        End If

                    End Using

                End Using

            End Using


            '==================================================
            ' IF NOT FOUND IN USERS TABLE
            ' CHECK INVESTORS TABLE
            '==================================================

            LoginInvestor(
                email,
                password,
                conStr
            )


        Catch ex As Exception

            ShowMessage(
                "Login error: " &
                ex.Message
            )

        End Try

    End Sub


    '==================================================
    ' INVESTOR LOGIN
    '==================================================

    Private Sub LoginInvestor(
        ByVal email As String,
        ByVal password As String,
        ByVal conStr As String
    )


        Try

            Using con As New SqlConnection(conStr)

                con.Open()


                '==================================================
                ' FIND INVESTOR
                '
                ' Existing investor structure:
                '
                ' InvestorID
                ' Name
                ' Email
                ' Mobile
                ' Department
                ' Designation
                ' InvestmentAmount
                ' PasswordHash
                '==================================================

                Dim sql As String =
                    "SELECT " &
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


                        '==================================================
                        ' INVESTOR NOT FOUND
                        '==================================================

                        If Not reader.Read() Then

                            ShowMessage(
                                "Account not found. Please check your email."
                            )

                            Return

                        End If


                        '==================================================
                        ' INVESTOR INFORMATION
                        '==================================================

                        Dim investorId As Integer =
                            Convert.ToInt32(
                                reader("InvestorID")
                            )


                        Dim investorEmail As String =
                            reader("Email").ToString().Trim()


                        Dim storedHash As String =
                            ""


                        If Not IsDBNull(
                            reader("PasswordHash")
                        ) Then

                            storedHash =
                                reader("PasswordHash").ToString().Trim()

                        End If


                        '==================================================
                        ' HASH ENTERED PASSWORD
                        '==================================================

                        Dim enteredHash As String =
                            HashPassword(password)


                        '==================================================
                        ' CHECK PASSWORD
                        '==================================================

                        If storedHash = "" Then

                            ShowMessage(
                                "Your investor account does not have a password configured."
                            )

                            Return

                        End If


                        If Not storedHash.Equals(
                            enteredHash,
                            StringComparison.OrdinalIgnoreCase
                        ) Then

                            ShowMessage(
                                "Invalid email or password."
                            )

                            Return

                        End If


                        '==================================================
                        ' CLOSE READER
                        '==================================================

                        reader.Close()


                        '==================================================
                        ' SET INVESTOR SESSION
                        '==================================================

                        Session("UserID") =
                            investorId

                        Session("Email") =
                            investorEmail

                        Session("Role") =
                            "Investor"

                        Session("Status") =
                            "Active"


                        '==================================================
                        ' LOGIN SUCCESS
                        '==================================================

                        lblMessage.Visible = False


                        '==================================================
                        ' REDIRECT INVESTOR
                        '==================================================

                        Response.Redirect(
                            "~/MyProfile.aspx",
                            False
                        )

                        Context.ApplicationInstance.CompleteRequest()

                        Return


                    End Using

                End Using

            End Using


        Catch ex As Exception

            ShowMessage(
                "Investor login error: " &
                ex.Message
            )

        End Try

    End Sub


    '==================================================
    ' SHA-256 PASSWORD HASH
    '==================================================

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


    '==================================================
    ' SHOW MESSAGE
    '==================================================

    Private Sub ShowMessage(
        ByVal message As String
    )


        lblMessage.Text =
            message


        lblMessage.Visible =
            True


        lblMessage.ForeColor =
            Drawing.ColorTranslator.FromHtml(
                "#842029"
            )


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