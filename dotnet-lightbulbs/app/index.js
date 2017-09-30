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
                $("#runRoom").addClass("disabled");
            },
            unhighlight: function(element, errorClass, validClass) {
                $(element).parentsUntil(".form-group").removeClass(errorClass).addClass(validClass);
            },
            success: function() {
                $("#runRoom").removeClass("disabled");
            },
            submitHandler: function(form) {
                let numberOfLightBulbs = $("#numberOfLightBulbs").val();
                let numberOfPersons = $("#numberOfPersons").val();
                let uri = "http://localhost:5000/api/lightbulbs/" + numberOfLightBulbs + "/persons/" + numberOfPersons;
                $.getJSON(uri, function(data) {
                    console.log(data);
                });
            }
        });
    });
}(jQuery));
