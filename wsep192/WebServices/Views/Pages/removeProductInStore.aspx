<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="removeProductInStore.aspx.cs" Inherits="WebServices.Views.Pages.removeProductInStore" %>

<asp:Content ID="content1" ContentPlaceHolderID="MainContent" runat="server">
    <!--================Login Box Area =================-->
    <section class="login_box_area section-margin">
        <div class="container">
            <div class="row">
                <div class="col-lg-6">
                    <div class="login_box_img">
                        <div class="hover">
                            <h4>Forgot to add the product to your store?</h4>
                            <p>Jump to add the product to your store!</p>
                            <a class="button button-account" href="/AddProductInStoreButton">Add product to store</a>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="login_form_inner register_form_inner">
                        <h3>Remove Product From Store</h3>
                        <form class="row login_form" action="#/" id="removeProductInStore_form">
                            <div class="col-md-12 form-group">
                                <input type="text" class="form-control" id="productName" name="name" placeholder="Product Name" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Product Name'">
                            </div>
                            <div class="col-md-12 form-group">
                                <input type="text" class="form-control" id="productQuantity" name="productQuantity" placeholder="Product Quantity" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Product Quantity'">
                            </div>
                            <div class="col-md-12 form-group">
                                <input type="text" class="form-control" id="storeName" name="storeName" placeholder="Store Name" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Store Name'">
                            </div>

                            <small id="removeProductInStoreAlert" class="form-text text-muted text-Alert"></small>

                            <button id="removeProductInStoreButton" type="submit" value="submit" class="button button-login w-100">Remove Product</button>

                        </form>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!--================End Login Box Area =================-->

    <script type="text/javascript">
        $(document).ready(function () {
            $("#removeProductInStoreButton").click(function () {

                var userName = getCookie("LoggedUser");
                if (userName != null) {
                    productName = $("#productName").val();
                    productQuantity = $("#productQuantity").val();
                    storeName = $("#storeName").val();

                    jQuery.ajax({
                        type: "GET",
                        url: baseUrl + "/api/store/RemoveProductInStore?userName=" + userName
                            + "&productName=" + productName + "&productQuantity=" + productQuantity +
                            "&storeName=" + storeName,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            if (response == "Product successfully removed from store") {
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
                else {
                    $("#loginAlert").html('Failure - ' + "user not logged in!");
                }
            });
        });

    </script>

</asp:Content>
