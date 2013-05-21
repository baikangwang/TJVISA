<%@ Page Title="" Language="C#" MasterPageFile="~/Edit.Master" AutoEventWireup="true" CodeBehind="CollectionDetails.aspx.cs" Inherits="TJVISA.CollectionDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="detailsContent" runat="server">
    <div style="height: 400px;">
    <table class="details">
        <tr>
            <td><asp:Label ID="Label1" runat="server" Text="客户姓名"/></td>
            <td><asp:Label ID="lbCustomerName" runat="server"/></td>
        </tr>
        <tr>
            <td><asp:Label ID="Label2" runat="server" Text="收款金额"/></td>
            <td>
                <telerik:RadNumericTextBox ID="txtValue" runat="server"
                                           IncrementSettings-Step="1" IncrementSettings-InterceptMouseWheel="true" 
                                           NumberFormat-DecimalDigits="2"/>
                </td>
        </tr>
        <tr>
            <td><asp:Label ID="Label3" runat="server" Text="退补金额"/></td>
            <td>
                <telerik:RadNumericTextBox ID="txtOffset" runat="server"
                IncrementSettings-Step="1" IncrementSettings-InterceptMouseWheel="true" 
                                           NumberFormat-DecimalDigits="2"/>
                </td>
        </tr>
    </table>
</div></asp:Content>
