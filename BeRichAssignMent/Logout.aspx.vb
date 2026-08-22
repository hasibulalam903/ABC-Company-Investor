Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration


Partial Class Logout

    Inherits System.Web.UI.Page


    Protected Sub Page_Load(
        ByVal sender As Object,
        ByVal e As EventArgs
    ) Handles Me.Load


        Dim investorId As Integer = 0

        Dim userId As Integer = 0

        Dim loginType As String = ""


        ' =====================================================
        ' GET LOGIN TYPE
        ' =====================================================

        If Session("LoginType") IsNot Nothing Then

            loginType =
                Session("LoginType").ToString().Trim()

        End If


        ' =====================================================
        ' INVESTOR
        ' =====================================================

        If loginType.Equals(
            "Investor",
            StringComparison.OrdinalIgnoreCase
        ) Then


            If Session("InvestorID") IsNot Nothing Then

                Integer.TryParse(
                    Session("InvestorID").ToString(),
                    investorId
                )

            End If


            Try

                If investorId > 0 Then

                    SetInvestorOffline(
                        investorId
                    )

                End If

            Catch ex As Exception

                ' Do not stop logout if database update fails.

            End Try


        Else


            ' =================================================
            ' USER / ADMIN
            ' =================================================

            If Session("UserID") IsNot Nothing Then

                Integer.TryParse(
                    Session("UserID").ToString(),
                    userId
                )

            End If


            Try

                If userId > 0 Then

                    SetUserOffline(
                        userId
                    )

                End If

            Catch ex As Exception

                ' Do not stop logout if database update fails.

            End Try


        End If


        ' =====================================================
        ' CLEAR SESSION
        ' =====================================================

        Session.Clear()

        Session.RemoveAll()

        Session.Abandon()


        ' =====================================================
        ' PREVENT BROWSER CACHE
        ' =====================================================

        Response.Cache.SetCacheability(
            System.Web.HttpCacheability.NoCache
        )

        Response.Cache.SetNoStore()

        Response.Cache.SetExpires(
            DateTime.UtcNow.AddDays(-1)
        )


        ' =====================================================
        ' REDIRECT TO PUBLIC HOME PAGE
        ' =====================================================

        Response.Redirect(
            "~/Home.aspx",
            False
        )

        Context.ApplicationInstance.CompleteRequest()


    End Sub


    ' =========================================================
    ' INVESTOR OFFLINE
    ' =========================================================

    Private Sub SetInvestorOffline(
        ByVal investorId As Integer
    )


        Dim connectionSettings =
            ConfigurationManager.
            ConnectionStrings(
                "InvestorDB"
            )


        If connectionSettings Is Nothing Then

            Return

        End If


        Dim connectionString As String =
            connectionSettings.ConnectionString


        If String.IsNullOrWhiteSpace(
            connectionString
        ) Then

            Return

        End If


        Using con As New SqlConnection(
            connectionString
        )


            Dim sql As String =
                "UPDATE dbo.Investors " &
                "SET LoginStatus = 'Offline' " &
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


    End Sub


    ' =========================================================
    ' USER / ADMIN OFFLINE
    ' =========================================================

    Private Sub SetUserOffline(
        ByVal userId As Integer
    )


        Dim connectionSettings =
            ConfigurationManager.
            ConnectionStrings(
                "InvestorDB"
            )


        If connectionSettings Is Nothing Then

            Return

        End If


        Dim connectionString As String =
            connectionSettings.ConnectionString


        If String.IsNullOrWhiteSpace(
            connectionString
        ) Then

            Return

        End If


        Using con As New SqlConnection(
            connectionString
        )


            Dim sql As String =
                "UPDATE dbo.Users " &
                "SET LoginStatus = 'Offline' " &
                "WHERE UserID = @UserID"


            Using cmd As New SqlCommand(
                sql,
                con
            )


                cmd.Parameters.Add(
                    "@UserID",
                    SqlDbType.Int
                ).Value =
                    userId


                con.Open()

                cmd.ExecuteNonQuery()


            End Using

        End Using


    End Sub


End Class