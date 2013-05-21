<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true"
    CodeBehind="Home.aspx.cs" Inherits="TJVISA.Home" %>
<%@ Import Namespace="TJVISA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
         #content {
             width: 100%;
         }
         body {
             background: url('images/home.png') no-repeat;
             background-size: 100% 400px;
         }
   </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <div style="position: relative;float: right; font-size: 16px;">
        <fieldset style="padding: 10px; ">
            <legend><h3 style="font-weight: bold;color: #fff;">速查通道</h3></legend>
            <table>
                <tr>
                    <td style="color: #fff;">身份证号：</td>
                    <td><telerik:RadTextBox ID="txtCustomerId" runat="server"/></td>
                </tr>
                <tr>
                    <td style="color: #fff;">申请表编号：</td>
                    <td><telerik:RadTextBox ID="txtAppId" runat="server"/></td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: right;">
                        <telerik:RadButton ID="btSearch" runat="server" AutoPostBack="false" OnClientClicked="RunSearch" Text="查询"/>
                    </td>
                </tr>
            </table>
        </fieldset>
        <fieldset id="result" style="visibility: hidden;padding: 10px;">
            <legend><h3 style="font-weight: bold;color: #fff;">查询结果</h3></legend>
            <div id="resultContent" style="color: #000; background-color: lightgray; margin: 10px; padding: 2px;"></div>
        </fieldset>
    </div>
    <script language="javascript" type="text/javascript">
        function ShowResult(result) {
            var content = $get('resultContent');
            if(content) {
                if (result == null) {
                    content.innerHTML = "<h4 style='color:red;'>未找到，请重新确认申请表编号或者身份证号。</h4>";
                }
                else {
                    var args = result.split("|");
                    var appid = args[0];
                    var appDate = args[1];
                    var status = args[2];
                    content.innerHTML = "编号：" + appid + '<br/>';
                    content.innerHTML += "申请日期：" + appDate + '<br/>';
                    content.innerHTML += "处理状态：<b>" + status + '</b><br/>';
                }

                var resultPanel = $get('result');
                if (resultPanel) {
                    resultPanel.style.visibility = "visible";
                }
            }
        }

        function RunSearch(sender, args) {
            var ajax = $find('<%=AjaxManager.ClientID %>');
            var appId = $find('<%=txtAppId.ClientID %>').get_textBoxValue();
            var customerId = $find('<%=txtCustomerId.ClientID %>').get_textBoxValue();
            if (appId && customerId) {
                if (ajax) {
                    var arg = 'RunSearch|' + appId + '|' + customerId;
                    ajax.ajaxRequest(arg);
                }
            } else {
                radalert('请填写身份证号和申请表编号。',400,100,'<%=GlobalConstant.ProductName %>');
            }
        }
    </script>
</asp:Content>