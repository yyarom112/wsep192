<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="editProductInStore.aspx.cs" Inherits="WebServices.Views.Pages.editProductInStore" %>

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
                            <a class="button button-account" href="/AddProductInStore">Add product to store</a>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="login_form_inner register_form_inner">
                        <h3>Edit Product In Store</h3>
                        <form class="row login_form" action="#/" id="editProductInStore_form">
                            <div class="col-md-12 form-group">
                                <input type="text" class="form-control" id="currProductName" name="currProductName" placeholder="Current Product Name" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Current product name'">
                            </div>

                            <div class="col-md-12 form-group">
                                <input type="text" class="form-control" id="newProductName" name="newProductName" placeholder="New product name" onfocus="this.placeholder = ''" onblur="this.placeholder = 'New product name'">
                            </div>
                            <div class="col-md-12 form-group">
                                <input type="text" class="form-control" id="categoryProductName" name="categoryProductName" placeholder="Category product name" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Category product name'">
                            </div>
                            <div class="col-md-12 form-group">
                                <input type="text" class="form-control" id="detailOnProduct" name="detailOnProduct" placeholder="Details on product" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Details on product'">
                            </div>
                            <div class="col-md-12 form-group">
                                <input type="text" class="form-control" id="productPrice" name="productPrice" placeholder="Product price" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Product price'">
                            </div>
                            <div class="col-md-12 form-group">
                                <input type="text" class="form-control" id="storeName" name="storeName" placeholder="Store Name" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Store Name'">
                            </div>

                            <button id="editProductInStoreButton" type="submit" value="submit" class="button button-login w-100">Edit Product</button>

                        </form>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!--================End Login Box Area =================-->

    <script type="text/javascript">
        $(document).ready(function () {
            $("#editProductInStoreButton").click(function () {

                var userName = getCookie("LoggedUser");
                if (userName != null) {
                    productName = $("#currProductName").val();
                    newProductName = $("#newProductName").val();
                    category = $("#categoryProductName").val();
                    details = $("#detailOnProduct").val();
                    price = $("#productPrice").val();
                    storeName = $("#storeName").val();

                    jQuery.ajax({
                        type: "GET",
                        url: baseUrl + "/api/store/EditProductInStore?userName=" + userName + "&productName=" + productName
                            + "&newProductName=" + newProductName + "&category=" + category +
                            "&detail=" + details + "&productPrice=" + price + "&storeName=" + storeName,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            if (response == "Product successfully edited in store") {
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
                    alert('User not logged in');
                }
            });
        });

    </script>

</asp:Content>
