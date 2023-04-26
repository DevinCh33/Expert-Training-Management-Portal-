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
    var stateOk = checkState();
    var cityOk = checkCity();
    var zipOk = checkZip();
    var cnameOK = chkCardName();
    var cardnumberOk = checkCardNumber();
    var expmonthOk = checkExpMonth();
    var expyearOk = checkExpYear();
    var cvvOk = checkCvv();
    if (fnameOK && emailOK && addressOk && zipOk && stateOk && cityOk && cnameOK && cardnumberOk && expmonthOk && expyearOk && cvvOk) {
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

function checkCity() {
    var city = document.getElementById("city").value;
    var isOk = true;
    if (city == "") {
        isOk = false;
        gErrorMsg = gErrorMsg + "Please enter your city\n";
    }
    else if (city.length > 20) {
        isOk = false;
        gErrorMsg = gErrorMsg + "Maximum of 20 characters in city field.\n";
    }
    if (!isOk) {
        document.getElementById("city").style.cssText += "border: 2px solid red";
    }

    return isOk;
}

// Use a regular expression to validate the zip code format
function checkZip() {
    var zip = document.getElementById("zip").value;
    var pattern = /[0-9]/;
    var isOk = true;
    if (zip == "") {
        isOk = false;
        gErrorMsg = gErrorMsg + "Please enter a valid zip code ";
    }
    else if (zip.length > 5) {
        isOk = false;
        gErrorMsg = gErrorMsg + "Do not enter more than 5 numbers!";
    }

    else if (!pattern.test(zip)) {
        gErrorMsg = gErrorMsg + "Invalid zip!";
        isOk = false;
    }
    if (!isOk) {
        document.getElementById("zip").style.cssText += "border: 2px solid red";
    }

    return isOk;
}

function chkCardName() {
    var fname = document.getElementById("cname").value;
    var pattern = /^[a-zA-Z ]+$/;//check only alpha characters or space 
    var nameOk = true;
    if ((fname.length == 0)) { //same as owner==""
        gErrorMsg = gErrorMsg + "Please enter your card name.\n";
        nameOk = false; //if condition or clause complex more readable if branches on separate lines
    }
    else if ((fname.length > 25)) {
        gErrorMsg = gErrorMsg + "Do not enter more than 25 characters in your card name!\n";
        nameOk = false;
    }
    else if (!pattern.test(fname)) {
        gErrorMsg = gErrorMsg + "Your card name can only contain alpha characters\n";
        nameOk = false; //if condition or clause complex more readable if branches on separate lines
    }
    if (!nameOk) {
        document.getElementById("cname").style.cssText += "border: 2px solid red";
    }

    return nameOk;
}

// Check if card number is valid
function checkCardNumber() {
    var cardnumber = document.getElementById("cardnumber").value;
    var cnumber = cardnumber.replace(/-/g, ''); // remove dash
    var pattern = /^[0-9]{16}$/;//check only alpha characters or space 
    var isOk = true;
    if (cardnumber == "") {
        isOk = false;
        gErrorMsg = gErrorMsg + "Please enter your Card Number";
    }
    else if (cnumber.length < 16) {
        isOk = false;
        gErrorMsg = gErrorMsg + "Do not enter less than 16 numbers!";
    }

    else if (!pattern.test(cnumber)) {
        gErrorMsg = gErrorMsg + "Invalid Card Number!";
        isOk = false;
    }
    if (!isOk) {
        document.getElementById("cardnumber").style.cssText += "border: 2px solid red";
    }

    return isOk;
}


// Check if expiration month is valid
function checkExpMonth(){
    var expMonth = document.getElementById("expmonth").value;
    var pattern = /^(january|february|march|april|may|june|july|august|september|october|november|december)$/i;

    if (expMonth == "") {
        isOk = false;
        gErrorMsg = gErrorMsg + "Please enter your expiration month";
    }

    if (!pattern.test(expMonth)) {
        gErrorMsg = gErrorMsg + "Please enter a valid expiration month!";
        isOk = false;
    }
    if (!isOk) {
        document.getElementById("expmonth").style.cssText += "border: 2px solid red";
    }

    return isOk;
}

// Check if expiration year is valid
function checkExpYear(){
    var expYear = document.getElementById("expyear").value;
    var pattern = /^(20[2-9][2-9]|21[0-9][0-9])$/;

    if (expYear == "") {
        isOk = false;
        gErrorMsg = gErrorMsg + "Please enter your expiration year";
    }

    if (!pattern.test(expYear)) {
        gErrorMsg = gErrorMsg + "Please enter a valid expiration year!";
        isOk = false;
    }
    if (!isOk) {
        document.getElementById("expyear").style.cssText += "border: 2px solid red";
    }

    return isOk;
}

function checkCvv() { 
    var cvv = document.getElementById("cvv").value;
    var pattern = /[0-9]/;
    var isOk = true;
    if (cvv == "") {
        isOk = false;
        gErrorMsg = gErrorMsg + "Please enter your CVV number";
    }
    else if (cvv.length > 4 && cvv.length < 3) {
        isOk = false;
        gErrorMsg = gErrorMsg + "Please enter a valid 3- or 4-digit CVV number";
    }

    else if (!pattern.test(cvv)) {
        gErrorMsg = gErrorMsg + "Invalid CVV number!";
        isOk = false;
    }
    if (!isOk) {
        document.getElementById("cvv").style.cssText += "border: 2px solid red";
    }

    return isOk;
}



function storeBooking(fullname, email, address, state, city, zip, cname, cnumber, expmonth, expyear, cvv) {
    //get values from the parameters above and store them as sessionStorage attribute.
    //we use the same name for the attribute and the element id to avoid confusion
    sessionStorage.fullname = fullname;
    sessionStorage.email = email;
    sessionStorage.address = address;
    sessionStorage.state = state;
    sessionStorage.city = city;
    sessionStorage.zip = zip;
    sessionStorage.cname = cname;
    sessionStorage.cnumber = cnumber;
    sessionStorage.expmonth = expmonth;
    sessionStorage.expyear = expyear;
    sessionStorage.cvv = cvv;

}
function validateToStore() {
    "use strict"; //directive to ensure variables are declared
    var isAllOK = false;
    var fullname = document.getElementById("fname").value;
    var email = document.getElementById("email").value;
    var address = document.getElementById("address").value;
    var city = document.getElementById("city").value;
    var state = document.getElementById("state").value;
    var zip = document.getElementById("zip").value;
    var cname = document.getElementById("cardname").value;
    var cnumber = document.getElementById("cardnumber").value;
    var expmonth = document.getElementById("expmonth").value;
    var expyear = document.getElementById("expyear").value;
    var cvv = document.getElementById("cvv").value;

    if ((fullname.length > 0) && (email.length > 0) && (address.length > 0) && (city.length > 0) && (state != "") && (zip.length > 0) && (cname != "") && (cnumber != "") && (expmonth != "") && (expyear.length > 0) && (cvv.length > 0)) {
        isAllOK = true;

    }
    if (isAllOK) {
        storeBooking(fullname, email, address, state, city, zip, cname, cnumber, expmonth, expyear, cvv);

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

function init_validate() {
    var detail = document.getElementById("detail");
    detail.onsubmit = validateForm;
}
window.onload = init_validate;