<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OnlineExamPage.aspx.cs" Inherits="ExamOnline.Student.OnlineExamPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .sub-header {
            background: #363636;
            color: #f7f64e;
            font-family: Arial,Helvetica,sans-serif;
            height: 30px;
            line-height: 30px;
            padding: 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Repeater ID="rptExamPage" runat="server">
                <HeaderTemplate>
                    <table style="width: 100%">
                        <thead>
                            <tr class="sub-header">
                                <td>Test name</td>
                                <td>View Instruction</td>
                            </tr>
                            <tr>
                                <td>Sections</td>
                                <td>Time Left</td>
                                <td>
                                    <img />
                                    Student Name</td>
                            </tr>
                        </thead>
                        <tbody></tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>Section 1  
                        Sestion 2
                        Section 3
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblQuestion" runat="server" ClientIDMode="Static" Text='<%#Eval("Question")%>'></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButton ID="RadioButton1" runat="server" Text='<%#Eval("Option1")%>' />
                        </td>
                        <td>
                            <asp:RadioButton ID="RadioButton2" runat="server" Text='<%#Eval("Option2")%>' />
                        </td>
                        <td>
                            <asp:RadioButton ID="RadioButton3" runat="server" Text='<%#Eval("Option3")%>' />
                        </td>
                        <td>
                            <asp:RadioButton ID="RadioButton4" runat="server" Text='<%#Eval("Option4")%>' />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                                 <%--  <tfoot>
                                       <tr>
                                           <th>Exam Name</th>
                                           <th>Duration</th>
                                           <th>No of Section</th>
                                           <th>No Of Question</th>
                                           <th>Action</th>
                                       </tr>
                                   </tfoot>--%>
                    </table>
                </FooterTemplate>
            </asp:Repeater>

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
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
