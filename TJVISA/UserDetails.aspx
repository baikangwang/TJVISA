<%@ Page Title="" Language="C#" MasterPageFile="~/Edit.Master" AutoEventWireup="true" CodeBehind="UserDetails.aspx.cs" Inherits="TJVISA.UserDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="detailsContent" runat="server">
    <table class="details">
        <tr><td>
            <asp:Label ID="lblName" runat="server" Text="员工名"></asp:Label></td><td>
                <telerik:RadTextBox ID="txtUserName" runat="server">
                </telerik:RadTextBox></td></tr>
        <tr><td>
            <asp:Label ID="lblPassword" runat="server" Text="密码"></asp:Label></td><td>
                <telerik:RadTextBox ID="txtPassword" runat="server">
                </telerik:RadTextBox></td></tr>
        <tr><td>
            <asp:Label ID="lblRole" runat="server" Text="角色"></asp:Label></td>
            <td>
                <telerik:RadComboBox ID="cmbRole" runat="server">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Selected="True" Text="前台人员" Value="2" />
                        <telerik:RadComboBoxItem runat="server" Text="业务人员" Value="1" />
                        <telerik:RadComboBoxItem runat="server" Text="管理员" Value="3" />
                    </Items>
                    
                </telerik:RadComboBox>
            </td></tr>
    </table>
</asp:Content>
