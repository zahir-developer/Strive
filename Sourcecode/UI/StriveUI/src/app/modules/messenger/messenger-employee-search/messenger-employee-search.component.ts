import { Component, OnInit } from '@angular/core';
import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';
declare var $: any;
@Component({
  selector: 'app-messenger-employee-search',
  templateUrl: './messenger-employee-search.component.html',
  styleUrls: ['./messenger-employee-search.component.css']
})
export class MessengerEmployeeSearchComponent implements OnInit {
  search = '';
  empList = [];
  selectAll = false;
  constructor(private empService: EmployeeService) { }

  ngOnInit(): void {
    this.getAllEmployees();
  }
  closeemp() {
    $('#show-search-emp').hide();
    $('.internal-employee').addClass('col-xl-9');
    $('.internal-employee').removeClass('col-xl-6');
    $('.view-msg').addClass('Message-box-slide');
    $('.view-msg').removeClass('Message-box');
    $('.plus-icon').removeClass('opacity-16');
    this.clearValue();
  }
  clearValue() {
    this.search = '';
    this.clearSelectAllFlag();
    this.getAllEmployees();
  }
  clearSelectAllFlag() {
    (document.getElementById('selectAll') as HTMLInputElement).checked = false;
  }
  getAllEmployees() {
    this.clearSelectAllFlag();
    this.empService.searchEmployee(this.search).subscribe(data => {
      if (data.status === 'Success') {
        const empList = JSON.parse(data.resultData);
        this.empList = empList.EmployeeList;
        this.setDefaultBoolean(false);
        this.setName();
      }
    });
  }
  setName() {
    this.empList.map(item => {
      const intial = item.FirstName.charAt(0).toUpperCase() + item.LastName.charAt(0).toUpperCase();
      console.log(intial);
      item.Initial = intial;
    });
  }
  setDefaultBoolean(flag) {
    this.empList.forEach(item => {
      item.isSelected = flag;
    });
  }
  selectAllEmployees(event) {
    if (event.target.checked === true) {
      this.setDefaultBoolean(true);
    } else {
      this.setDefaultBoolean(false);
    }
  }
  addEmployees() {
    const selectedEmp = this.empList.filter(item => item.isSelected === true);
  }
  searchFocus() {
    this.search = this.search.trim();
  }
  changeState() {
    this.clearSelectAllFlag();
  }
}
