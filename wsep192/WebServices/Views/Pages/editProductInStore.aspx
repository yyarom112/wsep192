<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="editProductInStore.aspx.cs" Inherits="WebServices.Views.Pages.editProductInStore" %>

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
                        <h3>Edit Product In Store</h3>
                        <form class="row login_form" action="#/" id="editProductInStore_form">
                            <div class="col-md-12 form-group">
                                <input type="text" class="form-control" id="currProductName" name="currProductName" placeholder="Current Product Name" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Current product name'">
                            </div>

                            <div class="col-md-12 form-group">
                                <input type="text" class="form-control" id="newProductName" name="newProductName" placeholder="New product name" onfocus="this.placeholder = ''" onblur="this.placeholder = 'New product name'">
                            </div>
                             <div class="col-md-12 form-group">
                                <input type="text" class="form-control" id="categoryProductName" name="categoryProductName" placeholder="Category produc name" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Category produc name'">
                            </div>
                            <div class="col-md-12 form-group">
                                <input type="text" class="form-control" id="detailOnProduct" name="detailOnProduct" placeholder="Detail on product" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Detail on product'">
                            </div>
                            <div class="col-md-12 form-group">
                                <input type="text" class="form-control" id="productPrice" name="productPrice" placeholder="Product price" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Product price'">
                            </div>
                            <div class="col-md-12 form-group">
                                <input type="text" class="form-control" id="storeName" name="storeName" placeholder="Store Name" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Store Name'">
                            </div>

                            <small id="registerAlert" class="form-text text-muted text-Alert"></small>

                            <div class="col-md-12 form-group">
                                <input type="button" class="button button-register w-100" id="editProductInStoreButton" value="Edit Product"></>
                            </div>

                        </form>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!--================End Login Box Area =================-->

</asp:Content>
