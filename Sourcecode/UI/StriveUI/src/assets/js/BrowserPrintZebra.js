    var BrowserPrint = function() {
function e(e) {
    return s + e
}

function n(e, n) {
    var i = new XMLHttpRequest;
    return "withCredentials" in i ? i.open(e, n, !0) : "undefined" != typeof XDomainRequest ? (i = new XDomainRequest, i.open(e, n)) : i = null, i
}

function i(e, n, i, t) {
    return void 0 != e && (void 0 == i && (i = e.sendFinishedCallback), void 0 == t && (t = e.sendErrorCallback)), n.onreadystatechange = function() {
        n.readyState === XMLHttpRequest.DONE && 200 === n.status ? i(n.responseText) : n.readyState === XMLHttpRequest.DONE && t(n.responseText)
    }, n
}
var t = {},
    r = 2,
    s = "http://localhost:9100/";
return "https:" === location.protocol && (s = "https://localhost:9101/"), t.Device = function(t) {
    var s = this;
    this.name = t.name, this.deviceType = t.deviceType, this.connection = t.connection, this.uid = t.uid, this.version = r, this.provider = t.provider, this.manufacturer = t.manufacturer, this.sendErrorCallback = function(e) {}, this.sendFinishedCallback = function(e) {}, this.send = function(t, r, o) {
        var a = n("POST", e("write"));
        if (a) {
            i(s, a, r, o);
            var c = {
                device: {
                    name: this.name,
                    uid: this.uid,
                    connection: this.connection,
                    deviceType: this.deviceType,
                    version: this.version,
                    provider: this.provider,
                    manufacturer: this.manufacturer
                },
                data: t
            };
            a.send(JSON.stringify(c))
        }
    }, this.sendUrl = function(t, r, o) {
        var a = n("POST", e("write"));
        if (a) {
            i(s, a, r, o);
            var c = {
                device: {
                    name: this.name,
                    uid: this.uid,
                    connection: this.connection,
                    deviceType: this.deviceType,
                    version: this.version,
                    provider: this.provider,
                    manufacturer: this.manufacturer
                },
                url: t
            };
            a.send(JSON.stringify(c))
        }
    }, this.readErrorCallback = function(e) {}, this.readFinishedCallback = function(e) {}, this.read = function(t, r) {
        var o = n("POST", e("read"));
        if (o) {
            i(s, o, t, r);
            var a = {
                device: {
                    name: this.name,
                    uid: this.uid,
                    connection: this.connection,
                    deviceType: this.deviceType,
                    version: this.version,
                    provider: this.provider,
                    manufacturer: this.manufacturer
                }
            };
            o.send(JSON.stringify(a))
        }
    }, this.sendThenRead = function(e, n, i) {
        this.send(e, function(e) {
            return function() {
                e.read(n, i)
            }
        }(this), i)
    }
}, t.getLocalDevices = function(r, s, o) {
    var a = n("GET", e("available"));
    a && (finishedFunction = function(e) {
        response = e, response = JSON.parse(response);
        for (var n in response)
            if (response.hasOwnProperty(n) && response[n].constructor === Array) {
                arr = response[n];
                for (var i = 0; i < arr.length; ++i) arr[i] = new t.Device(arr[i])
            }
        return void 0 == o ? void r(response) : void r(response[o])
    }, i(void 0, a, finishedFunction, s), a.send())
}, t.getDefaultDevice = function(r, s, o) {
    var a = "default";
    void 0 != r && null != r && (a = a + "?type=" + r);
    var c = n("GET", e(a));
    c && (finishedFunction = function(e) {
        if (response = e, "" == response) return void s(null);
        response = JSON.parse(response);
        var n = new t.Device(response);
        s(n)
    }, i(void 0, c, finishedFunction, o), c.send())
}, t.readOnInterval = function(e, n, i) {
    void 0 != i && 0 != i || (i = 1), readFunc = function() {
        e.read(function(e) {
            n(e), setTimeout(readFunc, i)
        }, function(e) {
            setTimeout(readFunc, i)
        })
    }, setTimeout(readFunc, i)
}, t.bindFieldToReadData = function(e, n, i, r) {
    t.readOnInterval(e, function(e) {
        "" != e && (n.value = e, void 0 != r && null != r && r())
    }, i)
}, t
}();

var available_printers = null;
var selected_category = null;
var default_printer = null;
var selected_printer = null;
var format_start = "^XA^LL200^FO80,50^A0N36,36^FD";
var format_end = "^FS^XZ";
var default_mode = true;
var selected_printer_index = 0;

