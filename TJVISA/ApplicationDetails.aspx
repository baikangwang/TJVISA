<%@ Page Language="C#" MasterPageFile="~/Edit.Master" AutoEventWireup="true" CodeBehind="ApplicationDetails.aspx.cs" Inherits="TJVISA.ApplicationDetails"%>
<asp:Content ID="Content1" ContentPlaceHolderID="detailsContent" runat="server">
    <table class="details">
        <tr>
            <td style="width: 120px;"><asp:Label ID="Label1" runat="server" Text="业务类别"/></td>
            <td style="width: 250px;">
                <telerik:RadComboBox ID="cmbAppType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AppType_SelectedIndexChanged">
                    <Items>
                        <telerik:RadComboBoxItem Text="签证" Value="1" Selected="true" />
                        <telerik:RadComboBoxItem Text="认证" Value="2" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td style="width: 120px;"><asp:Label ID="Label2" runat="server" Text="数量"/></td>
            <td><telerik:RadComboBox ID="cmbSubType" runat="server"/>
                </td>
        </tr>
        <tr>
            <td colspan="4" style="color: black; font-weight: bold;">客户信息</td>
        </tr>
        <tr>
            <td colspan="4">
            <table class="details">
            <tr>
            <td style="width: 120px;"><asp:Label ID="Label3" runat="server" Text="客户身份证号"/></td>
            <td colspan="3">
                <telerik:RadTextBox ID="txtCustomerID" Width="100%" BackColor="yellow" BorderStyle="None" BorderWidth="0" runat="server"/>
                </td>
            </tr>
            <tr>
            <td><asp:Label ID="Label7" runat="server" Text="客户姓名"/></td>
            <td style="width: 250px;">
                <telerik:RadTextBox ID="txtCustomerName" runat="server"/>
                </td>
            <td style="width: 120px;"><asp:Label ID="Label9" runat="server" Text="客户性别"/></td>
            <td >
                <telerik:RadComboBox ID="cmbCustomerSex" runat="server">
                    <Items>
                        <telerik:RadComboBoxItem Text="男" Value="男" Selected="true" />
                        <telerik:RadComboBoxItem Text="女" Value="女" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            </tr>
            <tr>
            <td><asp:Label ID="Label11" runat="server" Text="联系电话"/></td>
            <td >
                <telerik:RadTextBox ID="txtCustomerPhone" runat="server" Width="100%" BorderStyle="None" BorderWidth="0"/>
                </td>
            <td><asp:Label ID="Label13" runat="server" Text="收件地址"/></td>
            <td >
                <telerik:RadTextBox ID="txtCustomerAddress" runat="server" Width="100%" BorderStyle="None" BorderWidth="0"/>
                </td>
            </tr>
            </table>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="Label4" runat="server" Text="申请国家"/></td>
            <td colspan="3">
                <telerik:RadComboBox ID="cmbRegion" runat="server">
                    <Items>
                        <telerik:RadComboBoxItem Text="美国" Value="美国" Selected="true" />
                        <telerik:RadComboBoxItem Text="加拿大" Value="加拿大" />
                        <telerik:RadComboBoxItem Text="澳大利亚" Value="澳大利亚" />
                        <telerik:RadComboBoxItem Text="新加坡" Value="新加坡" />
                        <telerik:RadComboBoxItem Text="日本" Value="日本"  />
                        <telerik:RadComboBoxItem Text="韩国" Value="韩国" />
                        <telerik:RadComboBoxItem Text="马来西亚" Value="马来西亚"  />
                        <telerik:RadComboBoxItem Text="法国" Value="法国" />
                        <telerik:RadComboBoxItem Text="德国" Value="德国"  />
                        <telerik:RadComboBoxItem Text="英国" Value="英国"  />
                    </Items>
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="Label5" runat="server" Text="申请日期"/></td>
            <td>
                <telerik:RadDatePicker ID="dpkAppliedDate" runat="server">
                </telerik:RadDatePicker>
            </td>
            <td><asp:Label ID="Label16" runat="server" Text="启程日期"/></td>
            <td>
                <telerik:RadDatePicker ID="dpkTravedDate" runat="server">
                </telerik:RadDatePicker>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="Label6" runat="server" Text="照会号"/></td>
            <td>
                <telerik:RadTextBox ID="txtOffNoteNo" runat="server">
                </telerik:RadTextBox></td>
            <td><asp:Label ID="Label19" runat="server" Text="照会日期"/></td>
            <td>
                <telerik:RadDatePicker ID="dpkOffNoteDate" runat="server">
                </telerik:RadDatePicker>
            </td>
        </tr>
        <tr>
            <td colspan="4">材料说明</td>
        </tr>
        <tr>
            <td colspan="4">
                <telerik:RadEditor ID="txtRemark" runat="server" 
                Width="100%" BorderWidth="0" BorderStyle="None" EnableResize="false" AutoResizeHeight="True"/>
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
