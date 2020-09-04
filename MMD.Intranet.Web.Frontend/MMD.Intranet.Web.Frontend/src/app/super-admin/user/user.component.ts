import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { GrowlerService, GrowlerMessageType } from '../../core/growler/growler.service';

import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { CustomValidators } from 'ng2-validation';
import { ValidationService } from '../../core/services/validation.service';

import { UserService } from '../services/user.service';
import { RoleService } from '../services/role.service';

import * as _ from 'lodash';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  submitted = false;
  user;
  isEdit = false;
  userForm: FormGroup;
  roles = [];
  claims = [];
  selectedRoles = [];
  selectedClaims = [];
  constructor(private router: Router,
    private route: ActivatedRoute,
    public userService: UserService,
    public locationNg: Location,
    public fb: FormBuilder,
    public growler: GrowlerService,
    public roleService: RoleService) { }

  ngOnInit() {
    let userId = this.route.snapshot.params['id'];
    if (userId) {
      this.getUser(userId);
      this.isEdit = true;
    }
    this.setUpForm();
    this.getClaims();
    this.getRoles();
  }

  setUpForm() {
    let password = new FormControl('', Validators.compose([Validators.required, ValidationService.passwordValidator]));
    let confirmPassword = new FormControl('', CustomValidators.equalTo(password));
    this.userForm = this.fb.group({
      email: ['', [Validators.required, ValidationService.emailValidator]],
      password: password,
      confirmPassword: confirmPassword
    });
  }
  getClaims() {
    this.userService.getClaims().subscribe((res) => {
      this.claims = res;
    });
  }

  getRoles() {
    this.roleService.getRoles().subscribe(res => {
      this.roles = res;
    })
  }

  getUser(userId) {
    this.userService.getUser(userId).subscribe((res) => {
      this.user = res.user;
      this.selectedClaims = res.claims;
      this.selectedRoles = res.roles;
    }, error => {
      console.log(error);
    });
  }

  submit() {

    if (this.user) {
      this.userService.updateUser({ userId: this.user.id, claims: this.selectedClaims, roles: this.selectedRoles }).subscribe(this.handleSaveSuccess.bind(this), error => {
        this.growler.growl(error.message, GrowlerMessageType.Danger);
      });
    }
    this.submitted = true;
    if (this.userForm.invalid) {
      console.log('invalid form');
      return;
    }
    let data = this.userForm.value;
    data.claims = this.selectedClaims;
    data.roles = this.selectedRoles;
    this.userService.saveUser(data).subscribe(this.handleSaveSuccess.bind(this), error => {
      this.growler.growl(error.message, GrowlerMessageType.Danger);
    });

  }


  handleSaveSuccess(res) {
    let msg = this.user ? 'user update succesfully' : 'user created successfully';
    this.growler.growl(msg, GrowlerMessageType.Success);
    this.userForm.markAsPristine();
    if (res.id) {
      this.router.navigate(['/superAdmin/user', res.id]);
    }
  }

  isRoleSelected(role) {
    if (this.selectedRoles.indexOf(role.name) > -1) {
      return true;
    } else {
      return false;
    }
  }

  isClaimSelected(claimType) {
    let claim = _.find(this.selectedClaims, { type: claimType });
    if (claim) {
      return true;
    }
    else {
      return false;
    }
  }

  toggleClaims(claim) {
    let idx = _.findIndex(this.selectedClaims, { type: claim.type });
    if (idx > -1) {
      this.selectedClaims.splice(idx, 1);
    } else {
      this.selectedClaims.push(claim);
    }
    console.log(this.selectedClaims);
  }

  toggleRoles(role) {
    let idx = this.selectedRoles.indexOf(role.name);
    if (idx > -1) {
      this.selectedRoles.splice(idx, 1);
    } else {
      this.selectedRoles.push(role.name);
    }
  }

  back() {
    this.locationNg.back();
  }

}
