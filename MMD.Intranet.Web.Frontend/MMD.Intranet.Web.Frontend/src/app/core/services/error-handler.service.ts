import { Injectable, ErrorHandler } from '@angular/core';

@Injectable()
export class ErrorHandlerService implements ErrorHandler {

    constructor() { }

    handleError(err: any) {
        console.log(JSON.stringify(err));
    }
}