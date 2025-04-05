
function displayTime() {
    /*if ($("#hdnIsOTPActive").val() == "True") {*/
    document.getElementById("divCountdown").style.display = "block";
    var timer2 = "02:59";
    var timer2 = "02:59";
    document.getElementById("btnVarifyOTPLogin").disabled = true;
    setInterval(function () {
        var timer = timer2.split(':');
        //by parsing integer, Iavoid all extra string processing
        var minutes = parseInt(timer[0], 10);
        var seconds = parseInt(timer[1], 10);
        --seconds;
        minutes = (seconds < 0) ? --minutes : minutes;
        if (minutes < 0) {
            clearInterval(interval);
            minutes = 0;
            seconds = 0;
        }
        seconds = (seconds < 0) ? 59 : seconds;
        seconds = (seconds < 10) ? '0' + seconds : seconds;
        //minutes = (minutes <10) ? minutes : minutes;
        $('.countdown').html(minutes + ':' + seconds);
        timer2 = minutes + ':' + seconds;
        if (seconds < 1) {
            document.getElementById("divCountdown").style.display = "none";
            document.getElementById("btnVarifyOTPLogin").disabled = false;
        }
    }, 1000);
    //}
}


function VarifyResendOTPLogin() {
    document.getElementById("btnVarifyOTPLogin").disabled = true;
    document.getElementById("OTPError1").style.display = "none";
    var timer2 = "00:59";
    setInterval(function () {
        var timer = timer2.split(':');
        //by parsing integer, Iavoid all extra string processing
        var minutes = parseInt(timer[0], 10);
        var seconds = parseInt(timer[1], 10);
        --seconds;
        minutes = (seconds < 0) ? --minutes : minutes;
        if (minutes < 0) clearInterval(interval);
        seconds = (seconds < 0) ? 59 : seconds;
        seconds = (seconds < 10) ? '0' + seconds : seconds;
        //minutes = (minutes <10) ? minutes : minutes;
        $('.countdown').html(minutes + ':' + seconds);
        timer2 = minutes + ':' + seconds;
        if (seconds < 1) {
            document.getElementById("divCountdown").style.display = "none";
            document.getElementById("btnVarifyOTPLogin").disabled = false;
        }
    }, 1000);

    document.getElementById("divCountdown").style.display = "block";

    var CompCode = parseInt($("#hdnCCode").val());
    var OTP = document.getElementById("txtVarifyLogin").value
    var Emailid = $("#hdnVarifyEmailid").val();
    $.ajax({
        url: "./Home/GetOTP?CompCode=" + CompCode + "&Emailid=" + Emailid,
        type: 'GET',
        datatype: "json",
        success: function (data) {
            if (data == "Success") {
                Alert.render("Your Resend OTP have been sent to your EmailId.");
                return false;
            }
            else if (data == "Fail") {
                Alert.render("Some error occured while sending OTP");
                return false;
            }
        },
        error: function (ex) {
        }
    });
    alert("Your Resend OTP have been sent to your EmailId.");
}

//function Register() {
//   // event.preventDefault(); // Prevent default form submission

//    // Remove existing error messages
//    $(".error-message").remove();

//    // Disable the register button to prevent multiple submissions
//    $('#btnRegister').attr('disabled', 'disabled');

//    // Get form input values
//    var name = $("#txtname").val().trim();
//    var email = $("#txtemail").val().trim();
//    var password = $("#txtpassword").val().trim();
//    var address = $("#txtaddress").val().trim();
//    var phone = $("#txtphone").val().trim();
//    var url = $("#RegisterModel").attr("action");

//    // Validation checks
//    var emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
//    var phonePattern = /^[0-9]{10}$/;
//    var isValid = true;

//    if (name === "") {
//        $("#txtname").after("<span class='error-message text-danger'>Name is required</span>");
//        isValid = false;
//    }

//    if (email === "" || !emailPattern.test(email)) {
//        $("#txtemail").after("<span class='error-message text-danger'>Enter a valid email address</span>");
//        isValid = false;
//    }

