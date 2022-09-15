<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistrationAdmin.aspx.cs" Inherits="ExamOnline.RegistrationAdmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Register</title>

    <!-- Custom fonts for this template-->
    <link href="vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css">
    <link
        href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i"
        rel="stylesheet">

    <!-- Custom styles for this template-->
    <link href="css/sb-admin-2.min.css" rel="stylesheet">
</head>
<body class="bg-gradient-primary">
    <form id="form1" runat="server">
        <div class="container">
            <div class="card o-hidden border-0 shadow-lg my-5">
                <div class="card-body p-0">
                    <!-- Nested Row within Card Body -->
                    <div class="row">
                        <div class="col-lg-5 d-none d-lg-block bg-register-image"></div>
                        <div class="col-lg-7">
                            <div class="p-5">
                                <div class="text-center">
                                    <h1 class="h4 text-gray-900 mb-4">Create an Account!</h1>
                                </div>
                                <div class="form-group row">
                                    <div class="col-sm-6 mb-3 mb-sm-0">
                                        <asp:TextBox ID="txtName" runat="server" class="form-control" type="text" placeholder="Name"></asp:TextBox>
                                       <%-- <label for="inputFirstName">Name</label>--%>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtUsername" runat="server" class="form-control" type="text" placeholder="Username"></asp:TextBox>
                                        <%--<label for="inputLastName">Username</label>--%>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox ID="txtEmail" runat="server" class="form-control" type="Email" placeholder="Email address"></asp:TextBox>
                                    <%--<label for="inputEmail">Email address</label>--%>
                                </div>
                                <div class="form-group row">
                                    <div class="col-sm-6 mb-3 mb-sm-0">
                                        <asp:TextBox ID="txtPassword" runat="server" class="form-control" type="password" placeholder="Password"></asp:TextBox>
                                        <%--<label for="inputPassword">Create a password</label>--%>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtConfirmPassword" runat="server" class="form-control" type="password" placeholder="Confirm Password"></asp:TextBox>
                                        <%--<label for="inputPasswordConfirm">Confirm Password</label>--%>
                                    </div>
                                </div>
                                <div class="mt-4 mb-0">
                                    <div class="d-grid">
                                        <asp:Button ID="btnSubmit" runat="server" class="btn btn-primary btn-block" Text="Create Account" OnClick="btnSubmit_Click" />
                                    </div>
                                </div>

                                <div class="text-center">
                                    <a class="small" href="ForgotPassword.aspx">Forgot Password?</a>
                                </div>
                                <div class="text-center">
                                    <a class="small" href="Index.aspx">Have an account? Go to login</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </div>
            </div>
        </div>

       <%-- <div id="layoutAuthentication_footer">
            <footer class="py-4 bg-light mt-auto">
                <div class="container-fluid px-4">
                    <div class="d-flex align-items-center justify-content-between small">
                        <div class="text-muted">Copyright &copy; SRV Software Solutions 2022</div>
                        <div>
                            <a href="#">Privacy Policy</a>
                            &middot;
                               
                                <a href="#">Terms &amp; Conditions</a>
                        </div>
                    </div>
                </div>
            </footer>
        </div>--%>

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
        <!-- Bootstrap core JavaScript-->
        <script src="vendor/jquery/jquery.min.js"></script>
        <script src="vendor/bootstrap/js/bootstrap.bundle.min.js"></script>

        <!-- Core plugin JavaScript-->
        <script src="vendor/jquery-easing/jquery.easing.min.js"></script>

        <!-- Custom scripts for all pages-->
        <script src="js/sb-admin-2.min.js"></script>
        <script type="text/javascript">
            function ShowMessageForm() {
                $('#MessageModel').modal('show');
            }
        </script>
    </form>
</body>
</html>
