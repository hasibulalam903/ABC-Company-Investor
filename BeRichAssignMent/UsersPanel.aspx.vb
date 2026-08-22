Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.IO
Imports System.Text

Imports iTextSharp.text
Imports iTextSharp.text.pdf


Partial Class UsersPanel

    Inherits System.Web.UI.Page


    ' =========================================================
    ' CONNECTION STRING
    ' =========================================================

    Private ReadOnly conStr As String =
        ConfigurationManager.ConnectionStrings(
            "InvestorDB"
        ).ConnectionString


    ' =========================================================
    ' PAGINATION
    ' =========================================================

    Private Const PageSize As Integer = 5


    Private Property CurrentPage As Integer

        Get

            If ViewState("CurrentPage") Is Nothing Then

                Return 1

            End If

            Return Convert.ToInt32(
                ViewState("CurrentPage")
            )

        End Get

        Set(value As Integer)

            ViewState("CurrentPage") = value

        End Set

    End Property


    ' =========================================================
    ' PAGE LOAD
    ' =========================================================

    Protected Sub Page_Load(
        ByVal sender As Object,
        ByVal e As EventArgs
    ) Handles Me.Load


        ' =====================================================
        ' LOGIN CHECK
        ' =====================================================

        If Session("UserID") Is Nothing Then

            Response.Redirect(
                "~/Login.aspx"
            )

            Return

        End If


        ' =====================================================
        ' ROLE CHECK
        ' =====================================================

        If Session("Role") Is Nothing Then

            Response.Redirect(
                "~/Login.aspx"
            )

            Return

        End If


        If Session("Role").ToString().
            Trim().
            ToLower() <> "admin" Then

            Response.Redirect(
                "~/Home.aspx"
            )

            Return

        End If


        ' =====================================================
        ' FIRST LOAD
        ' =====================================================

        If Not IsPostBack Then

            CurrentPage = 1

            LoadStatistics()

            LoadUsers()

        End If

    End Sub


    ' =========================================================
    ' LOAD STATISTICS
    ' =========================================================

    Private Sub LoadStatistics()

        Try

            Using con As New SqlConnection(conStr)

                con.Open()


                ' -------------------------------------------------
                ' TOTAL USERS
                ' -------------------------------------------------

                Using cmd As New SqlCommand(
                    "SELECT COUNT(*) " &
                    "FROM dbo.Users",
                    con
                )

                    lblTotalUsers.Text =
                        Convert.ToInt32(
                            cmd.ExecuteScalar()
                        ).ToString()

                End Using


                ' -------------------------------------------------
                ' ACTIVE USERS
                ' -------------------------------------------------

                Using cmd As New SqlCommand(
                    "SELECT COUNT(*) " &
                    "FROM dbo.Users " &
                    "WHERE [Status] = @Status",
                    con
                )

                    cmd.Parameters.Add(
                        "@Status",
                        SqlDbType.VarChar,
                        50
                    ).Value = "Active"


                    lblActiveUsers.Text =
                        Convert.ToInt32(
                            cmd.ExecuteScalar()
                        ).ToString()

                End Using


                ' -------------------------------------------------
                ' INACTIVE USERS
                ' -------------------------------------------------

                Using cmd As New SqlCommand(
                    "SELECT COUNT(*) " &
                    "FROM dbo.Users " &
                    "WHERE [Status] = @Status",
                    con
                )

                    cmd.Parameters.Add(
                        "@Status",
                        SqlDbType.VarChar,
                        50
                    ).Value = "Inactive"


                    lblInactiveUsers.Text =
                        Convert.ToInt32(
                            cmd.ExecuteScalar()
                        ).ToString()

                End Using


                ' -------------------------------------------------
                ' ADMIN USERS
                ' -------------------------------------------------

                Using cmd As New SqlCommand(
                    "SELECT COUNT(*) " &
                    "FROM dbo.Users " &
                    "WHERE [Role] = @Role",
                    con
                )

                    cmd.Parameters.Add(
                        "@Role",
                        SqlDbType.VarChar,
                        50
                    ).Value = "Admin"


                    lblAdminUsers.Text =
                        Convert.ToInt32(
                            cmd.ExecuteScalar()
                        ).ToString()

                End Using

            End Using


        Catch ex As Exception

            ShowError(
                "Statistics error: " &
                ex.Message
            )

        End Try

    End Sub


    ' =========================================================
    ' LOAD USERS
    ' =========================================================

    Private Sub LoadUsers()

        LoadUsers(
            txtSearch.Text.Trim(),
            ddlRole.SelectedValue,
            ddlStatus.SelectedValue
        )

    End Sub


    ' =========================================================
    ' LOAD USERS WITH PAGINATION
    '
    ' ONLY GRIDVIEW USES OFFSET / FETCH
    ' =========================================================

    Private Sub LoadUsers(
        ByVal searchText As String,
        ByVal role As String,
        ByVal status As String
    )

        Try

            Using con As New SqlConnection(conStr)

                con.Open()


                ' =================================================
                ' COUNT FILTERED USERS
                ' =================================================

                Dim countSql As String =
                    "SELECT COUNT(*) " &
                    "FROM dbo.Users " &
                    "WHERE 1 = 1 "


                AddWhereConditions(
                    countSql,
                    searchText,
                    role,
                    status
                )


                Dim totalUsers As Integer


                Using cmd As New SqlCommand(
                    countSql,
                    con
                )


                    AddFilterParameters(
                        cmd,
                        searchText,
                        role,
                        status
                    )


                    totalUsers =
                        Convert.ToInt32(
                            cmd.ExecuteScalar()
                        )

                End Using


                ' =================================================
                ' CALCULATE TOTAL PAGES
                ' =================================================

                Dim totalPages As Integer


                If totalUsers = 0 Then

                    totalPages = 1

                Else

                    totalPages =
                        CInt(
                            Math.Ceiling(
                                totalUsers /
                                CDbl(PageSize)
                            )
                        )

                End If


                If CurrentPage < 1 Then

                    CurrentPage = 1

                End If


                If CurrentPage > totalPages Then

                    CurrentPage = totalPages

                End If


                Dim offset As Integer =
                    (CurrentPage - 1) *
                    PageSize


                ' =================================================
                ' PAGINATED GRIDVIEW QUERY
                ' =================================================

                Dim sql As String =
                    "SELECT " &
                    "UserID, " &
                    "[Name], " &
                    "[Email], " &
                    "[Mobile], " &
                    "[Role], " &
                    "[Status] " &
                    "FROM dbo.Users " &
                    "WHERE 1 = 1 "


                AddWhereConditions(
                    sql,
                    searchText,
                    role,
                    status
                )


                sql &=
                    "ORDER BY UserID DESC " &
                    "OFFSET @Offset ROWS " &
                    "FETCH NEXT @PageSize ROWS ONLY"


                Using cmd As New SqlCommand(
                    sql,
                    con
                )


                    AddFilterParameters(
                        cmd,
                        searchText,
                        role,
                        status
                    )


                    cmd.Parameters.Add(
                        "@Offset",
                        SqlDbType.Int
                    ).Value = offset


                    cmd.Parameters.Add(
                        "@PageSize",
                        SqlDbType.Int
                    ).Value = PageSize


                    Dim dt As New DataTable()


                    Using da As New SqlDataAdapter(cmd)

                        da.Fill(dt)

                    End Using


                    gvUsers.DataSource = dt

                    gvUsers.DataBind()

                End Using


                ' =================================================
                ' PAGE INFORMATION
                ' =================================================

                lblUserCount.Text =
                    totalUsers.ToString() &
                    " users"


                lblPageInfo.Text =
                    "Page " &
                    CurrentPage.ToString() &
                    " of " &
                    totalPages.ToString()


                btnPrevious.Enabled =
                    CurrentPage > 1


                btnNext.Enabled =
                    CurrentPage < totalPages

            End Using


        Catch ex As Exception

            ShowError(
                "Unable to load users: " &
                ex.Message
            )

        End Try

    End Sub


    ' =========================================================
    ' WHERE CONDITIONS
    ' =========================================================

    Private Sub AddWhereConditions(
        ByRef sql As String,
        ByVal searchText As String,
        ByVal role As String,
        ByVal status As String
    )


        If searchText <> "" Then

            sql &=
                "AND (" &
                "[Name] LIKE @Search " &
                "OR [Email] LIKE @Search " &
                "OR [Mobile] LIKE @Search" &
                ") "

        End If


        If role <> "" Then

            sql &=
                "AND [Role] = @Role "

        End If


        If status <> "" Then

            sql &=
                "AND [Status] = @Status "

        End If

    End Sub


    ' =========================================================
    ' FILTER PARAMETERS
    ' =========================================================

    Private Sub AddFilterParameters(
        ByVal cmd As SqlCommand,
        ByVal searchText As String,
        ByVal role As String,
        ByVal status As String
    )


        If searchText <> "" Then

            cmd.Parameters.Add(
                "@Search",
                SqlDbType.VarChar,
                500
            ).Value =
                "%" &
                searchText &
                "%"

        End If


        If role <> "" Then

            cmd.Parameters.Add(
                "@Role",
                SqlDbType.VarChar,
                50
            ).Value = role

        End If


        If status <> "" Then

            cmd.Parameters.Add(
                "@Status",
                SqlDbType.VarChar,
                50
            ).Value = status

        End If

    End Sub


    ' =========================================================
    ' SEARCH
    ' =========================================================

    Protected Sub btnSearch_Click(
        ByVal sender As Object,
        ByVal e As EventArgs
    ) Handles btnSearch.Click

        CurrentPage = 1

        HideMessage()

        LoadUsers()

    End Sub


    ' =========================================================
    ' RESET
    ' =========================================================

    Protected Sub btnReset_Click(
        ByVal sender As Object,
        ByVal e As EventArgs
    ) Handles btnReset.Click

        txtSearch.Text = ""

        ddlRole.SelectedIndex = 0

        ddlStatus.SelectedIndex = 0

        CurrentPage = 1

        HideMessage()

        LoadUsers()

    End Sub


    ' =========================================================
    ' PREVIOUS
    ' =========================================================

    Protected Sub btnPrevious_Click(
        ByVal sender As Object,
        ByVal e As EventArgs
    ) Handles btnPrevious.Click

        If CurrentPage > 1 Then

            CurrentPage -= 1

            LoadUsers()

        End If

    End Sub


    ' =========================================================
    ' NEXT
    ' =========================================================

    Protected Sub btnNext_Click(
        ByVal sender As Object,
        ByVal e As EventArgs
    ) Handles btnNext.Click

        CurrentPage += 1

        LoadUsers()

    End Sub


    ' =========================================================
    ' GRIDVIEW ROW COMMAND
    ' =========================================================

    Protected Sub gvUsers_RowCommand(
        ByVal sender As Object,
        ByVal e As GridViewCommandEventArgs
    ) Handles gvUsers.RowCommand


        Dim userID As Integer


        If Not Integer.TryParse(
            e.CommandArgument.ToString(),
            userID
        ) Then

            ShowError(
                "Invalid User ID."
            )

            Return

        End If


        Select Case e.CommandName


            Case "DownloadUser"

                DownloadSingleUserPdf(userID)


            Case "ExportUserExcel"

                ExportSingleUserExcel(userID)


            Case "ToggleStatus"

                ToggleUserStatus(userID)


            Case "DeleteUser"

                DeleteUser(userID)


        End Select

    End Sub


    ' =========================================================
    ' GET SINGLE USER
    ' =========================================================

    Private Function GetUser(
        ByVal userID As Integer
    ) As DataRow


        Dim dt As New DataTable()


        Using con As New SqlConnection(conStr)

            con.Open()


            Dim sql As String =
                "SELECT " &
                "UserID, " &
                "[Name], " &
                "[Email], " &
                "[Mobile], " &
                "[Role], " &
                "[Status] " &
                "FROM dbo.Users " &
                "WHERE UserID = @UserID"


            Using cmd As New SqlCommand(
                sql,
                con
            )


                cmd.Parameters.Add(
                    "@UserID",
                    SqlDbType.Int
                ).Value = userID


                Using da As New SqlDataAdapter(cmd)

                    da.Fill(dt)

                End Using

            End Using

        End Using


        If dt.Rows.Count = 0 Then

            Return Nothing

        End If


        Return dt.Rows(0)

    End Function


    ' =========================================================
    ' SINGLE USER PDF
    ' =========================================================

    Private Sub DownloadSingleUserPdf(
        ByVal userID As Integer
    )

        Try

            Dim row As DataRow =
                GetUser(userID)


            If row Is Nothing Then

                ShowError(
                    "User not found."
                )

                Return

            End If


            Response.Clear()

            Response.Buffer = True

            Response.ContentType =
                "application/pdf"


            Response.AddHeader(
                "Content-Disposition",
                "attachment;filename=User_" &
                userID.ToString() &
                ".pdf"
            )


            Using ms As New MemoryStream()


                ' =================================================
                ' FULLY QUALIFIED iTextSharp DOCUMENT
                ' =================================================

                Dim document As New iTextSharp.text.Document(
                    iTextSharp.text.PageSize.A4,
                    40,
                    40,
                    40,
                    40
                )


                iTextSharp.text.pdf.PdfWriter.GetInstance(
                    document,
                    ms
                )


                document.Open()


                ' =================================================
                ' FONTS
                ' =================================================

                Dim titleFont As iTextSharp.text.Font =
                    iTextSharp.text.FontFactory.GetFont(
                        iTextSharp.text.FontFactory.HELVETICA_BOLD,
                        18
                    )


                Dim normalFont As iTextSharp.text.Font =
                    iTextSharp.text.FontFactory.GetFont(
                        iTextSharp.text.FontFactory.HELVETICA,
                        10
                    )


                ' =================================================
                ' TITLE
                ' =================================================

                document.Add(
                    New iTextSharp.text.Paragraph(
                        "User Information",
                        titleFont
                    )
                )


                document.Add(
                    New iTextSharp.text.Paragraph(
                        " "
                    )
                )


                ' =================================================
                ' TABLE
                ' =================================================

                Dim table As New iTextSharp.text.pdf.PdfPTable(2)

                table.WidthPercentage = 100


                AddPdfCell(
                    table,
                    "User ID",
                    row("UserID").ToString()
                )


                AddPdfCell(
                    table,
                    "Name",
                    row("Name").ToString()
                )


                AddPdfCell(
                    table,
                    "Email",
                    row("Email").ToString()
                )


                AddPdfCell(
                    table,
                    "Mobile",
                    row("Mobile").ToString()
                )


                AddPdfCell(
                    table,
                    "Role",
                    row("Role").ToString()
                )


                AddPdfCell(
                    table,
                    "Status",
                    row("Status").ToString()
                )


                document.Add(table)


                document.Add(
                    New iTextSharp.text.Paragraph(
                        " "
                    )
                )


                document.Add(
                    New iTextSharp.text.Paragraph(
                        "Generated: " &
                        DateTime.Now.ToString(
                            "dd-MM-yyyy HH:mm"
                        ),
                        normalFont
                    )
                )


                document.Close()


                Response.BinaryWrite(
                    ms.ToArray()
                )


            End Using


            Response.Flush()


            HttpContext.Current.
                ApplicationInstance.
                CompleteRequest()


        Catch ex As Exception

            ShowError(
                "PDF download error: " &
                ex.Message
            )

        End Try

    End Sub


    ' =========================================================
    ' PDF TWO-COLUMN CELL
    ' =========================================================

    Private Sub AddPdfCell(
        ByVal table As iTextSharp.text.pdf.PdfPTable,
        ByVal label As String,
        ByVal value As String
    )


        Dim labelFont As iTextSharp.text.Font =
            iTextSharp.text.FontFactory.GetFont(
                iTextSharp.text.FontFactory.HELVETICA_BOLD,
                10
            )


        Dim valueFont As iTextSharp.text.Font =
            iTextSharp.text.FontFactory.GetFont(
                iTextSharp.text.FontFactory.HELVETICA,
                10
            )


        Dim labelCell As New iTextSharp.text.pdf.PdfPCell(
            New iTextSharp.text.Phrase(
                label,
                labelFont
            )
        )


        Dim valueCell As New iTextSharp.text.pdf.PdfPCell(
            New iTextSharp.text.Phrase(
                value,
                valueFont
            )
        )


        labelCell.Padding = 7

        valueCell.Padding = 7


        table.AddCell(labelCell)

        table.AddCell(valueCell)

    End Sub


    ' =========================================================
    ' SINGLE USER EXCEL
    ' =========================================================

    Private Sub ExportSingleUserExcel(
        ByVal userID As Integer
    )

        Try

            Dim row As DataRow =
                GetUser(userID)


            If row Is Nothing Then

                ShowError(
                    "User not found."
                )

                Return

            End If


            Dim sw As New StringWriter()


            sw.WriteLine("<html>")

            sw.WriteLine("<head>")

            sw.WriteLine(
                "<meta http-equiv='Content-Type' " &
                "content='text/html; charset=utf-8'>"
            )

            sw.WriteLine("</head>")

            sw.WriteLine("<body>")


            sw.WriteLine(
                "<h2>User Information</h2>"
            )


            sw.WriteLine(
                "<table border='1' cellpadding='8' cellspacing='0'>"
            )


            WriteExcelRow(
                sw,
                "User ID",
                row("UserID").ToString()
            )


            WriteExcelRow(
                sw,
                "Name",
                row("Name").ToString()
            )


            WriteExcelRow(
                sw,
                "Email",
                row("Email").ToString()
            )


            WriteExcelRow(
                sw,
                "Mobile",
                row("Mobile").ToString()
            )


            WriteExcelRow(
                sw,
                "Role",
                row("Role").ToString()
            )


            WriteExcelRow(
                sw,
                "Status",
                row("Status").ToString()
            )


            sw.WriteLine("</table>")

            sw.WriteLine("</body>")

            sw.WriteLine("</html>")


            Response.Clear()

            Response.Buffer = True

            Response.Charset = "utf-8"

            Response.ContentEncoding =
                Encoding.UTF8

            Response.ContentType =
                "application/vnd.ms-excel"


            Response.AddHeader(
                "Content-Disposition",
                "attachment;filename=User_" &
                userID.ToString() &
                ".xls"
            )


            Response.Write(
                sw.ToString()
            )


            sw.Close()

            Response.Flush()


            HttpContext.Current.
                ApplicationInstance.
                CompleteRequest()


        Catch ex As Exception

            ShowError(
                "Excel export error: " &
                ex.Message
            )

        End Try

    End Sub


    ' =========================================================
    ' EXCEL ROW
    ' =========================================================

    Private Sub WriteExcelRow(
        ByVal sw As StringWriter,
        ByVal title As String,
        ByVal value As String
    )


        sw.WriteLine("<tr>")


        sw.WriteLine(
            "<th>" &
            Server.HtmlEncode(title) &
            "</th>"
        )


        sw.WriteLine(
            "<td>" &
            Server.HtmlEncode(value) &
            "</td>"
        )


        sw.WriteLine("</tr>")

    End Sub


    ' =========================================================
    ' EXPORT ALL FILTERED USERS TO EXCEL
    '
    ' NO OFFSET
    ' NO FETCH
    '
    ' PAGINATION IS IGNORED
    ' =========================================================

    Protected Sub btnExportExcel_Click(
        ByVal sender As Object,
        ByVal e As EventArgs
    ) Handles btnExportExcel.Click


        Try

            Dim searchText As String =
                txtSearch.Text.Trim()


            Dim role As String =
                ddlRole.SelectedValue


            Dim status As String =
                ddlStatus.SelectedValue


            Dim sql As String =
                "SELECT " &
                "UserID, " &
                "[Name], " &
                "[Email], " &
                "[Mobile], " &
                "[Role], " &
                "[Status] " &
                "FROM dbo.Users " &
                "WHERE 1 = 1 "


            AddWhereConditions(
                sql,
                searchText,
                role,
                status
            )


            ' IMPORTANT:
            ' NO OFFSET / FETCH HERE

            sql &=
                "ORDER BY UserID DESC"


            Dim dt As New DataTable()


            Using con As New SqlConnection(conStr)

                con.Open()


                Using cmd As New SqlCommand(
                    sql,
                    con
                )


                    AddFilterParameters(
                        cmd,
                        searchText,
                        role,
                        status
                    )


                    Using da As New SqlDataAdapter(cmd)

                        da.Fill(dt)

                    End Using

                End Using

            End Using


            If dt.Rows.Count = 0 Then

                ShowError(
                    "No users found for the selected filters."
                )

                Return

            End If


            Dim sw As New StringWriter()


            sw.WriteLine("<html>")

            sw.WriteLine("<head>")

            sw.WriteLine(
                "<meta http-equiv='Content-Type' " &
                "content='text/html; charset=utf-8'>"
            )

            sw.WriteLine("</head>")

            sw.WriteLine("<body>")


            sw.WriteLine(
                "<h2>Users Report</h2>"
            )


            sw.WriteLine(
                "<p><b>Total Records:</b> " &
                dt.Rows.Count.ToString() &
                "</p>"
            )


            sw.WriteLine(
                "<p><b>Role:</b> " &
                Server.HtmlEncode(
                    If(role = "", "All", role)
                ) &
                "</p>"
            )


            sw.WriteLine(
                "<p><b>Status:</b> " &
                Server.HtmlEncode(
                    If(status = "", "All", status)
                ) &
                "</p>"
            )


            If searchText <> "" Then

                sw.WriteLine(
                    "<p><b>Search:</b> " &
                    Server.HtmlEncode(
                        searchText
                    ) &
                    "</p>"
                )

            End If


            sw.WriteLine(
                "<table border='1' cellpadding='8' cellspacing='0'>"
            )


            sw.WriteLine("<tr>")

            sw.WriteLine("<th>User ID</th>")

            sw.WriteLine("<th>Name</th>")

            sw.WriteLine("<th>Email</th>")

            sw.WriteLine("<th>Mobile</th>")

            sw.WriteLine("<th>Role</th>")

            sw.WriteLine("<th>Status</th>")

            sw.WriteLine("</tr>")


            For Each row As DataRow In dt.Rows


                sw.WriteLine("<tr>")


                sw.WriteLine(
                    "<td>" &
                    Server.HtmlEncode(
                        row("UserID").ToString()
                    ) &
                    "</td>"
                )


                sw.WriteLine(
                    "<td>" &
                    Server.HtmlEncode(
                        row("Name").ToString()
                    ) &
                    "</td>"
                )


                sw.WriteLine(
                    "<td>" &
                    Server.HtmlEncode(
                        row("Email").ToString()
                    ) &
                    "</td>"
                )


                sw.WriteLine(
                    "<td>" &
                    Server.HtmlEncode(
                        row("Mobile").ToString()
                    ) &
                    "</td>"
                )


                sw.WriteLine(
                    "<td>" &
                    Server.HtmlEncode(
                        row("Role").ToString()
                    ) &
                    "</td>"
                )


                sw.WriteLine(
                    "<td>" &
                    Server.HtmlEncode(
                        row("Status").ToString()
                    ) &
                    "</td>"
                )


                sw.WriteLine("</tr>")


            Next


            sw.WriteLine("</table>")

            sw.WriteLine("</body>")

            sw.WriteLine("</html>")


            Response.Clear()

            Response.Buffer = True

            Response.Charset = "utf-8"

            Response.ContentEncoding =
                Encoding.UTF8

            Response.ContentType =
                "application/vnd.ms-excel"


            Response.AddHeader(
                "Content-Disposition",
                "attachment;filename=Users_" &
                If(role = "", "All", role) &
                "_" &
                DateTime.Now.ToString(
                    "yyyyMMdd_HHmmss"
                ) &
                ".xls"
            )


            Response.Write(
                sw.ToString()
            )


            sw.Close()

            Response.Flush()


            HttpContext.Current.
                ApplicationInstance.
                CompleteRequest()


        Catch ex As Exception

            ShowError(
                "Excel export error: " &
                ex.Message
            )

        End Try

    End Sub


    ' =========================================================
    ' DOWNLOAD ALL FILTERED USERS TO PDF
    '
    ' NO OFFSET
    ' NO FETCH
    '
    ' PAGINATION IS IGNORED
    ' =========================================================

    Protected Sub btnDownloadAll_Click(
        ByVal sender As Object,
        ByVal e As EventArgs
    ) Handles btnDownloadAll.Click


        Try

            Dim searchText As String =
                txtSearch.Text.Trim()


            Dim role As String =
                ddlRole.SelectedValue


            Dim status As String =
                ddlStatus.SelectedValue


            ' =================================================
            ' ALL FILTERED USERS
            ' =================================================

            Dim sql As String =
                "SELECT " &
                "UserID, " &
                "[Name], " &
                "[Email], " &
                "[Mobile], " &
                "[Role], " &
                "[Status] " &
                "FROM dbo.Users " &
                "WHERE 1 = 1 "


            AddWhereConditions(
                sql,
                searchText,
                role,
                status
            )


            ' IMPORTANT:
            ' NO OFFSET / FETCH

            sql &=
                "ORDER BY UserID DESC"


            Dim dt As New DataTable()


            Using con As New SqlConnection(conStr)

                con.Open()


                Using cmd As New SqlCommand(
                    sql,
                    con
                )


                    AddFilterParameters(
                        cmd,
                        searchText,
                        role,
                        status
                    )


                    Using da As New SqlDataAdapter(cmd)

                        da.Fill(dt)

                    End Using

                End Using

            End Using


            If dt.Rows.Count = 0 Then

                ShowError(
                    "No users found for the selected filters."
                )

                Return

            End If


            ' =================================================
            ' PDF RESPONSE
            ' =================================================

            Response.Clear()

            Response.Buffer = True

            Response.ContentType =
                "application/pdf"


            Response.AddHeader(
                "Content-Disposition",
                "attachment;filename=Users_" &
                If(role = "", "All", role) &
                "_" &
                DateTime.Now.ToString(
                    "yyyyMMdd_HHmmss"
                ) &
                ".pdf"
            )


            Using ms As New MemoryStream()


                ' =================================================
                ' FULLY QUALIFIED DOCUMENT
                ' =================================================

                Dim document As New iTextSharp.text.Document(
                    iTextSharp.text.PageSize.A4.Rotate(),
                    25,
                    25,
                    25,
                    25
                )


                iTextSharp.text.pdf.PdfWriter.GetInstance(
                    document,
                    ms
                )


                document.Open()


                ' =================================================
                ' FONTS
                ' =================================================

                Dim titleFont As iTextSharp.text.Font =
                    iTextSharp.text.FontFactory.GetFont(
                        iTextSharp.text.FontFactory.HELVETICA_BOLD,
                        18
                    )


                Dim normalFont As iTextSharp.text.Font =
                    iTextSharp.text.FontFactory.GetFont(
                        iTextSharp.text.FontFactory.HELVETICA,
                        10
                    )


                ' =================================================
                ' TITLE
                ' =================================================

                document.Add(
                    New iTextSharp.text.Paragraph(
                        "Users Report",
                        titleFont
                    )
                )


                document.Add(
                    New iTextSharp.text.Paragraph(
                        "Total Records: " &
                        dt.Rows.Count.ToString(),
                        normalFont
                    )
                )


                document.Add(
                    New iTextSharp.text.Paragraph(
                        "Role: " &
                        If(role = "", "All", role),
                        normalFont
                    )
                )


                document.Add(
                    New iTextSharp.text.Paragraph(
                        "Status: " &
                        If(status = "", "All", status),
                        normalFont
                    )
                )


                If searchText <> "" Then

                    document.Add(
                        New iTextSharp.text.Paragraph(
                            "Search: " &
                            searchText,
                            normalFont
                        )
                    )

                End If


                document.Add(
                    New iTextSharp.text.Paragraph(
                        " "
                    )
                )


                ' =================================================
                ' PDF TABLE
                ' =================================================

                Dim table As New iTextSharp.text.pdf.PdfPTable(
                    6
                )


                table.WidthPercentage = 100


                table.SetWidths(
                    New Single() {
                        0.7F,
                        2.0F,
                        2.8F,
                        1.7F,
                        1.2F,
                        1.2F
                    }
                )


                ' =================================================
                ' HEADER
                ' =================================================

                AddPdfHeader(
                    table,
                    "ID"
                )


                AddPdfHeader(
                    table,
                    "Name"
                )


                AddPdfHeader(
                    table,
                    "Email"
                )


                AddPdfHeader(
                    table,
                    "Mobile"
                )


                AddPdfHeader(
                    table,
                    "Role"
                )


                AddPdfHeader(
                    table,
                    "Status"
                )


                ' =================================================
                ' ALL RECORDS
                ' =================================================

                For Each row As DataRow In dt.Rows


                    AddPdfValue(
                        table,
                        row("UserID").ToString()
                    )


                    AddPdfValue(
                        table,
                        row("Name").ToString()
                    )


                    AddPdfValue(
                        table,
                        row("Email").ToString()
                    )


                    AddPdfValue(
                        table,
                        row("Mobile").ToString()
                    )


                    AddPdfValue(
                        table,
                        row("Role").ToString()
                    )


                    AddPdfValue(
                        table,
                        row("Status").ToString()
                    )


                Next


                document.Add(table)


                document.Add(
                    New iTextSharp.text.Paragraph(
                        " "
                    )
                )


                document.Add(
                    New iTextSharp.text.Paragraph(
                        "Generated: " &
                        DateTime.Now.ToString(
                            "dd-MM-yyyy HH:mm"
                        ),
                        normalFont
                    )
                )


                document.Close()


                Response.BinaryWrite(
                    ms.ToArray()
                )


            End Using


            Response.Flush()


            HttpContext.Current.
                ApplicationInstance.
                CompleteRequest()


        Catch ex As Exception

            ShowError(
                "PDF export error: " &
                ex.Message
            )

        End Try

    End Sub


    ' =========================================================
    ' PDF HEADER
    ' =========================================================

    Private Sub AddPdfHeader(
        ByVal table As iTextSharp.text.pdf.PdfPTable,
        ByVal text As String
    )


        Dim font As iTextSharp.text.Font =
            iTextSharp.text.FontFactory.GetFont(
                iTextSharp.text.FontFactory.HELVETICA_BOLD,
                9
            )


        Dim cell As New iTextSharp.text.pdf.PdfPCell(
            New iTextSharp.text.Phrase(
                text,
                font
            )
        )


        cell.HorizontalAlignment =
            iTextSharp.text.Element.ALIGN_CENTER


        cell.Padding = 6


        table.AddCell(cell)

    End Sub


    ' =========================================================
    ' PDF VALUE
    ' =========================================================

    Private Sub AddPdfValue(
        ByVal table As iTextSharp.text.pdf.PdfPTable,
        ByVal text As String
    )


        Dim font As iTextSharp.text.Font =
            iTextSharp.text.FontFactory.GetFont(
                iTextSharp.text.FontFactory.HELVETICA,
                9
            )


        Dim cell As New iTextSharp.text.pdf.PdfPCell(
            New iTextSharp.text.Phrase(
                text,
                font
            )
        )


        cell.Padding = 5


        table.AddCell(cell)

    End Sub


    ' =========================================================
    ' TOGGLE STATUS
    ' =========================================================

    Private Sub ToggleUserStatus(
        ByVal userID As Integer
    )

        Try

            Using con As New SqlConnection(conStr)

                con.Open()


                Dim currentStatus As String = ""


                Using cmd As New SqlCommand(
                    "SELECT [Status] " &
                    "FROM dbo.Users " &
                    "WHERE UserID = @UserID",
                    con
                )


                    cmd.Parameters.Add(
                        "@UserID",
                        SqlDbType.Int
                    ).Value = userID


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


                Dim newStatus As String


                If currentStatus.Trim().
                    ToLower() = "active" Then

                    newStatus = "Inactive"

                Else

                    newStatus = "Active"

                End If


                Using cmd As New SqlCommand(
                    "UPDATE dbo.Users " &
                    "SET [Status] = @Status " &
                    "WHERE UserID = @UserID",
                    con
                )


                    cmd.Parameters.Add(
                        "@Status",
                        SqlDbType.VarChar,
                        50
                    ).Value = newStatus


                    cmd.Parameters.Add(
                        "@UserID",
                        SqlDbType.Int
                    ).Value = userID


                    cmd.ExecuteNonQuery()

                End Using

            End Using


            ShowSuccess(
                "User status updated successfully."
            )


            LoadStatistics()

            LoadUsers()


        Catch ex As Exception

            ShowError(
                "Status update error: " &
                ex.Message
            )

        End Try

    End Sub


    ' =========================================================
    ' DELETE USER
    ' =========================================================

    Private Sub DeleteUser(
        ByVal userID As Integer
    )

        Try

            ' =================================================
            ' PREVENT ADMIN FROM DELETING HIMSELF
            ' =================================================

            If Session("UserID") IsNot Nothing Then

                Dim currentUserID As Integer


                If Integer.TryParse(
                    Session("UserID").ToString(),
                    currentUserID
                ) Then


                    If currentUserID = userID Then

                        ShowError(
                            "You cannot delete your own account."
                        )

                        Return

                    End If

                End If

            End If


            Using con As New SqlConnection(conStr)

                con.Open()


                Using cmd As New SqlCommand(
                    "DELETE FROM dbo.Users " &
                    "WHERE UserID = @UserID",
                    con
                )


                    cmd.Parameters.Add(
                        "@UserID",
                        SqlDbType.Int
                    ).Value = userID


                    Dim affected As Integer =
                        cmd.ExecuteNonQuery()


                    If affected = 0 Then

                        ShowError(
                            "User not found."
                        )

                        Return

                    End If

                End Using

            End Using


            ShowSuccess(
                "User deleted successfully."
            )


            LoadStatistics()

            LoadUsers()


        Catch ex As Exception

            ShowError(
                "Delete error: " &
                ex.Message
            )

        End Try

    End Sub


    ' =========================================================
    ' STATUS CSS
    ' =========================================================

    Protected Function GetStatusCss(
        ByVal status As Object
    ) As String


        If status Is Nothing OrElse
           status Is DBNull.Value Then

            Return "inactive-status"

        End If


        If status.ToString().
            Trim().
            ToLower() = "active" Then

            Return "active-status"

        End If


        Return "inactive-status"

    End Function


    ' =========================================================
    ' STATUS BUTTON TEXT
    ' =========================================================

    Protected Function GetStatusButtonText(
        ByVal status As Object
    ) As String


        If status Is Nothing OrElse
           status Is DBNull.Value Then

            Return "Activate"

        End If


        If status.ToString().
            Trim().
            ToLower() = "active" Then

            Return "Deactivate"

        End If


        Return "Activate"

    End Function


    ' =========================================================
    ' STATUS BUTTON CSS
    ' =========================================================

    Protected Function GetStatusButtonCss(
        ByVal status As Object
    ) As String


        If status Is Nothing OrElse
           status Is DBNull.Value Then

            Return "activate-button"

        End If


        If status.ToString().
            Trim().
            ToLower() = "active" Then

            Return "deactivate-button"

        End If


        Return "activate-button"

    End Function


    ' =========================================================
    ' SUCCESS MESSAGE
    ' =========================================================

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


    ' =========================================================
    ' ERROR MESSAGE
    ' =========================================================

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


    ' =========================================================
    ' HIDE MESSAGE
    ' =========================================================

    Private Sub HideMessage()

        lblMessage.Text = ""

        lblMessage.Visible = False

    End Sub


End Class