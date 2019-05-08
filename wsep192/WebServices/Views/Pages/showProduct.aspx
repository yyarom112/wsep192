<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="showProduct.aspx.cs" Inherits="WebServices.Views.Pages.showProduct" %>

<asp:Content ID="content1" ContentPlaceHolderID="MainContent" runat="server">


    <!--================Cart Area =================-->
    <section class="cart_area">
        <div class="container">
            <div class="row" id="head">
                <div class="col-md-12 ftco-animate">
                    <div class="cart-list">
                        <table class="table">
                            <thead class="thead-primary">
                                <tr class="text-center">
                                    <th>Product Name</th>
                                    <th>Store</th>
                                    <th>Quantity</th>
                                </tr>
                            </thead>
                            <tbody id="table">
                            </tbody>
                        </table>
                        <a href="/SearchProduct">
                            <input style="max-width: 180px" class="button button-login w-100" id="SearchAgainButton" value="Search Again" />
                        </a>
                    </div>
                </div>
            </div>
        </div>

    </section>



    <!--================End Cart Area =================-->
    <script type="text/javascript">
        $(document).ready(function () {
            $('#table').append("");
            var user = getCookie("LoggedUser");
            var guest = getCookie("GuestUser");
            let store = null, productName = null, quantity = null;
            if (user == null)
                user = guest;
            let searchParams = new URLSearchParams(window.location.search);
            var i = 0;
            var str = '';
            $('#head').before('<h3>Search Results</h3>');
            while (searchParams.has('store' + i) && searchParams.has('name' + i) && searchParams.has('quantity' + i)) {
                store = searchParams.get('store' + i);
                productName = searchParams.get('name' + i);
                quantity = searchParams.get('quantity' + i);
                str += '<tr class="text-center"><td class="product-name"><p>' +
                    productName + '</p></td>';
                str += '<td class="store-name"><p>' + store + '</p></td >';
                str += '<td class="quantity"><p>' + quantity + '</p></td ></tr >';
                i++;
            }
            $('#table').append(str);

        });
    </script>
</asp:Content>
