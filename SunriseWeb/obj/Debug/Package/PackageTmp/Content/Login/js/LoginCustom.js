

function open_side() {
	document.getElementById("myScrollspy").style.width = "250px";
}
function close_side() {
	document.getElementById("myScrollspy").style.width = "0";
}

$(function () {

	$('#login-form-link').click(function (e) {
		$("#login-form").delay(100).fadeIn(100);
		$("#forgot-form").fadeOut(100);
		e.preventDefault();
	});
	$('#forgot-form-link').click(function (e) {
		$("#forgot-form").delay(100).fadeIn(100);
		$("#login-form").fadeOut(100);
		e.preventDefault();
	});

});

