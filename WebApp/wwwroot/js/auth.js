const Auth = {
    login: function(username, password) {
        return $.ajax({
            url: `${API_BASE_URL}/api/auth/token`,
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ Username: username, Password: password })
        });
    },

    saveJwtToken: function(accessToken, refreshToken) {
        this.saveAccessToken(accessToken);
        localStorage.setItem('refreshToken', refreshToken);
    },
    
    saveAccessToken: function(accessToken) {
        localStorage.setItem('accessToken', accessToken);
    },

    getAccessToken: function() {
        return localStorage.getItem('accessToken');
    },

    getRefreshToken: function() {
        return localStorage.getItem('refreshToken');
    },

    logout: function() {
        localStorage.removeItem('accessToken');
        localStorage.removeItem('refreshToken');
        window.location.href = '/Auth/Login';
    },

    isAuthenticated: function() {
        return this.getAccessToken() !== null;
    },

    isTokenExpired: function(token) {
        if (!token) return true;
        try {
            const payload = JSON.parse(atob(token.split('.')[1]));
            const nowUtcSeconds = Date.now / 1000;
            return payload.exp < nowUtcSeconds;
        } catch (e) {
            return true;
        }
    },

    checkAccessToken: function() {
        if(!this.isTokenExpired(this.getAccessToken())) {
            return this.checkRefreshAccessToken();
        }
        return true;
    },

    checkRefreshAccessToken() {
        const refreshToken = this.getRefreshToken();
        if (this.isTokenExpired(refreshToken)) {
            this.logout();
        }

        try {
            const response = $.ajax({
                url: `${API_BASE_URL}/api/auth/token`,
                method: 'GET',
                headers: {
                    'Authorization': `Bearer ${refreshToken}`,
                }
            }).done(function(response) {
                this.saveAccessToken(response.accessToken);
            }).fail(function(xhr) {
                this.logout();
            });
        } catch (e) {
            this.logout();
        }
    },
};

