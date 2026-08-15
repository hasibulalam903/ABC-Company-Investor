Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Partial Class Register
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(
        ByVal sender As Object,
        ByVal e As EventArgs
    ) Handles Me.Load

        If Not IsPostBack Then

            lblMessage.Visible = False
            lblMessage.Text = ""

        End If

    End Sub


    Protected Sub btnRegister_Click(
        ByVal sender As Object,
        ByVal e As EventArgs
    ) Handles btnRegister.Click

        ' ==========================================
        ' GET VALUES
        ' ==========================================

        Dim name As String =
            txtName.Text.Trim()

        Dim email As String =
            txtEmail.Text.Trim()

        Dim mobile As String =
            txtMobile.Text.Trim()

        Dim password As String =
            txtPassword.Text.Trim()

        Dim confirmPassword As String =
            txtConfirmPassword.Text.Trim()


        ' ==========================================
        ' VALIDATION
        ' ==========================================

        If name = "" Then

            ShowError("Please enter your name.")
            Return

        End If


        If email = "" Then

            ShowError("Please enter your email.")
            Return

        End If


        If mobile = "" Then

            ShowError("Please enter your mobile number.")
            Return

        End If


        If password = "" Then

            ShowError("Please enter your password.")
            Return

        End If


        If confirmPassword = "" Then

            ShowError("Please confirm your password.")
            Return

        End If


        If password <> confirmPassword Then

            ShowError("Passwords do not match.")
            Return

        End If


        ' ==========================================
        ' DATABASE CONNECTION
        ' ==========================================

        Dim conStr As String =
            ConfigurationManager.ConnectionStrings(
                "InvestorDB"
            ).ConnectionString


        Using con As New SqlConnection(conStr)

            Try

                con.Open()


                ' ==========================================
                ' CHECK EMAIL
                ' ==========================================

                Dim checkSql As String =
                    "SELECT COUNT(*) " &
                    "FROM Users " &
                    "WHERE Email = @Email"


                Using checkCmd As New SqlCommand(
                    checkSql,
                    con
                )

                    checkCmd.Parameters.Add(
                        "@Email",
                        SqlDbType.VarChar,
                        150
                    ).Value = email


                    Dim existingUser As Integer =
                        Convert.ToInt32(
                            checkCmd.ExecuteScalar()
                        )


                    If existingUser > 0 Then

                        ShowError(
                            "An account with this email already exists."
                        )

                        Return

                    End If

                End Using


                ' ==========================================
                ' INSERT NEW USER
                ' ==========================================
                '
                ' Database columns:
                '
                ' UserID
                ' Name
                ' Email
                ' Mobile Number
                ' Password
                ' Role
                ' Status
                '
                ' ==========================================

                Dim insertSql As String =
                    "INSERT INTO Users " &
                    "([Name], [Email], [Mobile], [Password], [Role], [Status]) " &
                    "VALUES " &
                    "(@Name, @Email, @Mobile, @Password, 'User', 'Inactive')"


                Using cmd As New SqlCommand(
                    insertSql,
                    con
                )

                    cmd.Parameters.Add(
                        "@Name",
                        SqlDbType.VarChar,
                        150
                    ).Value = name


                    cmd.Parameters.Add(
                        "@Email",
                        SqlDbType.VarChar,
                        150
                    ).Value = email


                    cmd.Parameters.Add(
                        "@Mobile",
                        SqlDbType.VarChar,
                        15
                    ).Value = mobile


                    cmd.Parameters.Add(
                        "@Password",
                        SqlDbType.VarChar,
                        100
                    ).Value = password


                    cmd.ExecuteNonQuery()

                End Using


                ' ==========================================
                ' SUCCESS
                ' ==========================================

                ShowSuccess(
                    "Account created successfully. Please login."
                )


                ' Clear form

                txtName.Text = ""
                txtEmail.Text = ""
                txtMobile.Text = ""
                txtPassword.Text = ""
                txtConfirmPassword.Text = ""


            Catch ex As Exception

                ShowError(
                    "Registration error: " &
                    ex.Message
                )

            End Try

        End Using

    End Sub


    ' ==========================================
    ' ERROR MESSAGE
    ' ==========================================

    Private Sub ShowError(
        ByVal message As String
    )

        lblMessage.Text = message

        lblMessage.Visible = True

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

    End Sub


    ' ==========================================
    ' SUCCESS MESSAGE
    ' ==========================================

    Private Sub ShowSuccess(
        ByVal message As String
    )

        lblMessage.Text = message

        lblMessage.Visible = True

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

    End Sub

End Class