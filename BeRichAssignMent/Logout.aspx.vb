Imports System
Imports System.Data.SqlClient
Imports System.Configuration

Partial Class Logout
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(
        ByVal sender As Object,
        ByVal e As EventArgs
    ) Handles Me.Load

        Dim userId As Integer = 0

        ' Get UserID from session
        If Session("UserID") IsNot Nothing Then

            Integer.TryParse(
                Session("UserID").ToString(),
                userId
            )

        End If


        ' ==========================
        ' UPDATE USER STATUS
        ' ==========================

        If userId > 0 Then

            Dim conStr As String =
                ConfigurationManager.ConnectionStrings(
                    "InvestorDB"
                ).ConnectionString

            Using con As New SqlConnection(conStr)

                Dim sql As String =
                    "UPDATE Users " &
                    "SET Status = 'Inactive' " &
                    "WHERE UserID = @UserID"

                Using cmd As New SqlCommand(sql, con)

                    cmd.Parameters.Add(
                        "@UserID",
                        System.Data.SqlDbType.Int
                    ).Value = userId

                    con.Open()

                    cmd.ExecuteNonQuery()

                End Using

            End Using

        End If


        ' ==========================
        ' CLEAR SESSION
        ' ==========================

        Session.Clear()
        Session.Abandon()


        ' ==========================
        ' REDIRECT TO LOGIN
        ' ==========================

        Response.Redirect(
            "~/Login.aspx",
            False
        )

        Context.ApplicationInstance.CompleteRequest()

    End Sub

End Class