import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.css']
})
export class CardComponent implements OnInit {
  @Input() showHeader = true;
  @Input() showFooter = false;
  @Input() loading = false;
  @Input() collapsable = false;
  constructor() { }

  ngOnInit(): void {
  }

}
