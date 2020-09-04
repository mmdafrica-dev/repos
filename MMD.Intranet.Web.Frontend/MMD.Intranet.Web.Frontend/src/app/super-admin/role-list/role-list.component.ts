import { Component, OnInit } from '@angular/core';

import { RoleService } from '../services/role.service';

@Component({
  selector: 'app-role-list',
  templateUrl: './role-list.component.html',
  styleUrls: ['./role-list.component.css']
})
export class RoleListComponent implements OnInit {
  roles = [];
  constructor(public roleService: RoleService) {

  }

  ngOnInit() {
    this.getRoles();
  }

  getRoles() {
    this.roleService.getRoles().subscribe(res => {
      this.roles = res;
    });
  }

}
