import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Location } from '@angular/common';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { GrowlerService, GrowlerMessageType } from '../../core/growler/growler.service';



import { RoleService } from '../services/role.service';

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
    public location: Location) { }

  ngOnInit() {
    this.roleId = this.route.snapshot.params['id'];
    if (this.roleId)
      this.getRole();
    this.setupForm();
  }

  getRole() {
    this.roleService.getRole(this.roleId).subscribe(res => {
      this.role = res;
      this.addValueToForm(res);
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
          this.growler.growl(error.message, GrowlerMessageType.Danger);
          this.inProgress = false;
          if (error && error.modelState) {
            this.serverErrors = error.modelState.error;
          } else {
            this.serverErrors = error;
          }
        });
    } else {
      this.roleService.updateRole(this.role.id, this.roleForm.value)
        .subscribe(this.handleSaveSuccess.bind(this), error => {
          this.growler.growl(error.message, GrowlerMessageType.Danger);
          this.inProgress = false;
          if (error && error.modelState) {
            this.serverErrors = error.modelState.error;
          } else {
            this.serverErrors = error;
          }
        });
    }
  }

  handleSaveSuccess(res) {
    let msg = this.role ? 'Role update succesfully' : 'Role created successfully';
    this.growler.growl(msg, GrowlerMessageType.Success);
    this.roleForm.markAsPristine();
    this.inProgress = false;
    if (res.id) {
      this.router.navigate(['/superAdmin/role', res.id]);
    }
  }

  goBack() {
    this.router.navigate(['/superAdmin/roles']);
  }

}
