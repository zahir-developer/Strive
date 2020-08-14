import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { WashService } from '../../services/data-service/wash.service';

@Component({
  selector: 'app-no-of-washes',
  templateUrl: './no-of-washes.component.html',
  styleUrls: ['./no-of-washes.component.css']
})
export class NoOfWashesComponent implements OnInit {
  washCount: any;
  constructor(private wash: WashService) { }

  ngOnInit() {
    this.getDashboardDetails();
  }
  getDashboardDetails = () => {
    this.wash.data.subscribe((data: any) => {
      if (data.WashesCount !== undefined) {
        this.washCount = data.WashesCount.WashesCount;
      }
    });
  }
}
