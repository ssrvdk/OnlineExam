<%@ Page Title="" Language="C#" MasterPageFile="~/OnlineExam.Master" AutoEventWireup="true" CodeBehind="QuestionPaperSectionMapping.aspx.cs" Inherits="ExamOnline.QuestionPaperSectionMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--  <link href="https://cdn.jsdelivr.net/npm/simple-datatables@latest/dist/style.css" rel="stylesheet" />
    <link href="~/css/styles.css" rel="stylesheet" />
    <script src="https://use.fontawesome.com/releases/v6.1.0/js/all.js" crossorigin="anonymous"></script>--%>
    <link href="vendor/datatables/dataTables.bootstrap4.min.css" rel="stylesheet">
    <%--<link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" type="text/css">--%>
    <%--<link href="https://cdn.datatables.net/1.10.22/css/dataTables.bootstrap.min.css" rel="stylesheet" type="text/css">--%>
    <style>
        .floatRight {
            float: right;
        }

        .backspace {
            margin-left: 6px;
        }

        .largerCheckbox {
            width: 30px;
            height: 30px;
        }

        .mycheckBig input {
            width: 25px;
            height: 25px;
        }

        .mycheckSmall input {
            width: 10px;
            height: 10px;
        }

        .fontcolour {
            color: red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <main>
        <div class="container-fluid px-4">
            <asp:HiddenField ID="hdMessage" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdQuePprSectionMappingId" runat="server" Value="0" />
            <h1 class="mt-4">QuestionPaperSectionMapping</h1>
            <ol class="breadcrumb mb-4">
                <li class="breadcrumb-item"><a href="index.html">Dashboard</a></li>
                <li class="breadcrumb-item active">Question Paper Section Mapping</li>
            </ol>
            <div class="card shadow mb-4" runat="server" id="tblQuestionPaperSectionMapping">
                <div class="card-header py-3">
                    <h6 class="m-0 font-weight-bold text-primary" style="display: inline-block;">Question Paper Section Mapping</h6>
                    <asp:Button ID="btnQuestionPaperSectionMapping" runat="server" Text="Add New" class="btn btn-success btn-use floatRight" OnClick="btnQuestionPaperSectionMapping_Click" />
                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <asp:Repeater ID="rptQuestionPaperSectionMapping" runat="server" OnItemCommand="rptQuestionPaperSectionMapping_ItemCommand">
                            <HeaderTemplate>
                                <table class="table table-bordered" id="dataTable" width="100%" cellspacing="0">
                                    <thead>
                                        <tr>
                                            <th>Question Name</th>
                                            <th>Section Name</th>
                                            <th>Number Of Question</th>
                                            <th>Status</th>
                                            <th>Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblQuestion" runat="server" ClientIDMode="Static" Text='<%#Eval("sName")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSection" runat="server" ClientIDMode="Static" Text='<%#Eval("SectionName")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbNumberOfQuestion" runat="server" ClientIDMode="Static" Text='<%#Eval("NumberOfQuestion")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <a class="btn btn-<%# Convert.ToString(Eval("bActive"))=="1" ? "info" : "secondary"  %> btn-icon-split" style="color: white;">
                                            <span class="icon text-white-50">
                                                <i class="fas fa-arrow-right"></i>
                                            </span>
                                            <span class="text">
                                                <asp:Label ID="LblStatus" runat="server" ClientIDMode="Static" Text='<%# Convert.ToString(Eval("bActive"))=="1" ? "Active":"InActive" %>'></asp:Label></span>
                                        </a>
                                    </td>
                                    <td>
                                        <asp:HiddenField ID="hdnId" runat="server" Value='<%#Eval("QuePprSectionMappingId") %>' />
                                        <asp:HiddenField ID="hdnSectionId" runat="server" Value='<%#Eval("Sectionid") %>' />
                                        <asp:HiddenField ID="hdnQuestionId" runat="server" Value='<%#Eval("QuestionPaperid") %>' />
                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="CatEdit" CssClass="btn btn-success btn-circle btn-sm" CommandArgument='<%#Eval("QuePprSectionMappingId") %>'><i class="fas fa-edit"></i></asp:LinkButton>
                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="CatDelete" CssClass="btn btn-danger btn-circle btn-sm" CommandArgument='<%#Eval("QuePprSectionMappingId") %>' OnClientClick="return confirm('Are you sure you want delete');"><i class="fas fa-trash"></i></asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                            </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>

            <div class="card shadow mb-4" runat="server" id="frmQuestionPaperSectionMapping">
                <div class="card-header py-3">
                    <h6 class="m-0 font-weight-bold text-primary" style="display: inline-block;">Add Question paper Section Mapping</h6>
                </div>
                <div class="card-body">
                    <div class="form-group row">
                        <div class="col-sm-6 mb-3 mb-sm-0">
                            <label for="ddlSection" class="form-label">Section</label>
                            <asp:DropDownList ID="ddlSection" runat="server" class="form-control form-select"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="fontcolour" ControlToValidate="ddlSection" runat="server" ErrorMessage="Please select a section." ValidationGroup="save"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-sm-6 mb-3 mb-sm-0">
                            <label for="ddlQuestion" class="form-label">Question Name</label>
                            <asp:DropDownList ID="ddlQuestion" runat="server" class="form-control form-select"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="fontcolour" ControlToValidate="ddlQuestion" runat="server" ErrorMessage="Please select a question name." ValidationGroup="save"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-sm-6 mb-3 mb-sm-0">
                            <label for="txtNumberOfQuestion" class="form-label">No. Of Question</label>
                            <asp:TextBox ID="txtNumberOfQuestion" runat="server" class="form-control form-control-user" placeholder="Enter a no. of question" MaxLength="1000"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="fontcolour" ControlToValidate="txtNumberOfQuestion" runat="server" ErrorMessage="Please enter a no. of question." ValidationGroup="save"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator runat="server" CssClass="fontcolour" ErrorMessage="Numeric Only" ControlToValidate="txtNumberOfQuestion" ValidationExpression="^[0-9]*$" ValidationGroup="save"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="custom-control custom-checkbox large">
                            <span class="backspace"></span>
                            <asp:CheckBox ID="chkStatus" runat="server" CssClass="custom-control-input" ClientIDMode="Static" />
                            <label class="custom-control-label" for="chkStatus">Active</label>
                        </div>
                    </div>
                    <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-success btn-use floatRight" OnClick="btnSave_Click" Style="margin-left: 10px;" ValidationGroup="save" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-danger btn-use floatRight" OnClick="btnCancel_Click" />
                </div>
            </div>

        </div>
    </main>
    <!-- Page level plugins -->
    <%-- <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" crossorigin="anonymous"></script>
        <script src="/scripts/scripts.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/simple-datatables@latest" crossorigin="anonymous"></script>
        <script src="/scripts/datatables-simple-demo.js"></script>--%>
    <script src="https://code.jquery.com/jquery-3.5.1.js"></script>
    <script src="https://cdn.datatables.net/1.10.22/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.22/js/dataTables.bootstrap.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#dataTable').DataTable({
                "pagingType": "full_numbers"
            });
        });
    </script>

</asp:Content>
