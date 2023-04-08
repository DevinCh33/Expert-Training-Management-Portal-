// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var priceRange = document.getElementById("price-range");
var priceDisplay = document.getElementById("price-display");

priceRange.addEventListener("input", function () {
    priceDisplay.innerHTML = "$" + priceRange.value;
});