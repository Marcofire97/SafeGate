$(document).ready(function () {

    console.log("ViewData Loaded:", viewData);

    loadSection('home');

    // Gestisce apertura e chiusura della barra laterale
    function toggleSidebar() {
        var sidebar = $('#sidebar');
        var content = $('#content');
        var icon = $('.sidebar-toggle-btn i');

        sidebar.toggleClass('closed');
        content.toggleClass('content-expanded');

        var newIcon = sidebar.hasClass('closed') ? 'bi-chevron-right' : 'bi-chevron-left';
        icon.removeClass('bi-chevron-left bi-chevron-right').addClass(newIcon);
    }

    $('.sidebar-toggle-btn').on('click', toggleSidebar);

    // Gestisce le chiamate ajax per caricare una nuova sezione a destra della barra laterale
    function loadSection(section) {
        var contentDiv = $('#content');

        contentDiv.addClass('fade-out');

        setTimeout(function () {
            $.ajax({
                url: `/Home/GetSection`,
                type: 'GET',
                data: {
                    section: section
                },
                success: function (data) {
                    if (data.success) {
                        contentDiv.html(data.data);
                        contentDiv.removeClass('fade-out').addClass('fade-in');
                    }
                    else {
                        contentDiv.removeClass('fade-out').html(
                            data.data
                        ).addClass('fade-in');
                    }
                },
                error: function () {
                    contentDiv.removeClass('fade-out').html(
                        `<div class='error-message text-center p-4'>
                            <i class='bi bi-exclamation-triangle-fill text-danger' style='font-size: 2rem;'></i>
                            <h2 class='mt-3 text-danger'>Errore nel caricamento della sezione</h2>
                            <p class='text-muted'>Si è verificato un problema. Riprova più tardi.</p>
                        </div>`
                    ).addClass('fade-in');
                }
            });
        }, 300);
    }

    $('.sidebar-item').on('click', function (e) {
        e.preventDefault();
        var section = $(this).attr('section');
        loadSection(section);
    });
});
