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

$(document).ready(function () {
    $("#applyFilterBtn").click(function () {
        $(".filterInterface").toggleClass("visible");
    });
});


var slideshowContainer = document.querySelector(".slideshowContainer");
var imageContainers = document.querySelectorAll(".imageContainer");
var arrows = document.querySelectorAll(".arrow");
var dots = document.querySelectorAll(".dot");

var slideIndex = 1;
showSlides(slideIndex);

// Next/previous controls
arrows.forEach(arrow => {
    arrow.addEventListener("click", () => {
        if (arrow.classList.contains("prev")) {
            showSlides(slideIndex -= 1);
        } else {
            showSlides(slideIndex += 1);
        }
    });
});

// Thumbnail image controls
dots.forEach((dot, index) => {
    dot.addEventListener("click", () => {
        showSlides(slideIndex = index + 1);
    });
});

function showSlides(n) {
    if (n > imageContainers.length) {
        slideIndex = 1;
    }
    if (n < 1) {
        slideIndex = imageContainers.length;
    }
    imageContainers.forEach(container => container.classList.remove("active"));
    dots.forEach(dot => dot.classList.remove("active"));
    imageContainers[slideIndex - 1].classList.add("active");
    dots[slideIndex - 1].classList.add("active");
}
