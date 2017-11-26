(function () {
    $('.tenant-change-component a')
        .click(function (e) {
            e.preventDefault(); //阻止元素发生默认的行为
            $.ajax({
                url: abp.appPath + 'Account/TenantChangeModal',
                type: 'POST',
                contentType: 'application/html',
                success: function (content) {
                    $('#TenantChangeModal div.modal-content').html(content);
                },
                error: function (e) { }
            });
        });
})();