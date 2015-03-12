(function (window, undefined) {
    "use strict";

    var doc = window.document;

    var utils = {
        writeCookie: function () {

        },
        insertBefore: function (el, ref) {
            ref = ref || doc.getElementsByTagName("script")[0];
            ref.parentNode.insertBefore(el, ref);
        },
        isFunction: function (func) {
            return func && typeof (func) == "function";
        }

    };

    var enhance = {};

    var supports = function () {
        var features = [];

        function registerFeature(featureName, support) {
            features[featureName] = support;
        }

        registerFeature('querySelector', 'querySelector' in doc);
        registerFeature('async', 'async' in document.createElement('script'));

        return function (feature) {
            return features[feature] || false;
        }
    }();

    enhance.supports = supports;

    function loadCSS(href, before, media) {
        var ss = doc.createElement('link');
        ss.rel = 'stylesheet';
        ss.href = href;
        ss.media = 'only x';

        var sheets = doc.styleSheets;
        utils.insertBefore(ss);

        function toggleMedia() {
            for (var i = 0; i < sheets.length; i++) {
                if (sheets[i].href && sheets[i].href.indexOf(href) > -1) {
                    ss.media = media || "all";
                    return;
                }
            }
            setTimeout(toggleMedia);
        }

        toggleMedia();
        return ss;
    }

    enhance.loadCSS = loadCSS;

    function loadJS(src, onload, onfail, timeout) {
        var script = doc.createElement('script'),
            fail,
            gate = false;
        
        if (utils.isFunction(onfail))
            fail = setTimeout(onfail, timeout);

        if (utils.isFunction(onload))
            script.onload = script.onreadystatechange = function () {
                var state = this.readyState;

                if (gate || state && state != "complete" && state != "loaded")
                    return;

                gate = true;
                script.onload = script.onreadystatechange == null;

                if (fail)
                    clearTimeout(fail);

                onload(this);
            }
        
        script.async = true;
        script.src = src;
        utils.insertBefore(script);
    }

    enhance.loadJS = loadJS;

    function getMeta(metaName) {
        var metas = doc.getElementsByTagName("meta");
        var meta;
        for (var i = 0; i < metas.length; i++) {
            if (metas[i].name && metas[i].name === metaName) {
                meta = metas[i];
                break;
            }
        }
        return meta;
    }

    enhance.getMeta = getMeta;

    var cookie = {
        get: function(name) {
            if (!name)
                return null;

            var cookiestring = "; " + doc.cookie;
            var _cookies = cookiestring.split("; " + name + "=");

            return (_cookies.length == 2) ? _cookies.pop().split(";").shift() : null;
        },
        set: function(name, value, days) {
            if (!name || !days)
                return;

            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            var expires = date.toUTCString();

            doc.cookie = name + "=; expires = " + expires + "; path=/";
        },
        expire: function(name) {
            this.set(name, false, -1);
        },
        enabled: function() {
            if (window.navigator.cookieEnabled) return true;
            var value = '_';
            var ret = cookie.set(value, value).get(value) === value;
            cookie.expire(value);
            return ret;
        }
    };

    enhance.cookie = cookie;
    
    var loadJsPipeline = function () {
        var pipeline = [];

        function executeStep() {
            var src,
            step = pipeline.shift(),
            stepCount = step.length,
            loadedCount = 0;

            while (src = step.shift()) {
                loadJS(src,
                function () {
                    loadedCount++;

                    if (loadedCount === stepCount && pipeline.length > 0)
                        executeStep();
                });
            }
        }

        return function (srcFiles) {
            pipeline = srcFiles.split('|');
            
            for (var i = 0; i < pipeline.length; i++)
                pipeline[i] = pipeline[i].split(',');

            if (pipeline.length == 0)
                return;

            executeStep();
        }
    }();

    enhance.loadJsPipeline = loadJsPipeline;

    window.enhance = enhance;
}(window));