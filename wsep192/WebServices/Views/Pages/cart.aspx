<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="WebServices.Views.Pages.cart" %>

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

                                    <th>Remove</th>
                                    <th>Product</th>
                                    <th>Quantity</th>



                                </tr>
                            </thead>
                            <tbody id="table">
                            </tbody>
                        </table>
                        <input type="button" style="max-width: 250px; float: right;" class="button button-login w-100" id="ApplyButton" value="Apply"></>
                    </div>

                </div>
            </div>
        </div>

    </section>



    <!--================End Cart Area =================-->
    <script type="text/javascript">

        $(document).ready(function () {
            var user = getCookie("LoggedUser");
            var guest = getCookie("GuestUser");
            let store, cart = null;
            if (user == null)
                user = guest;
            let searchParams = new URLSearchParams(window.location.search);
            if (searchParams.has('store') && searchParams.has('cart')) {
                store = searchParams.get('store');
                cart = searchParams.get('cart');
                $('#head').before('<h3>' + store + '</h3>');
                let products = cart.split(",");
                var i = 0;
                var str = '';

                for (i = 0; i < products.length - 1; i++) {
                    if (i % 2 == 0) {
                        str += '<tr class="text-center"><td class="remove"><input class="check" id="check' + products[i]+'" type="checkbox" name="' + products[i] + '"><br></td><td class="product-name"><p>' +
                            products[i] + '</p></td><td class="quantity"><div class="input-group mb-4"><input  id="quantityBox" style="width: 10px;" name="'
                            + products[i] + '" type="text" class="quantity form-control input-number" value="';
                    }
                    else {
                        str += products[i] + '" min="1" max="100"></div ></td ></tr >';
                    }
                }
                $('#table').append(str);
            }
            else {
                alert('Failure');
            }



            $("#ApplyButton").click(function () {
                
                    var list = '';
                    $(".check").each(function (e) {
                        if ($(this).is(':checked'))
                            list = list + $(this).attr('name') + ",";
                    })
                    jQuery.ajax({
                        type: "GET",
                        url: baseUrl + "/api/user/RemoveFromCart?list=" + list + "&store=" + store + "&user=" + user,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            if (response != "false") {
                                $('#quantityBox').each(function (e) {
                                    var quantity = $(this).val();
                                    var product = $(this).attr('name');
                                    let removed = $('#check' + product).is(':checked');
                                    if (removed == false) {
                                        jQuery.ajax({
                                            type: "GET",
                                            async: false,
                                            url: baseUrl + "/api/user/EditCart?product=" + product + "&quantity=" + quantity + "&store=" + store + "&user=" + user,
                                            contentType: "application/json; charset=utf-8",
                                            dataType: "json",
                                            success: function (response) {
                                                if (response == "false") {
                                                    alert('Error- Couldnt edit the cart - invalid input of produc' + product + ' Please try again.');
                                                }
                                            },
                                            error: function (response) {
                                                alert('Somthing went wrong with editing this product, the product is not exist.Please try again');
                                            }
                                        });
                                    }
                                })
                                
                                jQuery.ajax({
                                    type: "GET",
                                    url: baseUrl + "/api/user/ShoppingCart?store=" + store + "&user=" + user,
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (response) {
                                        if (response != "false") {
                                            window.location.href = baseUrl + "/Cart?store=" + store + "&cart=" + response;
                                            
                                        }
                                        else {
                                            alert('Couldnt show the cart. Please make sure you have a cart.');
                                        }
                                    },
                                    error: function (response) {
                                        alert('Fatal error in showing the cart.Please check if you own a cart.');
                                    }
                                });

                            }
                            else {
                                alert('Could not remove this product. Please check the priduct exists.');
                            }
                        },
                        error: function (response) {
                            alert('Somthing went wrong with removing this product. Please try again');
                        }
                    });
                

            });
        });





    </script>
</asp:Content>
