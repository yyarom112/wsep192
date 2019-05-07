<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="openStore.aspx.cs" Inherits="WebServices.Views.Pages.openStore" %>


<asp:Content ID="content1" ContentPlaceHolderID="MainContent" runat="server">
    <!--================Login Box Area =================-->
    <section class="login_box_area section-margin">
        <div class="container">
            <div class="row">
                <div class="col-lg-6">
                    <div class="login_box_img">
                        <div class="hover">
                            <h4>Already have an account?</h4>
                            <p>There are advances being made in science and technology everyday, and a good example of this is the</p>
                            <a class="button button-account" href="/LoginUser">Login Now</a>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="login_form_inner register_form_inner">
                        <h3>Create an account</h3>
                        <form class="row login_form" action="#/" id="register_form">
                            <div class="col-md-12 form-group">
                                <input type="text" class="form-control" id="storeName" name="storeName" placeholder="Store name" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Store name'">
                            </div>

                            <div class="col-md-12 form-group">
                                <input type="button" class="button button-register w-100" id="openStoreButton" value="Open store"></>
                            </div>




                        </form>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!--================End Login Box Area =================-->
    <script type="text/javascript">
        $(document).ready(function () {
            $("#openStoreButton").click(function () {

                username = getCookie("LoggedUser");
                if (username != null) {
                    storename = $("#storeName").val();

                    jQuery.ajax({
                        type: "GET",
                        url: baseUrl + "/api/user/OpenStore?username=" + username + "&storename=" + storename,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            if (response == "Store created successfully") {
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
