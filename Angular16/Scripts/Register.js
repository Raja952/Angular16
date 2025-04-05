


function OpenLoginModel() {

    $("#RegisterModel").modal("hide");
    $("#LoginModel").modal("show");
}

function OpenRegisterModel() {
    $("#LoginModel").modal("hide");
    $("#RegisterModel").modal("show");
}



function Login() {

    var Email = $("#Ltxtemail").val();
    var Password = $("#Ltxtpassword").val();

    if (Email == "" || Email == undefined) {
        alert("Enter The Email Number");
        return false;
    }
    if (Password == "" || Password == undefined) {
        alert("Enter The Password");
        return false;
    }

    $.ajax({
        type: "POST",
        url: "../Home/GetLogin",
        data: JSON.stringify({
            "UserName": Email,
            "Password": Password
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (response) {
            alert(response);
            if (response == 1) {
                alert("You Have Login Sucessfully");
                /*window.location.replace("Home");*/
                $("#LoginModel").modal("hide");?
                $("#OTPModal").modal("show");

            }
            else {
                alert("Your Id Passsword wrong");
            }
        }
    });
}

function Register() {
   
    var Name = $("#txtname").val();
    var Password = $("#txtpassword").val();
    var Email = $("#txtemail").val();
    var Address = $("#txtaddress").val();
    var Phone = $("#txtphone").val();

    if (Name == "" || Name == undefined) {
        alert("Enter The Name Number");
        return false;
    }
    if (Password == "" || Password == undefined) {
        alert("Enter The Password");
        return false;
    }
    if (Email == "" || Email == undefined) {
        alert("Enter The Email");
        return false;
    }
    if (Address == "" || Address == undefined) {
        alert("Enter The Address");
        return false;
    }
    if (Phone == "" || Phone == undefined) {
        alert("Enter The Phone");
        return false;
    }


    $.ajax({
        type: "POST",
        url: "../Members/InsertGetMemberDetails",
        data: JSON.stringify({
            "Name": Name,
            "Email": Password,
            "Password": Email,
            "Address": Address,
            "Phone":Phone
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (response) {
            alert(response);
            if (response == 1) {
                alert("You Have Register Sucessfully");
                window.location.replace("Home");

            }
            else {
                alert("There are Some Problem");
            }
        }
    });

}

function VarifyOTPLogin() {
  
    var OTP = document.getElementById("txtVarifyLogin").value;
    var Emailid = "";

    if (OTP == "") {
        alert("Enter Your OTP");
    }
    else {
        var Emailid = "sushilguptamcmt@gmail.com"
        $.ajax({
            url: "../Home/VerifyLoginOTP",
            type: 'GET',
            data: JSON.stringify({
                "OTP": OTP,
                "Emailid": Emailid
            }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data == "True" || data == true) {
                    window.location.href = "Home/VerifyLoginOTPRedirect?OTP=" + OTP + "&Emailid=" + Emailid;
                    return false;
                }
                else if (data == "False" || data == false) {
                    document.getElementById("OTPError1").style.display = "";
                    return false;
                }
            },
            error: function (ex) {
            }
        });

    }
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