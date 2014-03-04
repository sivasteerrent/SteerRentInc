$(document).ready(function(e) {
    
	
	//for menu_box
	$('.menu_part').on({
		mouseenter:function(){
			$('.menu_space').stop(true,true).slideDown();
		},mouseleave:function(){
			$('.menu_space').stop(true,true).slideUp('fast');
		}
	});
	
	//body background
	$('body').backstretch("images/body_bg1.jpg");
	
	// bind slider
	var slidetabs = $('#slidetabs').slidetabs({
		orientation:'horizontal',
		autoHeight:true,
		touchSupport:true
	});
	

	//click tab delete
	$('#slidetabs').off('click').on('click', '.cancel_btn', function(e) {
        var eleIndex= $(this).parents('li:first').index();
			eleIndex= parseInt(eleIndex);
		slidetabs.removeTab(eleIndex+1);
    });
	
	//add tab 
	$('.page_name').off('click').on('click', function(){
		var ele= $(this),
			pageUrl= ele.attr('data-url');
		$('.menu_space').slideUp();
		$.ajax({
			type:'POST',
			url:pageUrl,
			success: function(response){
				var tabName= ele.text();
				var tabContent = response;
				slidetabs.addTab(tabName, tabContent, true);
			}
		});		
	});
	
	// datepicker
	$( "#datepicker" ).datepicker({dateFormat:"dd-mm-yy"});
	
	//user menu box
	$('.header_dropdown').on({
		mouseenter:function(){
			$('.user_part').stop(true,true).slideDown();
		},mouseleave:function(){
			$('.user_part').stop(true,true).slideUp('fast');
		}
	});
	
	//user select menu
	$('.user_item').click(function(e) {
        $('.user_part').stop(true,true).slideUp('fast');
    });
	
	//popup open
	$('.popup-link').magnificPopup({
		type: 'inline',
		closeBtnInside: true,
		preloader: false,
		midClick: true,
		closeOnBgClick:false,
		removalDelay: 300,
		mainClass: 'my-mfp-zoom-in',
		closeMarkup: '<a href="javascript:;" class="mfp-close"></a>'
	});

		
	//set width of hover menu
	setwidth();

});

function setwidth()
{
	var btnwidth =$('.user_name').outerWidth();
	$('.user_part').width(btnwidth);
}















