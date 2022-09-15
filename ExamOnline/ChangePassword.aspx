<%@ Page Title="" Language="C#" MasterPageFile="~/OnlineExam.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="ExamOnline.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Custom styles for this page -->
    <link href="vendor/datatables/dataTables.bootstrap4.min.css" rel="stylesheet">
    <style>
        .floatRight {
            float: right;
        }

        .fontcolour {
            color: red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="card shadow mb-4" runat="server" id="frmChangedPassword">
            <div class="card-header py-3">
                <asp:hiddenfield id="hdUserId" runat="server" />
                <asp:hiddenfield id="hdUserName" runat="server" />
                <asp:hiddenfield id="hdCurrentPassword" runat="server" />
                <asp:hiddenfield id="hdMessage" runat="server" />
                <%--Change Password--%>
                <asp:label id="lblMess" class="m-0 font-weight-bold text-primary" style="display: inline-block;" runat="server" clientidmode="Static"></asp:label>
                <asp:button id="btnUpdate" runat="server" text="Update" class="btn btn-success btn-use floatRight" onclick="btnUpdate_Click" style="margin-left: 10px;" validationgroup="save" />
                <asp:button id="btnCancel" runat="server" text="Cancel" class="btn btn-danger btn-use floatRight" onclick="btnCancel_Click" />
            </div>
            <div class="card-body">
                <div class="form-group row">
                    <div class="col-sm-6 mb-3 mb-sm-0">
                        Current Password
                        <asp:textbox id="txtCurrentPassword" runat="server" class="form-control form-control-user" textmode="Password" placeholder="CurrentPassword"></asp:textbox>
                        <asp:requiredfieldvalidator id="RequiredFieldValidator2" cssclass="fontcolour" controltovalidate="txtCurrentPassword" runat="server" errormessage="Please Enter Current Password." validationgroup="save" display="Dynamic"></asp:requiredfieldvalidator>
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col-sm-6 mb-3 mb-sm-0">
                        New Password
                        <asp:textbox id="txtPassword" runat="server" class="form-control form-control-user" placeholder="Password"></asp:textbox>
                        <asp:requiredfieldvalidator id="RequiredFieldValidator1" cssclass="fontcolour" controltovalidate="txtPassword" runat="server" errormessage="Please Enter Password." validationgroup="save" display="Dynamic"></asp:requiredfieldvalidator>
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col-sm-6 mb-3 mb-sm-0">
                        Confirm Password
                        <asp:textbox id="txtConfirmPassword" runat="server" class="form-control form-control-user" placeholder="Confirm Password"></asp:textbox>
                        <asp:comparevalidator id="CompareValidator1" runat="server" controltovalidate="txtConfirmPassword" cssclass="fontcolour ValidationError" controltocompare="txtPassword" errormessage="Password No Match" tooltip="Password must be the same" display="Dynamic" />
                        <asp:requiredfieldvalidator id="RequiredFieldValidator3" cssclass="fontcolour" controltovalidate="txtConfirmPassword" runat="server" errormessage="Please Enter Confirm Password." validationgroup="save" display="Dynamic"></asp:requiredfieldvalidator>
                    </div>
                </div>
            </div>
            <div class=""></div>
        </div>
    </div>
    <div class="modal fade" id="MessageModel" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Message</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>

                    </div>
                    <div class="modal-body">
                        <p>
                            <asp:Label ID="lblMessage" runat="server"></asp:Label></p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnOk" runat="server" Text="OK" class="btn" OnClick="btnOk_Click" />
                    </div>
                </div>
            </div>
        </div>

    <!-- Page level plugins -->
    <script src="vendor/datatables/jquery.dataTables.min.js"></script>
    <script src="vendor/datatables/dataTables.bootstrap4.min.js"></script>
    <script type="text/javascript">
        function ShowMessageForm() {
            $('#MessageModel').modal('show');
        }

        function Validate() {
            var password = document.getElementById("txtPassword").value;
            var confirmPassword = document.getElementById("txtConfirmPassword").value;
            if (password != confirmPassword) {
                alert("Passwords do not match.");
                return false;
            }
            return true;
        }
        $("#frmChangedPassword").validate({
            rules: {
                password: {
                    minlength: 5,
                    maxlength: 10
                },
                password_confirm: {
                    minlength: 5,
                    maxlength: 10,
                    equalTo: "#password"
                }


            }
        });
    </script>
</asp:Content>
