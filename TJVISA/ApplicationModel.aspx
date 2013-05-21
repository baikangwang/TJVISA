<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="ApplicationModel.aspx.cs" Inherits="TJVISA.ApplicationModel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .rpText {
             font-size: 20px;
             font-family: "Microsoft YaHei" !important;
             line-height: 50px;
             text-align: center !important;
             letter-spacing: 30px;
         }
         #content {
             width: 100%;
         }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <table>
        <tr>
            <td><h1>编号：<asp:Label ID="lbAppId" runat="server" Text="Label"/></h1></td>
            <td>
                <telerik:RadButton EnableSplitButton="true" ID="btStatus" AutoPostBack="false" runat="server" Text="[Status]" OnClientClicked="OnClientClicked"/>
                <telerik:RadContextMenu ID="menuProcessStatus" runat="server" OnClientItemClicked="OnClientItemClicked"/>
            </td>
        </tr>
    </table>
    <telerik:RadSplitter ID="RadSplitter1" runat="server" Width="100%">
        <telerik:RadPane ID="RadPane1" runat="server" Width="20%">
            <telerik:RadPanelBar
            runat="server" ID="RadPanelBar1" Height="100%" Width="100%"
            ExpandMode="FullExpandedItem" AllowCollapseAllItems="False" OnClientItemClicked="MenuClicked">
                <Items>
                    <telerik:RadPanelItem Text="申请表" Expanded="True" Value="details" Selected="True" />
                    <telerik:RadPanelItem Text="付款" Expanded="True" Value="collection"/>
                    <telerik:RadPanelItem Text="问题" Expanded="True" Value="case"/>
                    <telerik:RadPanelItem Text="返回" Expanded="True" Value="return"/>
                </Items>
            </telerik:RadPanelBar>
        </telerik:RadPane>
        <telerik:RadPane ID="contentPane" runat="server" Width="80%" Scrolling="None" MinHeight="400">
        </telerik:RadPane>
    </telerik:RadSplitter>
    <telerik:RadCodeBlock runat="server" ID="codeblock1">
    <script type="text/javascript" language="javascript">
        window.ItemId = '';
        
        function MenuClicked(sender, args) {
            var item = args.get_item();
            var value = item.get_value();
            switch (value) {
            case "details":
                SetContent('ApplicationDetails.aspx?itemId=' + window.ItemId);
                break;
            case "collection":
                SetContent('CollectionDetails.aspx?itemId=' + window.ItemId);
                break;
            case "case":
                SetContent('CaseDetails.aspx?itemId=' + window.ItemId);
                break;
            default:
                document.location = 'EntityList.aspx?entityName=Application';
                break;
            }
        }
        
        function SetAppId(appid) {
            window.ItemId = appid;
            // check if browser is IE
            if (appid == null)
                document.getElementById('<%= lbAppId.ClientID %>').innerHTML = "(未知)";
            else
                document.getElementById('<%= lbAppId.ClientID %>').innerHTML = appid;
            
            var ajaxMgr = $find('<%=AjaxManager.ClientID %>');

            if (ajaxMgr) {
                ajaxMgr.ajaxRequest('InitStatus|' + appid);
            }
        }
        
        function SetContent(url) {
            var contentPane = $find("<%=contentPane.ClientID%>");
            if (contentPane) {
                contentPane.set_contentUrl(url);
            }
        }

        function GetRadSplitter() {
            return $find("<%= RadSplitter1.ClientID %>");
        }

        function OnClientClicked(sender, args) {

            if (args.IsSplitButtonClick() || !sender.get_commandName()) {
                var currentLocation = $telerik.getLocation(sender.get_element());
                var contextMenu = $find("<%=menuProcessStatus.ClientID%>");
                contextMenu.showAt(currentLocation.x, currentLocation.y + 22);
            }
        }

        function OnClientItemClicked(sender, args) {
            var itemText = args.get_item().get_text();
            var ajaxMgr = $find('<%=AjaxManager.ClientID %>');

            if (ajaxMgr) {
                ajaxMgr.ajaxRequest('ChangeStatus|' + itemText);
            }
        }

        function RunOnce() {
            window.ItemId = '<%=ItemId %>';
            SetContent('ApplicationDetails.aspx?itemId=' + window.ItemId);
            Sys.Application.remove_load(RunOnce);
        }
        Sys.Application.add_load(RunOnce);
        
    </script>
    </telerik:RadCodeBlock>
</asp:Content>
