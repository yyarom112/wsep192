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
                                    <th>Quantity To Add</th>
                                    <th>Add To Cart</th>
                                </tr>
                            </thead>
                            <tbody id="table">
                            </tbody>
                        </table>
                        <input type="button" style=" max-width: 180px; float: right;" class="button button-login w-100" id="ApplyButton" value="Add To Cart"></>
                            <a style="max-width: 180px; float: right;" href="/SearchProduct">
                            <input class="button button-login w-100" id="SearchAgainButton" value="Search Again" />
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
                str += '<td class="quantity"><p>' + quantity + '</p></td >';
                str += '<td class="quantity"><div class="input-group mb-4"><input style="width: 10px;" id="quantity'
                    + productName + '" type="text" name="quantity" class="quantity form-control input-number" value="1" min="1" max="100"></div ></td >';
                str += '<td class="remove"><input class="check" type="checkbox" id="' + productName + '"><br></td></tr >';
                i++;
            }
            $('#table').append(str);

            $("#ApplyButton").click(function () {

                var list = '';
                $(".check").each(function (e) {
                    if ($(this).is(':checked')) {
                        
                        quantity = document.getElementById('quantity' + $(this).attr('id')).value;
                        list = list + $(this).attr('id') + "," + quantity + ",";
                    }
                })
                console.log(list);

                jQuery.ajax({
                    type: "GET",
                    url: baseUrl + "/api/user/AddtoCart?list=" + list + "&store=" + store + "&user=" + user,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response != "false") {
                            alert('Products added to cart successfully');
                        }
                        else {
                            alert('Failure - add to cart failed');
                        }
                    },
                    error: function (response) {
                        alert('Failure - add to cart error');
                    }
                });

            });
        });
    </script>
</asp:Content>
