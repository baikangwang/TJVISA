<%@ Page Language="C#" MasterPageFile="~/Edit.Master" AutoEventWireup="true" CodeBehind="ApplicationDetails.aspx.cs" Inherits="TJVISA.ApplicationDetails"%>
<asp:Content ID="Content1" ContentPlaceHolderID="detailsContent" runat="server">
    <table class="details">
        <tr>
            <td style="width: 120px;"><asp:Label ID="Label1" runat="server" Text="ҵ�����"/></td>
            <td style="width: 250px;">
                <telerik:RadComboBox ID="cmbAppType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AppType_SelectedIndexChanged">
                    <Items>
                        <telerik:RadComboBoxItem Text="ǩ֤" Value="1" Selected="true" />
                        <telerik:RadComboBoxItem Text="��֤" Value="2" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td style="width: 120px;"><asp:Label ID="Label2" runat="server" Text="����"/></td>
            <td><telerik:RadComboBox ID="cmbSubType" runat="server"/>
                </td>
        </tr>
        <tr>
            <td colspan="4" style="color: black; font-weight: bold;">�ͻ���Ϣ</td>
        </tr>
        <tr>
            <td colspan="4">
            <table class="details">
            <tr>
            <td style="width: 120px;"><asp:Label ID="Label3" runat="server" Text="�ͻ����֤��"/></td>
            <td colspan="3">
                <telerik:RadTextBox ID="txtCustomerID" Width="100%" BackColor="yellow" BorderStyle="None" BorderWidth="0" runat="server"/>
                </td>
            </tr>
            <tr>
            <td><asp:Label ID="Label7" runat="server" Text="�ͻ�����"/></td>
            <td style="width: 250px;">
                <telerik:RadTextBox ID="txtCustomerName" runat="server"/>
                </td>
            <td style="width: 120px;"><asp:Label ID="Label9" runat="server" Text="�ͻ��Ա�"/></td>
            <td >
                <telerik:RadComboBox ID="cmbCustomerSex" runat="server">
                    <Items>
                        <telerik:RadComboBoxItem Text="��" Value="��" Selected="true" />
                        <telerik:RadComboBoxItem Text="Ů" Value="Ů" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            </tr>
            <tr>
            <td><asp:Label ID="Label11" runat="server" Text="��ϵ�绰"/></td>
            <td >
                <telerik:RadTextBox ID="txtCustomerPhone" runat="server" Width="100%" BorderStyle="None" BorderWidth="0"/>
                </td>
            <td><asp:Label ID="Label13" runat="server" Text="�ռ���ַ"/></td>
            <td >
                <telerik:RadTextBox ID="txtCustomerAddress" runat="server" Width="100%" BorderStyle="None" BorderWidth="0"/>
                </td>
            </tr>
            </table>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="Label4" runat="server" Text="�������"/></td>
            <td colspan="3">
                <telerik:RadComboBox ID="cmbRegion" runat="server">
                    <Items>
                        <telerik:RadComboBoxItem Text="����" Value="����" Selected="true" />
                        <telerik:RadComboBoxItem Text="���ô�" Value="���ô�" />
                        <telerik:RadComboBoxItem Text="�Ĵ�����" Value="�Ĵ�����" />
                        <telerik:RadComboBoxItem Text="�¼���" Value="�¼���" />
                        <telerik:RadComboBoxItem Text="�ձ�" Value="�ձ�"  />
                        <telerik:RadComboBoxItem Text="����" Value="����" />
                        <telerik:RadComboBoxItem Text="��������" Value="��������"  />
                        <telerik:RadComboBoxItem Text="����" Value="����" />
                        <telerik:RadComboBoxItem Text="�¹�" Value="�¹�"  />
                        <telerik:RadComboBoxItem Text="Ӣ��" Value="Ӣ��"  />
                    </Items>
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="Label5" runat="server" Text="��������"/></td>
            <td>
                <telerik:RadDatePicker ID="dpkAppliedDate" runat="server">
                </telerik:RadDatePicker>
            </td>
            <td><asp:Label ID="Label16" runat="server" Text="��������"/></td>
            <td>
                <telerik:RadDatePicker ID="dpkTravedDate" runat="server">
                </telerik:RadDatePicker>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="Label6" runat="server" Text="�ջ��"/></td>
            <td>
                <telerik:RadTextBox ID="txtOffNoteNo" runat="server">
                </telerik:RadTextBox></td>
            <td><asp:Label ID="Label19" runat="server" Text="�ջ�����"/></td>
            <td>
                <telerik:RadDatePicker ID="dpkOffNoteDate" runat="server">
                </telerik:RadDatePicker>
            </td>
        </tr>
        <tr>
            <td colspan="4">����˵��</td>
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
