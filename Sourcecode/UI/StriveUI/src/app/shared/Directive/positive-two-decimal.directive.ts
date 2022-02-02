import { Directive, ElementRef, HostListener } from '@angular/core';

@Directive({
  selector: '[appPosTwoDecimalNumber]'
})
export class PositiveTwoDecimalNumberDirective {

  // Allow decimal numbers and negative values
  private regex: RegExp = new RegExp(/^(?!$)\d{0,9}(?:\.\d{0,2})?$/g);
  // Allow key codes for special events. Reflect :
  // Backspace, tab, end, home
  private specialKeys: Array<string> = ['Backspace', 'Tab', 'End', 'Home', 'ArrowLeft', 'ArrowRight', 'Del', 'Delete', '-' ];

  constructor(private el: ElementRef) { }

  @HostListener('keydown', ['$event'])
  keydown(event: KeyboardEvent) {    
    if(event.code === 'Minus' && this.el.nativeElement.value.indexOf(event.key) !== -0)
    {
      event.preventDefault();
    }

    // Allow Backspace, tab, end, and home keys
    if (this.specialKeys.indexOf(event.key) !== -1) {
      return;
    }

    let current: string = this.el.nativeElement.value.replace('-', '');
    const position = this.el.nativeElement.selectionStart;
    const next: string = [current.slice(0, position), event.key == 'Decimal' ? '.' : event.key, current.slice(position)].join('');
    if (next && !String(next).match(this.regex)) {
      event.preventDefault();
    }
  }

}