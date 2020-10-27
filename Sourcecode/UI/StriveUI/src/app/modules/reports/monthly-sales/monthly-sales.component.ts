import { Component, OnInit } from '@angular/core';
import { ReportsService } from 'src/app/shared/services/data-service/reports.service';

@Component({
  selector: 'app-monthly-sales',
  templateUrl: './monthly-sales.component.html',
  styleUrls: ['./monthly-sales.component.css']
})
export class MonthlySalesComponent implements OnInit {
  monthlySalesReport = [];
  employees = [];
  empCount = 1;
  originaldata = [];
  empName = '';
  total = 0;
  constructor(private reportService: ReportsService) { }

  ngOnInit(): void {
    this.getMonthlySalesReport();
  }
  getMonthlySalesReport() {
    const obj = {
      locationId: localStorage.getItem('empLocationId'),
      date: new Date(),
    };
    this.reportService.getMonthlySalesReport(obj).subscribe(data => {
      console.log(data);
      if (data.status === 'Success') {
        const monthlySalesReport = JSON.parse(data.resultData);
        if (monthlySalesReport?.GetMonthlySalesReport !== null) {
          this.employees = monthlySalesReport?.GetMonthlySalesReport?.EmployeeViewModel ?
          monthlySalesReport?.GetMonthlySalesReport?.EmployeeViewModel : [];
          this.monthlySalesReport = monthlySalesReport?.GetMonthlySalesReport?.MonthlySalesReportViewModel;
          this.originaldata = monthlySalesReport?.GetMonthlySalesReport?.MonthlySalesReportViewModel;
          this.employeeListFilter(this.empCount);
        }
      }
});
  }
  count(action) {
    if (action === 'add') {
      this.empCount = (this.empCount < this.employees?.length) ? (this.empCount + 1) : this.employees.length;
      this.employeeListFilter(this.empCount);
    } else {
      this.empCount = (this.empCount > 1) ? (this.empCount - 1) : 1;
      this.employeeListFilter(this.empCount);
    }
  }
  employeeListFilter(count) {
    this.monthlySalesReport = this.originaldata;
    this.empName = this.employees[count - 1]?.EmployeeName;
    this.monthlySalesReport = this.monthlySalesReport.filter(emp => emp.EmployeeId === this.employees[count - 1].EmployeeId);
    this.calculatePrice();
  }
  calculatePrice() {
    this.total = this.monthlySalesReport.reduce((sum, i) => {
      return sum + (+i.Total);
    }, 0);
  }
}