//    if (password === "" || password.length < 6) {
//        $("#txtpassword").after("<span class='error-message text-danger'>Password must be at least 6 characters</span>");
//        isValid = false;
//    }

//    if (address === "") {
//        $("#txtaddress").after("<span class='error-message text-danger'>Address is required</span>");
//        isValid = false;
//    }

//    if (phone === "" || !phonePattern.test(phone)) {
//        $("#txtphone").after("<span class='error-message text-danger'>Enter a valid 10-digit phone number</span>");
//        isValid = false;
//    }

//    // Stop function if validation fails
//    if (!isValid) {
//        $('#btnRegister').removeAttr('disabled'); // Re-enable the button
//        return;
//    }

//    // Show loader
//    document.getElementById('loader').style.display = 'flex';

//    // Send data as JSON
//    $.ajax({
//        type: "POST",
//        url: "../Home/Registration",
//        contentType: "application/json",
//        data: JSON.stringify({
//            Name: name,
//            Email: email,
//            Password: password,
//            Address: address,
//            Phone: phone
//        }),
//        success: function (response) {
//            document.getElementById('loader').style.display = 'none'; // Hide loader

//            if (response.success) {
//                $("#RegisterModel").modal("hide");
//                alert(response.message);
//                window.location.reload(); // Refresh the page after successful registration
//            } else {
//                alert("Registration failed! Please check your details and try again.");
//                $('#btnRegister').removeAttr('disabled'); // Re-enable the button
//            }
//        },
//        error: function () {
//            document.getElementById('loader').style.display = 'none'; // Hide loader
//            alert("Error! Registration could not be completed. Try again later.");
//            $('#btnRegister').removeAttr('disabled'); // Re-enable the button
//        }
//    });
//}

$(document).ready(function () {
    var states = [
        { id: 1, title: 'Item 1', description: 'Sofa', label: 'Sofa' },
        { id: 1, title: 'Item 2', description: 'Beds', label: 'Beds' },
        { id: 3, title: 'Item 3', description: 'DinningSeats', label: 'DinningSeats' },
        { id: 4, title: 'Item 4', description: 'LuxuryFurniture', label: 'LuxuryFurniture' },
        { id: 5, title: 'Item 5', description: 'CenterTables', label: 'CenterTables' },
        { id: 6, title: 'Item 6', description: 'OfficialFurniture', label: 'OfficialFurniture' },
        { id: 6, title: 'Item 7', description: 'CabinetSideBoard', label: 'CabinetSideBoard' },
        { id: 6, title: 'Item 8', description: 'Recliners', label: 'Recliners' },
        { id: 6, title: 'Item 9', description: 'SectionalSofa', label: 'SectionalSofa' },
        { id: 6, title: 'Item 10', description: 'ShoeRacks', label: 'ShoeRacks' },
        { id: 6, title: 'Item 11', description: 'StudyTables', label: 'StudyTables' },
        { id: 6, title: 'Item 12', description: 'Wardrobe', label: 'Wardrobes' },






        // ... (remaining items)
    ];

    $('#search').autocomplete({
        source: function (request, response) {
            var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), 'i');
            var matches = $.grep(states, function (value) {
                return matcher.test(value.title) || matcher.test(value.description) || matcher.test(value.label);
            });
            response(matches);
        },
        select: function (event, ui) {
            // Log the selected item
            console.log('Selected: ' + ui.item.id + ', ' + ui.item.title + ', ' + ui.item.description);

            var Lbl = ui.item.label;
            $("#hdnSearch_test").val(Lbl);
            // Here you can perform any action with the selected item
            // For example, display the selected item details in a specific div
            $('#search').html('Selected Item: ' + ui.item.title + '<br>Description: ' + ui.item.description);
            //var searchUrl = '@Url.Action("SearchItem", "Home")';
            //window.location.href = searchUrl; // This will generate the correct URL


            window.location.href = '/Furniture/Home/SearchItem?Id=' + ui.item.label; // Ensure this matches your routing

        },
        focus: function (event, ui) {
            $('#search').val(ui.item.label);
            return false;
        }
    });

    $.ui.autocomplete.prototype._renderItem = function (ul, item) {
        return $('<li>')
            //.append('<a>' + item.id + ' - ' + item.title + ': ' + item.description + '</a>')
            .append('<a>' + item.description + '</a>')

            .appendTo(ul);
    };
});


