const TimesheetCreate = {
    init: function() {
        Auth.checkAuth();
        this.bindEvents();
        this.loadClasses();
    },

    bindEvents: function() {
        $('#timesheetForm').on('submit', (e) => {
            e.preventDefault();
            this.createTimesheet();
        });
    },

    loadClasses: function() {
        ApiHelper.request('/api/class', 'GET')
            .done((response) => {
                const options = response.data
                    .filter(cls => cls.isActive)
                    .map(cls => `<option value="${cls.id}">${cls.name} - ${cls.centerName}</option>`);
                $('#classId').append(options.join(''));
            })
            .fail(() => {
                ApiHelper.showAlert('Failed to load classes', 'danger');
            });
    },

    createTimesheet: function() {
        const classId = parseInt($('#classId').val(), 10);
        if (!classId) {
            ApiHelper.showAlert('Please select a class', 'warning');
            return;
        }

        const payload = {
            classId: classId,
            startTime: this.toIso($('#startTime').val()),
            endTime: this.toIso($('#endTime').val()),
            isOnline: $('#isOnline').is(':checked'),
            title: $('#title').val().trim(),
            description: $('#description').val().trim() || null
        };

        if (!payload.startTime || !payload.endTime) {
            ApiHelper.showAlert('Start time and end time are required', 'warning');
            return;
        }

        $('#saveBtn').prop('disabled', true).text('Creating...');

        ApiHelper.request('/api/timesheet', 'POST', payload)
            .done((response) => {
                ApiHelper.showAlert(response.message, 'success');
                $('#timesheetForm')[0].reset();
            })
            .fail((xhr) => {
                const message = xhr.responseJSON?.message || 'Failed to create timesheet';
                ApiHelper.showAlert(message, 'danger');
            })
            .always(() => {
                $('#saveBtn').prop('disabled', false).text('Create');
            });
    },

    toIso: function(value) {
        if (!value) return null;
        const date = new Date(value);
        return isNaN(date.getTime()) ? null : date.toISOString();
    }
};

$(document).ready(() => TimesheetCreate.init());
