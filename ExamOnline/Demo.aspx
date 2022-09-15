<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Demo.aspx.cs" Inherits="ExamOnline.Demo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        <h1>
                            <asp:Label ID="lblQuestion" runat="server" /></h1>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:RadioButtonList ID="rbtnOptions" runat="server">
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnNext" Text="Next" runat="server" OnClick="OnNext" Style="float: left;" />
                        <asp:Button ID="btnPrevious" Text="Previous" runat="server" OnClick="OnPrevious" Style="float: right;" />
                        <asp:HiddenField ID="hdnQuestionId" runat="server"  />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
