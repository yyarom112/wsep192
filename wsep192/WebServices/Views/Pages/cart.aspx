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
                        str += '<tr class="text-center"><td class="remove"><input class="check" type="checkbox" id="' + products[i] + '"><br></td><td class="product-name"><p>' +
                            products[i] + '</p></td><td class="quantity"><div class="input-group mb-4"><input style="width: 10px;" id="' + products[i] + '" type="text" name="quantity" class="quantity form-control input-number" value="';
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
                {
                    var list = '';
                    $(".check").each(function (e) {
                        if ($(this).is(':checked'))
                            list = list + $(this).attr('id') + ",";
                    })
                    jQuery.ajax({
                        type: "GET",
                        url: baseUrl + "/api/user/RemoveFromCart?list=" + list + "&store=" + store + "&user=" + user,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            if (response != "false") {


                                $(".input-group mb-4").each(function (e) {
                                    var quantity = $(this).val();
                                    var product = $(this).val();
                                        jQuery.ajax({
                                            type: "GET",
                                            url: baseUrl + "/api/user/EditCart?product=" + product + "&quantity=" + quantity + "&store=" + store + "&user=" + user,
                                            contentType: "application/json; charset=utf-8",
                                            dataType: "json",
                                            success: function (response) {
                                                if (response = "false") {
                                                    alert('Failure - Edit Product ' + product + ' Failed');
                                                }
                                            },
                                            error: function (response) {
                                                alert('Failure - Edit Product ' + product + ' Error');
                                            }
                                        });
                                })

                                jQuery.ajax({
                                    type: "GET",
                                    url: baseUrl + "/api/user/ShoppingCart?store=" + store + "&user=" + user,
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (response) {
                                        if (response != "false") {
                                            window.location.href = baseUrl + "/Cart?store=" + store + "&cart" + response;
                                        }
                                        else {
                                            alert('Failure - Show Cart Failed');
                                        }
                                    },
                                    error: function (response) {
                                        alert('Failure - Show Cart Error');
                                    }
                                });

                            }
                            else {
                                alert('Remove Products Failed');
                            }
                        },
                        error: function (response) {
                            alert('Failure - Remove Products Error');
                        }
                    });
                }

            });
        });





    </script>
</asp:Content>
