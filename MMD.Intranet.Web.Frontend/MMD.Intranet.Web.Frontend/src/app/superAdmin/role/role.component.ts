import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Location } from '@angular/common';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { GrowlerService, GrowlerMessageType } from '../../core/growler/growler.service';

import { SettingService } from '../../core/services/setting.service';

import { RoleService } from '../services/role.service';
import { ToastService, TostType } from '../../core/services/toast.service';
import { Events } from '../../core/events';

@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.css']
})
export class RoleComponent implements OnInit {
  roleForm: FormGroup;
  roleId: string = '';
  role: any;
  serverErrors: any;
  submitted = false;
  inProgress = false;
  constructor(private router: Router,
    private route: ActivatedRoute,
    public fb: FormBuilder,
    public roleService: RoleService,
    public growler: GrowlerService,
    public location: Location,
    private tostService: ToastService,
    private event: Events,
    private setting: SettingService) {
  }

  ngOnInit() {

    this.roleId = this.route.snapshot.params['id'];
    if (this.roleId)
      this.getRole();
    this.setupForm();
  }

  getRole() {
    this.event.publish('loaderShow', true);
    this.roleService.getRole(this.roleId).subscribe(res => {
      this.event.publish('loaderShow', false);
      this.role = res;
      if (this.role.name === 'superadmin') {
        this.goBack();
      } else {
        this.addValueToForm(res);
      }
    });
  }

  setupForm() {
    this.roleForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required]
    });
  }


  addValueToForm(role) {
    this.roleForm.controls['name'].setValue(role.name);
    this.roleForm.controls['description'].setValue(role.description);
  }

  submit() {


    this.submitted = true;
    if (this.roleForm.invalid) {
      return;
    }
    this.inProgress = true;
    if (!this.role) {
      this.roleService.saveRole(this.roleForm.value)
        .subscribe(this.handleSaveSuccess.bind(this), error => {
          // this.growler.growl(error.message, GrowlerMessageType.Danger);
          // this.inProgress = false;
          if (error.error && error.error.modelState) {
            this.serverErrors = error.error.modelState.error[0];
          } else {
            this.serverErrors = error.error;
          }
          this._errorAlert(this.serverErrors);
        });
    } else {
      this.roleService.updateRole(this.role.id, this.roleForm.value)
        .subscribe(this.handleSaveSuccess.bind(this), error => {
          // this.growler.growl(error.message, GrowlerMessageType.Danger);
          // this.inProgress = false;        
          if (error.error && error.error.modelState) {
            this.serverErrors = error.error.modelState.error[0];
          } else {
            this.serverErrors = error.error;
          }
          this._errorAlert(this.serverErrors);
        });
    }
  }

  handleSaveSuccess(res) {
    let msg = this.role ? 'Role update succesfully' : 'Role created successfully';
    // this.growler.growl(msg, GrowlerMessageType.Success);
    // this.roleForm.markAsPristine();
    // this.inProgress = false;
    if (res.id) {
      // this.router.navigate(['/superAdmin/roles']);
      //this.router.navigate(['/superAdmin/role', res.id]);
    }
    this._successAlert(msg);
  }

  goBack() {
    this.router.navigate(['/superAdmin/roles']);
  }

  _errorAlert(message) {
    this.tostService.tostMessage(TostType.error, "Error", message);
  }
  _successAlert(message) {
    this.tostService.tostMessage(TostType.success, "Success", message);
  }
}
