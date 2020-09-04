import { Injectable, EventEmitter } from '@angular/core';
import { Events } from '../events';

@Injectable()
export class SettingService {
    loadingText = 'Loading...';
    childRender = new EventEmitter();
    constructor(private events: Events) {

        this.events.subscribe('loadingText', (text) => {
            this.loadingText = text;
        });

        this.events.subscribe('initLoadingText', () => {
            this.loadingText = 'Loading.....';
        });
    }



    emitChildRender() {
        this.childRender.emit();
    }

}