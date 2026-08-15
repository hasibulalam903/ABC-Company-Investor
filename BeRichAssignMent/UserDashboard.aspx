
<%@ Page Title="User Dashboard"
    Language="VB"
    MasterPageFile="~/Site.master"
    AutoEventWireup="false"
    CodeFile="UserDashboard.aspx.vb"
    Inherits="UserDashboard" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <div class="container mt-5">

        <div class="text-center mb-5">

            <h1 class="fw-bold">
                User Dashboard
            </h1>

            <p class="text-muted">
                Welcome to your Investor Management Dashboard
            </p>

        </div>


        <!-- Welcome Card -->

        <div class="card shadow-sm mb-4">

            <div class="card-body">

                <h3>
                    Welcome,
                    <asp:Label
                        ID="lblUserName"
                        runat="server"
                        Text="User">
                    </asp:Label>
                </h3>


                <p class="mb-1">

                    Email:

                    <strong>

                        <asp:Label
                            ID="lblEmail"
                            runat="server">
                        </asp:Label>

                    </strong>

                </p>


                <p class="mb-1">

                    Role:

                    <strong>

                        <asp:Label
                            ID="lblRole"
                            runat="server">
                        </asp:Label>

                    </strong>

                </p>


                <p class="mb-0">

                    Account Status:

                    <strong>

                        <asp:Label
                            ID="lblStatus"
                            runat="server">
                        </asp:Label>

                    </strong>

                </p>

            </div>

        </div>


        <!-- Dashboard Cards -->

        <div class="row g-4">


            <!-- Profile -->

            <div class="col-md-4">

                <div class="card shadow-sm h-100">

                    <div class="card-body text-center">

                        <h4>
                            My Profile
                        </h4>

                        <p class="text-muted">
                            View and update your profile information.
                        </p>

                        <asp:Button
                            ID="btnProfile"
                            runat="server"
                            Text="View Profile"
                            CssClass="btn btn-primary" />

                    </div>

                </div>

            </div>


            <!-- Investments -->

            <div class="col-md-4">

                <div class="card shadow-sm h-100">

                    <div class="card-body text-center">

                        <h4>
                            My Investments
                        </h4>

                        <p class="text-muted">
                            View your investment information.
                        </p>

                        <asp:Button
                            ID="btnInvestments"
                            runat="server"
                            Text="View Investments"
                            CssClass="btn btn-success" />

                    </div>

                </div>

            </div>


            <!-- Logout -->

 <div class="col-md-4">

     <div class="card shadow-sm h-100">

        

     </div>

 </div>


        </div>

    </div>

</asp:Content>
