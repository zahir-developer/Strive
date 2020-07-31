import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-collision-list',
  templateUrl: './collision-list.component.html',
  styleUrls: ['./collision-list.component.css']
})
export class CollisionListComponent implements OnInit {
  @Input() employeeId?: any;
  @Input() employeeCollision?: any;
  constructor() { }

  ngOnInit(): void {
    console.log(this.employeeCollision, 'collision');
  }

}
