<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="checkoutBasket.aspx.cs" Inherits="WebServices.Views.Pages.checkoutBasket" %>

<asp:Content ID="content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--================Checkout Area =================-->
    <section class="checkout_area section-margin--small">
        <div class="container">
            <div class="billing_details">
                <div class="row">
                    <div class="col-lg-8">
                        <h2>Basket</h2>
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
                    </div>
                </div>
            </div>



            <div class="billing_details">
                <div class="row">
                    <div class="col-lg-8">

                        <h3>Shipping Address</h3>
                        <form class="row contact_form" action="#" method="post" novalidate="novalidate">
                            <div class="col-md-12 form-group p_star">
                                <select class="country_select" id="value">
                                    <option value="1">telaviv</option>
                                    <option value="2">beersheva</option>
                                    <option value="3">haifa</option>
                                </select>
                            </div>
                            <div class="text-center">
                                <a class="button button-paypal" id="priceButton" href="#">Calculate Price</a>
                            </div>
                        </form>


                        <div id="paymentDiv" style="visibility: hidden">
                            <h3 style="padding-top: 30px;">Billing Details</h3>
                            <form class="row contact_form" action="#" id="paymentForm" method="post" novalidate="novalidate">
                                <div class="col-md-12 form-group p_star"> Price:
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
                                        <input type="text" class="form-control" id="expiredDate" placeholder="Expired date">
                                        <span class="placeholder" data-placeholder="Expired date"></span>
                                    </div>
                                </div>
                                <div class="text-center">
                                    <a class="button button-paypal" id="applyButton" href="#">Apply</a>
                                </div>
                            </form>
                        </div>
                    </div>

                    <div class="col-lg-4">
                        <div class="order_box">
                            <h2>Your Order</h2>

                            <div class="text-center">
                                <a class="button button-paypal" href="#">Pay</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!--================End Checkout Area =================-->

    <script type="text/javascript">
        $(document).ready(function () {
            $("#priceButton").click(function () {
                document.getElementById("paymentDiv").style.visibility = "visible";
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
                        if (response == -1) {
                            alert("Error in checkout basket");
                            window.location.href = baseUrl + "/";
                        }
                        else {
                            $('#price').append(response);
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
                    url: baseUrl + "/api/user/payForBasket?cardNum=" + cardNum + "&expiredDate=" + expiredDate+ "&username=" + userName,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                            alert(response);
                    },
                    error: function (response) {
                        alert(response);
                    }
                });


            });
        });

    </script>

</asp:Content>
