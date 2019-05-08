<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShoppingCart.aspx.cs" Inherits="WebServices.Views.Pages.shoppingCart" %>

<asp:Content ID="content1" ContentPlaceHolderID="MainContent" runat="server">


    <!--================Cart Area =================-->
    <section class="cart_area">
        <div class="container">
            <div class="cart_inner">				
					<div class="login_form_inner">
						<h3>Enter Store Name</h3>
						<form class="row login_form" action="#/" id="contactForm" >
							<div class="col-md-12 form-group">
								<input type="text" class="form-control" id="storeName" name="name" placeholder="Store Name" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Store Name'">
							</div>
							<div class="col-md-12 form-group">
                                <input type="button" class="button button-login w-100" id="showCartButton" value="Show"></>
							</div>
						</form>
				
				</div>
            </div>
        </div>
    </section>
    <!--================End Cart Area =================-->
    <script type="text/javascript">

        $(document).ready(function () {
            $("#showCartButton").click(function () {
                var user = getCookie("LoggedUser");
                var guest = getCookie("GuestUser");
                if (user == null)
                    user = guest;
                var store = $("#storeName").val();
                jQuery.ajax({
                    type: "GET",
                    url: baseUrl + "/api/user/ShoppingCart?store=" + store + "&user=" + user,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response != "null") {
                            window.location.href = baseUrl + "/Cart?store=" + store + "&cart" + response;
                        }
                        else {
                            alert('Failure - Store/Cart not available');
                        }
                    },
                    error: function (response) {
                        console.log(response);
                        alert('Failure');
                    }
                });


            });
        });





    </script>
</asp:Content>
