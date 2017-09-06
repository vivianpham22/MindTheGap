var events = [];
$.ajax({
    type: "GET",
    url: "GetCalendarDatabase",
    success: function (data) {
        $.each(data, function (key, value) {
            events.push({
                title: value.summary,
                start: value.starttime.toISOString(),
                end: value.endtime.toISOString(),
            });
        });
        CallCalender(events);
    },

    error: function (error) {
        alert('failed');
    }
});

function CallCalendar(events) {
    $('#calendar').fullCalendar({
        events: events
    })
};
        });