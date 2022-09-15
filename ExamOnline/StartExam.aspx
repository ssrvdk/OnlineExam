<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StartExam.aspx.cs" Inherits="ExamOnline.StartExam" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Start Exam</title>
    <link href="~/css/styles.css" rel="stylesheet" />
    <script src="https://use.fontawesome.com/releases/v6.1.0/js/all.js" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
    <style>
        .btn-style {
            width: 35px;
            border: 1px solid black;
            height: 35px;
            border-radius: 50%;
            background-color: white;
        }

            .btn-style:hover {
                background-color: dodgerblue;
            }

            .btn-style:active {
                background-color: green;
            }

        .btnAns {
            width: 35px;
            border: 1px solid black;
            height: 35px;
            border-radius: 50%;
            background-color: #49be25;
        }

        .btnVisit {
            width: 35px;
            border: 1px solid black;
            height: 35px;
            border-radius: 50%;
            background-color: red;
        }

        .btnNotVisit {
            width: 35px;
            border: 1px solid black;
            height: 35px;
            border-radius: 50%;
            background-color: White;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scriptmanager1" runat="server"></asp:ScriptManager>
        <div>
            <%--<asp:Timer ID="Timer1" runat="server" OnTick="GetTime" Interval="1000" />--%>
            <asp:UpdatePanel ID="updatepnl" runat="server">
                <ContentTemplate>
                    <div class="container-fluid">
                        <div class="row content">
                            <div class="col-md-12 card-header">
                                <div class="col-md-10">
                                    <h3 class="">Online Exam</h3>
                                </div>
                                <div class="col-md-2">
                                    <h4 style="line-height: 2em;">
                                        <asp:Label ID="lblTime" runat="server" /></h4>

                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <hr />
                            <div class="card-body">

                                <div class="col-sm-9" style="border-right: 1px solid #cecece">
                                    <asp:Repeater ID="rptSection" runat="server" OnItemCommand="rptSection_ItemCommand">
                                        <ItemTemplate>
                                            <div class="col-md-4">
                                                <asp:Button ID="btnSectionName" Width="100" runat="server" Text='<%#Eval("SectionName")%>' />
                                                <asp:HiddenField ID="hdnSectionId" runat="server" Value='<%#Eval("SectionId")%>' />
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                    <div class="clearfix"></div>
                                    <hr />
                                    <h2>Question:
                                        <asp:Label ID="lblQuestNo" runat="server" Text="1"></asp:Label></h2>
                                    <h4>
                                        <asp:Label ID="lblQuestion" runat="server"></asp:Label></h4>
                                    <asp:HiddenField ID="hdnQuestionId" runat="server" />
                                    <%--<h4>First Question In the first step we need to create order with WestCor so we need to open a page it should display all the information that we will use for Order Opening. with Create Button and when it is created then we need to let user go on Listing.aspx page.</h4>--%>
                                    <%--<h5><span class="label label-danger">Food</span> <span class="label label-primary">Ipsum</span></h5>--%>
                                    <br>
                                    <asp:RadioButtonList ID="rbtnOptions" runat="server">
                                        <%--  <asp:ListItem Text="Option 1" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Option 2" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Option 3" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Option 4" Value="4"></asp:ListItem>--%>
                                    </asp:RadioButtonList>
                                    <div class="card-footer">
                                        <asp:Button ID="btnPrevious" Text="Previous" runat="server" OnClick="OnPrevious" />
                                        <asp:Button ID="btnNext" Text="Next" runat="server" OnClick="OnNext" Style="float: right;" />
                                    </div>
                                </div>
                                <div class="col-sm-3 sidenav">
                                    <%--<h4>Choose a Question</h4>--%>
                                    <asp:Button ID="btnAnswered" CssClass="btnAns" runat="server" Text="0" />&nbsp;<asp:Label ID="Label1" runat="server" Text="Answered"></asp:Label>
                                    <asp:Button ID="btnVisited" CssClass="btnVisit" runat="server" Text="0" />&nbsp;<asp:Label ID="Label2" runat="server" Text="Visited"></asp:Label>
                                    <asp:Button ID="btnNotVisited" CssClass="btnNotVisit" runat="server" Text="0" />&nbsp;<asp:Label ID="Label3" runat="server" Text="Not Visited"></asp:Label>
                                </div>
                                <div class="col-sm-3 sidenav">
                                    <h4>Choose a Question</h4>
                                    <ul class="nav nav-pills nav-stacked">
                                        <li>
                                            <asp:Repeater ID="rptQuestionNo" runat="server" OnItemCommand="rptQuestionNo_ItemCommand">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnQuestionNumber" CssClass="btn-style" runat="server" Text='<%#Eval("QuestionNumber")%>' />
                                                    <%--    <asp:HiddenField ID="hdnQuestionMasterId" runat="server" value='<%#Eval("QuestionMasterId")%>'/>--%>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                    </div>
                </ContentTemplate>
                <%--<Triggers>
                    <asp:AsyncPostBackTrigger ControlID="timer1" EventName="tick" />
                </Triggers>--%>
            </asp:UpdatePanel>
        </div>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" crossorigin="anonymous"></script>
    <script src="/scripts/scripts.js"></script>
    <script type="text/javascript">
        function myFunction() {
            document.getElementById("demo").style.color = "red";
        }
    </script>

</body>
</html>
