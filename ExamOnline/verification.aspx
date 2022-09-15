<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="verification.aspx.cs" Inherits="ExamOnline.verification" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <!-- breadcrumb area end -->
        <div class="my-account-wrapper section-padding">
            <div class="container">
                <div class="section-bg-color">
                    <div class="row">
                        <div class="col-lg-12">
                            <!-- My Account Page Start -->
                            <div class="myaccount-page-wrapper">
                                <!-- My Account Tab Menu Start -->
                                <asp:Panel ID="pnlSuccess" runat="server">
                                    <div class="row">
                                        <div class="col-lg-12 col-md-8">
                                            <div class="tab-content" id="myaccountContent">
                                                <!-- Single Tab Content Start -->

                                                <%-- <div class="tab-pane fade show active" id="dashboad" role="tabpanel">--%>
                                                <div class="myaccount-content">
                                                    <h5>
                                                        <asp:Label ID="lblHMessage" runat="server"></asp:Label></h5>
                                                    <div class="welcome">
                                                        <p>
                                                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                                        </p>
                                                    </div>
                                                    <div></div>
                                                    <p class="mb-0">
                                                        Click here to login: <a href="Index.aspx">Click here</a>
                                                    </p>

                                                </div>
                                                <%--</div>--%>

                                                <!-- Single Tab Content End -->
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlFailed" runat="server">
                                    <div class="row">
                                        <div class="col-lg-12 col-md-8">
                                            <div class="tab-content" id="myContent">
                                                <!-- Single Tab Content Start -->

                                                <%-- <div class="tab-pane fade show active" id="dashboad" role="tabpanel">--%>
                                                <div class="myaccount-content">
                                                    <h5>
                                                        <asp:Label ID="lblHMsg" runat="server"></asp:Label></h5>
                                                    <div class="welcome">
                                                        <p>
                                                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                                        </p>
                                                    </div>
                                                    <div></div>
                                                    <%--<p class="mb-0">
                                                Click here to login: <a href="../login">Click here</a>
                                            </p>--%>
                                                </div>
                                                <%--</div>--%>

                                                <!-- Single Tab Content End -->
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
