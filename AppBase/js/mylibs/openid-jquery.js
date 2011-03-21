var openid = {
    init: function (input_id) {

        $('.oid').each(function (index) {
            $(this).click(function (sender) {
                var currentLink = $(this);

                var input_area = $('#openid_input_area');

                var html = '';
                var id = 'openid_username';
                var value = '';
                var label = currentLink.data('label');
                var style = '';

                if (label) {
                    html = '<p>' + label + '</p>';
                }
                if (currentLink.hasClass('openid') == 'OpenID') {
                    id = this.input_id;
                    value = 'http://';
                    style = 'background:#FFF url(' + this.img_path + 'openid-inputicon.gif) no-repeat scroll 0 50%; padding-left:18px;';
                }
                html += '<input id="' + id + '" type="text" style="' + style + '" name="' + id + '" value="' + value + '" />' +
					'<input id="openid_submit" data-url="' + currentLink.data('url') + '" type="submit" value="Sign-In"/>';

                input_area.html(html);

                $('#' + id).focus();
            });
        });

        this.input_id = input_id;

        $('#openid_form').submit(this.submit);
    },
    /* Sign-in button click */
    submit: function () {

        var url = $('#openid_submit').data('url');
        if (url) {
            url = url.replace('{username}', $('#openid_username').val());
            $('#openid_identifier').val(url);
        }
        return true;
    },
    highlight: function (box_id) {

        // remove previous highlight.
        var highlight = $('#openid_highlight');
        if (highlight) {
            highlight.replaceWith($('#openid_highlight a')[0]);
        }
        // add new highlight.
        $('.' + box_id).wrap('<div id="openid_highlight"></div>');
    }
};
