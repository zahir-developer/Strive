import { Directive, HostListener, ElementRef } from '@angular/core';

@Directive({
  selector: '[appAlphaNumeric]'
})
export class AlphaNumericDirective {

  private regex: RegExp = new RegExp("^[a-zA-Z0-9\\s]+$");
  private specialKeys: Array<string> = ['Backspace', 'Space', 'Tab', 'End', 'Home'];

  constructor(private el: ElementRef) {}

  @HostListener('keydown', ['$event'])
  onKeyDown(event: KeyboardEvent) {
  
   let current: string = this.el.nativeElement.value;
  
   let next: string = current.concat(event.key);
   if (next.startsWith(" ")) {
    event.preventDefault();
    return;
  }
   if (next && !String(next).match(this.regex)) {
      event.preventDefault();
   }
  }  
 }