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
                        <h3 id="ifText" style="visibility: hidden">If Purchase polices</h3>
                        <form class="row contact_form" action="#" method="post" id="classIF" style="visibility: hidden" novalidate="novalidate">
                            <div class="col-md-12 form-group p_star">
                                <select class="policy_select" id="value2">
                                    <option value="0">Product condition</option>
                                    <option value="1">Inventory condition</option>
                                    <option value="2">Buy condition</option>
                                    <option value="3">User condition</option>
                                    <option value="5">Logical condition</option>
                                </select>
                            </div>
                            <div class="text-center">
                                <a class="button button-paypal" id="policyButton2" href="#">Select policy</a>
                            </div>
                        </form>

                        <h3 id="logic1Text" style="visibility: hidden">Logic 1 Purchase polices</h3>
                        <form class="row contact_form" action="#" method="post" id="classLogic1" style="visibility: hidden" novalidate="novalidate">
                            <div class="col-md-12 form-group p_star">
                                <select class="policy_select" id="value4">
                                    <option value="0">Product condition</option>
                                    <option value="1">Inventory condition</option>
                                    <option value="2">Buy condition</option>
                                    <option value="3">User condition</option>
                                </select>
                            </div>
                            <div class="text-center">
                                <a class="button button-paypal" id="policyButton4" href="#">Select policy</a>
                            </div>
                        </form>

                        <h3 id="logic2Text" style="visibility: hidden">Logic 2 Purchase polices</h3>
                        <form class="row contact_form" action="#" method="post" id="classLogic2" style="visibility: hidden" novalidate="novalidate">
                            <div class="col-md-12 form-group p_star">
                                <select class="policy_select" id="value5">
                                    <option value="0">Product condition</option>
                                    <option value="1">Inventory condition</option>
                                    <option value="2">Buy condition</option>
                                    <option value="3">User condition</option>
                                </select>
                            </div>
                            <div class="text-center">
                                <a class="button button-paypal" id="policyButton5" href="#">Select policy</a>
                            </div>
                        </form>

                        <h3 id="thenText" style="visibility: hidden">Then Purchase polices</h3>
                        <form class="row contact_form" action="#" method="post" id="classELSE" style="visibility: hidden" novalidate="novalidate">
                            <div class="col-md-12 form-group p_star">
                                <select class="policy_select" id="value3">
                                    <option value="0">Product condition</option>
                                    <option value="1">Inventory condition</option>
                                    <option value="2">Buy condition</option>
                                    <option value="3">User condition</option>
                                </select>
                            </div>
                            <div class="text-center">
                                <a class="button button-paypal" id="policyButton3" href="#">Select policy</a>
                            </div>
                        </form>
                        <input type="button" id="addPurchaseButton" style="visibility: hidden" value="Add Purchase Policy" class="button button-login w-100"></>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!--================End Checkout Area =================-->

    <script type="text/javascript">
        $(document).ready(function () {
            debugger;

            $("#policyButton").click(function () {
                var userName = getCookie("LoggedUser");
                if (userName != null) {
                    var details;
                    var type = $('#value').val();
                    if (document.getElementById("policyDetails"))
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
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="productID" placeholder="Product Name"><span class="placeholder" data-placeholder="Product Name"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="minNum" placeholder="Min products"><span class="placeholder" data-placeholder="Min products"></span> </div> </div>'
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
                    if (type == 4) {
                        document.getElementById("classIF").style.visibility = "visible";
                        document.getElementById("classELSE").style.visibility = "visible";
                        document.getElementById("ifText").style.visibility = "visible";
                        document.getElementById("thenText").style.visibility = "visible";
                    }
                    if (type == 5) {
                        document.getElementById("logic1Text").style.visibility = "visible";
                        document.getElementById("logic2Text").style.visibility = "visible";
                        document.getElementById("classLogic1").style.visibility = "visible";
                        document.getElementById("classLogic2").style.visibility = "visible";
                        document.getElementById("classIF").remove();
                        document.getElementById("classELSE").remove();
                        document.getElementById("ifText").remove();
                        document.getElementById("thenText").remove();

                    }

                    if (type < 4) {
                        if (document.getElementById("classIF"))
                            document.getElementById("classIF").remove();
                        if (document.getElementById("classELSE"))
                            document.getElementById("classELSE").remove();
                        if (document.getElementById("ifText"))
                            document.getElementById("ifText").remove();
                        if (document.getElementById("thenText"))
                            document.getElementById("thenText").remove();
                        if (document.getElementById("logic1Text"))
                            document.getElementById("logic1Text").remove();
                        if (document.getElementById("logic2Text"))
                            document.getElementById("logic2Text").remove();
                        if (document.getElementById("classLogic1"))
                            document.getElementById("classLogic1").remove();
                        if (document.getElementById("classLogic2"))
                            document.getElementById("classLogic2").remove();

                        document.getElementById("addPurchaseButton").style.visibility = "visible";
                    }
                    $('#policyDiv').append(details);
                    document.getElementById("policyDiv").style.visibility = "visible";
                }
                else
                    alert("User isn't logged in");
            });

            $("#addPurchaseButton").click(function () {
                var userName = getCookie("LoggedUser");
                if (userName != null) {
                    var type = $("#value").val();
                    var store = $("#store").val();
                    var policyDetails;
                    if (type == 0) {
                        productID = $("#productID").val();
                        minNum = $("#minNum").val();
                        maxNum = $("#maxNum").val();
                        logicCon = $("#logicCon").val();
                        policyDetails = "(" + type + "," + productID + "," + minNum + "," + maxNum + "," + logicCon + ")";
                    }
                    else if (type == 1) {
                        minNum = $("#minNum").val();
                        productID = $("#productID").val();
                        logicCon = $("#logicCon").val();
                        policyDetails = "(" + type + "," + productID + "," + minNum + "," + logicCon + ")";
                    }
                    else if (type == 2) {
                        minNum = $("#minNum").val();
                        maxNum = $("#maxNum").val();
                        minSum = $("#minSum").val();
                        maxSum = $("#maxSum").val();
                        logicCon = $("#logicCon").val();
                        policyDetails = "(" + type + "," + minNum + "," + maxNum + "," + minSum + "," + maxSum + "," + logicCon + ")";
                    }
                    else if (type == 3) {
                        address = $("#address").val();
                        isRegister = $("#isRegister").val();
                        logicCon = $("#logicCon").val();
                        policyDetails = "(" + type + "," + address + "," + isRegister + "," + logicCon + ")";
                    }

                    else if (type == 4) {
                        policyDetails = "(" + type + ",";
                        var type1 = $('#value2').val();
                        if (type1 == 0) {
                            productID = $("#productID").val();
                            minNum = $("#minNum").val();
                            maxNum = $("#maxNum").val();
                            logicCon = $("#logicCon").val();
                            policyDetails += "(" + type1 + "," + productID + "," + minNum + "," + maxNum + "," + logicCon + ")";
                        }
                        if (type1 == 1) {
                            minNum = $("#minNum").val();
                            productID = $("#productID").val();
                            logicCon = $("#logicCon").val();
                            policyDetails += "(" + type1 + "," + productID + "," + minNum + "," +  logicCon + ")";
                        }
                        if (type1 == 2) {
                            minNum = $("#minNum").val();
                            maxNum = $("#maxNum").val();
                            minSum = $("#minSum").val();
                            maxSum = $("#maxSum").val();
                            logicCon = $("#logicCon").val();
                            policyDetails += "(" + type1 + "," + minNum + "," + maxNum + "," + minSum + "," + maxSum + "," + logicCon + ")";
                        }
                        if (type1 == 3) {
                            address = $("#address").val();
                            isRegister = $("#isRegister").val();
                            logicCon = $("#logicCon").val();
                            policyDetails += "(" + type1 + "," + address + "," + isRegister + "," + logicCon + ")";
                        }

                        if (type1 == 5) {
                            type = type1;
                            type1 = $("#value4").val();
                            type2 = $("#value5").val();
                            if (type1 == 0) {
                                productID = $("#productID3").val();
                                minNum = $("#minNum3").val();
                                maxNum = $("#maxNum3").val();
                                logicCon = $("#logicCon3").val();
                                policyDetails += "(" + type + ",(" + type1 + "," + productID + "," + minNum + "," + maxNum + "," + logicCon + "),";
                            }
                            if (type1 == 1) {
                                minNum = $("#minNum3").val();
                                productID = $("#productID3").val();
                                logicCon = $("#logicCon3").val();
                                policyDetails += "(" + type + ",(" + type1 + "," +  productID + "," + minNum + "," + logicCon + "),";
                            }
                            if (type1 == 2) {
                                minNum = $("#minNum3").val();
                                maxNum = $("#maxNum3").val();
                                minSum = $("#minSum3").val();
                                maxSum = $("#maxSum3").val();
                                logicCon = $("#logicCon3").val();
                                policyDetails += "(" + type + ",(" + type1 + "," + minNum + "," + maxNum + "," + minSum + "," + maxSum + "," + logicCon + "),";
                            }
                            if (type1 == 3) {
                                address = $("#address3").val();
                                isRegister = $("#isRegister3").val();
                                logicCon = $("#logicCon3").val();
                                policyDetails += "(" + type + ",(" + type1 + "," + address + "," + isRegister + "," + logicCon + "),";
                            }

                            if (type2 == 0) {
                                productID = $("#productID4").val();
                                minNum = $("#minNum4").val();
                                maxNum = $("#maxNum4").val();
                                logicCon = $("#logicCon4").val();
                                policyDetails += "(" + type2 + "," + productID + "," + minNum + "," + maxNum + "," + logicCon + "),";
                            }
                            if (type2 == 1) {
                                minNum = $("#minNum4").val();
                                productID = $("#productID4").val();
                                logicCon = $("#logicCon4").val();
                                policyDetails += "(" + type2 + "," + productID + "," + minNum + "," +  logicCon + "),";
                            }
                            if (type2 == 2) {
                                minNum = $("#minNum4").val();
                                maxNum = $("#maxNum4").val();
                                minSum = $("#minSum4").val();
                                maxSum = $("#maxSum4").val();
                                logicCon = $("#logicCon4").val();
                                policyDetails += "(" + type2 + "," + minNum + "," + maxNum + "," + minSum + "," + maxSum + "," + logicCon + "),";
                            }
                            if (type2 == 3) {
                                address = $("#address4").val();
                                isRegister = $("#isRegister4").val();
                                logicCon = $("#logicCon4").val();
                                policyDetails += "(" + type2 + "," + address + "," + isRegister + "," + logicCon + "),";
                            }
                            logicConAll = $("#logicConAll").val();
                            policyDetails += logicConAll + ",0)"
                            store = $("#store3").val();
                        }




                        //MISSING TYPE1= 5

                        policyDetails += ',';
                        type1 = $('#value3').val();
                        if (type1 == 0) {
                            productID = $("#productID2").val();
                            minNum = $("#minNum2").val();
                            maxNum = $("#maxNum2").val();
                            logicCon = $("#logicCon2").val();
                            policyDetails += "(" + type1 + "," + productID + "," + minNum + "," + maxNum + "," + logicCon + ")";
                        }
                        if (type1 == 1) {
                            minNum = $("#minNum2").val();
                            productID = $("#productID2").val();
                            logicCon = $("#logicCon2").val();
                            policyDetails += "(" + type1 + "," + productID + "," + minNum + "," +  logicCon + ")";
                        }
                        if (type1 == 2) {
                            minNum = $("#minNum2").val();
                            maxNum = $("#maxNum2").val();
                            minSum = $("#minSum2").val();
                            maxSum = $("#maxSum2").val();
                            logicCon = $("#logicCon2").val();
                            policyDetails += "(" + type1 + "," + minNum + "," + maxNum + "," + minSum + "," + maxSum + "," + logicCon + ")";
                        }
                        if (type1 == 3) {
                            address = $("#address2").val();
                            isRegister = $("#isRegister2").val();
                            logicCon = $("#logicCon").val();
                            policyDetails += "(" + type1 + "," + address + "," + isRegister + "," + logicCon + ")";
                        }
                        policyDetails += ')';
                    }
                    else if (type == 5) {

                        type1 = $("#value4").val();
                        type2 = $("#value5").val();
                        if (type1 == 0) {
                            productID = $("#productID3").val();
                            minNum = $("#minNum3").val();
                            maxNum = $("#maxNum3").val();
                            logicCon = $("#logicCon3").val();
                            policyDetails = "(" + type + ",(" + type1 + "," + productID + "," + minNum + "," + maxNum + "," + logicCon + "),";
                        }
                        if (type1 == 1) {
                            minNum = $("#minNum3").val();
                            productID = $("#productID3").val();
                            logicCon = $("#logicCon3").val();
                            policyDetails = "(" + type + ",(" + type1 + "," + productID + "," + minNum + "," +  logicCon + "),";
                        }
                        if (type1 == 2) {
                            minNum = $("#minNum3").val();
                            maxNum = $("#maxNum3").val();
                            minSum = $("#minSum3").val();
                            maxSum = $("#maxSum3").val();
                            logicCon = $("#logicCon3").val();
                            policyDetails = "(" + type + ",(" + type1 + "," + minNum + "," + maxNum + "," + minSum + "," + maxSum + "," + logicCon + "),";
                        }
                        if (type1 == 3) {
                            address = $("#address3").val();
                            isRegister = $("#isRegister3").val();
                            logicCon = $("#logicCon3").val();
                            policyDetails = "(" + type + ",(" + type1 + "," + address + "," + isRegister + "," + logicCon + "),";
                        }

                        if (type2 == 0) {
                            productID = $("#productID4").val();
                            minNum = $("#minNum4").val();
                            maxNum = $("#maxNum4").val();
                            logicCon = $("#logicCon4").val();
                            policyDetails += "(" + type2 + "," + productID + "," + minNum + "," + maxNum + "," + logicCon + "),";
                        }
                        if (type2 == 1) {
                            minNum = $("#minNum4").val();
                            productID = $("#productID4").val();
                            logicCon = $("#logicCon4").val();
                            policyDetails += "(" + type2 + "," + productID + "," + minNum + "," +  logicCon + "),";
                        }
                        if (type2 == 2) {
                            minNum = $("#minNum4").val();
                            maxNum = $("#maxNum4").val();
                            minSum = $("#minSum4").val();
                            maxSum = $("#maxSum4").val();
                            logicCon = $("#logicCon4").val();
                            policyDetails += "(" + type2 + "," + minNum + "," + maxNum + "," + minSum + "," + maxSum + "," + logicCon + "),";
                        }
                        if (type2 == 3) {
                            address = $("#address4").val();
                            isRegister = $("#isRegister4").val();
                            logicCon = $("#logicCon4").val();
                            policyDetails += "(" + type2 + "," + address + "," + isRegister + "," + logicCon + "),";
                        }
                        logicConAll = $("#logicConAll").val();
                        policyDetails += logicConAll + ",0)"
                        store = $("#store3").val();
                    }


                    jQuery.ajax({
                        type: "GET",
                        url: baseUrl + "/api/store/AddPurchasePolicy?details=" + policyDetails + "&store=" + store + "&user=" + userName,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            if (response != -1) {
                                alert("Policy " + response + " has created successfully");
                                window.location.href = baseUrl + "/";
                            }
                            else {
                                alert("Error in add new purchase policy, does not meet the conditions");
                                window.location.href = baseUrl + "/";
                            }
                        },
                        error: function (response) {
                            alert('Error in addPurchasePolicy');
                        }
                    });
                }
                else
                    alert("User isn't logged in");
            });


            $("#policyButton2").click(function () {
                var userName = getCookie("LoggedUser");
                if (userName != null) {
                    var details;
                    var type = $('#value2').val();
                    document.getElementById("policyButton2").remove();

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
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="productID" placeholder="Product Name"><span class="placeholder" data-placeholder="Product Name"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="minNum" placeholder="Min products"><span class="placeholder" data-placeholder="Min products"></span> </div> </div>'
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
                    if (type == 5) {
                        document.getElementById("logic1Text").style.visibility = "visible";
                        document.getElementById("logic2Text").style.visibility = "visible";
                        document.getElementById("classLogic1").style.visibility = "visible";
                        document.getElementById("classLogic2").style.visibility = "visible";
                    }

                    $('#classIF').append(details);
                    document.getElementById("classIF").style.visibility = "visible";
                }
                else
                    alert("User isn't logged in");
            });

            $("#policyButton3").click(function () {
                var userName = getCookie("LoggedUser");
                if (userName != null) {
                    var details;
                    var type0 = $('#value').val();
                    var type = $('#value3').val();
                    var type1 = $('#value2').val();
                    document.getElementById("policyButton3").remove();

                    if (type == 0) {
                        details = '<form class="row contact_form" action="#" id="policyDetails" method="post" novalidate="novalidate">'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="store2" placeholder="Store name"><span class="placeholder" data-placeholder="Store name"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="productID2" placeholder="Product ID"><span class="placeholder" data-placeholder="Product ID"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="minNum2" placeholder="Min products"><span class="placeholder" data-placeholder="Min products"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="maxNum2" placeholder="Max products"><span class="placeholder" data-placeholder="Max products"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="logicCon2" placeholder="Logical condition"><span class="placeholder" data-placeholder="Logical condition"></span> </div> </div>';
                        + '</form>';
                    }
                    if (type == 1) {
                        details = '<form class="row contact_form" action="#" id="policyDetails" method="post" novalidate="novalidate">'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="store2" placeholder="Store name"><span class="placeholder" data-placeholder="Store name"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="productID2" placeholder="Product Name"><span class="placeholder" data-placeholder="Product Name"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="minNum2" placeholder="Min products"><span class="placeholder" data-placeholder="Min products"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="logicCon2" placeholder="Logical condition"><span class="placeholder" data-placeholder="Logical condition"></span> </div> </div>';
                        + '</form>';
                    }
                    if (type == 2) {
                        details = '<form class="row contact_form" action="#" id="policyDetails" method="post" novalidate="novalidate">'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="store2" placeholder="Store name"><span class="placeholder" data-placeholder="Store name"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="minNum2" placeholder="Min products"><span class="placeholder" data-placeholder="Min products"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="maxNum2" placeholder="Max products"><span class="placeholder" data-placeholder="Max products"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="minSum2" placeholder="Min price"><span class="placeholder" data-placeholder="Min price"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="maxSum2" placeholder="Max price"><span class="placeholder" data-placeholder="Max price"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="logicCon2" placeholder="Logical condition"><span class="placeholder" data-placeholder="Logical condition"></span> </div> </div>';
                        + '</form>';
                    }
                    if (type == 3) {
                        details = '<form class="row contact_form" action="#" id="policyDetails" method="post" novalidate="novalidate">'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="store2" placeholder="Store name"><span class="placeholder" data-placeholder="Store name"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="address2" placeholder="Address"><span class="placeholder" data-placeholder="Address"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="isRegister2" placeholder="Is registered"><span class="placeholder" data-placeholder="Is registered"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="logicCon2" placeholder="Logical condition"><span class="placeholder" data-placeholder="Logical condition"></span> </div> </div>';
                        + '</form>';
                    }
                    policyDetails += ")"

                    $('#classELSE').append(details);
                    document.getElementById("classELSE").style.visibility = "visible";
                    document.getElementById("addPurchaseButton").style.visibility = "visible";
                }
                else
                    alert("User isn't logged in");
            });


            $("#policyButton4").click(function () {
                var userName = getCookie("LoggedUser");
                if (userName != null) {
                    var details;
                    var type = $('#value4').val();
                    document.getElementById("policyButton4").remove();

                    if (type == 0) {
                        details = '<form class="row contact_form" action="#" id="policyDetails" method="post" novalidate="novalidate">'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="store3" placeholder="Store name"><span class="placeholder" data-placeholder="Store name"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="productID3" placeholder="Product ID"><span class="placeholder" data-placeholder="Product ID"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="minNum3" placeholder="Min products"><span class="placeholder" data-placeholder="Min products"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="maxNum3" placeholder="Max products"><span class="placeholder" data-placeholder="Max products"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="logicCon3" placeholder="Logical condition"><span class="placeholder" data-placeholder="Logical condition"></span> </div> </div>'
                            + '</form>'
                    }
                    if (type == 1) {
                        details = '<form class="row contact_form" action="#" id="policyDetails" method="post" novalidate="novalidate">'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="store3" placeholder="Store name"><span class="placeholder" data-placeholder="Store name"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="productID3" placeholder="Product Name"><span class="placeholder" data-placeholder="Product Name"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="minNum3" placeholder="Min products"><span class="placeholder" data-placeholder="Min products"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="logicCon3" placeholder="Logical condition"><span class="placeholder" data-placeholder="Logical condition"></span> </div> </div>'
                            + '</form>'
                    }
                    if (type == 2) {
                        details = '<form class="row contact_form" action="#" id="policyDetails" method="post" novalidate="novalidate">'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="store3" placeholder="Store name"><span class="placeholder" data-placeholder="Store name"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="minNum3" placeholder="Min products"><span class="placeholder" data-placeholder="Min products"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="maxNum3" placeholder="Max products"><span class="placeholder" data-placeholder="Max products"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="minSum3" placeholder="Min price"><span class="placeholder" data-placeholder="Min price"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="maxSum3" placeholder="Max price"><span class="placeholder" data-placeholder="Max price"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="logicCon3" placeholder="Logical condition"><span class="placeholder" data-placeholder="Logical condition"></span> </div> </div>'
                            + '</form>'
                    }
                    if (type == 3) {
                        details = '<form class="row contact_form" action="#" id="policyDetails" method="post" novalidate="novalidate">'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="store3" placeholder="Store name"><span class="placeholder" data-placeholder="Store name"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="address3" placeholder="Address"><span class="placeholder" data-placeholder="Address"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="isRegister3" placeholder="Is registered"><span class="placeholder" data-placeholder="Is registered"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="logicCon3" placeholder="Logical condition"><span class="placeholder" data-placeholder="Logical condition"></span> </div> </div>'
                            + '</form>';
                    }



                    $('#classLogic1').append(details);
                    document.getElementById("classLogic1").style.visibility = "visible";
                }
                else
                    alert("User isn't logged in");
            });

            $("#policyButton5").click(function () {
                var userName = getCookie("LoggedUser");
                if (userName != null) {
                    var details;
                    var type0 = $('#value').val();
                    var type = $('#value5').val();
                    document.getElementById("policyButton5").remove();

                    if (type == 0) {
                        details = '<form class="row contact_form" action="#" id="policyDetails" method="post" novalidate="novalidate">'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="store4" placeholder="Store name"><span class="placeholder" data-placeholder="Store name"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="productID4" placeholder="Product ID"><span class="placeholder" data-placeholder="Product ID"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="minNum4" placeholder="Min products"><span class="placeholder" data-placeholder="Min products"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="maxNum4" placeholder="Max products"><span class="placeholder" data-placeholder="Max products"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="logicCon4" placeholder="Logical condition"><span class="placeholder" data-placeholder="Logical condition"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="logicConAll" placeholder="Logical condition for both"><span class="placeholder" data-placeholder="Logical condition for both"></span> </div> </div>'
                            + '</form>'
                    }
                    if (type == 1) {
                        details = '<form class="row contact_form" action="#" id="policyDetails" method="post" novalidate="novalidate">'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="store4" placeholder="Store name"><span class="placeholder" data-placeholder="Store name"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="productID4" placeholder="Product Name"><span class="placeholder" data-placeholder="Product Name"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="minNum4" placeholder="Min products"><span class="placeholder" data-placeholder="Min products"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="logicCon4" placeholder="Logical condition"><span class="placeholder" data-placeholder="Logical condition"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="logicConAll" placeholder="Logical condition for both"><span class="placeholder" data-placeholder="Logical condition for both"></span> </div> </div>'
                            + '</form>'
                    }
                    if (type == 2) {
                        details = '<form class="row contact_form" action="#" id="policyDetails" method="post" novalidate="novalidate">'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="store4" placeholder="Store name"><span class="placeholder" data-placeholder="Store name"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="minNum4" placeholder="Min products"><span class="placeholder" data-placeholder="Min products"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="maxNum4" placeholder="Max products"><span class="placeholder" data-placeholder="Max products"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="minSum4" placeholder="Min price"><span class="placeholder" data-placeholder="Min price"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="maxSum4" placeholder="Max price"><span class="placeholder" data-placeholder="Max price"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="logicCon4" placeholder="Logical condition"><span class="placeholder" data-placeholder="Logical condition"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="logicConAll" placeholder="Logical condition for both"><span class="placeholder" data-placeholder="Logical condition for both"></span> </div> </div>'
                            + '</form>'
                    }
                    if (type == 3) {
                        details = '<form class="row contact_form" action="#" id="policyDetails" method="post" novalidate="novalidate">'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="store4" placeholder="Store name"><span class="placeholder" data-placeholder="Store name"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="address4" placeholder="Address"><span class="placeholder" data-placeholder="Address"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="isRegister4" placeholder="Is registered"><span class="placeholder" data-placeholder="Is registered"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="logicCon4" placeholder="Logical condition"><span class="placeholder" data-placeholder="Logical condition"></span> </div> </div>'
                            + '<div class="col-md-12 form-group p_star"><div class="col-md-6 form-group p_star"><input type="text" class="form-control" id="logicConAll" placeholder="Logical condition for both"><span class="placeholder" data-placeholder="Logical condition for both"></span> </div> </div>'
                            + '</form>';
                    }

                    $('#classLogic2').append(details);
                    document.getElementById("classLogic2").style.visibility = "visible";
                    if (type0 != 4)
                        document.getElementById("addPurchaseButton").style.visibility = "visible";

                }
                else
                    alert("User isn't logged in");
            });

        });




    </script>

</asp:Content>
