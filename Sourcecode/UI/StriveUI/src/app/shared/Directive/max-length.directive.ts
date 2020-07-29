import { Directive, ElementRef, HostListener, Input, Renderer2 } from '@angular/core';

@Directive({
  selector: '[appMaxLength]'
})
export class MaxLengthDirective {
  @Input() appMaxLength: number;
  constructor(
    private el: ElementRef,
    private renderer: Renderer2
  ) { }

  @HostListener('input', ['$event']) onChange(event) {
    this.renderer.setAttribute(this.el.nativeElement, 'maxLength', this.appMaxLength.toString());
  }

}
