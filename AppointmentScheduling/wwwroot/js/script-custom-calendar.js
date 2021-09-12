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
                select: function (event) {
                    onShowModal(event, null);
                }
            });
            calendar.render();
        }

    }
    catch (error) {
        alert(error);
    }

}

function onShowModal(obj, isEventDetail) {
    $("#appointmentInput").modal("show");
}

function onCloseModal() {
    $("#appointmentInput").modal("hide");
}