// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// get a reference to the add button
var addBtn = document.getElementById("addBtn");

// get a reference to the dropdown content
var dropdown = document.getElementsByClassName("dropdown2-content")[0];

// get the list items from local storage, or initialize an empty array if there are none
var listItems = JSON.parse(localStorage.getItem("listItems")) || [];

// populate the dropdown with the list items from local storage
for (var i = 0; i < listItems.length; i++) {
    var li = document.createElement("li");
    li.innerHTML = listItems[i];
    dropdown.appendChild(li);
}

// add an event listener to the add button
addBtn.addEventListener("click", function () {
    // create a new list item element
    var li = document.createElement("li");

    // set the text of the list item element
    li.innerHTML = "New Item";

    // add the list item element to the dropdown
    dropdown.appendChild(li);

    // add the new list item to the list of list items in local storage
    listItems.push(li.innerHTML);
    localStorage.setItem("listItems", JSON.stringify(listItems));
});

