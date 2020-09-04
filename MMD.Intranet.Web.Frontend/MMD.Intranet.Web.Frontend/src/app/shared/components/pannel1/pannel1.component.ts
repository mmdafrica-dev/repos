import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'mmd-pannel1',
  templateUrl: './pannel1.component.html',
  styleUrls: ['./pannel1.component.scss']
})
export class Pannel1Component implements OnInit {
  @Input() panel1hearder: any;
  @Input() data: any;
  constructor() { }

  ngOnInit() {

  }

}
