import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'mmd-control-builder',
  templateUrl: './control-builder.component.html',
  styleUrls: ['./control-builder.component.css']
})
export class ControlBuilderComponent implements OnInit {

  @Input() searchoption: any;
  noValue: boolean = false;
  @Input() selectedValue: string;
  @Output() updateSelectedValue = new EventEmitter();


  constructor() { }

  ngOnInit() {
    if (this.searchoption.data.length > 0 && typeof (this.searchoption.data[0]) !== 'object') {
      this.noValue = true;
    }
  }

  change(newValue) {
    this.selectedValue = newValue;
    this.updateSelectedValue.next(newValue);
  }
}
