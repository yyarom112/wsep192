<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="setUp.aspx.cs" Inherits="WebServices.Views.Pages.setUp" %>


<asp:Content ID="content1" ContentPlaceHolderID="MainContent" runat="server"> 

     <script type="text/javascript">
            $(document).ready(function () {

                cleanCookies();

                jQuery.ajax({
                    type: "GET",
                    url: baseUrl+"/api/user/setUp?",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response == "System setup completed") {
                            alert(response);
                            window.location.href = baseUrl+"/";
                        }
                        else {
                            alert(response);
                            window.location.href = baseUrl+"/";
                        }
                    },
                    error: function (response) {
                        alert(response);
                        window.location.href = baseUrl+"/";
                    }
                });
	    });

</script>



</asp:Content>