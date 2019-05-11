<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="searchProduct.aspx.cs" Inherits="WebServices.Views.Pages.searchProduct" %>

<asp:Content ID="content1" ContentPlaceHolderID="MainContent" runat="server">
    <!--================Login Box Area =================-->
    <div class="login_form_inner register_form_inner">
        <h3>Search Product</h3>
        <form class="row login_form" action="#/" id="register_form">
            <div class="col-md-12 form-group">
                <input type="text" class="form-control" id="productName" name="productName" placeholder="Product Name" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Product Name'">
            </div>

            <div class="col-md-12 form-group">
                <input type="text" class="form-control" id="category" name="category" placeholder="Category" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Category'">
            </div>
            <div class="col-md-12 form-group">
                <input type="text" class="form-control" id="keyword" name="keyword" placeholder="Keyword" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Keyword'">
            </div>
            <div class="col-md-12 form-group">
                <input type="text" class="form-control" id="minPrice" name="minPrice" placeholder="Min Price" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Min Price'">
            </div>
            <div class="col-md-12 form-group">
                <input type="text" class="form-control" id="maxPrice" name="maxPrice" placeholder="Max Price" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Max Price'">
            </div>
            <div class="col-md-12 form-group">
                <input type="text" class="form-control" id="productRate" name="productRate" placeholder="Product Rate" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Product Rate'">
            </div>
            <div class="col-md-12 form-group">
                <input type="text" class="form-control" id="storeRate" name="storeRate" placeholder="Store Rate" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Store Rate'">
            </div>

            <input type="button" id="searchButton" value="Search Product" class="button button-login w-100"></>

        </form>
    </div>
    <!--================End Login Box Area =================-->

    <script type="text/javascript">
        $(document).ready(function () {
            $("#searchButton").click(function () {
                details = $("#productName").val();
                details += "," + $("#category").val();
                details += "," + $("#keyword").val();
                details += "," + $("#minPrice").val();
                details += "," + $("#maxPrice").val();
                details += "," + $("#productRate").val();
                details += "," + $("#storeRate").val();

                jQuery.ajax({
                    type: "GET",
                    url: baseUrl + "/api/user/SearchProduct?details=" + details,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response == "") {
                            alert("Nothing");
                            window.location.href = baseUrl + "/";
                        }
                        else {
                            window.location.href = baseUrl + "/ShowProduct?" + response;
                        }
                    },
                    error: function (response) {
                        alert(response);
                    }
                });
            });
        });

    </script>
</asp:Content>
