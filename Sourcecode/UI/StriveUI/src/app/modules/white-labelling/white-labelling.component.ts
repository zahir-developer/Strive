import { Component, OnInit, ViewChild } from '@angular/core';
import { Observable } from 'rxjs';
import { WhiteLabellingSectionComponent } from './white-labelling-section/white-labelling-section.component';

@Component({
  selector: 'app-white-labelling',
  templateUrl: './white-labelling.component.html',
  styleUrls: ['./white-labelling.component.css']
})
export class WhiteLabellingComponent implements OnInit {
  @ViewChild(WhiteLabellingSectionComponent) whiteLabellingSectionComponent: WhiteLabellingSectionComponent;
  constructor() { }

  ngOnInit(): void {
  }

  canDeactivate(): boolean | Observable<boolean> | Promise<boolean> {
    this.whiteLabellingSectionComponent.getAllWhiteLabelDetail();
    return true;
  }

}
