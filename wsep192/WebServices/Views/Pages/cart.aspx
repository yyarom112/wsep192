<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="WebServices.Views.Pages.cart" %>

<asp:Content ID="content1" ContentPlaceHolderID="MainContent" runat="server">


    <!--================Cart Area =================-->
    		 <section class="cart_area">
			<div class="container">
				<div class="row">
    			<div class="col-md-12 ftco-animate">
    				<div class="cart-list">
	    				<table class="table">
						    <thead class="thead-primary">
						      <tr class="text-center">
                                 <th>Remove</th>
						        <th>Product</th>
						        <th>Quantity</th>
						        <th>&nbsp;</th>
						      </tr>
						    </thead>
						    <tbody>
						      <tr class="text-center">
                                  <td class="remove">
                                  <input class="check" type="checkbox" id="p1"><br>
                                  </td>
						        <td class="product-name">
						        	<p>Far far away, behind the word mountains, far from the countries</p>
						        </td>

						        <td class="quantity">
						        	<div class="input-group mb-3">
					             	<input type="text" name="quantity" class="quantity form-control input-number" value="1" min="1" max="100">
					          	</div>
					          </td>
						      </tr><!-- END TR-->
                              <tr class="text-center">
                                  <td class="remove">
                                  <input class="check" type="checkbox" id="p2"><br> 
                                  </td>
						        <td class="product-name">
						        	<p>Far far away, behind the word mountains, far from the countries</p>
						        </td>
						        <td class="quantity">
						        	<div class="input-group mb-3">
					             	<input type="text" name="quantity" class="quantity form-control input-number" value="1" min="1" max="100">
					          	</div>
					          </td> 
						      </tr><!-- END TR-->
                                
						    </tbody> 
                 
						  </table>
                        <input type="button" class="button button-login w-100" id="ApplyButton" value="Apply"></>
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
            let store = null;
            if (user == null)
                user = guest;
            let searchParams = new URLSearchParams(window.location.search);
            if (searchParams.has('store')) { 
                store = searchParams.get('store');
            }
            else {
                alert('Failure');
            }

        $("#ApplyButton").click(function () {
            {
                jQuery.ajax({
                    type: "GET",
                    url: baseUrl + "/api/user/Cart?store=" + store + "&user=" + user,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response != "null") {
                            window.location.href = baseUrl + "/user/Cart";
                        }
                        else {
                            alert('Failure - Store/Cart not available');
                        }
                    },
                    error: function (response) {
                        console.log(response);
                        window.location.href = baseUrl + "/error";
                    }
                });
            }

        });
        });





    </script>
</asp:Content>
