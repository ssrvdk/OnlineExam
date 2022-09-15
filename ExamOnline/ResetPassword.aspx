<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="ExamOnline.ResetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Reset Password</title>

    <!-- Custom fonts for this template-->
    <link href="vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css">
    <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet">

    <!-- Custom styles for this template-->
    <link href="css/sb-admin-2.min.css" rel="stylesheet">
</head>
<body class="bg-primary">
    <form id="form1" runat="server">
        <div class="container">
            <div class="card o-hidden border-0 shadow-lg my-5">
                <div class="card-body p-0">
                    <!-- Nested Row within Card Body -->
                    <div class="row" style="min-height: 453px;">
                        <div class="col-lg-8 d-none d-lg-block bg-register-image"></div>
                        <div class="col-lg-4">
                            <div class="p-5">
                                <div class="text-center">
                                    <h1 class="h4 text-gray-900 mb-4">Reset Password</h1>
                                </div>
                                <div class="user">
                                    <div class="form-group row">
                                        <div class="col-sm-6 mb-3 mb-sm-0">
                                            <asp:TextBox ID="txtpassword" runat="server" class="form-control" type="password" placeholder="Password"></asp:TextBox>
                                            <%--<label for="inputPassword">Password</label>--%>
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtRepeatPassword" runat="server" class="form-control" type="password" placeholder="Repeat Password"></asp:TextBox>
                                            <%--<label for="inputPassword">Repeat Password</label>--%>
                                        </div>
                                    </div>
                                    <asp:Button ID="btnSubmit" runat="server" class="btn btn-primary" Text="Submit" OnClick="btnSubmit_Click" />
                                </div>
                                <hr>
                                <div class="text-center">
                                    <a class="small" href="RegistrationAdmin.aspx">Create an Account!</a>
                                </div>
                                <div class="text-center">
                                    <a class="small" href="Index.aspx">Already have an account? Login!</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
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
