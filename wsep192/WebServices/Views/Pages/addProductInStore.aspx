<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="addProductInStore.aspx.cs" Inherits="WebServices.Views.Pages.addProductInStore" %>

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
                            <a class="button button-account" href="/LoginUser">Open store now</a>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="login_form_inner register_form_inner">
                        <h3>Add Product In Store</h3>
                        <form class="row login_form" action="#/" id="addProductInStore_form">
                            <div class="col-md-12 form-group">
                                <input type="text" class="form-control" id="productName" name="name" placeholder="Product Name" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Product Name'">
                            </div>

                            <div class="col-md-12 form-group">
                                <input type="text" class="form-control" id="productQuantity" name="productQuantity" placeholder="product Quantity" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Product Quantity'">
                            </div>
                            <div class="col-md-12 form-group">
                                <input type="text" class="form-control" id="storeName" name="storeName" placeholder="Store Name" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Store Name'">
                            </div>

                            <small id="registerAlert" class="form-text text-muted text-Alert"></small>

                            <div class="col-md-12 form-group">
                                <input type="button" class="button button-register w-100" id="addProductInStoreButton" value="Add Product"></>
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
            $("#addProductInStoreButton").click(function () {

                productName = $("#productName").val();
                productQuantity = $("#productQuantity").val();
                storeName = $("#storeName").val();

                jQuery.ajax({
                    type: "GET",
                    url: baseUrl + "/api/store/AddProductInStore?productName=" + productName + "&productQuantity=" + productQuantity + 
                    "storeName" + storeName,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response == "Product successfully added to store") {
                            alert(response);
                            window.location.href = baseUrl + "/";
                        }
                        else {
                            $("#registerAlert").html('Failure - ' + response);
                        }
                    },
                    error: function (response) {
                        window.location.href = baseUrl + "/error";
                    }
                });

            });
        });

    </script>

</asp:Content>
