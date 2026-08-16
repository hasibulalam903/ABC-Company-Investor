Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Security.Cryptography
Imports System.Text

Partial Class InvestorRegister
    Inherits System.Web.UI.Page


    '==================================================
    ' CONNECTION STRING
    '==================================================

    Private ReadOnly conStr As String =
        ConfigurationManager.ConnectionStrings(
            "InvestorDB"
        ).ConnectionString


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

            LoadDepartments()

        End If

    End Sub


    '==================================================
    ' LOAD DEPARTMENTS
    '==================================================

    Private Sub LoadDepartments()

        Try

            Using con As New SqlConnection(conStr)

                Dim sql As String =
                    "SELECT DISTINCT [Department] " &
                    "FROM dbo.Investors " &
                    "WHERE [Department] IS NOT NULL " &
                    "AND LTRIM(RTRIM([Department])) <> '' " &
                    "ORDER BY [Department]"

                Using cmd As New SqlCommand(sql, con)

                    con.Open()

                    Using reader As SqlDataReader =
                        cmd.ExecuteReader()

                        ddlDepartment.Items.Clear()

                        ddlDepartment.Items.Add(
                            New ListItem(
                                "-- Select Department --",
                                ""
                            )
                        )

                        While reader.Read()

                            ddlDepartment.Items.Add(
                                New ListItem(
                                    reader("Department").ToString(),
                                    reader("Department").ToString()
                                )
                            )

                        End While

                    End Using

                End Using

            End Using

        Catch ex As Exception

            ShowError(
                "Error loading departments: " &
                ex.Message
            )

        End Try

    End Sub


    '==================================================
    ' REGISTER BUTTON
    '==================================================

    Protected Sub btnRegister_Click(
        ByVal sender As Object,
        ByVal e As EventArgs
    ) Handles btnRegister.Click


        '==================================================
        ' GET FORM VALUES
        '==================================================

        Dim name As String =
            txtName.Text.Trim()

        Dim email As String =
            txtEmail.Text.Trim()

        Dim mobile As String =
            txtPhone.Text.Trim()

        Dim password As String =
            txtPassword.Text

        Dim confirmPassword As String =
            txtConfirmPassword.Text

        Dim department As String =
            ddlDepartment.SelectedValue.Trim()

        Dim designation As String =
            ddlDesignation.SelectedValue.Trim()

        Dim investmentText As String =
            txtInvestmentAmount.Text.Trim()


        '==================================================
        ' VALIDATION
        '==================================================

        If name = "" Then

            ShowError(
                "Please enter your name."
            )

            Return

        End If


        If email = "" Then

            ShowError(
                "Please enter your email."
            )

            Return

        End If


        If mobile = "" Then

            ShowError(
                "Please enter your mobile number."
            )

            Return

        End If


        If password = "" Then

            ShowError(
                "Please enter a password."
            )

            Return

        End If


        If password.Length < 6 Then

            ShowError(
                "Password must be at least 6 characters."
            )

            Return

        End If


        If confirmPassword = "" Then

            ShowError(
                "Please confirm your password."
            )

            Return

        End If


        If password <> confirmPassword Then

            ShowError(
                "Password and confirm password do not match."
            )

            Return

        End If


        If department = "" Then

            ShowError(
                "Please select a department."
            )

            Return

        End If


        If designation = "" Then

            ShowError(
                "Please select a designation."
            )

            Return

        End If


        If investmentText = "" Then

            ShowError(
                "Please enter investment amount."
            )

            Return

        End If


        '==================================================
        ' INVESTMENT AMOUNT
        '==================================================

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


        '==================================================
        ' REGISTER INVESTOR
        '==================================================

        Try

            Using con As New SqlConnection(conStr)

                con.Open()


                '==================================================
                ' CHECK DUPLICATE EMAIL
                '==================================================

                Dim emailCheckSql As String =
                    "SELECT COUNT(*) " &
                    "FROM dbo.Investors " &
                    "WHERE [Email] = @Email"


                Using emailCmd As New SqlCommand(
                    emailCheckSql,
                    con
                )

                    emailCmd.Parameters.Add(
                        "@Email",
                        SqlDbType.NVarChar,
                        150
                    ).Value = email


                    Dim emailCount As Integer =
                        Convert.ToInt32(
                            emailCmd.ExecuteScalar()
                        )


                    If emailCount > 0 Then

                        ShowError(
                            "This email is already registered."
                        )

                        Return

                    End If

                End Using


                '==================================================
                ' CHECK DUPLICATE MOBILE
                '==================================================

                Dim mobileCheckSql As String =
                    "SELECT COUNT(*) " &
                    "FROM dbo.Investors " &
                    "WHERE [Mobile] = @Mobile"


                Using mobileCmd As New SqlCommand(
                    mobileCheckSql,
                    con
                )

                    mobileCmd.Parameters.Add(
                        "@Mobile",
                        SqlDbType.NVarChar,
                        20
                    ).Value = mobile


                    Dim mobileCount As Integer =
                        Convert.ToInt32(
                            mobileCmd.ExecuteScalar()
                        )


                    If mobileCount > 0 Then

                        ShowError(
                            "This mobile number is already registered."
                        )

                        Return

                    End If

                End Using


                '==================================================
                ' HASH PASSWORD
                '==================================================

                Dim passwordHash As String =
                    HashPassword(password)


                '==================================================
                ' INSERT INVESTOR
                '==================================================

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


                    '==================================================
                    ' NAME
                    '==================================================

                    cmd.Parameters.Add(
                        "@Name",
                        SqlDbType.NVarChar,
                        100
                    ).Value = name


                    '==================================================
                    ' EMAIL
                    '==================================================

                    cmd.Parameters.Add(
                        "@Email",
                        SqlDbType.NVarChar,
                        150
                    ).Value = email


                    '==================================================
                    ' MOBILE
                    '==================================================

                    cmd.Parameters.Add(
                        "@Mobile",
                        SqlDbType.NVarChar,
                        20
                    ).Value = mobile


                    '==================================================
                    ' DEPARTMENT
                    '==================================================

                    cmd.Parameters.Add(
                        "@Department",
                        SqlDbType.NVarChar,
                        100
                    ).Value = department


                    '==================================================
                    ' DESIGNATION
                    '==================================================

                    cmd.Parameters.Add(
                        "@Designation",
                        SqlDbType.NVarChar,
                        100
                    ).Value = designation


                    '==================================================
                    ' INVESTMENT
                    '==================================================

                    Dim pInvestment =
                        cmd.Parameters.Add(
                            "@InvestmentAmount",
                            SqlDbType.Decimal
                        )

                    pInvestment.Precision = 18
                    pInvestment.Scale = 2
                    pInvestment.Value = investmentAmount


                    '==================================================
                    ' PASSWORD HASH
                    '==================================================

                    cmd.Parameters.Add(
                        "@PasswordHash",
                        SqlDbType.NVarChar,
                        255
                    ).Value = passwordHash


                    '==================================================
                    ' EXECUTE
                    '==================================================

                    cmd.ExecuteNonQuery()

                End Using

            End Using


            '==================================================
            ' SUCCESS
            '==================================================

            ShowSuccess(
                "Registration successful. You can now login."
            )


            ClearForm()


            '==================================================
            ' REDIRECT TO LOGIN
            '==================================================

            Response.AddHeader(
                "REFRESH",
                "2;URL=Login.aspx"
            )


        Catch ex As Exception

            ShowError(
                "Registration failed: " &
                ex.Message
            )

        End Try

    End Sub


    '==================================================
    ' PASSWORD HASH
    '==================================================

    Private Function HashPassword(
        ByVal password As String
    ) As String

        Using sha256 As SHA256 =
            SHA256.Create()

            Dim bytes As Byte() =
                Encoding.UTF8.GetBytes(password)

            Dim hashBytes As Byte() =
                sha256.ComputeHash(bytes)

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
    ' CLEAR FORM
    '==================================================

    Private Sub ClearForm()

        txtName.Text = ""

        txtEmail.Text = ""

        txtPhone.Text = ""

        txtPassword.Text = ""

        txtConfirmPassword.Text = ""

        ddlDepartment.SelectedIndex = 0

        ddlDesignation.SelectedIndex = 0

        txtInvestmentAmount.Text = ""

    End Sub


    '==================================================
    ' SUCCESS MESSAGE
    '==================================================

    Private Sub ShowSuccess(
        ByVal message As String
    )

        lblMessage.Text = message

        lblMessage.Visible = True

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


    '==================================================
    ' ERROR MESSAGE
    '==================================================

    Private Sub ShowError(
        ByVal message As String
    )

        lblMessage.Text = message

        lblMessage.Visible = True

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