import { Injectable } from '@angular/core';
import * as FileSaver from 'file-saver';
import * as XLSX from 'xlsx';
const EXCEL_TYPE = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8';
const EXCEL_EXTENSION = '.xlsx';
import { jsPDF } from 'jspdf';
import html2canvas from 'html2canvas';
import { ExportToCsv } from 'export-to-csv';
import { NgxSpinnerService } from 'ngx-spinner';
@Injectable({
  providedIn: 'root'
})
export class ExcelService {

  constructor(private spinner: NgxSpinnerService) { }
  options = {
    fieldSeparator: ',',
    quoteStrings: '"',
    decimalSeparator: '.',
    showLabels: true,
    showTitle: true,
    title: '',
    filename: '',
    useTextFile: false,
    useBom: true,
    useKeysAsHeaders: true,
  };
  public exportAsExcelFile(json: any[], excelFileName: string): void {
    this.spinner.show();
    const myworksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(json, { header: [excelFileName] });
    const myworkbook: XLSX.WorkBook = { Sheets: { data: myworksheet }, SheetNames: ['data'] };
    const excelBuffer: any = XLSX.write(myworkbook, { bookType: 'xlsx', type: 'array' });
    this.saveAsExcelFile(excelBuffer, excelFileName);
  }
  private saveAsExcelFile(buffer: any, fileName: string): void {
    const data: Blob = new Blob([buffer], {
      type: EXCEL_TYPE
    });
    FileSaver.saveAs(data, fileName + EXCEL_EXTENSION);
    this.spinner.hide();
  }
  exportAsPDFFile(templateId, fileName) {
    this.spinner.show();
    const data = document.getElementById(templateId);
    html2canvas(data).then(canvas => {
      // Few necessary setting options
      const imgWidth = 188;
      const pageHeight = 295;
      const imgHeight = canvas.height * imgWidth / canvas.width;
      const heightLeft = imgHeight;
      const contentDataURL = canvas.toDataURL('image/png');
      const pdf = new jsPDF('p', 'mm', 'a4'); // A4 size page of PDF
      const position = 0;
      pdf.addImage(contentDataURL, 'PNG', 10, 10, imgWidth, imgHeight);
      pdf.save(fileName); // Generated PDF
      this.spinner.hide();
    });
  }
  exportAsCSVFile(data, fileName) {
    if ( data === undefined ||  data.length === 0 ) {
      return;
    }
    this.spinner.show();
    this.options.title = fileName;
    this.options.filename = fileName;
    const csvExporter = new ExportToCsv(this.options);
    csvExporter.generateCsv(data);
    this.spinner.hide();
  }
}
