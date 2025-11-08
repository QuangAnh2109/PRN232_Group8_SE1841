const ApiHelper = {
    request: function(url, method = 'GET', data = null) {
        const token = Auth.getToken();
        const config = {
            url: `${API_BASE_URL}${url}`,
            method: method,
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        };

        if (data && (method === 'POST' || method === 'PUT')) {
            config.data = JSON.stringify(data);
        }

        return $.ajax(config).fail(function(xhr) {
            if (xhr.status === 401) {
                Auth.logout();
            }
        });
    },

    showAlert: function(message, type = 'success') {
        const alertHtml = `
            <div class="alert alert-${type} alert-dismissible fade show" role="alert">
                ${message}
                <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
            </div>
        `;
        $('#alertContainer').html(alertHtml);
        setTimeout(() => {
            $('.alert').alert('close');
        }, 3000);
    }
};

