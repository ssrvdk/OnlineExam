<%@ Page Title="" Language="C#" MasterPageFile="~/OnlineExam.Master" AutoEventWireup="true" CodeBehind="StudentDetails.aspx.cs" Inherits="ExamOnline.StudentDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- <link href="https://cdn.jsdelivr.net/npm/simple-datatables@latest/dist/style.css" rel="stylesheet" />
    <link href="~/css/styles.css" rel="stylesheet" />
    <script src="https://use.fontawesome.com/releases/v6.1.0/js/all.js" crossorigin="anonymous"></script>--%>
    <!-- Custom styles for this page -->
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
            <asp:HiddenField ID="hdStudentDetailsId" runat="server" Value="0" />
            <h1 class="mt-4">StudentDetails</h1>
            <ol class="breadcrumb mb-4">
                <li class="breadcrumb-item"><a href="index.html">Dashboard</a></li>
                <li class="breadcrumb-item active">Student Details</li>
            </ol>
            <div class="card shadow mb-4" runat="server" id="tblStudentDetails">
                <div class="card-header py-3">
                    <h6 class="m-0 font-weight-bold text-primary" style="display: inline-block;">Student Details</h6>
                    <asp:Button ID="btnStudentDetails" runat="server" Text="Add New" class="btn btn-success btn-use floatRight" OnClick="btnStudentDetails_Click" />
                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <asp:Repeater ID="lstStudentDetails" runat="server" OnItemCommand="lstStudentDetails_ItemCommand">
                            <HeaderTemplate>
                                <table class="table table-bordered" id="dataTable" width="100%" cellspacing="0">
                                    <thead>
                                        <tr>
                                            <th>Student Name</th>
                                            <th>Mobile Number</th>
                                            <%-- <th>Password</th>--%>
                                            <th>Address</th>
                                            <th>Email Address</th>
                                            <th>City</th>
                                            <th>State</th>
                                            <th>bVerified</th>
                                            <th>Status</th>
                                            <th>Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblstudentName" runat="server" ClientIDMode="Static" Text='<%#Eval("Name")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblMobileNumber" runat="server" ClientIDMode="Static" Text='<%#Eval("MobileNumber")%>'></asp:Label>
                                    </td>
                                    <%--<td>
                                        <asp:Label ID="lblPassword" runat="server" ClientIDMode="Static" Text='<%#Eval("Password")%>'></asp:Label>
                                    </td>--%>
                                    <td>
                                        <asp:Label ID="lblAddress" runat="server" ClientIDMode="Static" Text='<%#Eval("Address")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblEmailAddress" runat="server" ClientIDMode="Static" Text='<%#Eval("EmailAddress")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCity" runat="server" ClientIDMode="Static" Text='<%#Eval("City")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblState" runat="server" ClientIDMode="Static" Text='<%#Eval("State")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <a class="btn btn-<%# Convert.ToString(Eval("Verify"))=="True" ? "info" : "secondary"  %> btn-icon-split" style="color: white;">
                                            <span class="icon text-white-50">
                                                <i class="fas fa-arrow-right"></i>
                                            </span>
                                            <span class="text">
                                                <asp:Label ID="lblVerify" runat="server" ClientIDMode="Static" Text='<%# Convert.ToString(Eval("Verify"))=="True" ? "Active":"InActive" %>'></asp:Label></span>
                                        </a>
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
                                        <asp:HiddenField ID="hdnId" runat="server" Value='<%#Eval("StudentDetailsId") %>' />
                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="CatEdit" CssClass="btn btn-success btn-circle btn-sm" CommandArgument='<%#Eval("StudentDetailsId") %>'><i class="fas fa-edit"></i></asp:LinkButton>
                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="CatDelete" CssClass="btn btn-danger btn-circle btn-sm" CommandArgument='<%#Eval("StudentDetailsId") %>' OnClientClick="return confirm('Are you sure you want delete');"><i class="fas fa-trash"></i></asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                                   <%-- <tfoot>
                                        <tr>
                                            <th>Name</th>
                                            <th>Salary</th>
                                        </tr>
                                    </tfoot>--%>
                            </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>

            <div class="card shadow mb-4" runat="server" id="frmStudentDetails">
                <div class="card-header py-3">
                    <h6 class="m-0 font-weight-bold text-primary" style="display: inline-block;">Add Student Details</h6>
                </div>
                <div class="card-body">
                    <div class="form-group row">
                        <div class="col-sm-6 mb-3 mb-sm-0">
                            <asp:TextBox ID="txtStudentName" runat="server" class="form-control form-control-user" placeholder="Student Name" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="fontcolour" ControlToValidate="txtStudentName" runat="server" ErrorMessage="Please Enter Student Name." ValidationGroup="save"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-sm-6 mb-3 mb-sm-0">
                            <asp:TextBox ID="txtMobileNumber" runat="server" class="form-control form-control-user" placeholder="Mobile Number" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="fontcolour" ControlToValidate="txtMobileNumber" runat="server" ErrorMessage="Please Enter Mobile Number." ValidationGroup="save"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-sm-6 mb-3 mb-sm-0">
                            <asp:TextBox ID="txtAddress" runat="server" class="form-control form-control-user" placeholder="Address" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="fontcolour" ControlToValidate="txtAddress" runat="server" ErrorMessage="Please Enter Address." ValidationGroup="save"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-sm-6 mb-3 mb-sm-0">
                            <asp:TextBox ID="txtEmailAddress" runat="server" class="form-control form-control-user" placeholder="Email Address" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="fontcolour" ControlToValidate="txtEmailAddress" runat="server" ErrorMessage="Please Enter Email Address." ValidationGroup="save"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-sm-6 mb-3 mb-sm-0">
                            <asp:TextBox ID="txtCity" runat="server" class="form-control form-control-user" placeholder="City" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="fontcolour" ControlToValidate="txtCity" runat="server" ErrorMessage="Please Enter City." ValidationGroup="save"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-sm-6 mb-3 mb-sm-0">
                            <asp:TextBox ID="txtState" runat="server" class="form-control form-control-user" placeholder="State" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" CssClass="fontcolour" ControlToValidate="txtState" runat="server" ErrorMessage="Please Enter State." ValidationGroup="save"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <%--  <div class="form-group row">
                        <div class="col-sm-6 mb-3 mb-sm-0">
                            <asp:TextBox ID="txtbVerified" runat="server" class="form-control form-control-user" placeholder="bVerified" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="fontcolour" ControlToValidate="txtbVerified" runat="server" ErrorMessage="Please Enter bVerified." ValidationGroup="save"></asp:RequiredFieldValidator>
                        </div>
                    </div>--%>
                    <div class="form-group">
                        <%--<div class="custom-control custom-checkbox large">
                            <span class="backspace"></span>
                            <asp:CheckBox ID="chkbVerified" runat="server" CssClass="checkedOption" ClientIDMode="Static" />
                            <label class="" for="chkbVerified">Active</label>
                        </div>--%>
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
    <%--  <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" crossorigin="anonymous"></script>
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
            // $('#dataTa').DataTable();
        });
    </script>
</asp:Content>
