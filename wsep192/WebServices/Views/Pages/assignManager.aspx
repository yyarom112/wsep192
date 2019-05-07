<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="assignManager.aspx.cs" Inherits="WebServices.Views.Pages.assignManager" %>
 <asp:Content ID="content1" ContentPlaceHolderID="MainContent" runat="server"> 
<!--================Login Box Area =================-->
	<section class="login_box_area section-margin">
				
					<div class="login_form_inner register_form_inner">
						<h3>Assign New Manager</h3>
						<form class="row login_form" action="#/" id="register_form" >
							
              <div class="col-md-12 form-group">
								<input type="text" class="form-control" id="userToAssign" name="userToAssign" placeholder="User To Assign" onfocus="this.placeholder = ''" onblur="this.placeholder = 'userToAssign'">
              </div>
              <div class="col-md-12 form-group">
				<div><input type="text" class="form-control" id="storeName" name="storeName" placeholder="Store Name" onfocus="this.placeholder = ''" onblur="this.placeholder = 'storeName'"></div>
							<div>
                            <input class="per" type="checkbox" id="p1" value="AddDiscountPolicy">AddDiscountPolicy<br>
                            <input class="per" type="checkbox" id="p2" value="AddPurchasePolicy">AddPurchasePolicy<br>
                            <input class="per" type="checkbox" id="p3" value="CreateNewProductInStore">CreateNewProductInStore<br>
                            <input class="per" type="checkbox" id="p4" value="AddProductsInStore">AddProductsInStore<br>
                            <input class="per" type="checkbox" id="p5" value="RemoveProductsInStore">RemoveProductsInStore<br>
                            <input class="per" type="checkbox" id="p6" value="EditProductInStore">EditProductInStore<br>
                            <input class="per" type="checkbox" id="p7" value="CommunicationWithCustomers">CommunicationWithCustomers<br>
                            <input class="per" type="checkbox" id="p8" value="PurchasesHistory">PurchasesHistory<br>
                    </div>         
                  </div>
							<div class="col-md-12 form-group">
								<input type="button" class="button button-login w-100" id="assignManagerButton" value="Assign This Manager"></>
							</div>
						</form>
					</div>
				
	</section>
  	<!--================End Login Box Area =================-->
          <script type="text/javascript">
              $(document).ready(function () {
                  $("#assignManagerButton").bind("click", function (e) {
                      var permissions = "";
                      $(".per").each(function (e) {
                          if ($(this).is(':checked'))
                              permissions = permissions + $(this).attr('value') + " ";
                      },)
                      var ownerName = getCookie("LoggedUser");
                      if (ownerName != null) {
                          userToAssign = $("#userToAssign").val();
                          storeName = $("#storeName").val();
                          jQuery.ajax({
                              type: "GET",
                              url: baseUrl + "/api/user/assignManager?ownerName=" + ownerName + "&userToAssign=" + userToAssign + "&storeName=" + storeName + "&permissions=" + permissions,
                              contentType: "application/json; charset=utf-8",
                              dataType: "json",
                              success: function (response) {
                                  if (response == "Manager successfuly was assigned") {
                                      document.cookie = "LoggedUser=" + ownerName
                                      alert(response);
                                  }
                                  else {
                                      alert("Error" + response);
                                  }
                              },
                              error: function (response) {
                                  alert(response);
                              }
                          });
                      }
                      else
                          alert("User isn't logged in");
                  });
              });


    </script>
</asp:Content>