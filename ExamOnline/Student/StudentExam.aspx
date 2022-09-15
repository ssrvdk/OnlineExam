<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Student.Master" AutoEventWireup="true" CodeBehind="StudentExam.aspx.cs" Inherits="ExamOnline.Student.StudentExam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main>
        <div class="container-fluid px-4">
            <h1 class="mt-4">Online Exam</h1>
            <ol class="breadcrumb mb-4">
                <li class="breadcrumb-item"><a href="StudentDashBoard.aspx">Dashboard</a></li>
                <li class="breadcrumb-item active">Online Exam</li>
            </ol>
            <div class="card mb-4">
                <div class="card-body">
                    DataTables is a third party plugin that is used to generate the demo table below. For more information about DataTables, please visit the
                               
                    <a target="_blank" href="https://datatables.net/">official DataTables documentation</a>
                    .
                           
                </div>
            </div>
            <div class="card mb-4">
                <div class="card-header">
                    <i class="fas fa-table me-1"></i>
                    List of Exams
                           
                </div>
                <div class="card-body">
                    <asp:Repeater ID="rptExam" runat="server">
                        <HeaderTemplate>
                            <table id="datatablesSimple">
                                <thead>
                                    <tr>
                                        <th>Exam Name</th>
                                        <th>Duration</th>
                                        <th>No of Section</th>
                                        <th>No Of Question</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("sName")%></td>
                                <td><%#Eval("sTime")%> Min.</td>
                                <td><%#Eval("NumberofSections")%></td>
                                <td><%#Eval("NumberofQuestions")%></td>
                                <td><a href="javascript:openExamForm(<%#Eval("QuestionPaperMasterId")%>)">Start</a></td>
                                
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                                   <tfoot>
                                       <tr>
                                           <th>Exam Name</th>
                                           <th>Duration</th>
                                           <th>No of Section</th>
                                           <th>No Of Question</th>
                                           <th>Action</th>
                                       </tr>
                                   </tfoot>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </main>
    <script>
        function openExamForm(examid) {
            window.open("OnlineExamPage.aspx");
        }
    </script>
</asp:Content>
