import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-no-of-washes',
  templateUrl: './no-of-washes.component.html',
  styleUrls: ['./no-of-washes.component.css']
})
export class NoOfWashesComponent implements OnInit {

  @Input() dashBoard? : any;
  washCount: any;
  constructor() { }

  ngOnInit() {
    console.log(this.dashBoard);
    this.washCount = this.dashBoard.WashesCount.WashesCount;
  }

}
