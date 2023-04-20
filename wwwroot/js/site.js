// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
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
    } else if (dropdown.value === "edit-training") {
        addForm.style.display = "none";
        editForm.style.display = "block";
    } else if (dropdown.value === "add-training") {
        addForm.style.display = "block";
        editForm.style.display = "none";
    }
});