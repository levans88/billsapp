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
});