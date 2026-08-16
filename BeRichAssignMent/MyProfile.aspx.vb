Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient


Partial Class MyProfile
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


        '==================================================
        ' CHECK LOGIN
        '==================================================

        If Session("UserID") Is Nothing Then

            Response.Redirect(
                "~/Login.aspx",
                False
            )

            Context.ApplicationInstance.CompleteRequest()

            Return

        End If


        '==================================================
        ' CHECK ROLE
        '==================================================

        If Session("Role") Is Nothing Then

            Response.Redirect(
                "~/Login.aspx",
                False
            )

            Context.ApplicationInstance.CompleteRequest()

            Return

        End If


        '==================================================
        ' GET ROLE
        '==================================================

        Dim role As String =
            Session("Role").ToString().Trim().ToLower()


        '==================================================
        ' ONLY USER AND INVESTOR CAN OPEN PROFILE
        '==================================================

        If role <> "user" AndAlso
           role <> "investor" Then

            Response.Redirect(
                "~/Home.aspx",
                False
            )

            Context.ApplicationInstance.CompleteRequest()

            Return

        End If


        '==================================================
        ' LOAD PROFILE
        '==================================================

        If Not IsPostBack Then

            LoadMyProfile()

        End If

    End Sub


    '==================================================
    ' LOAD CURRENT PROFILE
    '==================================================

    Private Sub LoadMyProfile()

        Try

            Dim userId As Integer


            '==================================================
            ' GET USER ID FROM SESSION
            '==================================================

            If Not Integer.TryParse(
                Session("UserID").ToString(),
                userId
            ) Then

                ShowError(
                    "Invalid user session."
                )

                Return

            End If


            '==================================================
            ' DATABASE
            '==================================================

            Using con As New SqlConnection(conStr)


                '==================================================
                ' INVESTOR PROFILE
                '==================================================

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


                Using cmd As New SqlCommand(
                    sql,
                    con
                )


                    cmd.Parameters.Add(
                        "@InvestorID",
                        SqlDbType.Int
                    ).Value = userId


                    con.Open()


                    Using reader As SqlDataReader =
                        cmd.ExecuteReader()


                        If reader.Read() Then


                            '==================================================
                            ' INVESTOR ID
                            '==================================================

                            lblInvestorID.Text =
                                reader("InvestorID").ToString()


                            '==================================================
                            ' NAME
                            '==================================================

                            Dim investorName As String =
                                reader("Name").ToString().Trim()


                            lblName.Text =
                                Server.HtmlEncode(
                                    investorName
                                )


                            '==================================================
                            ' INITIAL
                            '==================================================

                            If investorName <> "" Then

                                lblInitial.Text =
                                    investorName.Substring(
                                        0,
                                        1
                                    ).ToUpper()

                            Else

                                lblInitial.Text = "I"

                            End If


                            '==================================================
                            ' EMAIL
                            '==================================================

                            lblEmail.Text =
                                Server.HtmlEncode(
                                    reader("Email").ToString()
                                )


                            '==================================================
                            ' MOBILE
                            '==================================================

                            lblMobile.Text =
                                Server.HtmlEncode(
                                    reader("Mobile").ToString()
                                )


                            '==================================================
                            ' DEPARTMENT
                            '==================================================

                            lblDepartment.Text =
                                Server.HtmlEncode(
                                    reader("Department").ToString()
                                )


                            '==================================================
                            ' DESIGNATION
                            '==================================================

                            lblDesignation.Text =
                                Server.HtmlEncode(
                                    reader("Designation").ToString()
                                )


                            '==================================================
                            ' INVESTMENT
                            '==================================================

                            If Not IsDBNull(
                                reader("InvestmentAmount")
                            ) Then

                                Dim investmentAmount As Decimal =
                                    Convert.ToDecimal(
                                        reader("InvestmentAmount")
                                    )


                                lblInvestmentAmount.Text =
                                    investmentAmount.ToString(
                                        "N2"
                                    )

                            Else

                                lblInvestmentAmount.Text =
                                    "0.00"

                            End If


                        Else

                            ShowError(
                                "Investor profile was not found."
                            )

                        End If

                    End Using

                End Using

            End Using


        Catch ex As Exception

            ShowError(
                "Unable to load profile: " &
                ex.Message
            )

        End Try

    End Sub


    '==================================================
    ' ERROR MESSAGE
    '==================================================

    Private Sub ShowError(
        ByVal message As String
    )

        lblMessage.Text =
            Server.HtmlEncode(message)

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