function setup_web_print() {
$('#printer_select').on('change', onPrinterSelected);
showLoading("Loading Printer Information...");
default_mode = true;
selected_printer = null;
available_printers = null;
selected_category = null;
default_printer = null;

BrowserPrint.getDefaultDevice('printer', function (printer) {
  console.log('getDefaultDevice');
  default_printer = printer
  if ((printer != null) && (printer.connection != undefined)) {
    selected_printer = printer;
    var printer_details = $('#printer_details');
    var selected_printer_div = $('#selected_printer');

    selected_printer_div.text("Using Default Printer: " + printer.name);
    hideLoading();
    printer_details.show();
    $('#print_form').show();

  }
  BrowserPrint.getLocalDevices(function (printers) {
    available_printers = printers;
    //var sel = document.getElementById("printers");
    var printers_available = false;
    //sel.innerHTML = "";
    if (printers.connection !== undefined) {
      for (var i = 0; i < printers.length; i++) {
        if (printers[i].connection == 'usb') {
          var opt = document.createElement("option");
          opt.innerHTML = printers[i].connection + ": " + printers[i].uid;
          opt.value = printers[i].uid;
          selected_printer_index = printers[i].uid;
          //sel.appendChild(opt);
          printers_available = true;
        }
      }
    }

    if (!printers_available) {
      showErrorMessage("No Zebra Printers could be found!");
      hideLoading();
      $('#print_form').hide();
      return;
    }
    else if (selected_printer == null) {
      default_mode = false;
      changePrinter();
      $('#print_form').show();
      hideLoading();
    }
  }, undefined, 'printer');
},
  function (error_response) {
    showBrowserPrintNotFound();
  });
};
function showBrowserPrintNotFound() {
showErrorMessage("An error occured while attempting to connect to your Zebra Printer. You may not have Zebra Browser Print installed, or it may not be running. Install Zebra Browser Print, or start the Zebra Browser Print Service, and try again.");

};
function sendData(data) {
    console.log("PrintData", data);
console.log("Printing...");

if(default_printer.connection === null)
{
    alert('No Printer Found...!');
}

checkPrinterStatus(function (text) {
  if (text == "Ready to Print") {
    default_printer.send(data, printComplete, printerError);
  }
  else {
    printerError(text);
  }
});
};
function checkPrinterStatus(finishedFunction) {
selected_printer.sendThenRead("~HQES",
  function (text) {
    var that = this;
    var statuses = new Array();
    var ok = false;
    var is_error = text.charAt(70);
    var media = text.charAt(88);
    var head = text.charAt(87);
    var pause = text.charAt(84);
    // check each flag that prevents printing
    if (is_error == '0') {
      ok = true;
      statuses.push("Ready to Print");
    }
    if (media == '1')
      statuses.push("Paper out");
    if (media == '2')
      statuses.push("Ribbon Out");
    if (media == '4')
      statuses.push("Media Door Open");
    if (media == '8')
      statuses.push("Cutter Fault");
    if (head == '1')
      statuses.push("Printhead Overheating");
    if (head == '2')
      statuses.push("Motor Overheating");
    if (head == '4')
      statuses.push("Printhead Fault");
    if (head == '8')
      statuses.push("Incorrect Printhead");
    if (pause == '1')
      statuses.push("Printer Paused");
    if ((!ok) && (statuses.Count == 0))
      statuses.push("Error: Unknown Error");
    finishedFunction(statuses.join());
  }, printerError);
};
function hidePrintForm() {
$('#print_form').hide();
};
function showPrintForm() {
$('#print_form').show();
};
function showLoading(text) {
$('#loading_message').text(text);
$('#printer_data_loading').show();
hidePrintForm();
$('#printer_details').hide();
$('#printer_select').hide();
};
function printComplete() {
hideLoading();
alert("Printing complete");
}
function hideLoading() {
$('#printer_data_loading').hide();
if (default_mode == true) {
  showPrintForm();
  $('#printer_details').show();
}
else {
  $('#printer_select').show();
  showPrintForm();
}
};
function changePrinter() {
default_mode = false;
selected_printer = null;
$('#printer_details').hide();
if (available_printers == null) {
  showLoading("Finding Printers...");
  $('#print_form').hide();
  setTimeout(changePrinter, 200);
  return;
}
$('#printer_select').show();
onPrinterSelected();

}
function onPrinterSelected() {
selected_printer = available_printers[selected_printer_index];
}
function showErrorMessage(text) {
$('#main').hide();
$('#error_div').show();
$('#error_message').html(text);
}
function printerError(text) {
showErrorMessage("An error occurred while printing. Please try again." + text);
}
function trySetupAgain() {
$('#main').show();
$('#error_div').hide();
setup_web_print();
//hideLoading();
}