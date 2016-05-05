/* Add here all your JS customizations */

$(document).ready(function () {

    // Preserve scroll position
    if (typeof localStorage !== 'undefined') {
	    if (localStorage.getItem('sidebar-left-position') !== null) {
		    var initialPosition = localStorage.getItem('sidebar-left-position'),
			    sidebarLeft = document.querySelector('#sidebar-left .nano-content');

		    sidebarLeft.scrollTop = initialPosition;
	    }
    }

	// GitHub activity stream
    if (urlPath == "/" || urlPath == "/Home/Index") {
    	GitHubActivity.feed({
	        username: "levans88",
	        //repository: "your-repo", // optional
	        selector: "#feed"
	        //limit: 20 // optional
    	});
    }

    // GHA Stream Nanoscroller fix
    //
    // Wait 2 seconds and re-initialize Nanoscroller... This is a poor workaround 
    // to redraw the nano scrollbar after github-activity.js creates new DOM content.
    $(window).load(function () {
        function resetNano(){
            $('.scrollable').nanoScroller(); }
        window.setTimeout( resetNano, 2000 );
    });

    // Clear nav-active class from nav menu
    if ( $('nav > ul.nav > li.nav-active').length ) {
    	$('nav > ul.nav > li.nav-active').removeClass('nav-active');
    }

    // Set nav-active class in nav menu for current URL path
    if (urlPath == "/Manage") $('#nav-examples, #nav-manage').addClass('nav-active nav-expanded');
    if (urlPath == "/Account/Login") $('#nav-login').addClass('nav-active');
    if (urlPath == "/Account/Register") $('#nav-register').addClass('nav-active');

    // Set home nav item as nav-active if no menu items are highlighted or path is "/"
    if ( !$('nav > ul.nav > li.nav-active' ).length || urlPath == "/" || urlPath == "/Home/Index") {
    	$('#nav-home').addClass('nav-active');
    }

    // Modals
    (function($) {

        'use strict';

        //Basic
        $('.modal-basic').magnificPopup({
            type: 'inline',
            preloader: false,
            modal: true
        });

        //Sizes
        $('.modal-sizes').magnificPopup({
            type: 'inline',
            preloader: false,
            modal: true
        });

        //Modal with CSS animation
        $('.modal-with-zoom-anim').magnificPopup({
            type: 'inline',

            fixedContentPos: false,
            fixedBgPos: true,

            overflowY: 'auto',

            closeBtnInside: true,
            preloader: false,
            
            midClick: true,
            removalDelay: 300,
            mainClass: 'my-mfp-zoom-in',
            modal: true
        });

        $('.modal-with-move-anim').magnificPopup({
            type: 'inline',

            fixedContentPos: false,
            fixedBgPos: true,

            overflowY: 'auto',

            closeBtnInside: true,
            preloader: false,
            
            midClick: true,
            removalDelay: 300,
            mainClass: 'my-mfp-slide-bottom',
            modal: true
        });

        //Modal Dismiss
        $(document).on('click', '.modal-dismiss', function (e) {
            e.preventDefault();
            $.magnificPopup.close();
        });

        //Modal Confirm
        $(document).on('click', '.modal-confirm', function (e) {
            e.preventDefault();
            $.magnificPopup.close();

            //new PNotify({
            //    title: 'Success!',
            //    text: 'Modal Confirm Message.',
            //    type: 'success'
            //});
        });

        //Form
        $('.modal-with-form').magnificPopup({
            type: 'inline',
            preloader: false,
            focus: '#name',
            modal: true,

            // When elemened is focused, some mobile browsers in some cases zoom in
            // It looks not nice, so we disable it:
            callbacks: {
                beforeOpen: function() {
                    if($(window).width() < 700) {
                        this.st.focus = false;
                    } else {
                        this.st.focus = '#name';
                    }
                }
            }
        });

        //Ajax
        $('.simple-ajax-modal').magnificPopup({
            type: 'ajax',
            modal: true
        });

    }).apply(this, [jQuery]);



});

// Manually called functions

// Apply appropriate style via 'has-error' class to form-groups that failed validation
function StyleValidationErrors() {

    $('span.field-validation-valid, span.field-validation-error').each(function () {
        $(this).addClass('help-inline');
    });

    // Find the divs containing the form controls, find the error spans within
    $('form').submit(function () {
        if ($(this).valid()) {
            $(this).find('div.form-group').each(function () {
                if ($(this).find('span.field-validation-error').length == 0) {
                    $(this).removeClass('has-error');
                }                
            });
        }
        else {
            $(this).find('div.form-group').each(function () {
                if ($(this).find('span.field-validation-error').length > 0) {
                    $(this).addClass('has-error');
                }                
            });
        }
    });

    $('form').each(function () {
        $(this).find('div.form-group').each(function () {
            if ($(this).find('span.field-validation-error').length > 0) {
                $(this).addClass('has-error');
            }
        });
    });
}