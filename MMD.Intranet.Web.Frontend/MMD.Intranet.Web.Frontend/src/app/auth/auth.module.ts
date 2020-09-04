import { NgModule } from '@angular/core';
import { AuthRoutingModule } from './auth-routing.module';
import { SharedModule } from '../shared/shared.module';
import { GenericService } from '../pages/dashboard/services/generic.service';
import { UserService } from '../superAdmin/services/user.service';
@NgModule({
    imports: [SharedModule, AuthRoutingModule],
    exports: [SharedModule],
    declarations: [
        AuthRoutingModule.components
    ],
    providers: [GenericService, UserService]
})
export class AuthModule { }
