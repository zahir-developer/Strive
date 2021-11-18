import { Component, OnInit } from '@angular/core';
import { WashService } from '../../services/data-service/wash.service';
import { MessageConfig } from '../../services/messageConfig';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-score',
  templateUrl: './score.component.html'
})
export class ScoreComponent implements OnInit {
  score: any;

  constructor(private wash: WashService,
    private toastr : ToastrService) { }

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
    }
    , (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }
}
