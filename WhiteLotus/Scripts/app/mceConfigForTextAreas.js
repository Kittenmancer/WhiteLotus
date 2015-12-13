$(function() {
    dialog_path = MapleGroup.Urls.mceDialogPath;
    tinymce.init({
        selector: "textarea.edit-box",
        theme: "modern",
        plugins: [
            "advlist autolink lists link image charmap print anchor",
            "searchreplace visualblocks code fullscreen",
            "insertdatetime media table contextmenu paste filemanager media"
        ],
        add_unload_trigger: false,
        schema: "html5",
        inline: false,
        toolbar: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright | bullist numlist outdent indent | link image media filemanager",
        statusbar: false,
        relative_urls: true,
        remove_script_host: false,
        browser_spellcheck: true,
        extended_valid_elements: "iframe[src|frameborder|style|scrolling|class|width|height|name|align]",
        convert_urls: false,
        content_css: MapleGroup.Urls.editorCss,
        min_height: 350,
        max_height:400
        /*document_base_url: '~/Views/Upload/Show'*/
    });
});