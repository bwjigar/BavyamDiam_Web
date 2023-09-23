
 

$(document).ready(function() {
$(".card").click(function () {
    $(".card").removeClass("active");
    $(this).addClass("active");   
});
});

$(document).ready(function() {
$(".common-li li").click(function () {
    $(this).addClass("active");   
});
});

$(document).ready(function() {
$("ul.search li a").click(function () {
	$("ul.search li a").removeClass("active");
    $(this).addClass("active"); 
});
});

$(function () {
  $('[data-toggle="tooltip"]').tooltip()
})
/*----------------Testimonial--------------------*/
jQuery(document).ready(function($) {
        		"use strict";
        		//  TESTIMONIALS CAROUSEL HOOK
		        $('.aw-inner-testimonial').owlCarousel({
		            loop: true,
		            center: true,
		            
		            margin: 0,
		            autoplay: true,
		            dots:true,
		            autoplayTimeout: 2000,
		            smartSpeed: 450,
		            responsive: {
		              0: {
		                items: 1
		              },
		              768: {
		                items: 1
		              },
		              1170: {
		                items: 1
		              }
		            }
		        });
        	});