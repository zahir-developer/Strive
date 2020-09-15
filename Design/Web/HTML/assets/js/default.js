
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

//dashboard screen
var toggleState = false;
$("#mainstreet").click(function () {
    $(".dashboard-card").toggle();
    $(this).toggleClass("click-up-icon");
    $(this).toggleClass("click-down-icon");
	$(".dashbd-title").toggleClass("dashbd-margin");
    $("i", this).toggleClass("mdi mdi-chevron-down-circle  mdi mdi-chevron-up-circle");
    toggleState = !toggleState;
});

var toggleState = false;
$("#mainstreets").click(function () {
    $(".dashboard-cards").toggle();
    $(this).toggleClass("click-up-icon");
    $(this).toggleClass("click-down-icon");
	$(".dashbd-titles").toggleClass("dashbd-margin");
    $("i", this).toggleClass("mdi mdi-chevron-down-circle  mdi mdi-chevron-up-circle");
    toggleState = !toggleState;
});
var toggleState = false;
$("#mainstreet-avg").click(function () {
    $(".dashboard-cards-avg").toggle();
    $(this).toggleClass("click-up-icon");
    $(this).toggleClass("click-down-icon");
	$(".dashbd-titles-avg").toggleClass("dashbd-margin");
    $("i", this).toggleClass("mdi mdi-chevron-down-circle  mdi mdi-chevron-up-circle");
    toggleState = !toggleState;
});

function openemp() {
    $("#show-search-emp").show();
    $(".internal-employee").removeClass("col-xl-9");
    $(".internal-employee").addClass("col-xl-6");
    $(".view-msg").removeClass("Message-box-slide");
    $(".view-msg").addClass("Message-box");
    $(".plus-icon").addClass("opacity-16");
}

function openNav() {
    document.getElementById("navSliderMenu").style.width = "180px";
    document.getElementById("content-wrapper").style.marginLeft = "180px";
    $(".table-condensed").css("width", "360px");
}

function closeNav() {
    document.getElementById("navSliderMenu").style.width = "0";
    document.getElementById("content-wrapper").style.marginLeft = "0";
    $(".table-condensed").css("width", "430px");
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
$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
});

//datepicker//
$(document).ready(function () {
    var date = new Date();
    var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    var end = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    $(".datepicker-view").datepicker({
        format: "dd-mm-yyyy",
        todayHighlight: true,
        startDate: today,
        autoclose: true,
    });
    $(".datepicker-view").datepicker("setDate", today);
});
//date-inline
$(document).ready(function () {
    var date = new Date();
    var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    var end = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    $(".date-inline").datepicker({
        inline: true,
    });
    $(".date-inline").datepicker("setDate", today);
});

$(document).ready(function () {
    $(".daterange").daterangepicker();
});
// multiselect dropdown
$(document).ready(function () {
    $(".multiselect").multiselect({
        buttonWidth: "100%",
        includeSelectAllOption: true,
        nonSelectedText: "Select an Option",
    });
});

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

//vehicle
$("#idmake").on("change", function () {
    $modal = $("#addmakeModal");
    if ($(this).val() === "Others") {
        $modal.modal("show");
    }
});
$("#idmodel").on("change", function () {
    $modal = $("#addmodelModal");
    if ($(this).val() === "Others-model") {
        $modal.modal("show");
    }
});
$("#idcolor").on("change", function () {
    $modal = $("#addcolorModal");
    if ($(this).val() === "Others-color") {
        $modal.modal("show");
    }
});

//calendar
document.addEventListener("DOMContentLoaded", function () {
    var Calendar = FullCalendar.Calendar;
    var Draggable = FullCalendarInteraction.Draggable;
    var containerEl = document.getElementById("external-events");
    var calendarEl = document.getElementById("calendar");
    //var checkbox = document.getElementById('drop-remove');
    // initialize the external events
    // -----------------------------------------------------------------
    new Draggable(containerEl, {
        itemSelector: ".fc-event",
        eventData: function (eventEl) {
            return {
                title: eventEl.innerText,
                customAttribute: "custom attribute",
            };
        },
    });
    // initialize the calendar
    // -----------------------------------------------------------------
    var calendar = new Calendar(calendarEl, {
        plugins: ["interaction", "dayGrid", "timeGrid"],
        header: {
            left: "",
            center: "prev,title,next",
            right: "timeGridDay,timeGridWeek",
        },
        defaultView: "timeGridDay",
        editable: true,
        droppable: true,
        // this allows things to be dropped onto the calendar
        eventClick: function (info) {
            console.log(info.event.title);
            console.log(info.event.customAttribute);
            $("#calendarModal").modal();
        },
    });
    calendar.render();
});

