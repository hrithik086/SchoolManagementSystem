<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdmintMst.Master" AutoEventWireup="true" CodeBehind="TeacherSubject.aspx.cs" Inherits="SchoolManagementSystem.Admin.TeacherSubject" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="background-image:url('../Images/Color Pencils.jpg'); width: 100%; height: 720px; background-repeat: no-repeat; background-size: cover; background-attachment: fixed;">
        <div class="container p-md-4 p-sm-4">
            <div>
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </div>
            <h2 class="text-center">Add Teacher Subject</h2>

            <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">
                <div class="col md-6">
                    <label for="ddlClass">Class</label>
                    <asp:DropDownList ID="ddlClass" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Class is Required" 
                        ControlToValidate="ddlClass" Display="Dynamic" ForeColor="Red" InitialValue="Select Class" SetFocusOnError="true">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col md-6">
                    <label for="ddlSubject">Subject</label>
                    <asp:DropDownList ID="ddlSubject" runat="server" CssClass="form-control"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Subject is Required" 
                        ControlToValidate="ddlSubject" Display="Dynamic" ForeColor="Red" InitialValue="Select Subject" SetFocusOnError="true">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col md-6">
                    <label for="ddlTeacher">Teacher</label>
                    <asp:DropDownList ID="ddlTeacher" runat="server" CssClass="form-control"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Teacher is Required" 
                        ControlToValidate="ddlTeacher" Display="Dynamic" ForeColor="Red" InitialValue="Select Teacher" SetFocusOnError="true">
                    </asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="row mb-3 mr-lg-5 ml-lg-5>
                <div class="col-md-3 col-md-offset-2 mb-3">
                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-block" BackColor="#5558C9" Text="Add Subject" Width="745px" OnClick="btnAdd_Click" />
                </div>
            </div> 

            <div class="row mb-3 mr-lg-5 ml-lg-5>
                <div class="col-md-6" style="margin-left: 50%;">
                    <asp:GridView ID="GridView1" runat="server" DataKeyNames="Id" AutoGenerateColumns="False" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating">
                        <Columns>
                            <asp:TemplateField HeaderText="ClassId">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtClass" runat="server" Text='<%# Eval("ClassId") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("ClassId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SubjectId">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtSubject" runat="server" Text='<%# Eval("SubjectId") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("SubjectId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TeacherId">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtTeacher" runat="server" Text='<%# Eval("TeacherId") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("TeacherId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField HeaderText="Operations" ShowEditButton="True" CausesValidation="False" />
                        </Columns>
                        <HeaderStyle BackColor="#5558C9" ForeColor="White" />
                    </asp:GridView>
                </div>
            </div>




        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
