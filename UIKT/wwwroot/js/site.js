// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$("#account-link").on("click", function () {
    $("#account-link").show();
});

$("#account-link").on("click", function () {
    $("#account-link").hide();
});



function myFunction() {
    alert("Shranjeno!");
}

var usrCheckbox = document.getElementById("usr");
var privacyCheckbox = document.getElementById("privacy");
var cookiesCheckbox = document.getElementById("cookies");

function gdpr() {
    var usrChecked = usrCheckbox.checked;
    var privacyChecked = privacyCheckbox.checked;
    var cookiesChecked = cookiesCheckbox.checked;

    sessionStorage.setItem("usrChecked", usrChecked);
    sessionStorage.setItem("privacyChecked", privacyChecked);
    sessionStorage.setItem("cookiesChecked", cookiesChecked);
}

