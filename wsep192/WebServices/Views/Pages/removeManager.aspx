<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="removeManager.aspx.cs" Inherits="WebServices.Views.Pages.removeManager" %>

<asp:Content ID="content1" ContentPlaceHolderID="MainContent" runat="server">
    <!--================Login Box Area =================-->
    <section class="login_box_area section-margin">
        <div class="container">
            <div class="row">
                <div class="col-lg-6">
                    <div class="login_box_img">
                        <div class="hover">
                            <h4>Don't forget</h4>
                            <p>Only owner can remove manager.</p>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="login_form_inner register_form_inner">
                        <h3>Remove Existing Manager</h3>
                        <form class="row login_form" action="#/" id="register_form">

                            <div class="col-md-12 form-group">
                                <input type="text" class="form-control" id="managerToRemove" name="managerToRemove" placeholder="Manager Name" onfocus="this.placeholder = ''" onblur="this.placeholder = 'managerToRemove'">
                            </div>
                            <div class="col-md-12 form-group">
                                <input type="text" class="form-control" id="storeName" name="storeName" placeholder="Store Name" onfocus="this.placeholder = ''" onblur="this.placeholder = 'storeName'">
                            </div>

                            <input type="button" id="removeManagerButton" value="Remove Manager" class="button button-login w-100"></>

                        </form>
                    </div>
                </div>s
            </div>
        </div>
    </section>
    <!--================End Login Box Area =================-->
    <script type="text/javascript">
        $(document).ready(function () {
            $("#removeManagerButton").click(function () {
                var ownerName = getCookie("LoggedUser");
                if (ownerName != null) {
                    managerToRemove = $("#managerToRemove").val();
                    storeName = $("#storeName").val();
                    jQuery.ajax({
                        type: "GET",
                        url: baseUrl + "/api/user/RemoveManager?managerToRemove=" + managerToRemove + "&storeName=" + storeName + "&ownerName=" + ownerName,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            if (response == "Manager successfuly was removed") {
                                document.cookie = "LoggedUser=" + ownerName
                                alert(response);
                                window.location.href = baseUrl + "/";
                            }
                            else {
                                alert('The user is not the store manager or does not exists. Please try again.');
                            }
                        },
                        error: function (response) {
                            alert('The user is not an owner or doest not have premmision to preform this act.');
                        }
                    });
                }
                else
                    alert("User isn't logged in");
            });
        });
    </script>
</asp:Content>
