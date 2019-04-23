$(document).ready(function () {
    let equipment = {
        init: function () {
            this.registerEvents();
        },

        registerEvents: function () {
            $('.btn-active').off('click').on('click', function (e) {
                e.preventDefault();
                let btn = $(this);
                let equipmentID = btn.data('id');
                $.ajax({
                    type: 'POST',
                    url: '/Admin/Equipment/ChangeStatus',
                    data: { "ID": equipmentID },
                    dataType: 'json',
                    success: function (response) {
                        if (response.status) {
                            btn.text('Sẵn sàng');
                        }
                        else {
                            btn.text('Chưa sẵn sàng');
                        }
                    }
                });
            });
        }
    };

    equipment.init();
});