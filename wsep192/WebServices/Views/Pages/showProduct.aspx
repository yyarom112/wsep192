<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="showProduct.aspx.cs" Inherits="WebServices.Views.Pages.showProduct" %>

<asp:Content ID="content1" ContentPlaceHolderID="MainContent" runat="server">


    <!--================Cart Area =================-->
    		 <section class="cart_area">
			<div class="container" >
				<div class="row" id="head">
    			<div class="col-md-12 ftco-animate">
    				<div class="cart-list">
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
                        <input type="button" style="max-width: 250px; float:right;" class="button button-login w-100" id="ApplyButton" value="Apply"></>
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
            let store = null,productName = null,quantity = null;
            if (user == null)
                user = guest;
            let searchParams = new URLSearchParams(window.location.search);
            if (searchParams.has('store') && searchParams.has('name')&& searchParams.has('quantity')) {
                store = searchParams.get('store');
                productName = searchParams.get('name');
                quantity = searchParams.get('quantity');
                $('#head').before('<h3>Search Results</h3>');
                
                //console.log(products);
                var i = 0;
                var str = '';
                for (i = 0; i < 1; i++) {
                    //if (i % 3 == 0) {
                        str += '<tr class="text-center"><td class="product-name"><p>' +
                            productName + '</p></td>';
                    //}
                    //else if (i % 3 == 1) {
                        str += '<td class="store-name"><p>'+ store + '</p></td >';
                   // }
                   // else {
                        str += '<td class="quantity"><p>' + quantity + '</p></td ></tr >';
                    //}
                }



                $('#table').append(str);
            }
            else {
                alert('Failure');
            }
            
        });
    </script>
</asp:Content>
