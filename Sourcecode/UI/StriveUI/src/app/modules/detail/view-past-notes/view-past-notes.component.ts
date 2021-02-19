import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-view-past-notes',
  templateUrl: './view-past-notes.component.html',
  styleUrls: ['./view-past-notes.component.css']
})
export class ViewPastNotesComponent implements OnInit {
  @Input() viewNotes?: any;
  page = 1;
  pageSize = 5;
  collectionSize: number;
  constructor() { }

  ngOnInit(): void {
    this.collectionSize = Math.ceil(this.viewNotes.length / this.pageSize) * 10;
  }

}
