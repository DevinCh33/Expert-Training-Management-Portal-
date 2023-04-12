// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var sortBtn = document.querySelector("#sort-btn");
var dropdownContent = document.querySelector(".dropdown-content");
var priceRange = document.getElementById("price-range");
var priceDisplay = document.getElementById("price-display");


sortBtn.addEventListener("mouseenter", function () {
    dropdownContent.classList.add("active");
});

dropdownContent.addEventListener("mouseenter", function () {
    dropdownContent.classList.add("active");
});

dropdownContent.addEventListener("mouseleave", function () {
    dropdownContent.classList.remove("active");
});


priceRange.addEventListener("input", function () {
    priceDisplay.innerHTML = "RM" + priceRange.value;
});

$(document).ready(function () {
    $("#filter-btn").click(function () {
        $(".filterInterface").toggleClass("visible");
    });
});
