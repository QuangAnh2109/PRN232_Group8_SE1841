const TimesheetEdit = {
    timesheetId: null,

    init: function() {
        Auth.checkAuth();
        this.timesheetId = parseInt($('#timesheetId').val(), 10);

        if (!this.timesheetId) {
            ApiHelper.showAlert('Invalid timesheet identifier', 'danger');
            return;
        }

        this.bindEvents();
        this.loadClasses()
            .done(() => this.loadTimesheet())
            .fail(() => ApiHelper.showAlert('Failed to load required data', 'danger'));
    },

    bindEvents: function() {
        $('#timesheetEditForm').on('submit', (e) => {
            e.preventDefault();
            this.updateTimesheet();
        });
    },

    loadClasses: function() {
        return ApiHelper.request('/api/class', 'GET')
            .done((response) => {
                const options = response.data.map(cls => `<option value="${cls.id}">${cls.name} - ${cls.centerName}</option>`);
                $('#editClassId').append(options.join(''));
            })
            .fail(() => {
                ApiHelper.showAlert('Failed to load classes', 'danger');
            });
    },

    loadTimesheet: function() {
        ApiHelper.request(`/api/timesheet/${this.timesheetId}`, 'GET')
            .done((response) => {
                const data = response.data;
                $('#editClassId').val(data.classId);
                $('#editStartTime').val(this.formatForInput(data.startTime));
                $('#editEndTime').val(this.formatForInput(data.endTime));
                $('#editTitle').val(data.title);
                $('#editDescription').val(data.description || '');
                $('#editIsOnline').prop('checked', data.isOnline);
            })
            .fail(() => {
                ApiHelper.showAlert('Failed to load timesheet details', 'danger');
            });
    },

    updateTimesheet: function() {
        const classId = parseInt($('#editClassId').val(), 10);
        if (!classId) {
            ApiHelper.showAlert('Please select a class', 'warning');
            return;
        }

        const payload = {
            classId: classId,
            startTime: this.toIso($('#editStartTime').val()),
            endTime: this.toIso($('#editEndTime').val()),
            isOnline: $('#editIsOnline').is(':checked'),
            title: $('#editTitle').val().trim(),
            description: $('#editDescription').val().trim() || null
        };

        if (!payload.startTime || !payload.endTime) {
            ApiHelper.showAlert('Start time and end time are required', 'warning');
            return;
        }

        $('#updateBtn').prop('disabled', true).text('Saving...');

        ApiHelper.request(`/api/timesheet/${this.timesheetId}`, 'PUT', payload)
            .done((response) => {
                ApiHelper.showAlert(response.message, 'success');
            })
            .fail((xhr) => {
                const message = xhr.responseJSON?.message || 'Failed to update timesheet';
                ApiHelper.showAlert(message, 'danger');
            })
            .always(() => {
                $('#updateBtn').prop('disabled', false).text('Save changes');
            });
    },

    toIso: function(value) {
        if (!value) return null;
        const date = new Date(value);
        return isNaN(date.getTime()) ? null : date.toISOString();
    },

    formatForInput: function(value) {
        if (!value) return '';
        const date = new Date(value);
        if (isNaN(date.getTime())) {
            return '';
        }

        const pad = (num) => num.toString().padStart(2, '0');
        const year = date.getFullYear();
        const month = pad(date.getMonth() + 1);
        const day = pad(date.getDate());
        const hours = pad(date.getHours());
        const minutes = pad(date.getMinutes());
        return `${year}-${month}-${day}T${hours}:${minutes}`;
    }
};

$(document).ready(() => TimesheetEdit.init());
