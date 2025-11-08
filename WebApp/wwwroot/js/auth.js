const Auth = {
    login: function(username, password) {
        return $.ajax({
            url: `${API_BASE_URL}/api/auth/token`,
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ Username: username, Password: password })
        });
    },

    saveToken: function(token) {
        localStorage.setItem('accessToken', token);
    },

    getToken: function() {
        return localStorage.getItem('accessToken');
    },

    logout: function() {
        localStorage.removeItem('accessToken');
        window.location.href = '/Auth/Login';
    },

    isAuthenticated: function() {
        return this.getToken() !== null;
    },

    checkAuth: function() {
        if (!this.isAuthenticated()) {
            window.location.href = '/Auth/Login';
            return false;
        }
        return true;
    }
};