function OpenLoginModel() {

    /*  $("#RegisterModel").modal("hide");*/
    /*   $("#LoginModel").modal("show");*/
    /*  $("#access_account").modal("show");*/
    $("#loginModal").modal("show");

}


function AlreadyRegister() {
    $("#RegisterModel").modal("hide");
    $("#loginModal").modal("show");

}

function OpenRegisterModel() {
    $("#loginModal").modal("hide");
    $("#RegisterModel").modal("show");
}

function OpenRegisterModel1() {
    $("#LoginModel").modal("hide");
    $("#RegisterModel").modal("show");

}

function OpenForgetPasswordModel() {

    $("#loginModal").modal("hide");
    $("#ForgetPasswordModal").modal("show");
}

function Login(event) {
    event.preventDefault(); // Prevent default form submission

    // Remove existing validation messages
    $('.error-message').remove();

    // Get values from input fields
    var userEmail = $("#txtUserEmail").val().trim();
    var password = $("#txtPassword").val().trim();
    var url = "/Furniture/Admin/Login"; // Adjust this based on your routing
    var emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    var isValid = true;

    // Validation
    if (!userEmail) {
        $("#txtUserEmail").after("<span class='error-message text-danger'>Email is required</span>");
        isValid = false;
    } else if (!emailPattern.test(userEmail)) {
        $("#txtUserEmail").after("<span class='error-message text-danger'>Invalid email format</span>");
        isValid = false;
    }

    if (!password) {
        $("#txtPassword").after("<span class='error-message text-danger'>Password is required</span>");
        isValid = false;
    }

    if (!isValid) return;

    // Show loader
    document.getElementById('loader').style.display = 'flex';

    // AJAX request


    $.ajax({
        type: 'POST',
        url: url,
        data: {
            UserEmail: userEmail,
            Password: password,
            __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val() // Add CSRF token
        },
        dataType: 'json',
        success: function (response) {
            document.getElementById('loader').style.display = 'none';
            if (response.Success) {
                if (response.IsOTP) {
                    $("#OTPModal").modal("show");
                    displayTime();
                } else {
                    window.location.href = '/Furniture/Home/Home?Identity=' + response.SessionId;
                }
            } else {
                alert(response.Message || "User ID or Password is incorrect.");
            }
        },
        error: function (xhr, status, error) {
            document.getElementById('loader').style.display = 'none';
            alert("Login failed. Please check your network connection.");
        }
    });

}

function ForgetPassword(event) {

        event.preventDefault(); // Prevent default button behavior

        var userName = $("#Ftxtuserid").val().trim();
        var email = $("#Ftxtemailid").val().trim();
        var emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
        var url = "/Furniture/Admin/ForgetPassword"; // Adjust this based on your routing

        // Clear previous error messages
        $(".error-message").remove();

        var isValid = true;

        // Validate Username
        if (userName === "") {
            $("#Ftxtuserid").after("<span class='error-message text-danger'>Username is required</span>");
            isValid = false;
        }

        // Validate Email
        if (email === "") {
            $("#Ftxtemailid").after("<span class='error-message text-danger'>Email is required</span>");
            isValid = false;
        } else if (!emailPattern.test(email)) {
            $("#Ftxtemailid").after("<span class='error-message text-danger'>Invalid email format</span>");
            isValid = false;
        }

        // Stop function if validation fails
        if (!isValid) {
            return;
        }

    $('#btnForgetPassword').attr('disabled', 'disabled'); // Disable button to prevent multiple clicks
    var url = $("#ForgetPasswordModal").attr("action"); // Fetch form action URL

    var formData = {
        UserName: $("#Ftxtuserid").val(),
        EmailId: $("#Ftxtemailid").val(),
        __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val() // CSRF Token
    };

    $.ajax({
        type: "POST",
        url: url,
        data: formData,
        dataType: "json",
        success: function (response) {
            $('#btnForgetPassword').removeAttr('disabled'); // Re-enable button
            if (response.success) {
                alert("Check your email, the password reset link has been sent!");
                $("#ForgetPasswordModal").modal("hide");
            } else {
                alert(response.message || "User not found or invalid details.");
            }
        },
        error: function (xhr, status, error) {
            $('#btnForgetPassword').removeAttr('disabled'); // Re-enable button
            alert("An error occurred. Please try again.");
        }
    });
}



