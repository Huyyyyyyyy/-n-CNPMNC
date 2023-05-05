$(function () {
    $('.select-passengers').click(function () {
        var css_display = $('.passengers').css('display');
        if (css_display == 'none') {
            $('.passengers').css('display', 'block');
        } else {
            $('.passengers').css('display', 'none');
        }
    })

    $('.select-promotion').click(function () {
        var css_display = $('.promotion').css('display');
        if (css_display == 'none') {
            $('.promotion').css('display', 'block');
        } else {
            $('.promotion').css('display', 'none');
        }
    })
})