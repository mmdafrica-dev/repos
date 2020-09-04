import { Component, OnInit } from '@angular/core';

import { RoleService } from '../services/role.service';
import { Events } from '../../core/events';
import { SettingService } from '../../core/services/setting.service';
import { ModalDirective } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-role-list',
  templateUrl: './role-list.component.html',
  styleUrls: ['./role-list.component.css']
})
export class RoleListComponent implements OnInit {
  roles: any;
  public sortBy = "name";
  public sortOrder = "asc";
  public filterBycolumn = "name";
  public dangerModal;
  currentDeleteId: string;
  constructor(public roleService: RoleService,
    private event: Events,
    private setting: SettingService) {
    this.setting.emitChildRender();
  }

  ngOnInit() {
    this.getRoles();
  }

  getRoles() {
    this.event.publish('loaderShow', true);
    this.roleService.getRoles().subscribe(res => {
      this.event.publish('loaderShow', false);
      this.roles = res;
    }, error => {
      this.event.publish('loaderShow', false);
      console.log(error);
    });
  }

  curreentDelete(id) {
    this.currentDeleteId = id;
  }

  delete() {
    this.event.publish('loaderShow', true);
    this.roleService.deleteRole(this.currentDeleteId).subscribe(res => {
      this.event.publish('loaderShow', false);
      this.getRoles();
    }, error => {
      this.event.publish('loaderShow', false);
      console.log(error);
    });
  }

}
