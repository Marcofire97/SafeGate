function toggleSidebar() {
    var sidebar = document.getElementById('sidebar');
    var content = document.getElementById('content');

    sidebar.classList.toggle('closed');
    content.classList.toggle('content-expanded');

    var icon = sidebar.classList.contains('closed') ? 'bi-chevron-right' : 'bi-chevron-left';
    document.querySelector('.sidebar-toggle-btn i').classList.remove('bi-chevron-left', 'bi-chevron-right');
    document.querySelector('.sidebar-toggle-btn i').classList.add(icon);
}

function updateDateTime() {
    var now = new Date();
    var hours = now.getHours().toString().padStart(2, '0');
    var minutes = now.getMinutes().toString().padStart(2, '0');
    var seconds = now.getSeconds().toString().padStart(2, '0');
    var day = now.toLocaleString('default', { weekday: 'long' });

    var timeString = `${hours}:${minutes}:${seconds}`;
    var dateString = `${day}, ${now.toLocaleDateString()}`;

    document.getElementById('current-time').innerHTML = `Ora attuale: ${timeString}`;
    document.getElementById('current-day').innerHTML = `Giorno: ${dateString}`;
}

window.onload = updateDateTime;
setInterval(updateDateTime, 1000);



function loadSection(section) {
    var contentDiv = $("#main-content");

    contentDiv.addClass("fade-out");

    setTimeout(function () {
        $.ajax({
            url: "/sections/" + section + ".html",
            type: "GET",
            success: function (data) {
                contentDiv.html(data);
                contentDiv.removeClass("fade-out").addClass("fade-in");
            },
            error: function () {
                contentDiv.html("<h2>Error loading section</h2>");
            }
        });
    }, 300);
}
