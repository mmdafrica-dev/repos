
import { Component, OnInit, Inject } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, Validators, FormBuilder, FormControl } from '@angular/forms';

import { CustomValidators } from 'ng2-validation';

import { MMDValidation } from '../../../validators/mmd-validation';

import { AuthService } from '../../../core/services/auth.service';
import { GrowlerService, GrowlerMessageType } from '../../../core/growler/growler.service';
import { Events } from '../../../core/events';
import { PartAndBomService } from '../../part-and-bom/services/part-and-bom.service';
import 'rxjs/add/operator/catch';
import { StorageService } from '../../../core/services/storage.service';



@Component({
    templateUrl: './reset-password.component.html'
})
export class ResetPasswordComponent implements OnInit {
    resetPasswordForm: FormGroup;
    error = false;
    errorMessage = '';
    isUserPasswordReset: boolean = true;
    userId: string;
    selectedUserName: string;
    constructor(
        @Inject('guestUser') public guestUser,
        private formBuilder: FormBuilder,
        private router: Router,
        public growler: GrowlerService,
        private service: PartAndBomService,
        private event: Events,
        private route: ActivatedRoute,
        public storage: StorageService) {
    }

    ngOnInit() {
        this.userId = this.route.snapshot.params['id'];

        if (this.userId) {
            var user = this.storage.getObject('resetPassword');
            if (user) {
                if (user.user.firstName) {
                    this.selectedUserName = user.user.firstName + ' ' + user.user.lastName;
                }
                else {
                    this.selectedUserName = user.user.userName;
                }
            }
        }
        this.buildForm();
    }

    buildForm() {
        let password = new FormControl('', Validators.compose([Validators.required]));
        let confirmPassword = new FormControl('', CustomValidators.equalTo(password));
        if (!this.userId) {
            this.resetPasswordForm = this.formBuilder.group({
                oldPassword: ['', [Validators.required]],
                newPassword: password,
                confirmPassword: confirmPassword
            });
        }
        else {
            this.resetPasswordForm = this.formBuilder.group({
                //  oldPassword: ['', [Validators.required, MMDValidation.passwordValidator]],
                newPassword: password,
                confirmPassword: confirmPassword
            });
        }
    }
    // guestLogin() {
    //     this.submitForm({ email: this.guestUser.email, password: this.guestUser.password });
    // }

    submit({ value, valid }: { value: any, valid: boolean }) {
        //  console.log(valid);
        if (valid)
            this.submitForm(value);

    }

    submitForm(value) {
        value.userId = this.userId;
        this.service.resetPassword(value, null)
            .subscribe((status: any) => {
                this.router.navigate(['/extranet/dashboard']);
            }, (err: any) => {
                this.event.publish('loaderShow', false);
                if (err.status == 200) {
                    this.router.navigate(['/extranet/dashboard']); // authomatically handle by guard
                } else {
                    this.error = true;
                    if (err.error.modelState.error)
                        this.errorMessage = err.error.modelState.error[0];
                    else
                        this.errorMessage = err.error.modelState['model.NewPassword'][0];
                }
                // this.growler.growl('Unable to login: ' + err.error_description, GrowlerMessageType.Danger);
            });
    }


}
