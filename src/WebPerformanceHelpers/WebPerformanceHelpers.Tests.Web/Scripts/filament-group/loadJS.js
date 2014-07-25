/*! loadJS: load a JS file asynchronously. [c]2014 @scottjehl, Filament Group, Inc. (Based on http://goo.gl/REQGQ by Paul Irish). Licensed MIT */
function loadJS( src, callback){
    "use strict";
    var ref = window.document.getElementsByTagName("script")[0];
	var script = window.document.createElement( "script" );
	script.src = src;
	if (callback) {
	    script.addEventListener('load', function (e) { callback(null, e); }, false);
	}
	ref.parentNode.insertBefore( script, ref );
	return script;
}
