function wait() {
    $('.preloader').delay(500).fadeIn('slow');
    $('.preloader-icon').fadeIn(400);
}

function noWait() {
    $('.preloader-icon').fadeOut(500);
    $('.preloader').delay(400).fadeOut('slow');
}

$(document).ready(function () {
    noWait();
});