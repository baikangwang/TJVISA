<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="ContactMe.aspx.cs" Inherits="TJVISA.ContactMe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #content {
            width: 100%;
        }
        table {
            width: 50%;
            margin: 0 auto;
            font-size: 16px;
            table-layout: fixed;
        }
        table td {
            margin: 10px;
            
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <table>
        <tr><td>联系人：</td><td>李嘉</td></tr>
        <tr><td>电话：</td><td>13920120671</td></tr>
        <tr><td>邮箱：</td><td>zoom@sina.com</td></tr>
        <tr><td>地址：</td><td>天津科技大学</td></tr>
        <tr><td>邮编：</td><td>300000</td></tr>
    </table>
</asp:Content>
