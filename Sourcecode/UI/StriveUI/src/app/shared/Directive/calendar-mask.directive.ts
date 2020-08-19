import { Directive, HostListener, Input } from '@angular/core';
import { NgControl } from '@angular/forms';
import * as moment from 'moment';

@Directive({
  selector: '[formControlName][appCalendarMask]'
})
export class CalendarMaskDirective {

  disallowedKeyCodes = [
    65, 66, 67, 68, 69,
    70, 71, 72, 73, 74, 75, 76, 77, 78, 79,
    80, 81, 82, 83, 84, 85, 86, 87, 88, 89,
    90, 13
  ];

  dateKeyCodes = [
    47,
    48, 49, 50, 51, 52, 53, 54, 55, 56, 57,
    96, 97, 98, 99, 100, 101, 102, 103, 104, 105
  ];

  constructor(public model: NgControl) { }

  @HostListener('keydown', ['$event']) onKeyDown(event: KeyboardEvent) {
    if (event) {
      if (event.keyCode) {
        // prevent alphabetical input from being added
        if (this.disallowedKeyCodes.indexOf(event.keyCode) >= 0) {
          event.preventDefault();
          return;
        }

        // prevent extra characters (more than 8 digits) from being added
        if (this.dateKeyCodes.indexOf(event.keyCode) >= 0) {
          if (event.target) {
            var htmlInputElement = event.target as HTMLInputElement;
            if (htmlInputElement) {
              var value = htmlInputElement.value.replace(/\//g, '');
              if (value.length >= 9) {
                event.preventDefault();
                return;
              }
            }
          }
        }
      }
    }
  }

  @HostListener('keyup', ['$event']) onKeyUp(event: KeyboardEvent) {
    if (event) {
      if (event.keyCode) {
        // if deleting characters, ignore the following logic
        if (event.keyCode != 8) {
          if (event.target) {
            var htmlInputElement = event.target as HTMLInputElement;
            if (htmlInputElement) {
              // trim slashes out of input text
              var value = htmlInputElement.value.replace(/\//g, '');
              if (value != null) {
                // remove extra characters beyond the 8th
                if (value.length > 8) {
                  value = value.substring(0, 8);
                }

                // add slashes in the appropriate places
                if (value.length >= 4) {
                  value = value.slice(0, 4) + '/' + value.slice(4);
                }
                if (value.length >= 2) {
                  value = value.slice(0, 2) + '/' + value.slice(2);
                }

                // if we have the full date, submit it, otherwise just update the display
                if (value.length == 10) {
                  // let moment read in the parsed value
                  let dt = moment(value);

                  // push value back to the control
                  this.model.control.setValue(dt.format("MM/DD/YYYY"));  // this sets the model value
                  this.model.valueAccessor.writeValue(dt.format("MM/DD/YYYY"));  // this sets the view value
                  let a: HTMLAnchorElement;
                  a = document.createElement('a');
                  document.body.appendChild(a);
                  a.click();
                } else {
                  htmlInputElement.value = value;
                }
              }
            }
          }
        }
      }
    }
  }

  @HostListener('input', ['$event']) onChange(event: Event) {
    if (event) {
      if (event.target) {
        var htmlInputElement = event.target as HTMLInputElement;
        if (htmlInputElement) {
          // only attempt to reformat if input date does not contains '/'
          if (htmlInputElement.value.indexOf('/') == -1) {
            // only attempt to reformat if number of digits enter is 6 (MMDDYY) or 8 (MMDDYYYY)
            if (htmlInputElement.value.length == 6 || htmlInputElement.value.length == 8) {
              // parse into MM/DD/YY or MM/DD/YYYY
              var parsed =
                htmlInputElement.value.substring(0, 2) +
                '/' +
                htmlInputElement.value.substring(2, 4) +
                '/' +
                htmlInputElement.value.substring(4);

              // let moment read in the parsed value
              let dt = moment(parsed);

              // finally, let moment format to MM/DD/YYYY
              this.model.control.setValue(dt.format("MM/DD/YYYY"));  // this sets the model value
              this.model.valueAccessor.writeValue(dt.format("MM/DD/YYYY"));  // this sets the view value
              let a: HTMLAnchorElement;
              a = document.createElement('a');
              document.body.appendChild(a);
              a.click();
            } else if (htmlInputElement.value === '') {
              this.model.control.setValue('');  // this clears the model value
              this.model.valueAccessor.writeValue('');  // this clears the view value
            }
          }
        }
      }
    }
  }

}
