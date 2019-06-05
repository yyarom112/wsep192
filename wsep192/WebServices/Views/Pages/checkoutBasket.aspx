<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="checkoutBasket.aspx.cs" Inherits="WebServices.Views.Pages.checkoutBasket" %>

<asp:Content ID="content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--================Checkout Area =================-->
    <section class="checkout_area section-margin--small">
        <div class="container">

            <div class="billing_details" id="ship_and_pay_div">
                <div class="row">
                    <div class="col-lg-8">

                        <h3>Shipping Address</h3>
                        <form class="row contact_form" action="#" method="post" novalidate="novalidate">
                            <div class="col-md-12 form-group p_star">
                                <select class="country_select" id="value">
                                    <option value="1">telaviv - 50₪</option>
                                    <option value="2">beersheva - 10₪</option>
                                    <option value="3">haifa - 60₪</option>
                                </select>
                            </div>
                            <div class="text-center">
                                <a class="button button-paypal" id="priceButton" href="#">Calculate Price</a>
                            </div>
                        </form>


                        <div id="paymentDiv" style="visibility: hidden">
                            <h3 style="padding-top: 30px;">Billing Details</h3>
                            <form class="row contact_form" action="#" id="paymentForm" method="post" novalidate="novalidate">
                                <div class="col-md-12 form-group p_star">
                                    Price:
                                    <div id="price" class="col-md-6 form-group p_star"></div>
                                </div>
                                <div class="col-md-12 form-group p_star">
                                    <div class="col-md-6 form-group p_star">
                                        <input type="text" class="form-control" id="cardNumber" placeholder="Card number">
                                        <span class="placeholder" data-placeholder="Card number"></span>
                                    </div>
                                </div>
                                <div class="col-md-12 form-group p_star">
                                    <div class="col-md-6 form-group p_star">
                                        <input type="text" class="form-control" id="expiredDate" placeholder="Expired date _/_/_">
                                        <span class="placeholder" data-placeholder="Expired date _/_/_"></span>
                                    </div>
                                </div>
                                <div class="text-center">
                                    <a class="button button-paypal" id="applyButton" href="#">Pay</a>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>

            <div class="billing_details" id="orderTable" style="visibility: hidden">
                <div class="row">
                    <div class="col-lg-8">
                        <h2>Your Order Confirmation</h2>
                        <table class="table">
                            <thead class="thead-primary">
                                <tr class="text-center">
                                    <th>Product Name</th>
                                    <th>Store</th>
                                    <th>Quantity</th>
                                    <th>Price</th>
                                    <th>Total</th>
                                </tr>
                            </thead>
                            <tbody id="tableOfOrder">
                            </tbody>
                        </table>


                    </div>
                </div>
            </div>



        </div>
    </section>
    <!--================End Checkout Area =================-->

    <script type="text/javascript">
        $(document).ready(function () {
            $("#priceButton").click(function () {
                var userName = getCookie("LoggedUser");
                if (userName == null)
                    userName = getCookie("GuestUser");
                address = $("#value").val();
                jQuery.ajax({
                    type: "GET",
                    url: baseUrl + "/api/user/CheckoutBasket?address=" + address + "&username=" + userName,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response == "-1") {
                            alert("Purchase doesn't meet the conditions.");
                            window.location.href = baseUrl + "/";
                        }
                        else if (response == "0") {
                            alert("Basket is empty");
                        }
                        else {
                            document.getElementById('price').textContent = response;
                            document.getElementById("paymentDiv").style.visibility = "visible";
                        }

                    },
                    error: function (response) {
                        alert(response);
                    }
                });


            });



            $("#applyButton").click(function () {
                var userName = getCookie("LoggedUser");
                if (userName == null)
                    userName = getCookie("GuestUser");
                var price = document.getElementById('price').textContent;
                var cardNum = $('#cardNumber').val();
                var expiredDate = $('#expiredDate').val();

                jQuery.ajax({
                    type: "GET",
                    url: baseUrl + "/api/user/payForBasket?cardNum=" + cardNum + "&expiredDate=" + expiredDate + "&username=" + userName,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response != "Date or card number are wrong") {
                            var totalPrice = parseInt(document.getElementById('price').textContent, 10);
                            document.getElementById('ship_and_pay_div').remove();
                            $('#tableOfOrder').append("");
                            var user = getCookie("LoggedUser");
                            var guest = getCookie("GuestUser");
                            let store = null, productName = null, quantity = null;
                            if (user == null)
                                user = guest;
                            let searchParams = new URLSearchParams(response);
                            var i = 0;
                            var str = '';
                            while (searchParams.has('store' + i) && searchParams.has('name' + i) && searchParams.has('quantity' + i)
                                && searchParams.has('price' + i) && searchParams.has('total' + i)) {
                                store = searchParams.get('store' + i);
                                productName = searchParams.get('name' + i);
                                quantity = searchParams.get('quantity' + i);
                                price = searchParams.get('price' + i);
                                total = searchParams.get('total' + i);
                                str += '<tr class="text-center"><td class="product-name"><p>' +
                                    productName + '</p></td>';
                                str += '<td class="store-name"><p>' + store + '</p></td >';
                                str += '<td class="quantity"><p>' + quantity + '</p></td >';
                                str += '<td class="price"><p>' + price + '</p></td >';
                                str += '<td class="total"><p>' + total + '</p></td ></tr >';
                                i++;
                            }

                            var sum = parseInt(searchParams.get('sum'), 10);
                            str += '<tr class="text-center"><td class="product-name"><p>Shipping</p></td><td class="store-name"><p></p></td ><td class="quantity"><p></p></td ><td class="price"><p></p></td ><td class="total"><p>' + (totalPrice - sum) + '</p></td ></tr >';
                            str += '<tr class="text-center"><td class="product-name"><p>Total amount</p></td><td class="store-name"><p></p></td ><td class="quantity"><p></p></td ><td class="price"><p></p></td ><td class="total"><p>' + totalPrice + '</p></td ></tr >';
                            str += '<tr class="text-center"><td class="product-name"><p>Thank you for shopping in aroma.</p></td><td class="store-name"><p></p></td ><td class="quantity"><p></p></td ><td class="price"><p></p></td ><td class="total"><p> </p></td ></tr >';

                            $('#tableOfOrder').append(str);
                            document.getElementById('orderTable').style.visibility = "visible";
                        }
                        else
                            alert(response);
                    },
                    error: function (response) {
                        alert("Problem with pay for basket");
                    }
                });


            });
        });

    </script>

</asp:Content>
