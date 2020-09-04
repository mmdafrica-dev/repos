import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { GrowlerService, GrowlerMessageType } from '../../core/growler/growler.service';

import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { CustomValidators } from 'ng2-validation';
import { MMDValidation } from '../../validators/mmd-validation';

import { UserService } from '../services/user.service';
import { RoleService } from '../services/role.service';
import { ToastService, TostType } from '../../core/services/toast.service';
import { StorageService } from '../../core/services/storage.service';
import { TreeviewItem, TreeviewConfig } from '../../shared/modules/tree-view'

import * as _ from 'lodash';
import { Events } from '../../core/events';

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
  roles: any;
  claims: any;//[];
  selectedRoles = [];
  selectedClaims = [];
  serverErrors: any;
  rolesesult: any;
  dropdownEnabled = true;
  items: TreeviewItem[];
  values: number[];
  selectedClaimsList = [];
  saveListClaims = [];
  config = TreeviewConfig.create({
    hasAllCheckBox: false,
    hasFilter: false,
    hasCollapseExpand: false,
    decoupleChildFromParent: false,
    // maxHeight: 400
  });
  constructor(private router: Router,
    private route: ActivatedRoute,
    public userService: UserService,
    public locationNg: Location,
    public fb: FormBuilder,
    public growler: GrowlerService,
    public roleService: RoleService,
    private tostService: ToastService,
    private storage: StorageService,
    private event: Events) { }

  ngOnInit() {
    // this.items = this.roleService.getBooks();

    let userId = this.route.snapshot.params['id'];
    if (userId) {
      this.getUser(userId);
      this.isEdit = true;
    } else {
      this.getRoles();
    }
    this.setUpForm();

  }

  setUpForm() {
    let password = new FormControl('', Validators.compose([Validators.required]));
    let confirmPassword = new FormControl('', CustomValidators.equalTo(password));
    this.userForm = this.fb.group({
      email: ['', [Validators.required, MMDValidation.emailValidator]],
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      password: password,
      confirmPassword: confirmPassword
    });
  }
  getClaims() {
    this.userService.getClaims().subscribe((res) => {
      this.claims = res;
    }, error => {
      this._errorAlert(error);
    });

  }

  // getValue(item) {
  //   var d = [];
  //   item.forEach(element => {
  //     // console.log({ text: element.type, value: element.value, checked: this.isClaimSelected(element.type) });
  //     d.push({ text: element.type, value: element.value, checked: this.isClaimSelected(element.type) });
  //   });
  //   return d;
  // }

  checkvalue(value) {
  }

  getRoles() {
    this.roleService.getRoles().subscribe(res => {
      this.roles = _.sortBy(res, ['name']);
      this.bindClaim(res);
    }, error => {
      this._errorAlert(error);
    });
  }
  bindClaim(res) {
    var temp = this;
    var data = [];
    let userData = this.storage.getObject('userItem');
    let disableSuperAdminRole = false;
    if (this.user && this.user.email && userData && userData.userName && this.user.email === userData.userName) {
      disableSuperAdminRole = true;
    }
    // this.selectedRoles
    if (res) {
      res.forEach(element => {
        let itemDisable = false;
        if (element.name === 'superadmin') {
          itemDisable = disableSuperAdminRole;
        }
        data.push(
          new TreeviewItem({
            text: element.name, disabled: itemDisable, value: element.id, collapsed: true, checked: this.getrol(element.name), children:
              this.getval(element.claims)
          })
        );
      });
    }
    this.items = data;
  }

  getrol(role) {
    var isRole = this.selectedRoles.find(x => x == role);
    if (isRole) return true;
    else return false;
  }
  getval(ele) {
    var d = [];
    if (ele) {
      ele.forEach(cl => {
        d.push({ text: cl, value: cl, checked: this.isClaimSelected(cl) });
      });
    }
    return d;
  }

  getUser(userId) {
    this.event.publish('loaderShow', true);
    this.userService.getUser(userId).subscribe((res) => {
      this.event.publish('loaderShow', false);
      this.rolesesult = res;
      this.user = this.rolesesult.user;
      this.selectedClaims = this.rolesesult.claims;
      this.selectedRoles = this.rolesesult.roles;
      // this.bindClaim(this.roles);
      this.getRoles();
      // this.getClaims();
    }, error => {
      this._errorAlert(error);
    });
  }



  submit() {
    this.event.publish('loaderShow', true);
    var rolesLi = [];
    this.saveListClaims = [];
    this.roles.forEach(role => {
      role.claims.forEach(element => {
        this.selectedClaimsList.forEach(seleted => {
          if (seleted == element) {
            rolesLi.push(role.name);
            this.saveListClaims.push({ type: element, value: element });
          }
          if (seleted == role.id) {
            rolesLi.push(role.name);
          }
        });
      });
    });
    this.roles.forEach(role => {
      this.selectedClaimsList.forEach(seleted => {
        if (seleted == role.id) {
          rolesLi.push(role.name);
        }
      });
    });

    rolesLi = _.uniqBy(rolesLi);
    // this.saveListClaims = [];
    // this.submitClaims();
    if (!this.isEdit) {
      if (rolesLi.indexOf('generic') == -1) {
        rolesLi.push("generic");
      }
    }

    if (this.user) {
      this.userService.updateUser({ userId: this.user.id, claims: this.saveListClaims, roles: rolesLi }).subscribe(this.handleSaveSuccess.bind(this), error => {
        this.event.publish('loaderShow', false);
      });
    }
    this.submitted = true;
    if (this.userForm.invalid) {
      // this.event.publish('loaderShow', false);
      return;
    }
    let data = this.userForm.value;
    data.claims = this.saveListClaims;
    data.roles = rolesLi;
    this.userService.saveUser(data).subscribe(this.handleSaveSuccess.bind(this), error => {
      this.event.publish('loaderShow', false);
      if (error.error && error.error.modelState) {
        if (error.error.modelState.error)
          this.serverErrors = error.error.modelState.error[0];
        else
          this.serverErrors = error.error.modelState['model.Password'][0];


      } else {
        this.serverErrors = error.error;
      }
      this.event.publish('loaderShow', false);
      this._errorAlert(this.serverErrors);
    });

  }


  handleSaveSuccess(res) {
    this.event.publish('loaderShow', false);
    let msg = this.user ? 'user update succesfully' : 'user created successfully';
    // this.growler.growl(msg, GrowlerMessageType.Success);
    this.userForm.markAsPristine();
    if (res.id) {
      this.router.navigate(['/superAdmin/user', res.id]);
    }
    this._successAlert(msg);
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

  // toggleClaims(claim) {
  //   let idx = _.findIndex(this.selectedClaims, { type: claim.type });
  //   if (idx > -1) {
  //     this.selectedClaims.splice(idx, 1);
  //   } else {
  //     this.selectedClaims.push(claim);
  //   }
  //   // console.log(this.selectedClaims);
  // }

  // toggleRoles(role) {
  //   let idx = this.selectedRoles.indexOf(role.name);
  //   if (idx > -1) {
  //     this.selectedRoles.splice(idx, 1);
  //   } else {
  //     this.selectedRoles.push(role.name);
  //   }
  // }

  back() {
    this.locationNg.back();
  }

  _errorAlert(message) {
    this.tostService.tostMessage(TostType.error, "Error", message);
  }
  _successAlert(message) {
    this.tostService.tostMessage(TostType.success, "Success", message);
  }

  onSelectedChange(value) {
    this.selectedClaimsList = value;
    // console.log(value);
  }

  // submitClaims() {
  //   var data = Object.keys(this.claims)
  //     .map((key) => (
  //       {
  //         data: this.filterClaims(this.claims[key])
  //       }));
  // }

  filterClaims(filter) {
    filter.forEach(element => {
      this.selectedClaimsList.forEach(seleted => {
        if (seleted == element.value) {
          this.saveListClaims.push({ type: element.type, value: element.value });
        }
      });
    });
  }

}
