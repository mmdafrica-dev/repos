import { Injectable, EventEmitter } from '@angular/core';

@Injectable()
export class PartDetailService {

    params: any;
    hendleFormSubmit = new EventEmitter();

    formSubmit(params) {
        this.params = params;
        this.hendleFormSubmit.emit(params);
    }
}