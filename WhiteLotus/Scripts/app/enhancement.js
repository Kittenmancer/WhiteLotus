$(function () {

    /**
     * Adds javascript-based enhancements.
     *
     * - Confirmation messages for links.
     * - POST links
     * - Async form submission
     */

    // ReSharper disable InconsistentNaming
    // Constants
    var SUBMIT_LOADING_TEXT = "Loading...";
    // ReSharper restore InconsistentNaming

    function confirmation(element) {
        var message = $(element).attr("data-confirm");
        return confirm(message);
    }

    function postMethod(element) {
        var action = $(element).attr('href');
        var target = $(element).attr('target');
        var form = $('<form method="post" action="' + action + '"></form>');
        if (target) { form.attr('target', target); }
        form.hide().appendTo('body');
        form.submit();
    }

    function showLoadingMessage($element) {
        var loadingMessage = $element.attr("data-loading-message");
        if (loadingMessage !== undefined) {
            CristalWeb.showStatusMessage(loadingMessage);
        }
    }

    function performAjaxOperation(url, method, data) {
        var options = { type: method };
        if (data !== undefined && data !== null)
            options['data'] = data;

        $.ajax(url, options).always(function (result) {
            CristalWeb.hideStatusMessage();

            if (result.status !== undefined && result.status != 200) {  // Error
                CristalWeb.showTimedStatusMessage("Error: (" + result.status + ") " + result.statusText);
            } else {  // Success
                if (result.Message !== undefined)
                    CristalWeb.showTimedStatusMessage(result.Message);
            }
        });
    }

    // Set up confirmation and POSTing links
    $('a[data-confirm], a[data-method="post"]').click(function (e) {
        var $this = $(this);
        var hasConfirm = $this.attr('data-confirm') !== undefined;
        var hasMethod = $this.attr('data-method') !== undefined;
        var canContinue = true;

        // Show confirmation if necessary
        if (hasConfirm) {
            canContinue = confirmation($this);
        }

        // Handles method=post (observing the ability to continue or not)
        if (hasMethod && canContinue) {

            var submit = $this.attr('data-submit');
            if (hasMethod && submit !== undefined && submit == 'ajax') {
                // this is a link to be actioned via an ajax-submitted POST

                showLoadingMessage($this);
                performAjaxOperation($this.attr('href'), 'POST', '');

            } else {
                postMethod($this);
            }

            e.preventDefault();
            return;
        }

        if (!canContinue)
            e.preventDefault();
    });

    // Set up forms to POST via AJAX where required
    /*$(document).on('click', 'form[data-submit="ajax"] input[type="submit"], form[data-submit="ajax"] button[type="submit"]', function (e) {
        var $form = $(this).parents("form");

        // Get all the data to be submitted
        var dataString = $form.serialize();

        showLoadingMessage($form);
        performAjaxOperation($form.attr("action"), $form.attr("method"), dataString);

        e.preventDefault();
    });
*/
    /*
    // Set up submit buttons to not be clickable more than once (prevent double-submission and give some feedback)
    $('button[type="submit"], input[type="submit"], button#submit').click(function() {
        var $btn = $(this);
        $btn.attr('disabled', 'disabled');

        if ($btn.is("input"))
            $btn.val(SUBMIT_LOADING_TEXT);
        else
            $btn.text(SUBMIT_LOADING_TEXT);
    });
    */

});
