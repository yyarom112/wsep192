<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="removePolicy.aspx.cs" Inherits="WebServices.Views.Pages.removePolicy" %>

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
                                    <option value="0">Remove Purchase Policy</option>
                                    <option value="1">Remove Discount Policy</option>
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
                        <div class="text-center"><a class="button button-paypal" style="visibility: hidden" id="removeDiscountButton">remove Policy</a></div>

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
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="policyId" placeholder="Purchase Policy Id"><span class="placeholder" data-placeholder="Purchase Policy Id"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="store" placeholder="Store name"><span class="placeholder" data-placeholder="Store name"></span> </div> </div>'
                            + '</form>'
                    }
                    if (type == 1) {
                        details = '<form class="row contact_form" action="#" id="policyDetails" method="post" novalidate="novalidate">'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="discountId" placeholder="Discount Policy Id"><span class="placeholder" data-placeholder="Discount Policy Id"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="store" placeholder="Store name"><span class="placeholder" data-placeholder="Store name"></span> </div> </div>'
                            + '</form>'
                    }

                    $('#policyDiv').append(details);
                    document.getElementById("policyDiv").style.visibility = "visible";
                    document.getElementById("removeDiscountButton").style.visibility = "visible";

                }
                else
                    alert("User isn't logged in");
            });

            $("#removeDiscountButton").click(function () {
                var userName = getCookie("LoggedUser");
                if (userName != null) {
                    type = $("#value").val();
                    store = $("#store").val();
                    if (type == 0) {
                        purchaseId = $("#policyId").val();

                        jQuery.ajax({
                            type: "GET",
                            url: baseUrl + "/api/store/RemovePurchasePolicy?purchaseId="
                                + purchaseId + "&store=" + store + "&user=" + userName,

                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {
                                if (response == -1) {
                                    alert("Error in remove purchase policy");
                                    window.location.href = baseUrl + "/";
                                }
                                else
                                    alert("Success remove purchase policy");
                            },
                            error: function (response) {
                                alert('Error in removePurchasePolicy');
                            }
                        });
                    }
                    if (type == 1) {
                        discountId = $("#discountId").val();

                        jQuery.ajax({
                            type: "GET",
                            url: baseUrl + "/api/store/RemoveDiscountPolicy?discountId="
                                + discountId + "&store=" + store + "&user=" + userName,

                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {
                                if (response == -1) {
                                    alert("Error in remove discount policy");
                                    window.location.href = baseUrl + "/";
                                }
                                else
                                    alert("Success remove discount policy");
                            },
                            error: function (response) {
                                alert('Error in removeDiscountPolicy');
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
