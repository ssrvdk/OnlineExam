<%@ Page Title="" Language="C#" MasterPageFile="~/OnlineExam.Master" AutoEventWireup="true" CodeBehind="QuestionMaster.aspx.cs" Inherits="ExamOnline.QuestionMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="https://cdn.jsdelivr.net/npm/simple-datatables@latest/dist/style.css" rel="stylesheet" />
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

        .checkedOption {
            position: absolute;
            left: 0;
            width: 1rem;
            height: 1.25rem;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <main>
        <div class="container-fluid px-4">
            <asp:HiddenField ID="hdMessage" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdQuestionMasterId" runat="server" Value="0" />
            <h1 class="mt-4">QuestionMaster</h1>
            <ol class="breadcrumb mb-4">
                <li class="breadcrumb-item"><a href="index.html">Dashboard</a></li>
                <li class="breadcrumb-item active">Question Master</li>
            </ol>
            <div class="card shadow mb-4" runat="server" id="tblQuestionMaster">
                <div class="card-header py-3">
                    <h6 class="m-0 font-weight-bold text-primary" style="display: inline-block;">Question Master</h6>
                    <asp:Button ID="btnQuestionMaster" runat="server" Text="Add New" class="btn btn-success btn-use floatRight" OnClick="btnQuestionMaster_Click" />
                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <asp:Repeater ID="rptQuestionMaster" runat="server" OnItemCommand="rptQuestionMaster_ItemCommand">
                            <HeaderTemplate>
                                <table class="table table-bordered" id="dataTable" width="100%" cellspacing="0">
                                    <thead>
                                        <tr>
                                            <th>Question</th>
                                            <th>Section Name</th>
                                            <th>Option 1</th>
                                            <th>Option 2</th>
                                            <th>Option 3</th>
                                            <th>Option 4</th>
                                            <th>Answer</th>
                                            <th>Status</th>
                                            <th>Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblQuestion" runat="server" ClientIDMode="Static" Text='<%#Eval("Question")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSection" runat="server" ClientIDMode="Static" Text='<%#Eval("SectionName")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblOpt1" runat="server" ClientIDMode="Static" Text='<%#Eval("Option1")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblOpt2" runat="server" ClientIDMode="Static" Text='<%#Eval("Option2")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblOpt3" runat="server" ClientIDMode="Static" Text='<%#Eval("Option3")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblOpt4" runat="server" ClientIDMode="Static" Text='<%#Eval("Option4")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblAnswer" runat="server" ClientIDMode="Static" Text='<%#Eval("Answer")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <a class="btn btn-<%# Convert.ToString(Eval("Status"))=="True" ? "info" : "secondary"  %> btn-icon-split" style="color: white;">
                                            <span class="icon text-white-50">
                                                <i class="fas fa-arrow-right"></i>
                                            </span>
                                            <span class="text">
                                                <asp:Label ID="LblStatus" runat="server" ClientIDMode="Static" Text='<%# Convert.ToString(Eval("Status"))=="True" ? "Active":"InActive" %>'></asp:Label></span>
                                        </a>
                                    </td>
                                    <td>
                                        <asp:HiddenField ID="hdnId" runat="server" Value='<%#Eval("QuestionMasterId") %>' />
                                        <%--<asp:HiddenField ID="hdnAnswerId" runat="server" Value='<%#Eval("AnswerId") %>' />--%>
                                        <asp:HiddenField ID="hdnSectionId" runat="server" Value='<%#Eval("SectionId") %>' />
                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="CatEdit" CssClass="btn btn-success btn-circle btn-sm" CommandArgument='<%#Eval("QuestionMasterId") %>'><i class="fas fa-edit"></i></asp:LinkButton>
                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="CatDelete" CssClass="btn btn-danger btn-circle btn-sm" CommandArgument='<%#Eval("QuestionMasterId") %>' OnClientClick="return confirm('Are you sure you want delete');"><i class="fas fa-trash"></i></asp:LinkButton>
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

            <div class="card shadow mb-4" runat="server" id="frmQuestionMaster">
                <div class="card-header py-3">
                    <h6 class="m-0 font-weight-bold text-primary" style="display: inline-block;">Add Question</h6>
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
                            <label for="txtQuestion" class="form-label">Question</label>
                            <asp:TextBox ID="txtQuestion" runat="server" class="form-control form-control-user" placeholder="Enter a question" MaxLength="1000"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="fontcolour" ControlToValidate="txtQuestion" runat="server" ErrorMessage="Please enter a question." ValidationGroup="save"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-sm-6 mb-3 mb-sm-0">
                            <label for="txtOption1" class="form-label">Option1</label>
                            <asp:TextBox ID="txtOption1" runat="server" class="form-control form-control-user" placeholder="Enter an option1" MaxLength="500"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="fontcolour" ControlToValidate="txtOption1" runat="server" ErrorMessage="Please enter an option1." ValidationGroup="save"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-sm-6 custom-control custom-checkbox large" style="line-height: 6.4em; margin-top: 38px;">
                            <asp:CheckBox ID="chkOption1" runat="server" CssClass="custom-control-input" />
                            <label class="custom-control-label" for="chkOption1"></label>
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-sm-6 mb-3 mb-sm-0">
                            <label for="txtOption2" class="form-label">Option2</label>
                            <asp:TextBox ID="txtOption2" runat="server" class="form-control form-control-user" placeholder="Enter an option2" MaxLength="500"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="fontcolour" ControlToValidate="txtOption2" runat="server" ErrorMessage="Please enter an option2." ValidationGroup="save"></asp:RequiredFieldValidator>
                        </div>
                       <div class="col-sm-6 custom-control custom-checkbox large" style="line-height: 6.4em; margin-top: 38px;">
                            <asp:CheckBox ID="chkOption2" runat="server" CssClass="custom-control-input" />
                            <label class="custom-control-label" for="chkOption2"></label>
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-sm-6 mb-3 mb-sm-0">
                            <label for="txtOption3" class="form-label">Option3</label>
                            <asp:TextBox ID="txtOption3" runat="server" class="form-control form-control-user" placeholder="Enter an option3" MaxLength="500"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="fontcolour" ControlToValidate="txtOption3" runat="server" ErrorMessage="Please enter an option3." ValidationGroup="save"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-sm-6 custom-control custom-checkbox large" style="line-height: 6.4em; margin-top: 38px;">
                            <asp:CheckBox ID="chkOption3" runat="server" CssClass="custom-control-input" />
                            <label class="custom-control-label" for="chkOption3"></label>
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-sm-6 mb-3 mb-sm-0">
                            <label for="txtOption4" class="form-label">Option4</label>
                            <asp:TextBox ID="txtOption4" runat="server" class="form-control form-control-user" placeholder="Enter an option4" MaxLength="500"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="fontcolour" ControlToValidate="txtOption4" runat="server" ErrorMessage="Please enter an option4." ValidationGroup="save"></asp:RequiredFieldValidator>

                        </div>
                        <div class="col-sm-6 custom-control custom-checkbox large" style="line-height: 6.4em; margin-top: 38px;">
                            <asp:CheckBox ID="chkOption4" runat="server" CssClass="custom-control-input" />
                            <label class="custom-control-label" for="chkOption4"></label>
                        </div>
                    </div>
                    <div class="form-group row">
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
    <%--<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" crossorigin="anonymous"></script>
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
