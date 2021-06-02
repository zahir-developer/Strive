import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-view-past-notes',
  templateUrl: './view-past-notes.component.html'
})
export class ViewPastNotesComponent implements OnInit {
  @Input() viewNotes?: any;
  page = 1;
  pageSize = 5;
  collectionSize: number;
  constructor() { }

  ngOnInit(): void {
    this.collectionSize = Math.ceil(this.viewNotes.length / this.pageSize) * 10;
    if (this.viewNotes?.length > 0) {
      for (let i = 0; i < this.viewNotes.length; i++) {
        this.viewNotes[i].VehicleModel == 'None' ? this.viewNotes[i].VehicleModel =  'Unk' : this.viewNotes[i].VehicleModel ;
      }
    }
  }

}
