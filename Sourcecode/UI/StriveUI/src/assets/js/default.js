//sidebar js //

$(document).ready(function () {
    $(".mobile-view-title").click(function () {
        $("#hide-mainmenu").hide();
        $("#show-submenu").show();
    });
    $(".back-to-list").click(function () {
        $("#hide-mainmenu").show();
        $("#show-submenu").hide();
    });
});
function openNav() {
    document.getElementById("navSliderMenu").style.width = "180px";
    document.getElementById("content-wrapper").style.marginLeft = "180px";
}

function closeNav() {
    document.getElementById("navSliderMenu").style.width = "0";
    document.getElementById("content-wrapper").style.marginLeft = "1%";
}

function openmbsidebar() {
    document.getElementById("mySidenav").style.width = "200px";
}

function closembsidebar() {
    document.getElementById("mySidenav").style.width = "0";
}
$(".menu li").on("click", function () {
    $(".menu li").removeClass("theme-secondary-background-color active");
    $(this).addClass("theme-secondary-background-color active");
});
$(".nav-slider-menu-items li a").on("click", function () {
    $(".nav-slider-menu-items li a").removeClass("theme-secondary-color text-underline");
    $(this).addClass("theme-secondary-color text-underline");
});
//sidebar js //

//datepicker//
// $(document).ready(function () {
//     var date = new Date();
//     var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
//     var end = new Date(date.getFullYear(), date.getMonth(), date.getDate());
//     $(".datepicker-view").datepicker({
//         format: "dd-mm-yyyy",
//         todayHighlight: true,
//         startDate: today,
//         autoclose: true,
//     });
//     $(".datepicker-view").datepicker("setDate", today);
// });

// multiselect dropdown
// $(document).ready(function () {
//     $(".multiselect").multiselect({
//         buttonWidth: "100%",
//         includeSelectAllOption: true,
//         nonSelectedText: "Select an Option",
//     });
// });

// file upload
$(".custom-file-input").on("change", function () {
    var fileName = $(this).val().split("\\").pop();
    $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
});

function getSelectedValues() {
    var selectedVal = $(".multiselect").val();
    for (var i = 0; i < selectedVal.length; i++) {
        function innerFunc(i) {
            setTimeout(function () {
                location.href = selectedVal[i];
            }, i * 2000);
        }
        innerFunc(i);
    }
}
//readonly fields
$(".disableform :input").attr("disabled", true);
//toggle//
var toggleState = false;
$("#Employeelisttoggle").click(function () {
    $("#emplistform").toggle();
    $("i", this).toggleClass("fa fa-angle-up  fa fa-angle-down");
    toggleState = !toggleState;
});
var toggleState = false;
$("#personaltoggle").click(function () {
    $("#personalform").toggle();
    $("i", this).toggleClass("fa fa-angle-up  fa fa-angle-down");
    toggleState = !toggleState;
});
var toggleState = false;
$("#documenttoggle").click(function () {
    $("#docform").toggle();
    $("i", this).toggleClass("fa fa-angle-up  fa fa-angle-down");
    toggleState = !toggleState;
});
var toggleState = false;
$("#Collisiontoggle").click(function () {
    $("#collisionform").toggle();
    $("i", this).toggleClass("fa fa-angle-up  fa fa-angle-down");
    toggleState = !toggleState;
});

//viewedit employee js//
$("#enable-form").click(function () {
    $("#personalform :input").prop("disabled", false);
    $("#edit-personalform").show();
    $("#enable-form").removeClass("theme-secondary-icon-color");
    document.getElementById("enable-form").style.color = "#ECE9E9";
});

$("#disable-form").click(function () {
    $("#personalform :input").prop("disabled", true);
    $("#edit-personalform").hide();
    $("#enable-form").addClass("theme-secondary-icon-color");
});

$("#doc-enable").click(function () {
    $("#doc-enable").hide();
    $(".btn-enable").removeClass("d-none");
    $(".md-doc").removeClass("theme-disabled-icon-color");
    $(".md-doc").addClass("theme-primary-icon-color");
    $(".md-close").removeClass("theme-disabled-icon-color");
    $(".md-close").addClass("theme-optional-icon-color");
    $("#doc-enable-btn").show();
});
$("#doc-disable").click(function () {
    $("#doc-enable").show();
    $("#doc-enable-btn").hide();
    $(".btn-enable").addClass("d-none");
    $(".md-doc").removeClass("theme-primary-icon-color");
    $(".md-doc").addClass("theme-disabled-icon-color");
    $(".md-close").removeClass("theme-optional-icon-color");
    $(".md-close").addClass("theme-disabled-icon-color");
});

// Collision
$("#collision-edit").click(function () {
    $("#collision-edit").hide();
    $(".collision-btn").removeClass("d-none");
    $(".md-pencil").removeClass("theme-disabled-icon-color");
    $(".md-pencil").addClass("theme-secondary-icon-color");
    $(".md-delete").removeClass("theme-disabled-icon-color");
    $(".md-delete").addClass("theme-optional-icon-color");
    $("#collision-enable-btn").show();
});
$("#collision-view").click(function () {
    $("#collision-edit").show();
    $(".collision-btn").addClass("d-none");
    $(".md-pencil").removeClass("theme-secondary-icon-color");
    $(".md-pencil").addClass("theme-disabled-icon-color");
    $(".md-delete").removeClass("theme-optional-icon-color");
    $(".md-delete").addClass("theme-disabled-icon-color");
    $("#collision-enable-btn").hide();
});

function passwordform() {
    var checkBox = document.getElementById("password-protect");
    var text = document.getElementById("password-form");
    if (checkBox.checked == true) {
        text.style.display = "flex";
    } else {
        text.style.display = "none";
    }
}
