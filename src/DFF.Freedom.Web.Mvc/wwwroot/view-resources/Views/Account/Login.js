$(function () {
    $('#ReturnUrlHash').val(location.hash);

    var $loginForm = $('#LoginForm');

    //验证方法
    $loginForm.validate({
        //高亮方法
        highlight: function (input) {
            $(input).parents('.form-line').addClass('error');
        },
        //取消高亮方法
        unhighlight: function (input) {
            $(input).parents('.form-line').removeClass('error');
        },
        //错误提示方法
        errorPlacement: function (error, element) {
            $(element).parents('.input-group').append(error);
        }
    });

    $loginForm.submit(function (e) {
        e.preventDefault();

        if (!$loginForm.valid()) {
            return;
        }

        abp.ui.setBusy(
            $('#LoginArea'),

            abp.ajax({
                contentType: 'application/x-www-form-urlencoded',
                url: $loginForm.attr('action'),
                data: $loginForm.serialize()
            })
        );
    });

    $('a.social-login-link').click(function () {
        var $a = $(this);
        var $form = $a.closest('form');
        $form.find('input[name=provider]').val($a.attr('data-provider'));
        $form.submit();
    });

    $loginForm.find('input[type=text]:first-child').focus();
});
