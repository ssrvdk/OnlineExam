<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="ExamOnline.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Login</title>

    <!-- Custom fonts for this template-->
    <link href="vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css">
    <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet">

    <!-- Custom styles for this template-->
    <link href="css/sb-admin-2.min.css" rel="stylesheet">
    <link rel="stylesheet" href="plugins/jquery-toast/dist/jquery.toast.min.css">
</head>

<body class="bg-gradient-primary">
    <form id="form1" runat="server">
        <asp:HiddenField ID="hdMessage" runat="server" ClientIDMode="Static" />
        <div class="container">

            <!-- Outer Row -->
            <div class="row justify-content-center">
                <div class="col-xl-10 col-lg-12 col-md-9">
                    <div class="card o-hidden border-0 shadow-lg my-5">
                        <div class="card-body p-0">
                            <!-- Nested Row within Card Body -->
                            <div class="row" style="min-height: 350px;">
                                <div class="col-lg-8 d-none d-lg-block bg-login-image"></div>
                                <div class="col-lg-4">
                                    <div class="p-5">
                                        <div class="text-center">
                                            <h1 class="h4 text-gray-900 mb-4">Welcome Back!</h1>
                                        </div>
                                        <div class="user">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtUserName" required runat="server" type="email" class="form-control form-control-user" aria-describedby="emailHelp" placeholder="Enter Email Address..."></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:TextBox ID="txtpassword" required runat="server" type="password" class="form-control form-control-user" placeholder="Password"></asp:TextBox>
                                            </div>
                                            <asp:Button ID="btnLogin" runat="server" Text="Login" class="btn btn-primary btn-user btn-block" OnClick="btnLogin_Click" />
                                        </div>
                                        <hr>
                                        <div class="text-center">
                                            <a class="small" href="ForgotPassword.aspx">Forgot Password?</a>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2"></div>
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
                            <asp:Label ID="lblMessage" runat="server"></asp:Label></p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnOk" runat="server" Text="OK" class="btn" OnClick="btnOk_Click" />
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
        <!-- notifications -->
        <script src="plugins/jquery-toast/dist/jquery.toast.min.js"></script>
        <script src="plugins/jquery-toast/dist/toast.js"></script>
        <script type="text/javascript">
            function ShowMessageForm() {
                $('#MessageModel').modal('show');
            }

            $(document).ready(function () {
                /* remove the relative spam involving inputs disabled */
                $('input[type="checkbox"]').parent('.custom-control-input').each(function () {
                    var $this = $(this);
                    var cssClass = $this.attr('class');
                    $this.children('input[type="checkbox"]').addClass(cssClass).unwrap().parent('label[for],span').first().addClass('custom-control-input');
                });
            });


            function Successmsg() {
                var str = $('#hdMessage').val();
                var msgHead = str.split("|")[0];
                var msg = str.split("|")[1];
                $.toast({
                    heading: msgHead,
                    text: msg,
                    position: 'top-center',
                    loaderBg: '#ff6849',
                    icon: 'success',
                    hideAfter: 3500,
                    stack: 6
                });

            }

            function Errormsg() {
                var str = $('#hdMessage').val();
                var msgHead = str.split("|")[0];
                var msg = str.split("|")[1];
                $.toast({
                    heading: msgHead,
                    text: msg,
                    position: 'top-center',
                    loaderBg: '#ff6849',
                    icon: 'error',
                    hideAfter: 3500
                });
            }

        </script>

    </form>


</body>
</html>
