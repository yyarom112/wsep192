

<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="addPurchasePolicy.aspx.cs" Inherits="WebServices.Views.Pages.purchasePolicy" %>

<asp:Content ID="content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--================Checkout Area =================-->
    <section class="checkout_area section-margin--small">
        <div class="container">

            <div class="billing_details" id="ship_and_pay_div">
                <div class="row">
                    <div class="col-lg-8">

                        <h3>Purchase polices</h3>
                        <form class="row contact_form" action="#" method="post" novalidate="novalidate">
                            <div class="col-md-12 form-group p_star">
                                <select class="policy_select" id="value">
                                    <option value="0">Product condition</option>
                                    <option value="1">Inventory condition</option>
                                    <option value="2">Buy condition</option>
                                    <option value="3">User condition</option>
                                    <option value="4">If then condition</option>
                                    <option value="5">Logical condition</option>
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
                        <div class="text-center"><a class="button button-paypal" style="visibility: hidden" id="addPurchaseButton">Add Purchase Policy</a></div>


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
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="store" placeholder="Store name"><span class="placeholder" data-placeholder="Store name"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="productID" placeholder="Product ID"><span class="placeholder" data-placeholder="Product ID"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="minNum" placeholder="Min products"><span class="placeholder" data-placeholder="Min products"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="maxNum" placeholder="Max products"><span class="placeholder" data-placeholder="Max products"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="logicCon" placeholder="Logical condition"><span class="placeholder" data-placeholder="Logical condition"></span> </div> </div>'
                            + '</form>'
                    }
                    if (type == 1) {
                        details = '<form class="row contact_form" action="#" id="policyDetails" method="post" novalidate="novalidate">'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="store" placeholder="Store name"><span class="placeholder" data-placeholder="Store name"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="minNum" placeholder="Min products"><span class="placeholder" data-placeholder="Min products"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="productID" placeholder="Product ID"><span class="placeholder" data-placeholder="Product ID"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="logicCon" placeholder="Logical condition"><span class="placeholder" data-placeholder="Logical condition"></span> </div> </div>'
                            + '</form>'
                    }
                    if (type == 2) {
                        details = '<form class="row contact_form" action="#" id="policyDetails" method="post" novalidate="novalidate">'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="store" placeholder="Store name"><span class="placeholder" data-placeholder="Store name"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="minNum" placeholder="Min products"><span class="placeholder" data-placeholder="Min products"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="maxNum" placeholder="Max products"><span class="placeholder" data-placeholder="Max products"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="minSum" placeholder="Min price"><span class="placeholder" data-placeholder="Min price"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="maxSum" placeholder="Max price"><span class="placeholder" data-placeholder="Max price"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="logicCon" placeholder="Logical condition"><span class="placeholder" data-placeholder="Logical condition"></span> </div> </div>'
                            + '</form>'
                    }
                    if (type == 3) {
                        details = '<form class="row contact_form" action="#" id="policyDetails" method="post" novalidate="novalidate">'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="store" placeholder="Store name"><span class="placeholder" data-placeholder="Store name"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="address" placeholder="Address"><span class="placeholder" data-placeholder="Address"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="isRegister" placeholder="Is registered"><span class="placeholder" data-placeholder="Is registered"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="logicCon" placeholder="Logical condition"><span class="placeholder" data-placeholder="Logical condition"></span> </div> </div>'
                            + '</form>';
                    }

                    $('#policyDiv').append(details);
                    document.getElementById("policyDiv").style.visibility = "visible";
                    document.getElementById("addPurchaseButton").style.visibility = "visible";

                }
                else
                    alert("User isn't logged in");


            });

            $("#addPurchaseButton").click(function () {
                var userName = getCookie("LoggedUser");
                if (userName != null) {
                    type = $("#value").val();
                    store = $("#store").val();
                    var policyDetails;
                    if (type == 0) {
                        productID = $("#productID").val();
                        minNum = $("#minNum").val();
                        maxNum = $("#maxNum").val();
                        logicCon = $("#logicCon").val();
                        policyDetails = "(" + type + "," + productID + "," + minNum + "," + maxNum + "," + logicCon + ")";

                    }
                    if (type == 1) {

                        minNum = $("#minNum").val();
                        productID = $("#productID").val();
                        logicCon = $("#logicCon").val();
                        policyDetails = "(" + type + "," + minNum + "," + productID + "," + logicCon + ")";

                    }
                    if (type == 2) {

                        minNum = $("#minNum").val();
                        maxNum = $("#maxNum").val();
                        minSum = $("#minSum").val();
                        maxSum = $("#maxSum").val();
                        logicCon = $("#logicCon").val();
                        policyDetails = "(" + type + "," + minNum + "," + maxNum + "," + minSum + "," + maxSum + "," + logicCon + ")";

                    }
                    if (type == 3) {

                        address = $("#address").val();
                        isRegister = $("#isRegister").val();
                        logicCon = $("#logicCon").val();
                        policyDetails = "(" + type + "," + address + "," + isRegister + "," + logicCon + ")";

                    }
                    jQuery.ajax({
                        type: "GET",
                        url: baseUrl + "/api/store/AddPurchasePolicy?details=" + policyDetails + "&store=" + store + "&user=" + userName,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            if (response == 'success') {
                                alert("Policy has created successfully");
                                window.location.href = baseUrl + "/";
                            }
                            else
                                alert("purchase id:" + response);
                        },
                        error: function (response) {
                            alert('Error in addPurchasePolicy');
                        }
                    });
                }
                else
                    alert("User isn't logged in");
            });
        });

    </script>

</asp:Content>
