import { Component, OnInit, Input } from '@angular/core';

import { Subscription } from 'rxjs/Subscription';
import { AuthService } from 'app/core/services/auth.service';

@Component({
  selector: 'mmd-sitehighlighter',
  templateUrl: './sitehighlighter.component.html',
  styleUrls: ['./sitehighlighter.component.css']
})
export class SitehighlighterComponent implements OnInit {
    loginLogoutText: string = 'Login';
    sub: Subscription;
    loggedInUser: any = {userName:''};


  constructor(private authservice: AuthService,) { }

  ngOnInit() {
     this.sub = this.authservice.authChanged
            .subscribe((loggedIn: boolean) => {
                 this.setLoginLogoutText();
             },
            (err: any) => console.log(err));

  }

  setLoginLogoutText(){
     this.loggedInUser = this.authservice.loggedIn || {} ;
  }

}
