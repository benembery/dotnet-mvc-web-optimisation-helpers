(function (document, window) {

    var cssMetaName = 'fullcss';
    var _e = window.enhance;    

    //
    // Load Full CSS
    //
    var cssMeta = _e.getMeta(cssMetaName);
    if (cssMeta && !_e.cookie.get(cssMetaName)) {
        _e.loadCSS(cssMeta.content);
        _e.cookie.set(cssMetaName, "true", 7); // set cookie to mark this file fetched
       
    }

    //
    // Load Typekit
    //
    var config = {
            kitId: 'alo0rxc',
            scriptTimeout: 3000
        },
        h = document.documentElement;
    
    _e.loadJS('//use.typekit.net/' + config.kitId + '.js',
        function () { try { Typekit.load(config); } catch (e) { } },
        function () { h.className = h.className.replace(/\bwf-loading\b/g, "") + " wf-inactive"; },
        config.scriptTimeout);

    h.className += " wf-loading";

    //Bye bye old browsers
    if (!_e.supports('querySelector'))
        return;
    

    //TODO Update to pull from meta.
    var globalJS = '/bundles/js/jquery,/scripts/tests/a.js,/scripts/tests/b.js|/scripts/tests/1.js,/scripts/tests/2.js,/scripts/tests/3.js';

    loadJsPipeline(globalJS);
    


}(document, window));
