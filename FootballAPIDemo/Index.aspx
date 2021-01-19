<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="FootballAPIDemo.Index" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/FootballAPIDemo.css" rel="stylesheet" />
</head>
<body >
    <form id="form1" runat="server">

            <div class="centerdiv">
                <table>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlTeams" runat="server" OnSelectedIndexChanged="ddlTeams_SelectedIndexChanged" AutoPostBack="true"/>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlLanguage" runat="server" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" AutoPostBack="true" />
                        </td>
                    </tr>
                </table>
            
            
            </div>

            <div>
                <asp:Image ID="imgTeamLogo" runat="server" CssClass="displayed"/>

            </div>
            <br />
            <div>
                <asp:Label ID="lblTeamDescription" runat="server" CssClass="container"/>
            </div>
            <br />
            <div class="center">
                <asp:LinkButton ID="lnkWebSite" runat="server" />
            </div>

    </form>
</body>
</html>
