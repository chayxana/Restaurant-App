import { Component, OnInit } from '@angular/core';
import { DialogData } from './dialog-data';

@Component({
  selector: 'app-confirmation-dialog',
  templateUrl: './confirmation-dialog.component.html',
  styleUrls: ['./confirmation-dialog.component.css']
})
export class ConfirmationDialogComponent implements OnInit {
  data: DialogData;
  constructor() { }

  ngOnInit() {
  }

}
