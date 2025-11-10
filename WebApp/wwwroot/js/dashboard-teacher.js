const TeacherDashboard = {
    classes: [],
    classFiltered: [],
    classPage: 1,
    classPageSize: 5,
    sessions: [],
    sessionFiltered: [],
    sessionPage: 1,
    sessionPageSize: 5,

    init: function () {
        Auth.checkAccessToken();
        this.bindFilters();
        this.loadDashboard();
    },

    loadDashboard: function () {
        ApiHelper.request('/api/dashboard/teacher', 'GET')
            .done((response) => this.renderDashboard(response.data))
            .fail(() => ApiHelper.showAlert('Failed to load dashboard data', 'danger'));
    },

    renderDashboard: function (data) {
        $('#totalClasses').text(data.totalClasses);
        $('#activeClasses').text(data.activeClasses);
        $('#inactiveClasses').text(data.inactiveClasses);
        $('#totalStudents').text(data.totalStudents);
        $('#sessionsThisWeek').text(data.sessionsThisWeek);

        this.classes = data.classes || [];
        this.classFiltered = [...this.classes];
        this.populateClassFilters(this.classes);
        this.renderClassSummary();

        this.sessions = data.upcomingSessions || [];
        this.sessionFiltered = [...this.sessions];
        this.populateSessionClassFilter(this.classes);
        this.applySessionFilters(true);
    },

    renderClassSummary: function () {
        const classes = this.classFiltered;
        if (!classes.length) {
            $('#classSummaryBody').html('<tr><td colspan="4" class="text-center text-muted py-4">No classes assigned</td></tr>');
            $('#classPaginationInfo').text('No classes to display');
            $('#classPrevBtn, #classNextBtn').prop('disabled', true);
            return;
        }

        const pageSize = this.getClassPageSize();
        const totalPages = Math.ceil(classes.length / pageSize);
        this.classPage = Math.min(this.classPage, totalPages);
        const start = (this.classPage - 1) * pageSize;
        const paged = classes.slice(start, start + pageSize);

        const rows = paged.map(cls => `
            <tr>
                <td><strong>${cls.className}</strong></td>
                <td>${cls.centerName}</td>
                <td class="text-center">${cls.studentCount}</td>
                <td class="text-center">
                    <span class="badge ${cls.isActive ? 'bg-success' : 'bg-secondary'}">
                        ${cls.isActive ? 'Active' : 'Inactive'}
                    </span>
                </td>
            </tr>
        `);

        $('#classSummaryBody').html(rows.join(''));
        const from = start + 1;
        const to = Math.min(start + pageSize, classes.length);
        $('#classPaginationInfo').text(`Showing ${from}-${to} of ${classes.length} classes`);
        $('#classPrevBtn').prop('disabled', this.classPage <= 1);
        $('#classNextBtn').prop('disabled', this.classPage >= totalPages);
    },

    populateClassFilters: function (classes) {
        const centerSelect = $('#classCenterFilter');
        const centers = Array.from(new Set(classes.map(cls => cls.centerName))).sort();
        const currentCenter = centerSelect.val() || 'all';
        centerSelect.empty().append('<option value="all">All centers</option>');
        centers.forEach(center => centerSelect.append(`<option value="${center}">${center}</option>`));
        if (centers.includes(currentCenter)) {
            centerSelect.val(currentCenter);
        }
    },

    applyClassFilters: function () {
        const search = ($('#classSearch').val() || '').trim().toLowerCase();
        const status = $('#classStatusFilter').val();
        const center = $('#classCenterFilter').val();

        const filtered = this.classes.filter(cls => {
            if (status === 'active' && !cls.isActive) return false;
            if (status === 'inactive' && cls.isActive) return false;
            if (center && center !== 'all' && cls.centerName !== center) return false;
            if (search) {
                const target = `${cls.className} ${cls.centerName}`.toLowerCase();
                if (!target.includes(search)) return false;
            }
            return true;
        });

        this.classFiltered = filtered;
        this.classPage = 1;
        this.renderClassSummary();
    },

    populateSessionClassFilter: function (classes) {
        const select = $('#sessionClassFilter');
        const currentValue = select.val() || 'all';
        select.empty().append('<option value="all">All classes</option>');
        classes.forEach(cls => select.append(`<option value="${cls.classId}">${cls.className}</option>`));
        if (classes.some(cls => cls.classId?.toString() === currentValue)) {
            select.val(currentValue);
        }
    },

    applySessionFilters: function (resetPage = false) {
        const classFilter = $('#sessionClassFilter').val();
        const rangeFilter = $('#sessionRangeFilter').val();
        const modeFilter = $('#sessionModeFilter').val();
        const now = new Date();

        const filtered = this.sessions.filter(session => {
            if (classFilter && classFilter !== 'all' && session.classId?.toString() !== classFilter) {
                return false;
            }

            if (modeFilter === 'online' && !session.isOnline) return false;
            if (modeFilter === 'onsite' && session.isOnline) return false;

            const startTime = new Date(session.startTime);
            if (rangeFilter === 'week') {
                const startOfWeek = this.getStartOfWeek(now);
                const endOfWeek = new Date(startOfWeek);
                endOfWeek.setDate(endOfWeek.getDate() + 7);
                if (startTime < startOfWeek || startTime >= endOfWeek) {
                    return false;
                }
            } else if (rangeFilter !== 'all') {
                const rangeDays = parseInt(rangeFilter, 10);
                const end = new Date(now);
                end.setDate(end.getDate() + rangeDays);
                if (startTime < now || startTime > end) {
                    return false;
                }
            }

            return true;
        });

        this.sessionFiltered = filtered;
        if (resetPage) {
            this.sessionPage = 1;
        }
        this.renderUpcomingSessions();
    },

    renderUpcomingSessions: function () {
        const sessions = this.sessionFiltered;
        if (!sessions.length) {
            $('#upcomingSessionsBody').html('<tr><td colspan="5" class="text-center text-muted py-4">No sessions found</td></tr>');
            $('#sessionPaginationInfo').text('No sessions to display');
            $('#sessionPrevBtn, #sessionNextBtn').prop('disabled', true);
            return;
        }

        const pageSize = this.getSessionPageSize();
        const totalPages = Math.ceil(sessions.length / pageSize);
        this.sessionPage = Math.min(this.sessionPage, totalPages);
        const start = (this.sessionPage - 1) * pageSize;
        const paged = sessions.slice(start, start + pageSize);

        const rows = paged.map(session => `
            <tr>
                <td>${session.className}</td>
                <td>${session.centerName}</td>
                <td>${session.title}</td>
                <td>${new Date(session.startTime).toLocaleString()}</td>
                <td>
                    <span class="badge ${session.isOnline ? 'bg-info' : 'bg-secondary'}">
                        ${session.isOnline ? 'Online' : 'On-site'}
                    </span>
                </td>
            </tr>
        `);

        $('#upcomingSessionsBody').html(rows.join(''));
        const from = start + 1;
        const to = Math.min(start + pageSize, sessions.length);
        $('#sessionPaginationInfo').text(`Showing ${from}-${to} of ${sessions.length} sessions`);
        $('#sessionPrevBtn').prop('disabled', this.sessionPage <= 1);
        $('#sessionNextBtn').prop('disabled', this.sessionPage >= totalPages);
    },

    bindFilters: function () {
        $('#classSearch').on('input', () => this.applyClassFilters());
        $('#classStatusFilter').on('change', () => this.applyClassFilters());
        $('#classCenterFilter').on('change', () => this.applyClassFilters());
        $('#classPageSize').on('change', () => {
            this.classPageSize = this.getClassPageSize();
            this.classPage = 1;
            this.renderClassSummary();
        });
        $('#classPrevBtn').on('click', () => {
            if (this.classPage > 1) {
                this.classPage--;
                this.renderClassSummary();
            }
        });
        $('#classNextBtn').on('click', () => {
            const totalPages = Math.ceil(this.classFiltered.length / this.getClassPageSize());
            if (this.classPage < totalPages) {
                this.classPage++;
                this.renderClassSummary();
            }
        });

        $('#sessionClassFilter').on('change', () => this.applySessionFilters(true));
        $('#sessionRangeFilter').on('change', () => this.applySessionFilters(true));
        $('#sessionModeFilter').on('change', () => this.applySessionFilters(true));
        $('#sessionPageSize').on('change', () => {
            this.sessionPageSize = this.getSessionPageSize();
            this.sessionPage = 1;
            this.renderUpcomingSessions();
        });
        $('#sessionPrevBtn').on('click', () => {
            if (this.sessionPage > 1) {
                this.sessionPage--;
                this.renderUpcomingSessions();
            }
        });
        $('#sessionNextBtn').on('click', () => {
            const totalPages = Math.ceil(this.sessionFiltered.length / this.getSessionPageSize());
            if (this.sessionPage < totalPages) {
                this.sessionPage++;
                this.renderUpcomingSessions();
            }
        });
    },

    getStartOfWeek: function (date) {
        const newDate = new Date(date);
        const day = newDate.getDay();
        const diff = (day === 0 ? -6 : 1) - day;
        newDate.setDate(newDate.getDate() + diff);
        newDate.setHours(0, 0, 0, 0);
        return newDate;
    },

    getClassPageSize: function () {
        const size = parseInt($('#classPageSize').val(), 10);
        if (Number.isNaN(size)) return this.classPageSize;
        return Math.min(100, Math.max(5, size));
    },

    getSessionPageSize: function () {
        const size = parseInt($('#sessionPageSize').val(), 10);
        if (Number.isNaN(size)) return this.sessionPageSize;
        return Math.min(100, Math.max(5, size));
    }
};

$(document).ready(() => TeacherDashboard.init());
