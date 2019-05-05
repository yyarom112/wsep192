<%@ Page Title="index Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WebServices.Views.Pages.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="bg0 p-t-75 p-b-32" style="margin-left: auto; margin-right: auto; max-width: 100%;">

        <div class="container" style="max-width: 100%;">
            <div class="row centerElem" style="max-width: 100%;">
                 <img src="images/LOGO.jpg" class="centerElem" style="width:1200px;margin-top: -73px;height: 541px; " alt="IMG-LOGO">
                
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#homeMenuButton").addClass("active-menu")
        });
    </script>
</asp:Content>

