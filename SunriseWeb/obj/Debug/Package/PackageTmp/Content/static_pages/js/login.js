

		
		$(function() {

    $('#login-form-link').click(function(e) {
		$("#login-form").delay(100).fadeIn(100);
 		$("#forgot-form").fadeOut(100);
		e.preventDefault();
	});
	$('#forgot-form-link').click(function(e) {
		$("#forgot-form").delay(100).fadeIn(100);
 		$("#login-form").fadeOut(100);
		e.preventDefault();
	});

});

/*---------------- login-page-navbar-collapse -------------------*/
$(document).ready(function () {
            $('#sidebarCollapse').on('click', function () {
                $('#myScrollspy').toggleClass('active');
				$('.ragistration').toggleClass('active');
				$('.login-form').toggleClass('active');
            });
        });
      