function Register(event) {

    event.preventDefault(); // Prevent default form submission

   // $("#btnRegister").attr("disabled", true); // Disable button to prevent multiple clicks

   // var url = $(this).attr("action"); // Fetch form action URL
    var url = "/Furniture/Admin/Registration"; // Adjust this based on your routing

    var formData = {
        Name: $("#txtname").val(),
        Email: $("#txtemail").val(),
        Password: $("#txtpassword").val(),
        Address: $("#txtaddress").val(),
        Phone: $("#txtphone").val(),
        __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val() // CSRF Token
    };

    $.ajax({
        type: "POST",
        url: url,
        data: formData,
        dataType: "json",
        success: function (response) {
            $("#btnRegister").removeAttr("disabled"); // Re-enable button

            if (response.success) {
                alert("Registration successful! Redirecting to login page...");
                $("#RegisterModel").modal("hide"); // Hide modal on success
                // Redirect or do additional actions here
            } else {
                alert(response.message || "Registration failed! Please try again.");
            }
        },
        error: function (xhr, status, error) {
            $("#btnRegister").removeAttr("disabled"); // Re-enable button
            alert("An error occurred. Please check your inputs and try again.");
        }
    });
}



function VarifyOTP() {
    document.getElementById('loader').style.display = 'flex'; // Change to 'flex'
    $('#btnVarifyOTPLogin').attr('disabled', 'disabled');
    var url = $("#OTPModal").attr("action");
    var Emailid = "Test@123";
    var formdata = new FormData();

    formdata.append("OTP", $("#txtotp").val());

    $.ajax({
        type: "POST", // match the form method
        url: url, // get the action URL from the form
        data: formdata,
        processData: false,
        contentType: false
    }).done(function (data) {
        if (data == "true" || data == "True") {
            window.location.href = "Home/Home?OTP=" + OTP + "&Emailid=" + Emailid;
            $("#OTPModal").modal("hide");

            return false;
        }
        else if (data == "false" || data == false) {
            document.getElementById("OTPError1").style.display = "block";

            return false;
        }

    });

}


function VarifyOTPLogin() {
    var OTP = $("#txtVarifyLogin12").val();
    var Emailid = "";
    if (OTP == "") {
        alert("Enter Your OTP");
    }
    else {
        $.ajax({
            type: "POST",
            url: "/Home/VerifyLoginOTP1", // Use an absolute URL
            data: JSON.stringify({ "OTP": OTP }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data === true) {
                    window.location.href = "/Home/VerifyLoginOTPRedirect?OTP=" + OTP + "&Emailid=" + Emailid;
                } else {
                    document.getElementById("OTPError1").style.display = "";
                }
            },
            error: function (xhr, status, error) {
                console.error("Error occurred: ", status, error);
                alert("An error occurred: " + xhr.responseText);
            }
        });
    }
}


function Submit() {
    var Email = $("#Ltxtemail").val();
    var Password = $("#Ltxtpassword").val();

    $.ajax({
        type: "POST", //Get Is  Not Working 
        url: "../Home/GetLogin",
        data: JSON.stringify({
            "UserName": Email,
            "Password": Password,
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (response) {
            alert(response);
            if (response == "True") {
                alert("You Have Login Sucessfully");

                $("#loginModal").modal("hide");
                $("#OTPModal").modal("show");
            }
            else {
                alert("Your Id Passsword wrong");
            }
        }
    });

}
