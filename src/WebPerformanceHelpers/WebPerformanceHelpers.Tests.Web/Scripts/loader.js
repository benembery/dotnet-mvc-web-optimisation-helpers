(function (window, undefined) {
    var doc = window.document,
        docEl = window.documentElement,
        head = doc.head;
    
    function insertBefore(el, ref) {
        ref = ref || doc.getElementsByTagName("script")[0];
        ref.parentNode.insertBefore(el, ref);
    }

    window.enhance = {
        loadCSS: function(href,before,media) {
            var ss = doc.createElement('link');
            ss.rel = 'stylesheet';
            ss.href = href;
            ss.media = 'only x';

            var sheets = doc.styleSheets;
            insertBefore(ss);
            
            function toggleMedia() {
                var defined;
                for (var i = 0; i < sheets.length; i++) {
                    if (sheets[i].href && sheets[i].href.indexOf(href) > -1) {
                        defined = true;
                    }
                }
                if (defined) {
                    ss.media = media || 'all';
                }
                else {
                    setTimeout(toggleMedia);
                }
            }

            toggleMedia();
            return ss;
        },
        loadJS: function(src, onload) {
            var script = doc.createElement('script');
            script.src = src;
            script.async = true;
            if (onload && typeof(onload) == 'function') {
                script.onload = script.onreadystatechange = onload;
            }

            insertBefore(script);
        }
    };


}(window));

(function(d, window) {
    var config = {
            kitId: 'alo0rxc',
            scriptTimeout: 3000
        },
        h = d.documentElement,
        t = setTimeout(function() {
            h.className = h.className.replace(/\bwf-loading\b/g, "") + " wf-inactive";
        }, config.scriptTimeout),
        f = false,
        a;

    window.enhance.loadJS('//use.typekit.net/' + config.kitId + '.js', function() {
        a = this.readyState;
        if (f || a && a != "complete" && a != "loaded") return;
        f = true;
        clearTimeout(t);
        try {
            Typekit.load(config);
        } catch (e) {
        }
    });
        h.className += " wf-loading";
       
})(document, window);