<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="createProductInStore.aspx.cs" Inherits="WebServices.Views.Pages.createProductInStore" %>

<asp:Content ID="content1" ContentPlaceHolderID="MainContent" runat="server">
    <!--================Login Box Area =================-->
    <section class="login_box_area section-margin">
        <div class="container">
            <div class="row">
                <div class="col-lg-6">
                    <div class="login_box_img">
                        <div class="hover">
                            <h4>Doesn't have a store yet?</h4>
                            <p>You can open a store in our website in a few minutes, and start manage your own store!</p>
                            <a class="button button-account" href="/OpenStore">Open store now</a>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="login_form_inner register_form_inner">
                        <h3>Create Product In Store</h3>
                        <form class="row login_form" action="#/" id="createProductInStore_form">
                            <div class="col-md-12 form-group">
                                <input type="text" class="form-control" id="productName" name="name" placeholder="Product Name" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Product Name'">
                            </div>
                            <div class="col-md-12 form-group">
                                <input type="text" class="form-control" id="productCategory" name="productCategory" placeholder="Product Category" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Product Category'">
                            </div>

                            <div class="col-md-12 form-group">
                                <input type="text" class="form-control" id="productDetail" name="productDetail" placeholder="Product Details" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Product Details'">
                            </div>
                            <div class="col-md-12 form-group">
                                <input type="text" class="form-control" id="productPrice" name="productPrice" placeholder="Product Price" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Product Price'">
                            </div>
                            <div class="col-md-12 form-group">
                                <input type="text" class="form-control" id="storeName" name="storeName" placeholder="Store Name" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Store Name'">
                            </div>
                            <input type="button" id="createProductInStoreButton" value="Create Product" class="button button-login w-100"></>


                        </form>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!--================End Login Box Area =================-->

    <script type="text/javascript">
        $(document).ready(function () {
            $("#createProductInStoreButton").click(function () {

                var userName = getCookie("LoggedUser");
                if (userName != null) {
                    productName = $("#productName").val();
                    category = $("#productCategory").val();
                    detail = $("#productDetail").val();
                    productPrice = $("#productPrice").val();
                    storeName = $("#storeName").val();

                    jQuery.ajax({
                        type: "GET",
                        url: baseUrl + "/api/store/CreateProductInStore?userName=" + userName
                            + "&productName=" + productName + "&category=" + category +
                            "&detail=" + detail + "&productPrice=" + productPrice + "&storeName=" + storeName,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            if (response == "Product successfully created") {
                                alert(response);
                                window.location.href = baseUrl + "/";
                            }
                            else {
                                alert("Product name allready exists or store does not exists. Please try again.");
                            }
                        },
                        error: function (response) {
                            alert('Product name allready exists or store does not exists. Please try again.');
                        }
                    });
                }
                else {
                    alert('User is not an owner or premissioned manager');
                }
            });
        });

    </script>
</asp:Content>
