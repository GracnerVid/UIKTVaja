// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$("#account-link").on("click", function () {
    $("#account-link").show();
});

$("#account-link").on("click", function () {
    $("#account-link").hide();
});



if (isLoggedIn) {
    $("#account-link").show();
} else {
    $("#account-link").hide();
}