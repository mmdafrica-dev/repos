import { Component, OnInit } from '@angular/core';


/// PROVIDERS
import { UserService } from '../services/user.service';
@Component({
  selector: 'mmd-users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.css']
})
export class UsersListComponent implements OnInit {
  users = [];
  constructor(public userService: UserService) { }

  ngOnInit() {
    this.getUsers();
  }

  getUsers() {
    this.userService.getUserList().subscribe(res => {
      this.users = res;
    });
  }

}
