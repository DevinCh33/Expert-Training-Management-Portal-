//global variable
var gErrorMsg = "";
// check the input data from various controls are valid format
//display error message if not valid
function validateForm() {
    "use strict"; //directive to ensure variables are declared
    var isAllOK = false;
    gErrorMsg = ""; //reset error message
    var fnameOK = chkFullName();
    var emailOK = chkEmail();
    var addressOk = checkAddress();
    var cityOk = checkCity();
    var stateOk = checkState();
    var postcodeOk = checkPostcode();
    var subjectOk = checkSubject();
    if (fnameOK && emailOK && addressOk && cityOk && stateOk && postcodeOk && subjectOk) {
        isAllOK = true;
    }
    else {
        alert(gErrorMsg); //display concatenated error messages
        gErrorMsg = ""; //reset error msg
        isAllOK = false;
    }
    return isAllOK;
}
// check Full name valid format
function chkFullName() {
    var fname = document.getElementById("fname").value;
    var pattern = /^[a-zA-Z ]+$/;//check only alpha characters or space 
    var nameOk = true;
    if ((fname.length == 0)) { //same as owner==""
        gErrorMsg = gErrorMsg + "Please enter your first name.\n";
        nameOk = false; //if condition or clause complex more readable if branches on separate lines
    }
    else if ((fname.length > 25)) {
        gErrorMsg = gErrorMsg + "Do not enter more than 25 characters in your first name!\n";
        nameOk = false;
    }
    else if (!pattern.test(fname)) {
        gErrorMsg = gErrorMsg + "Your first name can only contain alpha characters\n";
        nameOk = false; //if condition or clause complex more readable if branches on separate lines
    }
    if (!nameOk) {
        document.getElementById("fname").style.cssText += "border: 2px solid red";
    }

    return nameOk;
}

// check Last name valid format


//check the pattern of email(regular expression)
function chkEmail() {
    var email = document.getElementById("email").value;
    var pattern = /[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-za-zA-Z0-9.-]{1,4}$/;
    var isOk = true;
    if (email.length == 0) {
        gErrorMsg = gErrorMsg + "Please enter your email.\n";
        isOk = false; //if condition or clause complex more readable if branches on separate lines
    }
    else if (!pattern.test(email)) {
        gErrorMsg = gErrorMsg + "The email entered is invalid!\n";
        isOk = false;
    }
    if (!isOk) {
        document.getElementById("email").style.cssText += "border: 2px solid red";
    }

    return isOk;
}


function checkAddress() {
    var address = document.getElementById("address").value;
    var isOk = true;
    if (address == "") {
        isOk = false;
        gErrorMsg = gErrorMsg + "Please enter your address\n";
    }
    else if (address.length > 40) {
        isOk = false;
        gErrorMsg = gErrorMsg + "Maximum of 40 characters in street address field.\n";
    }
    if (!isOk) {
        document.getElementById("address").style.cssText += "border: 2px solid red";
    }

    return isOk;
}

function checkCity() {
    var city = document.getElementById("city").value;
    var isOk = true;
    if (city == "") {
        isOk = false;
        gErrorMsg = gErrorMsg + "Please enter your city\n";
    }
    else if (address.length > 20) {
        isOk = false;
        gErrorMsg = gErrorMsg + "Maximum of 20 characters in city field.\n";
    }
    if (!isOk) {
        document.getElementById("city").style.cssText += "border: 2px solid red";
    }

    return isOk;
}

function checkState() {
    var state = document.getElementById("state").value;
    var selected = false;

    if (state != "") {
        selected = true;
    }
    else {
        selected = false;
        gErrorMsg = gErrorMsg + "Please select a state \n";
    }
    return selected;
}



function checkSubject() {
    var subject = document.getElementById("sbj").value;
    var isOk = true;
    if (subject == "" || subject == "RE: Enquiry on null") {
        isOk = false;
        gErrorMsg = gErrorMsg + "\nPlease choose a subject";
    }
    if (!isOk) {
        document.getElementById("sbj").style.cssText += "border: 2px solid red";
    }

    return isOk;
}


function storeBooking(firstname, lastname, email, phone, address, city, state, postcode, subject) {
    //get values from the parameters above and store them as sessionStorage attribute.
    //we use the same name for the attribute and the element id to avoid confusion
    sessionStorage.firstname = firstname;
    sessionStorage.lastname = lastname;
    sessionStorage.email = email;
    sessionStorage.phone = phone;
    sessionStorage.address = address;
    sessionStorage.city = city;
    sessionStorage.state = state;
    sessionStorage.postcode = postcode;
    sessionStorage.subject = subject;

}
function validateToStore() {
    "use strict"; //directive to ensure variables are declared
    var isAllOK = false;
    var fullname = document.getElementById("fname").value;
    var email = document.getElementById("email").value;
    var address = document.getElementById("address").value;
    var city = document.getElementById("city").value;
    var state = document.getElementById("state").value;
    var subject = document.getElementById("sbj").value;

    if ((fullname.length > 0) && (email.length > 0) && (address.length > 0) && (city.length > 0) && (state != "") && (subject.length > 0)) {
        isAllOK = true;

    }
    if (isAllOK) {
        storeBooking(fullname, email, address, city, state, subject);

    }
    return isAllOK;
}
//Confirm
"use strict";
/*get variables from form and check rules*/
function validate() {
    var result = true; /* assumes no errors */
    if (result) {
        alert("Booking Successfully");
    } else {
        alert("Booking Failed");
    }
    return result; //if false the information will not be sent to the server
}
function getBooking() {
    //if sessionStorage for username is not empty
    if (sessionStorage.firstname != undefined) {
        //confirmation text
        document.getElementById("confirm_fname").textContent = sessionStorage.fullname;
        document.getElementById("confirm_email").textContent = sessionStorage.email;
        document.getElementById("confirm_address").textContent = sessionStorage.address;
        document.getElementById("confirm_city").textContent = sessionStorage.city;
        document.getElementById("confirm_state").textContent = sessionStorage.state;
        document.getElementById("confirm_subject").textContent = sessionStorage.subject;
    }
}