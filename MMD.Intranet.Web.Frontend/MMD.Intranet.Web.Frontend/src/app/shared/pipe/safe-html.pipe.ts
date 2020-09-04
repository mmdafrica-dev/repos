import { DomSanitizer } from '@angular/platform-browser'
import { PipeTransform, Pipe } from '@angular/core';

@Pipe({ name: 'safeHtmls' })
export class SafeHtmlsPipe implements PipeTransform {
    constructor(private sanitized: DomSanitizer) { }
    transform(value) {
        const doc = new DOMParser().parseFromString(value, 'text/html');
        return doc.documentElement.textContent;
    }
}
