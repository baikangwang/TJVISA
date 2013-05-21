<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TJVISA.Default" %>
<%@ Import Namespace="TJVISA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>天津对外服务公司签证、认证业务管理系统</title>
    <meta name="description" content="website description" />
    <meta name="keywords" content="website keywords, website keywords" />
    <meta http-equiv="content-type" content="text/html; charset=UTF-8" />
    <link href="css/style.css" rel="stylesheet" type="text/css" media="screen" />
    <style type="text/css">
        .RadSplitter, .RadSplitter .rspSlideZone, .RadSplitter .rspSlideContainer, .RadSplitter .rspPaneTabContainer, .RadSplitter .rspPane, .RadSplitter .rspResizeBar, .RadSplitter .rspSlideContainerResize, .RadSplitter .rspPaneHorizontal, .RadSplitter .rspResizeBar
        {
            border: none !important;
        }
        
        .RadMenu {
	        height: 55px;
	        margin: 0 auto;
	        padding: 0;
        }
        .RadMenu .rmVertical {
	        margin: 0;
	        padding: 30px 0px 0px 70px;
        }
        
        .RadMenu .rmItem {
            background: none !important;
            height: 55px !important;
            line-height: 55px !important;
        }
        
        .RadMenu .rmLink {
            background: none !important;
	        display: block;
	        margin-right: 1px;
	        border: none;
        }
        
        .RadMenu .rmText {
	        text-decoration: none;
	        text-align: center;
	        text-transform: uppercase;
	        /*font-family: "Microsoft YaHei",Verdana, Arial, Helvetica, sans-serif;*/
	        font-size: 18px;
	        font-weight: bold;
            line-height: 55px !important;
            background: none !important;
        }
        
        #userInfo {
	        margin: 0;
	        padding: 30px 0px 0px 70px;
	        float: left;
	        list-style: none;
	        line-height: normal;
        }
        
        .RadWindow .rwDialogText  {
            font-family: "Microsoft YaHei" !important;
            font-size: 14px;
            margin: 10px 0;
        }
        .mainContent {
            overflow: hidden;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"/>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="OnAjaxRequest"/>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server"
    Localization-Cancel="取消" Localization-Close="关闭" Localization-Maximize="最大化"
    Localization-Minimize="最小化" Localization-No="否" Localization-OK="确定"
    Localization-PinOff="解锁" Localization-PinOn="锁定" Localization-Reload="重载"
    Localization-Restore="恢复" Localization-Yes="是">
        <Windows>
            <telerik:RadWindow ID="winLogin" runat="server" Width="350" Height="200" AutoSize="False" Style="display: none;" Title="<%=GlobalConstant.ProductName %>">
                <ContentTemplate>
                    <table style="width: 300px; margin-top:30px; table-layout: fixed; padding: 0px; border: none; border-collapse: collapse;">
                        <tr>
                            <td style="text-align: right; border-collapse: collapse;" colspan="2">
                                用户名：<telerik:RadTextBox ID="txtName" runat="server"/>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; border-collapse: collapse;" colspan="2">
                                密码：<telerik:RadTextBox ID="txtPassword" TextMode="Password" runat="server"/>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right;padding-top:10px;">
                                <telerik:RadButton runat="server" ID="btLogin" Text="登录" AutoPostBack="false" OnClientClicked="Login" />
                            </td>
                            <td style="text-align: center;padding-top:10px;">
                                <telerik:RadButton ID="btCancel" runat="server" Text="取消" AutoPostBack="false" OnClientClicked="CloseLoginDialog" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <div id="wrapper">
        <div id="header-wrapper">
            <div id="topmenu">
                <p>
                    THE BUSINESS MANAGEMENT OF TIANJIN FOREIGN SERVICE CORP</p>
            </div>
            <div id="header">
                <div id="userInfo">
                    <telerik:RadMenu ID="RadMenu1" runat="server" OnClientItemClicked="MenuItemClicked">
                        <Items>
                            <telerik:RadMenuItem runat="server" Text="[进入管理]" Value="">
                                <Items>
                                    <telerik:RadMenuItem runat="server" Text="登录" Value="Login">
                                    </telerik:RadMenuItem>
                                </Items>
                            </telerik:RadMenuItem>
                        </Items>
                    </telerik:RadMenu>
                </div>    
                <div id="menu">
                    <ul>
                        <li class="current_page_item">
                            <asp:LinkButton ID="lbHome" runat="server" OnClientClick="SetContent(this,'Home.aspx');return false;" Text="首页" /></li>
                        <li>
                            <asp:LinkButton ID="lbApplications" runat="server" OnClientClick="SetContent(this,'EntityList.aspx','Application');return false;" Text="申请表管理" /></li>
                        <li>
                            <asp:LinkButton ID="lbUsers" runat="server" OnClientClick="SetContent(this,'EntityList.aspx','User'); return false;" Text="用户管理" /></li>
                        <li>
                            <asp:LinkButton ID="lbContactMe" runat="server" OnClientClick="SetContent(this,'ContactMe.aspx');return false;" Text="联系我们" /></li>
                    </ul>
                </div>
                <div id="logo">
                    <h1>
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="SetContent(null,'Home.aspx');return false;"
                            Text="天津对外服务公司签证、认证业务管理系统" />
                    </h1>
                </div>
            </div>
        </div>
        <!-- end #header -->
        <div id="page">
            <div id="page-bgtop">
                <div id="page-bgbtm">
                    <telerik:RadSplitter ID="RadSplitter1" runat="server" Width="100%" CssClass="mainContent">
                        <telerik:RadPane ID="contentPane" runat="server" MinHeight="400"
                            Scrolling="None" ContentUrl="Home.aspx">
                        </telerik:RadPane>
                    </telerik:RadSplitter>
                    <div style="clear: both;">
                        &nbsp;</div>
                </div>
            </div>
        </div>
    </div>
    <div id="footer">
        <p>
            版权 (c) 李嘉. | <a href="" target="_blank">zoom@sina.com</a></p>
    </div>
    <!-- end #footer -->
    <!-- javascript at the bottom for fast page loading -->
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
    <script type="text/javascript">

        function GetRadSplitter() {
            return $find("<%= RadSplitter1.ClientID %>");
        }

        function SetContent(sender, url) {

            if(sender!=null) {
                var menu = $get('menu');
                if (menu) {
                    var items = menu.getElementsByTagName('li');
                    for (var i = 0; i < items.length; i++) {

                        items[i].setAttribute("class", "");
                    }
                    var parent = sender.parentElement;
                    while (parent.tagName != "LI") {
                        parent = parent.parentElement;
                    }
                    parent.setAttribute("class", "current_page_item");
                }
            }

            var contentPane = $find("<%=contentPane.ClientID%>");
            if (contentPane) {
                if (arguments[2])
                    url += "?entityName=" + arguments[2];
                contentPane.set_contentUrl(url);
            }
        }

        function MenuItemClicked(sender, args) {
            var item = args.get_item();
            if (item) {
                var value = item.get_value();
                switch (value) {
                case "Login":
                    ShowLoginDialog(sender, null);
                    break;
                case "Logout":
                    var msg = "确定要登出吗？";
                    radconfirm(msg, Logout,400,100, sender, "<%=GlobalConstant.ProductName %>");
                    break;
                default:
                    break;
                }
            }
        }

        function Logout(args) {

            if (args) {
                var ajaxManager = $find("<%=AjaxManager.ClientID %>");
                ajaxManager.ajaxRequest("Logout");
                ChangeUserInfo(null);
                SetContent($get("<%=lbHome.ClientID %>"),"Home.aspx");
            }
        }

        function ShowLoginDialog(sender, args) {
            var winLogin = $find("<%= winLogin.ClientID %>");
            winLogin.show();
        }

        function CloseLoginDialog(sender, args) {
            var winLogin = $find("<%= winLogin.ClientID %>");
            winLogin.hide();
        }

        function Login() {
            var name = $find("<%=txtName.ClientID %>").get_textBoxValue();
            var password = $find("<%=txtPassword.ClientID %>").get_textBoxValue();
            var ajaxManager = $find("<%=AjaxManager.ClientID %>");
            var args = "Login|" + name + "|" + password;
            ajaxManager.ajaxRequest(args);
        }

        function CleanUpDialogInput() {
            $find("<%=txtName.ClientID %>").set_value('');
            $find("<%=txtPassword.ClientID %>").set_value('');
        }

        function ChangeUserInfo(name) {
            var menu = $find('<%=RadMenu1.ClientID %>');
            menu.trackChanges();
            var root = menu.get_items().getItem(0);
            var child = root.get_items().getItem(0);
            if (name == null || typeof (name) == "undefined") {
                root.set_text("进入管理");
                child.set_text("登录");
                child.set_value("Login");
            }
            else {
                root.set_text("你好！" + name);
                child.set_text("登出");
                child.set_value("Logout");
            }
            menu.commitChanges();
        }
    </script>
    </telerik:RadCodeBlock>
    </form>
</body>
</html>
