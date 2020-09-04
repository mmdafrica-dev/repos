import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-gerneric-col-display',
  templateUrl: './gerneric-col-display.component.html',
  styleUrls: ['./gerneric-col-display.component.scss'],

})
export class GernericColDisplayComponent implements OnInit {
  @Input() index: any;
  @Input() th: any;
  @Input() item: any;
  @Input() header: any;
  constructor() { }

  ngOnInit() {
  }

  getValue(value) {
    if (typeof (value) === 'string') {
      return value.replace(new RegExp(' ', 'g'), '&nbsp');
    }
    return value;
  }

}
