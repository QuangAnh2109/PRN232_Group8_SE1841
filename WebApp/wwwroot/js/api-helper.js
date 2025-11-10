const ApiHelper = {
    request: function(url, method = 'GET', data = null) {
        Auth.checkAccessToken();
        const token = Auth.getAccessToken();
        const config = {
            url: `${API_BASE_URL}${url}`,
            method: method,
            contentType: 'application/json',
            data: JSON.stringify(data),
            headers: {
                'Authorization': `Bearer ${token}`
            }
        };

        return $.ajax(config).fail(function(xhr) {
            if (xhr.status === 401) {
                Auth.logout();
            }
        });
    },

    showAlert: function (message, type = 'info', onClose = null) {
        const alertHtml = `
            <div class="alert alert-${type} alert-dismissible fade show" role="alert">
                ${message}
                <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
            </div>
        `;

        const $alert = $(alertHtml);
        $('#alertContainer').html($alert);

        if (typeof onClose === 'function') {
            $alert.on('closed.bs.alert', function () {
                onClose();
            });
        }

        setTimeout(() => {
            $alert.alert('close');
        }, 3000);
    }
};

