$(document).ready(function () {
    InitializeCalendar();
});
var calendar;
function InitializeCalendar() {
    try {
        var calendarElement = document.getElementById('calendar');

        if (calendarElement != null) {
            calendar = new FullCalendar.Calendar(calendarElement, {
                initialView: 'dayGridMonth',
                headerToolbar: {
                    left: 'prev,next,today',
                    center: 'title',
                    right: 'dayGridMonth,timeGridWeek,timeGridDay'
                },
                selectable: true,
                editable: false,
            });
            calendar.render();
        }

    }
    catch (error) {
        alert(error);
    }

}