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

