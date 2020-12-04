import { Component, OnInit } from '@angular/core';
import { WashService } from '../../services/data-service/wash.service';

@Component({
  selector: 'app-score',
  templateUrl: './score.component.html',
  styleUrls: ['./score.component.css']
})
export class ScoreComponent implements OnInit {
  score: any;

  constructor(private wash: WashService) { }

  ngOnInit() {
    this.getDashboardDetails();
  }

  // Get Score
  getDashboardDetails = () => {
    const obj = {
      id: +localStorage.getItem('empLocationId'),
      date: new Date()
    };
    this.wash.getDashBoard(obj);
    this.wash.dashBoardData.subscribe((data: any) => {
        this.score = data.Score;
    });
  }
}
