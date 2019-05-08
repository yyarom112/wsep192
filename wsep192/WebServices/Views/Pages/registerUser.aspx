<%@ Page Title="Register Page" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="registerUser.aspx.cs" Inherits="WebServices.Views.Pages.Register" %>

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
						<h3>Create an account</h3>
						<form class="row login_form" action="#/" id="register_form" >
							<div class="col-md-12 form-group">
								<input type="text" class="form-control" id="name" name="name" placeholder="Username" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Username'">
							</div>
							
              <div class="col-md-12 form-group">
								<input type="text" class="form-control" id="password" name="password" placeholder="Password" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Password'">
              </div>
              <div class="col-md-12 form-group">
								<input type="text" class="form-control" id="confirmPassword" name="confirmPassword" placeholder="Confirm Password" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Confirm Password'">
							</div>
                            
							<div class="col-md-12 form-group">
								<input type="button" class="button button-login w-100" id="registerButton" value="Register"></>
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
	    $("#registerButton").click(function(){
		
		    username=$("#name").val();
            pass = $("#password").val();
            pass2 = $("#confirmPassword").val();
            if (pass != pass2) {
                alert("Passwords does not match");
            }
            else {
                jQuery.ajax({
                    type: "GET",
                    url: baseUrl+"/api/user/RegisterUser?username=" + username + "&password=" + pass,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response == "User successfuly registered") {
                            alert(response);
                            window.location.href = baseUrl+"/";
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
	    });
    });

</script>



</asp:Content>