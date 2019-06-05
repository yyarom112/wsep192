<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="addDiscountPolicy.aspx.cs" Inherits="WebServices.Views.Pages.addDiscountPolicy" %>

<asp:Content ID="content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--================Checkout Area =================-->
    <section class="checkout_area section-margin--small">
        <div class="container">

            <div class="billing_details" id="ship_and_pay_div">
                <div class="row">
                    <div class="col-lg-8">

                        <h3>Discount polices</h3>
                        <form class="row contact_form" action="#" method="post" novalidate="novalidate">
                            <div class="col-md-12 form-group p_star">
                                <select class="policy_select" id="value">
                                    <option value="0">Revealed discount</option>
                                    <option value="1">Conditional discount</option>
                                </select>
                            </div>
                            <div class="text-center">
                                <a class="button button-paypal" id="policyButton" href="#">Select policy</a>
                            </div>
                        </form>


                        <div id="policyDiv" style="visibility: hidden">
                            <h3 style="padding-top: 30px;">Policy Details</h3>
                            <form class="row contact_form" action="#" id="policyDetails" method="post" novalidate="novalidate"></form>

                        </div>
                        <div class="text-center"><a class="button button-paypal" style="visibility: hidden" id="addDiscountButton">Add Discount Policy</a></div>

                    </div>
                </div>
            </div>

        </div>
    </section>
    <!--================End Checkout Area =================-->

    <script type="text/javascript">
        $(document).ready(function () {

            $("#policyButton").click(function () {
                var userName = getCookie("LoggedUser");
                if (userName != null) {
                    var details;
                    var type = $('#value').val();
                    document.getElementById("policyDetails").remove();

                    if (type == 0) {
                        details = '<form class="row contact_form" action="#" id="policyDetails" method="post" novalidate="novalidate">'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="product" placeholder="Product name"><span class="placeholder" data-placeholder="Product name"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="quantity" placeholder="Product quantity"><span class="placeholder" data-placeholder="Product quantity"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="store" placeholder="Store name"><span class="placeholder" data-placeholder="Store name"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="discount" placeholder="Discount percentage"><span class="placeholder" data-placeholder="Discount percentage"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="expiredDate" placeholder="Num of Days to be expired"><span class="placeholder" data-placeholder="Num of Days to be expired"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="logic" placeholder="Logical condition - 0 = and or 1 = or"><span class="placeholder" data-placeholder="Logical condition"></span> </div> </div>'
                            + '</form>'
                    }
                    if (type == 1) {
                        details = '<form class="row contact_form" action="#" id="policyDetails" method="post" novalidate="novalidate">'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="product" placeholder="Product name"><span class="placeholder" data-placeholder="Product name"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="quantity" placeholder="Product quantity"><span class="placeholder" data-placeholder="Product quantity"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="condition" placeholder="Condition 0 = and, 1 = or, 2 = xor"><span class="placeholder" data-placeholder="Condition"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="store" placeholder="Store name"><span class="placeholder" data-placeholder="Store name"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="discount" placeholder="Discount percentage"><span class="placeholder" data-placeholder="Discount percentage"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="expiredDate" placeholder="Num of Days to be expired"><span class="placeholder" data-placeholder="Num of Days to be expired"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="logic" placeholder="Logical condition - 0 = and or 1 = or"><span class="placeholder" data-placeholder="Logical condition"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="duplicate" placeholder="Duplicate condition - 0 = with or 1 = without duplication"><span class="placeholder" data-placeholder="Duplicate condition"></span> </div> </div>'
                            + '</form>'
                    }

                    $('#policyDiv').append(details);
                    document.getElementById("policyDiv").style.visibility = "visible";
                    document.getElementById("addDiscountButton").style.visibility = "visible";

                }
                else
                    alert("User isn't logged in");
            });

            $("#addDiscountButton").click(function () {
                var userName = getCookie("LoggedUser");
                if (userName != null) {
                    type = $("#value").val();
                    store = $("#store").val();
                    var products = '', discount = '', expiredDate = '', logic = '';
                    if (type == 0) {
                        products = $("#product").val();
                        quantity = $("#quantity").val();
                        discount = $("#discount").val();
                        expiredDate = $("#expiredDate").val();
                        logic = $("#logic").val();

                        jQuery.ajax({
                            type: "GET",
                            url: baseUrl + "/api/store/AddRevealedDiscountPolicy?products="
                                + products + "&quantity=" + quantity + "&discount=" + discount + "&expiredDate=" + expiredDate
                                + "&logic=" + logic + "&store=" + store + "&user=" + userName,

                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {
                                if (response == -1) {
                                    alert("Error in add discount policy");
                                    window.location.href = baseUrl + "/";
                                }
                                else {
                                    alert("Discount id: " + response);
                                    window.location.href = baseUrl + "/";
                                }
                            },
                            error: function (response) {
                                alert('Error in addDiscountPolicy');
                                window.location.href = baseUrl + "/";
                            }
                        });
                    }
                    if (type == 1) {
                        products = $("#product").val();
                        quantity = $("#quantity").val();
                        discount = $("#discount").val();
                        expiredDate = $("#expiredDate").val();
                        condition = $("#condition").val();
                        logic = $("#logic").val();
                        duplicate = $("#duplicate").val();

                        jQuery.ajax({
                            type: "GET",
                            url: baseUrl + "/api/store/AddConditionalDiscountPolicy?products="
                                + products + "&quantity=" + quantity + "&condition=" + condition + "&discount=" + discount + "&expiredDate=" + expiredDate
                                + "&logic=" + logic + "&duplicate=" + duplicate + "&store=" + store + "&user=" + userName,

                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {
                                if (response == -1) {
                                    alert("Error in add discount policy");
                                    window.location.href = baseUrl + "/";
                                }
                                else
                                    alert("Discount id: " + response);
                            },
                            error: function (response) {
                                alert('Error in addDiscountPolicy');
                            }
                        });
                    }
                }
                else
                    alert("User isn't logged in");
            });
        });

    </script>

</asp:Content>
