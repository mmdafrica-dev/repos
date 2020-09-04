import { Component, OnInit, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';

import { CustomValidators } from 'ng2-validation';

import { MMDValidation } from '../../validators/mmd-validation';

import { AuthService } from '../../core/services/auth.service';
import { GrowlerService, GrowlerMessageType } from '../../core/growler/growler.service';
import { Events } from '../../core/events';
import { GenericService } from '../../pages/dashboard/services/generic.service';
@Component({
    templateUrl: 'login.component.html'
})
export class LoginComponent implements OnInit {
    loginForm: FormGroup;
    error = false;
    errorMessage = '';
    constructor(
        @Inject('guestUser') public guestUser,
        private formBuilder: FormBuilder,
        private router: Router,
        private genericService: GenericService,
        public growler: GrowlerService,
        private authService: AuthService,
        private event: Events) {
    }

    ngOnInit() {
        this.buildForm();
    }

    buildForm() {
        this.loginForm = this.formBuilder.group({
            email: ['', [Validators.required, MMDValidation.emailValidator]],
            password: ['', [Validators.required]]
        });
    }
    guestLogin() {
        this.submitForm({ email: this.guestUser.email, password: this.guestUser.password });
    }

    submit({ value, valid }: { value: any, valid: boolean }) {
        // this.event.publish('loaderShow', true);
        this.submitForm(value);
    }

    submitForm(value) {
        this.authService.login(value)
            .subscribe((status: any) => {
                // this.event.publish('loaderShow', false);
                if (status) {
                    //  this.growler.growl('Logged in', GrowlerMessageType.Info);
                    if (this.authService.redirectUrl) {
                        const redirectUrl = this.authService.redirectUrl;
                        this.authService.redirectUrl = '';
                        this.router.navigate([redirectUrl]);
                    } else {
                        this.router.navigate(['/auth/logined']); // authomatically handle by guard
                    }

                    this.cachedGenericFileData(status);
                } else {
                    this.errorMessage = 'Unable to login';
                    this.error = true;
                    // this.growler.growl(this.errorMessage, GrowlerMessageType.Danger);
                }
            },
                (err: any) => {
                    this.event.publish('loaderShow', false);
                    this.error = true;
                    this.errorMessage = err.error.error_description;
                    // this.growler.growl('Unable to login: ' + err.error_description, GrowlerMessageType.Danger);
                });
    }


    cachedGenericFileData(user) {
        let roles = JSON.parse(user.roles);
        let isSuperAdmin = false;
        if (roles.indexOf('superadmin') > -1) {
            isSuperAdmin = true;
        } else {
            isSuperAdmin = false;
        }
        this.genericService.getGenericDropDownFormatData(isSuperAdmin);
    }


}
