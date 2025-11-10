const AttendanceManager = {
    selectedTimesheet: null,

    init: function () {
        Auth.checkAccessToken();
        this.bindEvents();
        this.loadClasses();
    },

    bindEvents: function () {
        $('#attendanceClassSelect').on('change', () => this.handleClassChange());
        $('#attendanceTimesheetSelect').on('change', () => this.handleTimesheetChange());
        $('#loadAttendanceBtn').on('click', () => this.loadAttendance());
        $('#saveAttendanceBtn').on('click', () => this.saveAttendance());
    },

    loadClasses: function () {
        ApiHelper.request('/api/class', 'GET')
            .done((response) => {
                const options = response.data
                    .filter(cls => cls.isActive)
                    .map(cls => `<option value="${cls.id}">${cls.name} - ${cls.centerName}</option>`);
                $('#attendanceClassSelect').append(options.join(''));
            })
            .fail(() => {
                ApiHelper.showAlert('Failed to load classes', 'danger');
            });
    },

    handleClassChange: function () {
        this.selectedTimesheet = null;
        $('#attendanceTimesheetSelect').prop('disabled', true).empty().append('<option value="">Select session</option>');
        $('#loadAttendanceBtn').prop('disabled', true);
        $('#saveAttendanceBtn').prop('disabled', true);
        this.renderPlaceholder('Select a session to view attendance.');

        const classId = parseInt($('#attendanceClassSelect').val(), 10);
        if (!classId) return;

        ApiHelper.request(`/api/timesheet?classId=${classId}`, 'GET')
            .done((response) => {
                const sessions = response.data || [];
                if (!sessions.length) {
                    ApiHelper.showAlert('No sessions available for this class', 'info');
                    return;
                }

                const options = sessions
                    .sort((a, b) => new Date(b.startTime) - new Date(a.startTime))
                    .map(ts => {
                        const title = ts.title || `Session #${ts.id}`;
                        const timeLabel = ts.startTime ? new Date(ts.startTime).toLocaleString() : 'N/A';
                        return `<option value="${ts.id}">${title} - ${timeLabel}</option>`;
                    });

                $('#attendanceTimesheetSelect')
                    .append(options.join(''))
                    .prop('disabled', false);
                $('#loadAttendanceBtn').prop('disabled', false);
            })
            .fail(() => ApiHelper.showAlert('Failed to load sessions', 'danger'));
    },

    handleTimesheetChange: function () {
        $('#loadAttendanceBtn').prop('disabled', !$('#attendanceTimesheetSelect').val());
    },

    loadAttendance: function () {
        const timesheetId = parseInt($('#attendanceTimesheetSelect').val(), 10);
        if (!timesheetId) {
            ApiHelper.showAlert('Please select a session', 'warning');
            return;
        }

        ApiHelper.request(`/api/attendance/timesheet/${timesheetId}`, 'GET')
            .done((response) => {
                this.selectedTimesheet = timesheetId;
                this.renderAttendance(response.data);
            })
            .fail(() => ApiHelper.showAlert('Failed to load attendance', 'danger'));
    },

    renderAttendance: function (records) {
        if (!records.length) {
            this.renderPlaceholder('No students found for this class.');
            return;
        }

        const rows = records.map(record => `
            <tr data-student-id="${record.studentId}">
                <td><strong>${record.studentName}</strong></td>
                <td>${record.studentEmail}</td>
                <td class="text-center">
                    <input type="checkbox" class="form-check-input present-checkbox" ${record.isPresent ? 'checked' : ''}>
                </td>
                <td>
                    <input type="text" class="form-control note-input" value="${record.note || ''}" maxlength="500">
                </td>
            </tr>
        `);

        $('#attendanceTable tbody').html(rows.join(''));
        $('#saveAttendanceBtn').prop('disabled', false);
    },

    renderPlaceholder: function (message) {
        $('#attendanceTable tbody').html(`
            <tr>
                <td colspan="4" class="text-center text-muted py-4">${message}</td>
            </tr>
        `);
    },

    saveAttendance: function () {
        if (!this.selectedTimesheet) {
            ApiHelper.showAlert('Select a session before saving', 'warning');
            return;
        }

        const records = [];
        $('#attendanceTable tbody tr').each(function () {
            const studentId = parseInt($(this).data('student-id'), 10);
            if (!studentId) return;

            records.push({
                studentId: studentId,
                isPresent: $(this).find('.present-checkbox').is(':checked'),
                note: $(this).find('.note-input').val()?.trim() || null
            });
        });

        if (!records.length) {
            ApiHelper.showAlert('No records to save', 'warning');
            return;
        }

        $('#saveAttendanceBtn').prop('disabled', true).text('Saving...');

        ApiHelper.request(`/api/attendance/timesheet/${this.selectedTimesheet}`, 'POST', {
            timesheetId: this.selectedTimesheet,
            records: records
        })
            .done((response) => ApiHelper.showAlert(response.message, 'success'))
            .fail((xhr) => {
                const message = xhr.responseJSON?.message || 'Failed to save attendance';
                ApiHelper.showAlert(message, 'danger');
            })
            .always(() => {
                $('#saveAttendanceBtn').prop('disabled', false).text('Save Attendance');
            });
    }
};

$(document).ready(() => AttendanceManager.init());
