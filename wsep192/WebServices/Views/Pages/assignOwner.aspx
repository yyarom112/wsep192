<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="assignOwner.aspx.cs" Inherits="WebServices.Views.Pages.assignOwner" %>
 <asp:Content ID="content1" ContentPlaceHolderID="MainContent" runat="server"> 
  <!--================Login Box Area =================-->
	<section class="login_box_area section-margin">
		<div class="container">
			<div class="row">
				<div class="col-lg-6">
					<div class="login_box_img">
						<div class="hover">
							<h4>Already have an account?</h4>
							<p>There are advances being made in science and technology everyday, and a good example of this is the</p>
							<a class="button button-account" href="/LoginUser">Login Now</a>
						</div>
					</div>
				</div>
				<div class="col-lg-6">
					<div class="login_form_inner register_form_inner">
						<h3>ASSIGN NEW OWNER</h3>
						<form class="row login_form" action="#/" id="register_form" >
							<div class="col-md-12 form-group">
								<input type="text" class="form-control" id="ownerName" name="ownerName" placeholder="Owner Name" onfocus="this.placeholder = ''" onblur="this.placeholder = 'ownerName'">
							</div>
							
              <div class="col-md-12 form-group">
								<input type="text" class="form-control" id="userToAssign" name="userToAssign" placeholder="User To Assign" onfocus="this.placeholder = ''" onblur="this.placeholder = 'userToAssign'">
              </div>
              <div class="col-md-12 form-group">
								<input type="text" class="form-control" id="storeName" name="storeName" placeholder="Store Name" onfocus="this.placeholder = ''" onblur="this.placeholder = 'storeName'">
							</div>
                            
                             <small id="registerAlert" class="form-text text-muted text-Alert"></small>

							<div class="col-md-12 form-group">
								<input type="button" class="button button-register w-100" id="assignOwnerButton" value="Assign This Owner"></>
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
             $("#assignOwnerButton").click(function () {
                 var ownerName = getCookie("LoggedUser");
                 if (ownerName != null) {
                     console.log("OK");
                     userToAssign = $("#userToAssign").val();
                     storeName = $("#storeName").val();
                     jQuery.ajax({
                         type: "GET",
                         url: baseUrl + "/api/user/AssignOwner?ownerName=" + ownerName + "&userToAssign=" + userToAssign + "&storeName" + storeName,
                         contentType: "application/json; charset=utf-8",
                         dataType: "json",
                         success: function (response) {
                             if (response == "User successfuly was assigned") {
                                 document.cookie = "LoggedUser=" + username
                                 alert(response);
                                 window.location.href = baseUrl + "/";
                             }
                             else {
                                 $("#loginAlert").html('Failure - ' + response);
                             }
                         },
                         error: function (response) {
                             console.log(response);
                             window.location.href = baseUrl + "/error";
                         }
                     });
                 }
                 else
                     $("#loginAlert").html('Failure - ' + " already logged in");

             });
         });

    </script>
</asp:Content>