//timepicker
$(function () {
    $(".timepicker").timepicker({
        use24hours: true,
        minuteStep: 5,
        showSeconds: true,
        showMeridian: true,
        format: "hh:mm:ss",
        icons: {
            up: "fa fa-angle-up",
            down: "fa fa-angle-down",
        },
    });
});

// sales screen

function opengiftcard() {
    document.getElementById("Giftcardpopup").style.width = "300px";
    document.getElementById("creditcardpopup").style.width = "0";
}

function closegiftcard() {
    document.getElementById("Giftcardpopup").style.width = "0";
}

function opencreditcard() {
    document.getElementById("creditcardpopup").style.width = "300px";
    document.getElementById("Giftcardpopup").style.width = "0";
}

function closecreditcard() {
    document.getElementById("creditcardpopup").style.width = "0";
}

// Timeclock
$(".timeclock-table-toggle").on("click", function (e) {
    $(this).find('[class*="angle"]').toggleClass("fa-angle-down fa-angle-up");
    $("#collapse-timeclock-table").toggle();
    $(".hide-table").toggle();
});
//chart
		window.onload = function() {
			var chart = new CanvasJS.Chart("chartContainer", {
				title: {},
				legend: {
					verticalAlign: "bottom",
					horizontalAlign: "center"
				},
				axisX: {
					interval: 1
				},
				data: [{
					color: "#FD397A",
					type: "column",
					dataPoints: [{
						x: 1,
						y: 99,
						label: "Mainstreet"
					}, {
						x: 2,
						y: 99,
						label: "Old milton"
					}, {
						x: 3,
						y: 17,
						label: "Holcomb Bridge"
					}]
				}, {
					color: "#FFB822",
					type: "column",
					dataPoints: [{
						x: 1,
						y: 120,
						label: "Mainstreet"
					}, {
						x: 2,
						y: 10,
						label: "Old milton"
					}, {
						x: 3,
						y: 99,
						label: "Holcomb Bridge"
					}]
				}, {
					color: "#24489A",
					type: "column",
					dataPoints: [{
						x: 1,
						y: 99,
						label: "Mainstreet"
					}, {
						x: 2,
						y: 20,
						label: "Old milton"
					}, {
						x: 11,
						y: 10,
						label: "Holcomb Bridge"
					}]
				}]
			});
			chart.render();
		}
//dropzone

var dropzone = new Dropzone("#demo-upload", {
    previewTemplate: document.querySelector("#preview-template").innerHTML,
    parallelUploads: 2,
    thumbnailHeight: 120,
    thumbnailWidth: 120,
    maxFilesize: 3,
    filesizeBase: 1000,
    thumbnail: function (file, dataUrl) {
        if (file.previewElement) {
            file.previewElement.classList.remove("dz-file-preview");
            var images = file.previewElement.querySelectorAll("[data-dz-thumbnail]");
            for (var i = 0; i < images.length; i++) {
                var thumbnailElement = images[i];
                thumbnailElement.alt = file.name;
                thumbnailElement.src = dataUrl;
            }
            setTimeout(function () {
                file.previewElement.classList.add("dz-image-preview");
            }, 1);
        }
    },
});
// Now fake the file upload, since GitHub does not handle file uploads
// and returns a 404
var minSteps = 6,
    maxSteps = 60,
    timeBetweenSteps = 100,
    bytesPerStep = 100000;
dropzone.uploadFiles = function (files) {
    var self = this;
    for (var i = 0; i < files.length; i++) {
        var file = files[i];
        totalSteps = Math.round(Math.min(maxSteps, Math.max(minSteps, file.size / bytesPerStep)));
        for (var step = 0; step < totalSteps; step++) {
            var duration = timeBetweenSteps * (step + 1);
            setTimeout(
                (function (file, totalSteps, step) {
                    return function () {
                        file.upload = {
                            progress: (100 * (step + 1)) / totalSteps,
                            total: file.size,
                            bytesSent: ((step + 1) * file.size) / totalSteps,
                        };
                        self.emit("uploadprogress", file, file.upload.progress, file.upload.bytesSent);
                        if (file.upload.progress == 100) {
                            file.status = Dropzone.SUCCESS;
                            self.emit("success", file, "success", null);
                            self.emit("complete", file);
                            self.processQueue();
                            //document.getElementsByClassName("dz-success-mark").style.opacity = "1";
                        }
                    };
                })(file, totalSteps, step),
                duration
            );
        }
    }
};


