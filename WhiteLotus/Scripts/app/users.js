$(function() {


    $('#Password').focus();
    $('#generatePassword').click(function (e) {
        $('#generatePassword').popover('destroy');

        var sPassword = "";
        var length = 10;

        for (i = 0; i < length; i++) {
            numI = getRandomNum();
            while (checkPunc(numI)) {
                numI = getRandomNum();
            }
            sPassword = sPassword + String.fromCharCode(numI);
        }

        $('#password').val(sPassword);
        $('#confirmPassword').val(sPassword);
        $('#generatePassword').popover({
            content: '<strong>Password:</strong> <code>'
                + sPassword + '</code> <button class="btn btn-xs btn-default" id="copy-button" data-clipboard-text="'
                + sPassword + '" title="Copy to clipboard">Copy</button> <span id="copySuccess" class="glyphicon glyphicon-ok"></span> ' +
                '<button id="closePopover" type="button" class="close" aria-hidden="true">&times;</button>',
            trigger: 'manual',
            html: true
        });
        $('#generatePassword').popover('show');

        $('#generatePassword').on('shown.bs.popover', function () {
            var client = new ZeroClipboard(document.getElementById("copy-button"));
            var timeout = null;
            client.on('complete', function (client, args) {
                $('#copySuccess').show();
                if (timeout) clearTimeout(timeout);
                timeout = setTimeout(function () { $('#copySuccess').fadeOut(); }, 3000);
            });


            $('#closePopover').click(function () {
                $('#generatePassword').popover('hide');
            });
        });

        $('#generatePassword').on('hidden.bs.popover', function () {
            ZeroClipboard.destroy();
        });

        //return true;
        e.preventDefault();
    });
    function getRandomNum() {

        // between 0 - 1
        var rndNum = Math.random();

        // rndNum from 0 - 1000
        rndNum = parseInt(rndNum * 1000);

        // rndNum from 33 - 127
        rndNum = (rndNum % 94) + 33;

        return rndNum;
    };
    function checkPunc(num) {

        if ((num >= 33) && (num <= 47)) { return true; }
        if ((num >= 58) && (num <= 64)) { return true; }
        if ((num >= 91) && (num <= 96)) { return true; }
        if ((num >= 123) && (num <= 126)) { return true; }

        return false;
    };


    $('#saveButton').click(function () {

        $('#blankError').hide();
        $('#noMatchError').hide();
        var box1 = $('#password').val();
        var box2 = $('#confirmPassword').val();

        if ($.trim(box1).length < 10) {
            WhiteLotus.createError("Password must be at least 10 characters in length.");
            return false;
        }

        if (box1 == box2) {
            $('#form1').submit();
        } else {
            WhiteLotus.createError("Passwords entered do not match.");
            return false;
        }
    });

    $('#user-save-trigger').click(function(e) {
        e.preventDefault();
        var inputs = $('.req');
        var errors = 0;

        $.each(inputs, function(_, input) {
            if ($(input).val().trim().length == 0) {
                errors += 1;
                $(input).addClass('has-error');
            }
        });

        if (errors != 0) {
            WhiteLotus.createError("Please fill in all fields.");
        } else {
            $('#user-form').submit();
        }

    });

    $('#user-create-trigger').click(function(e) {
        e.preventDefault();
        var inputs = $('.req');
        var errors = 0;

        $.each(inputs, function (_, input) {
            if ($(input).val().trim().length == 0) {
                errors += 1;
                $(input).addClass('has-error');
            }
        });
        
        $('#blankError').hide();
        $('#noMatchError').hide();
        var box1 = $('#password').val();
        var box2 = $('#confirmPassword').val();

        if ($.trim(box1).length < 10) {
            WhiteLotus.createError("Password must be at least 10 characters in length.");
            return false;
        }

        if (box1 != box2) {
            WhiteLotus.createError("Passwords entered do not match.");
            return false;
        }

        if (errors != 0) {
            WhiteLotus.createError("Please fill in all fields.");
        } else {
            $('#user-form').submit();
        }
    });


    $('#passwordButton').click(function (e) {
        e.preventDefault();


        $('#passwordPopup').modal();
    });

    $('#passwordPopup').on('hidden.bs.modal', function (e) {
        $('#generatePassword').popover('destroy');
        $('#user_Password').val("");
        $('#confirmPassword').val("");
        $('#passwordMismatch').hide();
    });


    $('#passwordSubmit').click(function (e) {
        e.preventDefault();
        $('#passwordEmpty').hide();

        if ($('#password').val().length < 10) {
            $('#passwordEmpty').show();
            return;
        }

        if ($('#password').val() === $('#confirmPassword').val()) {
            $.post(WhiteLotus.Urls.changeOtherUserPassword,
                {
                    id: WhiteLotus.Ids.userId,
                    password: "" + $('#password').val() + ""
                },
                function () {
                    $('#passwordPopup').modal('hide');
                    WhiteLotus.createNotice("Password changed successfully.");
                });
        } else {
            $('#passwordMismatch').show();
        }
    });
});