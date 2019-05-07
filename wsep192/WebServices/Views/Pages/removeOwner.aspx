﻿<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="removeOwner.aspx.cs" Inherits="WebServices.Views.Pages.removeOwner" %>
 <asp:Content ID="content1" ContentPlaceHolderID="MainContent" runat="server"> 
<!--================Login Box Area =================-->
	<section class="login_box_area section-margin">
		<div class="container">
			<div class="row">
				<div class="col-lg-6">
					<div class="login_box_img">
						<div class="hover">
							<h4>LIKE TO LOGOUT?</h4>
							<p>click here to perform this action</p>
							<a class="button button-account" href="/Logout">LOGOUT NOW</a>
						</div>
					</div>
				</div>
				<div class="col-lg-6">
					<div class="login_form_inner register_form_inner">
						<h3>REMOVE EXISTING OWNER</h3>
						<form class="row login_form" action="#/" id="register_form" >
							
              <div class="col-md-12 form-group">
								<input type="text" class="form-control" id="ownerToRemove" name="ownerToRemove" placeholder="Owner To Remove" onfocus="this.placeholder = ''" onblur="this.placeholder = 'ownerToRemove'">
              </div>
              <div class="col-md-12 form-group">
								<input type="text" class="form-control" id="storeName" name="storeName" placeholder="Store Name" onfocus="this.placeholder = ''" onblur="this.placeholder = 'storeName'">
							</div>
                            
                             <small id="registerAlert" class="form-text text-muted text-Alert"></small>

							<div class="col-md-12 form-group">
								<input type="button" class="button button-register w-100" id="removeOwnerButton" value="Remove This Owner"></>
							</div>
						</form>
					</div>
				</div>
			</div>
		</div>
	</section>
  	<!--================End Login Box Area =================-->
     <script type="text/javascript">
         $(document).ready(function () {
             $("#removeOwnerButton").click(function () {
                 var ownerName = getCookie("LoggedUser");
                 if (ownerName != null) {
                     ownerToRemove = $("#ownerToRemove").val();
                     storeName = $("#storeName").val();
                     jQuery.ajax({
                         type: "GET",
                         url: baseUrl + "/api/user/removeOwner?ownerToRemove=" + ownerToRemove + "&storeName=" + storeName + "&ownerName=" + ownerName,
                         contentType: "application/json; charset=utf-8",
                         dataType: "json",
                         success: function (response) {
                             if (response == "Owner successfuly was removed") {
                                 document.cookie = "LoggedUser=" + ownerName
                                 alert(response);
                                 window.location.href = baseUrl + "/";
                             }
                             else {
                                 alert(response);
                             }
                         },
                         error: function (response) {
                             alert(response);
                         }
                     });
                 }
                 else
                     alert("already logged in");
             });
         });
    </script>
</asp:Content>