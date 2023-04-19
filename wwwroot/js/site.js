// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
/*

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
var trainingContainers = document.querySelectorAll(".trainingContainer");
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
    if (n > trainingContainers.length) {
        slideIndex = 1;
    }
    if (n < 1) {
        slideIndex = trainingContainers.length;
    }
    trainingContainers.forEach(container => container.classList.remove("active"));
    dots.forEach(dot => dot.classList.remove("active"));
    trainingContainers[slideIndex - 1].classList.add("active");
    dots[slideIndex - 1].classList.add("active");
}


//suggest and recommendation function for search bar
var searchBar = document.querySelector("#search-bar");
var suggestionsContainer = document.querySelector("#suggestions-container");

// Function to show dropdown suggestions
function showDropdownSuggestions(query) {
    // Clear any existing suggestions
    suggestionsContainer.innerHTML = "";

    // TODO: Implement logic to fetch and display dropdown suggestions based on user input
    var dropdownSuggestions = ["Suggestion 1", "Suggestion 2", "Suggestion 3"];
    dropdownSuggestions.forEach((suggestion) => {
        if (suggestion.toLowerCase().includes(query.toLowerCase())) {
            var suggestionElement = document.createElement("div");
            suggestionElement.classList.add("suggestion");
            suggestionElement.textContent = suggestion;

            // Add click listener to select the suggestion
            suggestionElement.addEventListener("click", () => {
                searchBar.value = suggestion;
                suggestionsContainer.innerHTML = "";
            });

            // Add suggestion to the suggestions container
            suggestionsContainer.appendChild(suggestionElement);
        }
    });

    // Show the suggestions container
    suggestionsContainer.style.display = "block";
}

// Function to show internal search bar suggestions
function showInternalSuggestions(query) {
    // Clear any existing suggestions
    suggestionsContainer.innerHTML = "";

    // TODO: Implement logic to fetch and display internal suggestions
    var internalSuggestions = ["apple", "banana", "orange"];
    internalSuggestions.forEach((suggestion) => {
        if (suggestion.toLowerCase().includes(query.toLowerCase())) {
            var suggestionElement = document.createElement("div");
            suggestionElement.classList.add("suggestion");

            // Split the suggestion into two parts: before and after the matched query
            var queryIndex = suggestion.toLowerCase().indexOf(query.toLowerCase());
            var beforeQuery = suggestion.slice(0, queryIndex);
            var afterQuery = suggestion.slice(queryIndex + query.length);

            // Create two span elements: one for the matched query, and one for the rest of the suggestion
            var querySpan = document.createElement("span");
            querySpan.textContent = suggestion.slice(queryIndex, queryIndex + query.length);
            querySpan.classList.add("highlight");
            var restSpan = document.createElement("span");
            restSpan.textContent = afterQuery;

            // Add the two span elements to the suggestion element
            suggestionElement.appendChild(document.createTextNode(beforeQuery));
            suggestionElement.appendChild(querySpan);
            suggestionElement.appendChild(document.createTextNode(restSpan.textContent));

            // Add click listener to select the suggestion
            suggestionElement.addEventListener("click", () => {
                searchBar.value = suggestion;
                suggestionsContainer.innerHTML = "";
            });

            // Add suggestion to the suggestions container
            suggestionsContainer.appendChild(suggestionElement);
        }
    });

    // Show the suggestions container
    suggestionsContainer.style.display = "block";
}

// Add input listener to show suggestions
searchBar.addEventListener("input", (event) => {
    var query = event.target.value;
    showDropdownSuggestions(query);
    showInternalSuggestions(query);
});

// Hide suggestions container when user clicks outside of it
document.addEventListener("click", (event) => {
    if (!event.target.closest("#searchBarContainer")) {
        suggestionsContainer.style.display = "none";
    }
});
*/

// Broken when used along the sort by button in browse training
// Dropdown menu selection that will show the respective form based on the selection for administrator training manager (Add/Remove/Edit)
var dropdown = document.getElementById("training-options");
var addForm = document.getElementById("add-training");
var editForm = document.getElementById("edit-training");

dropdown.addEventListener("change", function () {
    if (dropdown.value === "edit-training") {
        addForm.style.display = "none";
        editForm.style.display = "block";
    } else if (dropdown.value === "add-training") {
        addForm.style.display = "block";
        editForm.style.display = "none";
    }
});