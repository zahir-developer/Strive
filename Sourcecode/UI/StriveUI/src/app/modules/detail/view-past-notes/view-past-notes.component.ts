import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-view-past-notes',
  templateUrl: './view-past-notes.component.html',
  styleUrls: ['./view-past-notes.component.css']
})
export class ViewPastNotesComponent implements OnInit {
  @Input() viewNotes?: any;
  constructor() { }

  ngOnInit(): void {
    console.log(this.viewNotes, 'viewNotes');
  }

}
