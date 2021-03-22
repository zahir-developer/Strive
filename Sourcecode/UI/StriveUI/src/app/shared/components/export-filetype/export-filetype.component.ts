import { Component, OnInit, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-export-filetype',
  templateUrl: './export-filetype.component.html',
  styleUrls: ['./export-filetype.component.css']
})
export class ExportFiletypeComponent implements OnInit {
  fileExportType = [];
  @Output() emitExportType = new EventEmitter();
  constructor() { }

  ngOnInit(): void {
    this.filetype();
  }
  filetype() {
    this.fileExportType = [
    { id: 1, name: 'Acrobat (PDF) File' },
    { id: 2, name: 'CSV (comma delimited)' },
    { id: 3, name: 'Excel 97 - 2003' },
  ];
  }
  getFileType(event) {
    this.emitExportType.emit(event);
  }

}
