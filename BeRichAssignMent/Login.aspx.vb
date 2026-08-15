Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

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
            txtPassword.Text.Trim()


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


        Using con As New SqlConnection(conStr)

            Try

                con.Open()


                '==================================================
                ' FIND USER
                '==================================================

                Dim sql As String =
                    "SELECT UserID, Email, Password, Role " &
                    "FROM Users " &
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


                        '==================================================
                        ' ACCOUNT NOT FOUND
                        '==================================================

                        If Not reader.Read() Then

                            ShowMessage(
                                "Account not found. Please create your account."
                            )

                            Return

                        End If


                        '==================================================
                        ' GET USER INFORMATION
                        '==================================================

                        Dim userId As Integer =
                            Convert.ToInt32(
                                reader("UserID")
                            )


                        Dim dbEmail As String =
                            reader("Email").ToString()


                        Dim dbPassword As String =
                            reader("Password").ToString()


                        Dim role As String =
                            reader("Role").ToString().Trim()


                        '==================================================
                        ' CHECK PASSWORD
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
                        ' SET SESSION
                        '
                        ' IMPORTANT:
                        ' Site.master.vb uses UserRole
                        '==================================================

                        Session("UserID") = userId

                        Session("Email") = dbEmail

                        Session("UserRole") = role


                        '==================================================
                        ' CHANGE STATUS TO ACTIVE
                        '==================================================

                        Dim updateSql As String =
                            "UPDATE Users " &
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

                        Response.Redirect(
                            "~/UserDashboard.aspx",
                            False
                        )

                        Context.ApplicationInstance.CompleteRequest()

                        Return


                    End Using

                End Using


            Catch ex As Exception

                ShowMessage(
                    "Login error: " &
                    ex.Message
                )

            End Try

        End Using

    End Sub


    '==================================================
    ' SHOW MESSAGE
    '==================================================

    Private Sub ShowMessage(
        ByVal message As String
    )

        lblMessage.Text = message

        lblMessage.Visible = True

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