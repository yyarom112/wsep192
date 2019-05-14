<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="removeUser.aspx.cs" Inherits="WebServices.Views.Pages.removeUser" %>

<asp:Content ID="content1" ContentPlaceHolderID="MainContent" runat="server">
    <!--================Login Box Area =================-->
    <section class="login_box_area section-margin">
        <div class="container">
            <div class="row">
                <div class="col-lg-6">
                    <div class="login_box_img">
                        <div class="hover">
                            <h4>Don't forget,</h4>
                            <h4>Only Admin can remove users from the system.</h4>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="login_form_inner register_form_inner">
                        <h3>Remove user</h3>
                        <form class="row login_form" action="#/" id="register_form">
                            <div class="col-md-12 form-group">
                                <input type="text" class="form-control" id="removeuser" name="removeuser" placeholder="User name" onfocus="this.placeholder = ''" onblur="this.placeholder = 'User name'">
                            </div>

                            <input type="button" id="removeUserButton" value="Remove User" class="button button-login w-100"></>

                        </form>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!--================End Login Box Area =================-->
    <script type="text/javascript">
        $(document).ready(function () {
            $("#removeUserButton").click(function () {

                var username = getCookie("LoggedUser");
                if (username != null) {
                    removeuser = $("#removeuser").val();

                    jQuery.ajax({
                        type: "GET",
                        url: baseUrl + "/api/user/RemoveUser?username=" + username + "&removeuser=" + removeuser,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            if (response == "User successfuly removed") {
                                alert(response);
                                window.location.href = baseUrl + "/";
                            }
                            else {
                                alert(response);
                            }
                        },
                        error: function (response) {
                            alert(response);
                        }
                    });
                }
                else
                    alert("User isn't logged in");
            });
        });

    </script>



</asp:Content>
