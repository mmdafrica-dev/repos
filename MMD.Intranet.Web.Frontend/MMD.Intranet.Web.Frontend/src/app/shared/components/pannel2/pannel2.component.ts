import { Component, OnInit, Input } from '@angular/core';
import * as _ from "lodash";

@Component({
  selector: 'mmd-pannel2',
  templateUrl: './pannel2.component.html',
  styleUrls: ['./pannel2.component.scss']
})
export class Pannel2Component implements OnInit {
  @Input() panel2hearder: any;
  @Input() data: any;
  constructor() { }

  ngOnInit() {
    this.panel2hearder = _.chunk(this.panel2hearder, 2);
  }

}
