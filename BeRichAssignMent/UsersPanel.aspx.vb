Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Web.UI
Imports System.Web.UI.WebControls

Partial Class UsersPanel
    Inherits System.Web.UI.Page


    ' ==========================================
    ' CONNECTION STRING
    ' ==========================================

    Private ReadOnly conStr As String =
        ConfigurationManager.ConnectionStrings("InvestorDB").ConnectionString



    ' ==========================================
    ' PAGE LOAD
    ' ==========================================

    Protected Sub Page_Load(
        sender As Object,
        e As EventArgs
    ) Handles Me.Load


        ' ======================================
        ' CHECK LOGIN
        ' ======================================

        If Session("UserID") Is Nothing Then

            Response.Redirect("~/Login.aspx")
            Return

        End If


        ' ======================================
        ' CHECK ADMIN ROLE
        ' ======================================

        If Session("Role") Is Nothing Then

            Response.Redirect("~/Login.aspx")
            Return

        End If


        Dim role As String =
            Session("Role").ToString().Trim().ToLower()


        If role <> "admin" Then

            Response.Redirect("~/Home.aspx")
            Return

        End If


        ' ======================================
        ' FIRST LOAD
        ' ======================================

        If Not IsPostBack Then

            LoadStatistics()

            LoadUsers()

        End If

    End Sub



    ' ==========================================
    ' LOAD STATISTICS
    ' ==========================================

    Private Sub LoadStatistics()

        Try

            Using con As New SqlConnection(conStr)

                con.Open()


                ' ==================================
                ' TOTAL USERS
                ' ==================================

                Using cmd As New SqlCommand(
                    "SELECT COUNT(*) FROM dbo.Users",
                    con)

                    lblTotalUsers.Text =
                        Convert.ToInt32(
                            cmd.ExecuteScalar()
                        ).ToString()

                End Using


                ' ==================================
                ' ACTIVE USERS
                ' ==================================

                Using cmd As New SqlCommand(
                    "SELECT COUNT(*) FROM dbo.Users WHERE [Status] = @Status",
                    con)

                    cmd.Parameters.AddWithValue(
                        "@Status",
                        "Active"
                    )

                    lblActiveUsers.Text =
                        Convert.ToInt32(
                            cmd.ExecuteScalar()
                        ).ToString()

                End Using


                ' ==================================
                ' INACTIVE USERS
                ' ==================================

                Using cmd As New SqlCommand(
                    "SELECT COUNT(*) FROM dbo.Users WHERE [Status] = @Status",
                    con)

                    cmd.Parameters.AddWithValue(
                        "@Status",
                        "Inactive"
                    )

                    lblInactiveUsers.Text =
                        Convert.ToInt32(
                            cmd.ExecuteScalar()
                        ).ToString()

                End Using


                ' ==================================
                ' ADMIN USERS
                ' ==================================

                Using cmd As New SqlCommand(
                    "SELECT COUNT(*) FROM dbo.Users WHERE [Role] = @Role",
                    con)

                    cmd.Parameters.AddWithValue(
                        "@Role",
                        "Admin"
                    )

                    lblAdminUsers.Text =
                        Convert.ToInt32(
                            cmd.ExecuteScalar()
                        ).ToString()

                End Using

            End Using


        Catch ex As Exception

            ShowError(
                "Unable to load statistics: " &
                ex.Message
            )

        End Try

    End Sub



    ' ==========================================
    ' LOAD USERS
    ' ==========================================

    Private Sub LoadUsers()

        LoadUsers(
            txtSearch.Text.Trim(),
            ddlRole.SelectedValue,
            ddlStatus.SelectedValue
        )

    End Sub



    ' ==========================================
    ' LOAD USERS WITH SEARCH/FILTER
    ' ==========================================

    Private Sub LoadUsers(
        searchText As String,
        selectedRole As String,
        selectedStatus As String
    )

        Try

            Using con As New SqlConnection(conStr)


                ' ==================================
                ' BASE SQL
                ' ==================================

                Dim sql As String =
                    "SELECT UserID, [Name], [Email], [Mobile], [Role], [Status] " &
                    "FROM dbo.Users " &
                    "WHERE 1 = 1 "


                ' ==================================
                ' SEARCH
                ' ==================================

                If searchText <> "" Then

                    sql =
                        sql &
                        " AND ([Name] LIKE @Search " &
                        "OR [Email] LIKE @Search " &
                        "OR [Mobile] LIKE @Search) "

                End If


                ' ==================================
                ' ROLE FILTER
                ' ==================================

                If selectedRole <> "" Then

                    sql =
                        sql &
                        " AND [Role] = @Role "

                End If


                ' ==================================
                ' STATUS FILTER
                ' ==================================

                If selectedStatus <> "" Then

                    sql =
                        sql &
                        " AND [Status] = @Status "

                End If


                ' ==================================
                ' ORDER
                ' ==================================

                sql =
                    sql &
                    " ORDER BY UserID DESC"


                ' ==================================
                ' SQL COMMAND
                ' ==================================

                Using cmd As New SqlCommand(
                    sql,
                    con
                )


                    ' ==================================
                    ' SEARCH PARAMETER
                    ' ==================================

                    If searchText <> "" Then

                        cmd.Parameters.AddWithValue(
                            "@Search",
                            "%" & searchText & "%"
                        )

                    End If


                    ' ==================================
                    ' ROLE PARAMETER
                    ' ==================================

                    If selectedRole <> "" Then

                        cmd.Parameters.AddWithValue(
                            "@Role",
                            selectedRole
                        )

                    End If


                    ' ==================================
                    ' STATUS PARAMETER
                    ' ==================================

                    If selectedStatus <> "" Then

                        cmd.Parameters.AddWithValue(
                            "@Status",
                            selectedStatus
                        )

                    End If


                    ' ==================================
                    ' DATA TABLE
                    ' ==================================

                    Dim dt As New DataTable()


                    Using da As New SqlDataAdapter(cmd)

                        da.Fill(dt)

                    End Using


                    ' ==================================
                    ' GRIDVIEW
                    ' ==================================

                    gvUsers.DataSource = dt

                    gvUsers.DataBind()


                    ' ==================================
                    ' USER COUNT
                    ' ==================================

                    lblUserCount.Text =
                        dt.Rows.Count.ToString() &
                        " users"


                End Using

            End Using


        Catch ex As Exception

            ShowError(
                "Unable to load users: " &
                ex.Message
            )

        End Try

    End Sub



    ' ==========================================
    ' SEARCH BUTTON
    ' ==========================================

    Protected Sub btnSearch_Click(
        sender As Object,
        e As EventArgs
    ) Handles btnSearch.Click


        gvUsers.PageIndex = 0

        LoadUsers()

    End Sub



    ' ==========================================
    ' RESET BUTTON
    ' ==========================================

    Protected Sub btnReset_Click(
        sender As Object,
        e As EventArgs
    ) Handles btnReset.Click


        txtSearch.Text = ""

        ddlRole.SelectedIndex = 0

        ddlStatus.SelectedIndex = 0

        gvUsers.PageIndex = 0

        HideMessage()

        LoadUsers()

    End Sub



    ' ==========================================
    ' GRIDVIEW PAGING
    ' ==========================================

    Protected Sub gvUsers_PageIndexChanging(
        sender As Object,
        e As GridViewPageEventArgs
    )

        gvUsers.PageIndex =
            e.NewPageIndex

        LoadUsers()

    End Sub



    ' ==========================================
    ' GRIDVIEW ROW COMMAND
    ' ==========================================

    Protected Sub gvUsers_RowCommand(
        sender As Object,
        e As GridViewCommandEventArgs
    )


        If e.CommandName = "ToggleStatus" Then

            Dim userID As Integer


            If Integer.TryParse(
                e.CommandArgument.ToString(),
                userID
            ) Then

                ToggleUserStatus(userID)

            End If

        End If

    End Sub



    ' ==========================================
    ' TOGGLE USER STATUS
    ' ==========================================

    Private Sub ToggleUserStatus(
        userID As Integer
    )

        Try

            Using con As New SqlConnection(conStr)

                con.Open()


                Dim currentStatus As String = ""


                ' ==================================
                ' GET CURRENT STATUS
                ' ==================================

                Using cmd As New SqlCommand(
                    "SELECT [Status] " &
                    "FROM dbo.Users " &
                    "WHERE UserID = @UserID",
                    con
                )

                    cmd.Parameters.AddWithValue(
                        "@UserID",
                        userID
                    )


                    Dim result As Object =
                        cmd.ExecuteScalar()


                    If result Is Nothing OrElse
                       result Is DBNull.Value Then

                        ShowError(
                            "User not found."
                        )

                        Return

                    End If


                    currentStatus =
                        result.ToString()

                End Using


                ' ==================================
                ' DETERMINE NEW STATUS
                ' ==================================

                Dim newStatus As String


                If currentStatus.Trim().ToLower() =
                    "active" Then

                    newStatus = "Inactive"

                Else

                    newStatus = "Active"

                End If


                ' ==================================
                ' UPDATE STATUS
                ' ==================================

                Using cmd As New SqlCommand(
                    "UPDATE dbo.Users " &
                    "SET [Status] = @Status " &
                    "WHERE UserID = @UserID",
                    con
                )

                    cmd.Parameters.AddWithValue(
                        "@Status",
                        newStatus
                    )


                    cmd.Parameters.AddWithValue(
                        "@UserID",
                        userID
                    )


                    cmd.ExecuteNonQuery()

                End Using

            End Using


            ' ==================================
            ' SUCCESS
            ' ==================================

            ShowSuccess(
                "User status updated successfully."
            )


            ' ==================================
            ' REFRESH
            ' ==================================

            LoadStatistics()

            LoadUsers()


        Catch ex As Exception

            ShowError(
                "Unable to update user status: " &
                ex.Message
            )

        End Try

    End Sub



    ' ==========================================
    ' STATUS CSS
    ' ==========================================

    Protected Function GetStatusCss(
        status As Object
    ) As String


        If status Is Nothing OrElse
           status Is DBNull.Value Then

            Return "inactive-status"

        End If


        If status.ToString().Trim().ToLower() =
            "active" Then

            Return "active-status"

        End If


        Return "inactive-status"

    End Function



    ' ==========================================
    ' STATUS BUTTON TEXT
    ' ==========================================

    Protected Function GetStatusButtonText(
        status As Object
    ) As String


        If status Is Nothing OrElse
           status Is DBNull.Value Then

            Return "Activate"

        End If


        If status.ToString().Trim().ToLower() =
            "active" Then

            Return "Deactivate"

        End If


        Return "Activate"

    End Function



    ' ==========================================
    ' STATUS BUTTON CSS
    ' ==========================================

    Protected Function GetStatusButtonCss(
        status As Object
    ) As String


        If status Is Nothing OrElse
           status Is DBNull.Value Then

            Return "activate-button"

        End If


        If status.ToString().Trim().ToLower() =
            "active" Then

            Return "deactivate-button"

        End If


        Return "activate-button"

    End Function



    ' ==========================================
    ' SUCCESS MESSAGE
    ' ==========================================

    Private Sub ShowSuccess(
        message As String
    )

        lblMessage.Text =
            message

        lblMessage.CssClass =
            "message success-message"

        lblMessage.Visible =
            True

    End Sub



    ' ==========================================
    ' ERROR MESSAGE
    ' ==========================================

    Private Sub ShowError(
        message As String
    )

        lblMessage.Text =
            message

        lblMessage.CssClass =
            "message error-message"

        lblMessage.Visible =
            True

    End Sub



    ' ==========================================
    ' HIDE MESSAGE
    ' ==========================================

    Private Sub HideMessage()

        lblMessage.Text = ""

        lblMessage.Visible = False

    End Sub


End Class