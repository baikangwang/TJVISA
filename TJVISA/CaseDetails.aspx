<%@ Page Title="" Language="C#" MasterPageFile="~/Edit.Master" AutoEventWireup="true" CodeBehind="CaseDetails.aspx.cs" Inherits="TJVISA.CaseDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="detailsContent" runat="server">
    <table class="details">
        <tr>
            <td><asp:Label ID="Label1" runat="server" Text="业务类别"/></td>
            <td><asp:Label ID="lbAppType" runat="server"/></td>
        </tr>
        <tr>
            <td><asp:Label ID="Label2" runat="server" Text="客户姓名"/></td>
            <td><asp:Label ID="lbCustomerName" runat="server"/></td>
        </tr>
        <tr>
            <td><asp:Label ID="Label3" runat="server" Text="客户身份证号"/></td>
            <td><asp:Label ID="lbCustomerID" runat="server"/></td>
        </tr>
        <tr>
            <td><asp:Label ID="Label4" runat="server" Text="申请国家"/></td>
            <td><asp:Label ID="lbRegion" runat="server"/></td>
        </tr>
        <tr>
            <td><asp:Label ID="Label5" runat="server" Text="问题案类型"/></td>
            <td>
                <telerik:RadComboBox ID="cmbType" runat="server" Font-Names="Microsoft YaHei" Font-Size="18px">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Selected="True" Text="客户信息不正确" Value="客户信息不正确"/>
                        <telerik:RadComboBoxItem runat="server" Text="材料信息不完整" Value="材料信息不完整"/>
                        <telerik:RadComboBoxItem runat="server" Text="申请时间逾期" Value="申请时间逾期"/>
                        <telerik:RadComboBoxItem runat="server" Text="申请国家无效" Value="申请国家无效"/>
                    </Items>
                </telerik:RadComboBox>
                </td>
        </tr>
        <tr><td colspan="2"><asp:Label ID="Label6" runat="server" Text="问题案信息"/></td></tr>
        <tr>
            <td colspan="2">
                <telerik:RadEditor ID="txtValue" runat="server" Width="100%" 
                BorderWidth="0" BorderStyle="None" EnableResize="false" AutoResizeHeight="True"
                Font-Names="Microsoft YaHei" Font-Size="18px"/>
            </td>
        </tr>
    </table>
    <script type="text/javascript" language="javascript">
        function ResetAppId(appid) {
            var parent = window.parent;
            parent.document.location = "ApplicationModel.aspx?itemId=" + appid;
        }
        
    </script>
</asp:Content>
