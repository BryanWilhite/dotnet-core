/// <reference path="./node_modules/@types/jquery/index.d.ts" />
/*jslint this, white, browser */
/*global window, jQuery */

(function($) {
    "use strict";

    $(function() {
        const form = $("#LightBulbForm");

        form.validate({
            errorClass: "has-error",
            validClass: "has-success",
            errorElement: "span",
            errorPlacement: function(error, element) {
                if (element.attr("type") != "number") {
                    return;
                }
                let group = $(element).parent(".input-group");
                error.addClass("help-block").insertAfter(group);
            },
            highlight: function(element, errorClass, validClass) {
                $(element).parentsUntil(".form-group").addClass(errorClass).removeClass(validClass);
                $("#RunRoom").addClass("disabled");
            },
            unhighlight: function(element, errorClass, validClass) {
                $(element).parentsUntil(".form-group").removeClass(errorClass).addClass(validClass);
            },
            success: function() {
                $("#RunRoom").removeClass("disabled");
            },
            submitHandler: function(form) {
                let numberOfLightBulbs = $("#numberOfLightBulbs").val();
                let numberOfPersons = $("#numberOfPersons").val();
                let uri = "http://localhost:5000/api/lightbulbs/" + numberOfLightBulbs + "/persons/" + numberOfPersons;
                $.getJSON(uri, function(data) {
                    const container = $("#BulbContainer");
                    const fadeTime = 400;
                    const count = data.length;
                    container.empty().fadeIn(fadeTime);

                    (function loop(i) {
                        setTimeout(function() {

                            let d = data[i];
                            let src = d.isOn ? "./bulb-on.svg" : "./bulb-off.svg";
                            let html = '<img alt="' + d.ordinal +
                                ' (' + (d.isOn ? "on" : "off") + ')"' +
                                ' width="64" src="' + src + '" />';
                            let img = $(html);
                            container.append(img);
                            img.hide().fadeIn(fadeTime);

                            if (++i < count) {
                                loop(i);
                            }
                        }, fadeTime)
                    })(0);

                });
            }
        });
    });
}(jQuery));
