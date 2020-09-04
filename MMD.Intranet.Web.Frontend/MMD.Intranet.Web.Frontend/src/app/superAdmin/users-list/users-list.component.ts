import { Component, OnInit } from '@angular/core';


/// PROVIDERS
import { UserService } from '../services/user.service';
import { Events } from '../../core/events';
import { SettingService } from '../../core/services/setting.service';
import { StorageService } from '../../core/services/storage.service';


@Component({
  selector: 'mmd-users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.css']
})
export class UsersListComponent implements OnInit {
  users: any;
  public sortBy = "user.email";
  public sortOrder = "asc";
  public filterBycolumn = "user.email";
  constructor(public userService: UserService,
    private event: Events,
    private setting: SettingService,
    public storage: StorageService) {
    this.setting.emitChildRender();
  }

  ngOnInit() {
    this.getUsers();
  }

  getUsers() {
    this.event.publish('loaderShow', true);
    this.userService.getUserList().subscribe(res => {
      this.event.publish('loaderShow', false);
      this.users = res;
    });
  }

  restUserPassword(item) {
    this.storage.setObject('resetPassword', item);
  }

}
