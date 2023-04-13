// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var sortBtn = document.querySelector("#sort-btn");
var dropdownContent = document.querySelector(".dropdown-content");
var priceRange = document.getElementById("price-range");
var priceDisplay = document.getElementById("price-display");
var testJavaBtn1 = document.getElementById("testJavaBtn1");
//sort button - drop down
sortBtn.addEventListener("mouseenter", function () {
    dropdownContent.classList.add("active");
});

dropdownContent.addEventListener("mouseenter", function () {
    dropdownContent.classList.add("active");
});

dropdownContent.addEventListener("mouseleave", function () {
    dropdownContent.classList.remove("active");
});

//price change for slider
priceRange.addEventListener("input", function () {
    priceDisplay.innerHTML = "RM" + priceRange.value;
});
//filter button
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

//slideshow
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


// Broken when used along the sort by button in browse training
// Dropdown menu selection that will show the respective form based on the selection for administrator training manager (Add/Remove/Edit)
var dropdown = document.getElementById("training-options");
var addForm = document.getElementById("add-training");
//var removeForm = document.getElementById("remove-training");
var editForm = document.getElementById("edit-training");

dropdown.addEventListener("change", function () {
    if (dropdown.value === "remove-training") {
        addForm.style.display = "none";
        editForm.style.display = "none";
        //removeForm.style.display = "block";
        //alert("Hello, world!");
    } else if (dropdown.value === "edit-training"){
        addForm.style.display = "none";
        editForm.style.display = "block";
    } else if (dropdown.value === "add-training") {
    addForm.style.display = "block";
    editForm.style.display = "none";
    }
});


//testing java


/*testJavaBtn1.addEventListener("click", function () {
    alert("Java Worked!");
});*/



