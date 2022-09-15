<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SChangedPassword.aspx.cs" Inherits="ExamOnline.Student.SChangedPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Changed Password</title>
    <link href="../css/styles.css" rel="stylesheet" />
    <script src="https://use.fontawesome.com/releases/v6.1.0/js/all.js" crossorigin="anonymous"></script>
    <style>
        .floatRight {
            float: right;
        }

        .fontcolour {
            color: red;
        }
    </style>
</head>
<body class="bg-primary">
    <form id="form1" runat="server">
        <div id="layoutAuthentication">
            <div id="layoutAuthentication_content">
                <main>
                    <div class="container">
                        <div class="row justify-content-center">
                            <div class="col-lg-5">
                                <div class="card shadow-lg border-0 rounded-lg mt-5">
                                    <div class="card-header">
                                        <%--<h3 class="text-center font-weight-light my-4">Login</h3>--%>
                                        <asp:HiddenField ID="hdUserId" runat="server" />
                                        <asp:HiddenField ID="hdUserName" runat="server" />
                                        <asp:HiddenField ID="hdCurrentPassword" runat="server" />
                                        <asp:HiddenField ID="hdMessage" runat="server" />
                                        <%--Change Password--%>
                                        <asp:Label ID="lblMess" class="m-0 font-weight-bold text-primary" Style="display: inline-block;" runat="server" ClientIDMode="Static"></asp:Label>
                                        <asp:Button ID="btnUpdate" runat="server" Text="Update" class="btn btn-success btn-use floatRight" OnClick="btnUpdate_Click" Style="margin-left: 10px;" ValidationGroup="save" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-danger btn-use floatRight" OnClick="btnCancel_Click" />
                                    </div>
                                    <div class="card-body">
                                        <form>
                                            <div class="form-floating mb-3">
                                                <asp:TextBox ID="txtCurrentPassword" runat="server" class="form-control form-control-user" TextMode="Password" placeholder="CurrentPassword"></asp:TextBox>
                                                <label for="inputEmail">Current Password</label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="fontcolour" ControlToValidate="txtCurrentPassword" runat="server" ErrorMessage="Please Enter Current Password." ValidationGroup="save" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-floating mb-3">
                                                <asp:TextBox ID="txtPassword" runat="server" class="form-control form-control-user" placeholder="Password"></asp:TextBox>
                                                <label for="inputEmail">New Password</label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="fontcolour" ControlToValidate="txtPassword" runat="server" ErrorMessage="Please Enter Password." ValidationGroup="save" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-floating mb-3">
                                                <asp:TextBox ID="txtConfirmPassword" runat="server" class="form-control form-control-user" placeholder="Confirm Password"></asp:TextBox>
                                                <label for="inputEmail">Confirm Password</label>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtConfirmPassword" CssClass="fontcolour ValidationError" ControlToCompare="txtPassword" ErrorMessage="Password No Match" ToolTip="Password must be the same" Display="Dynamic" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="fontcolour" ControlToValidate="txtConfirmPassword" runat="server" ErrorMessage="Please Enter Confirm Password." ValidationGroup="save" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </form>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </main>
            </div>
            <div id="layoutAuthentication_footer">
                <footer class="py-4 bg-light mt-auto">
                    <div class="container-fluid px-4">
                        <div class="d-flex align-items-center justify-content-between small">
                            <div class="text-muted">Copyright &copy; SRV Software Soltions 2022</div>
                            <div>
                                <a href="#">Privacy Policy</a>
                                &middot;
                                <a href="#">Terms &amp; Conditions</a>
                            </div>
                        </div>
                    </div>
                </footer>
            </div>
        </div>
        <!-- The Modal -->
        <div class="modal fade" id="MessageModel" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Message</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>

                    </div>
                    <div class="modal-body">
                        <p>
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnOk" runat="server" Text="OK" class="btn btn-success" OnClick="btnOk_Click" />
                    </div>
                </div>
            </div>
        </div>

        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" crossorigin="anonymous"></script>
        <script src="js/scripts.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/jquery@3.6.0/dist/jquery.slim.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/js/bootstrap.bundle.min.js"></script>
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
    </form>
</body>
</html>
