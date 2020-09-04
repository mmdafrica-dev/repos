import { QueryEncoder } from '@angular/http'
export class MMDQueryEncoder extends QueryEncoder {
    encodeKey(k: string): string {
        k = super.encodeKey(k);
        return k.replace(/\+/gi, '%2B').replace(/@/gi, '%40');
    }
    encodeValue(v: string): string {
        v = super.encodeKey(v);
        return v.replace(/\+/gi, '%2B').replace(/@/gi, '%40');
    }
}