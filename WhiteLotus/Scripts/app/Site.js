
var WhiteLotus = WhiteLotus || {};
WhiteLotus.Urls = WhiteLotus.Urls || {};
WhiteLotus.Ids = WhiteLotus.Ids || {};

; (function () {

    var NOTICE_TIMEOUT_DURATION = 20000;


    function _initNoticeTimeout(notice) {
        var $notice = $(notice);
        var t = setTimeout(function () {
            $notice.removeData("timeout");
            $notice.slideUp();
        }, NOTICE_TIMEOUT_DURATION);
        $notice.data("timeout", t);
    };


    function removeNotice($notice) {
        var timeout = $notice.data("timeout");
        clearTimeout(timeout);
        $notice.removeData("timeout");
        $notice.slideUp();
    };


    function removeAllNotices() {
        $(".notice, .error").each(function (_, n) { removeNotice($(n)); });
    };


    function _attachCloseButtonHandler($closeButton) {
        $closeButton.click(function () {
            var $notice = $(this).parents(".notice, .error");
            removeNotice($notice);
        });
    };


    function _displayLoadingSpinner(requestsInProgress) {
        if (requestsInProgress > 0) {
            $("#load-spinner").fadeIn(160);
        } else {
            $("#load-spinner").fadeOut(160);
        }
    };

    function createNotice(message, options) {
        options = $.extend({}, { html: true }, options);

        removeAllNotices();

        var $notice = $(document.createElement("div"));
        $notice.addClass("container notice");
        options.html ? $notice.html(message) : $notice.text(message);
        $notice.hide();

        var $closeButton = $(document.createElement("button"));
        $closeButton.attr("type", "button");
        $closeButton.addClass("close");
        $closeButton.html("&times;");

        $notice.prepend($closeButton);

        $("#notices-container").append($notice);
        $notice.slideDown();
        _initNoticeTimeout($notice);
        _attachCloseButtonHandler($closeButton);
    };

    function createError(message, options) {
        options = $.extend({}, { html: true }, options);

        removeAllNotices();

        var $error = $(document.createElement("div"));
        $error.addClass("container error");
        options.html ? $error.html(message) : $error.text(message);
        $error.hide();

        var $closeButton = $(document.createElement("button"));
        $closeButton.attr("type", "button");
        $closeButton.addClass("close");
        $closeButton.html("&times;");

        $error.prepend($closeButton);

        $("#errors-container").append($error);
        $error.slideDown();
        _initNoticeTimeout($error);
        _attachCloseButtonHandler($closeButton);
    };

    WhiteLotus.createNotice = createNotice;
    WhiteLotus.removeAllNotices = removeAllNotices;
    WhiteLotus.createError = createError;


    $(function () {

        var currentAjaxRequestsInProgress = 0;

        $(".notice, .error").each(function (_) {
            _initNoticeTimeout(this);
            _attachCloseButtonHandler($(this).find("button.close"));
        });

        $(document)
            .ajaxSend(function () {
                currentAjaxRequestsInProgress++;
                _displayLoadingSpinner(currentAjaxRequestsInProgress);
            }).ajaxComplete(function () {
                currentAjaxRequestsInProgress--;
                _displayLoadingSpinner(currentAjaxRequestsInProgress);
            });

        $('.has-tooltip').tooltip();
    });

})();
