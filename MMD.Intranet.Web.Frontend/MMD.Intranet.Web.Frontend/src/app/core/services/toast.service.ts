import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { ToastyService, ToastyConfig, ToastOptions, ToastData } from 'ng2-toasty';
// import { TostType } from '../enums/tost-type-enum';
/**
 * Service helps communicate between the ToastComponent and AppComponent.
 */

@Injectable()
export class ToastService {
    // Observable string sources    
    private toastOptions: ToastOptions;

    constructor(private toastyService: ToastyService) {


    }

    tostMessage(type: number, title: string, message: string) {
        let toastOptions: ToastOptions = {
            title: title,
            msg: message,
            showClose: true,
            timeout: 1500,
            theme: "default",
            // onAdd: (toast: ToastData) => {
            //     console.log('Toast ' + toast.id + ' has been added!');
            // },
            // onRemove: function (toast: ToastData) {
            //     console.log('Toast ' + toast.id + ' has been removed!');
            // }
        };
        switch (type) {
            case TostType.default: this.toastyService.default(toastOptions); break;
            case TostType.info: this.toastyService.info(toastOptions); break;
            case TostType.success: this.toastyService.success(toastOptions); break;
            case TostType.wait: this.toastyService.wait(toastOptions); break;
            case TostType.error: this.toastyService.error(toastOptions); break;
            case TostType.warning: this.toastyService.warning(toastOptions); break;
        }
    }
}
export enum TostType {
    default,
    info,
    success,
    wait,
    error,
    warning
};

