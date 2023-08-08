

	$(document).ready(function(e) {
	   $('[data-toggle="tooltip"]').tooltip();
	});

	//Zoom Image
	$('.img-zoom').magnificPopup({
		type: 'image'
	});
	
	$(document).ready(function () {
		$('.video .full-video').on('click', function () {
			$('.f-video').addClass('active');
		});
		$('.f-video i').on('click', function () {
			$('.f-video').removeClass('active');
		});
	});
	
	//  ACCORDIAN COLLAPSE
	$('.collapse').on('shown.bs.collapse', function(){
		$(this).parent().find(".fa-plus-circle").removeClass("fa-plus-circle").addClass("fa-minus-circle");
	}).on('hidden.bs.collapse', function(){
		$(this).parent().find(".fa-minus-circle").removeClass("fa-minus-circle").addClass("fa-plus-circle");
	});
	