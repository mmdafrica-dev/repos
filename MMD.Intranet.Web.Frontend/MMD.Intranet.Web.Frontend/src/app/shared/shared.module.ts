import { NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';

//THIRD PARTY
import { DataTableModule } from 'angular2-datatable';
import { TypeaheadModule } from 'ngx-bootstrap';
import { TabsModule } from 'ngx-bootstrap/tabs';
// import { AccordionModule } from 'ngx-bootstrap';
import { AccordionModule } from 'ngx-bootstrap/accordion';

@NgModule({
    imports: [CommonModule, FormsModule, ReactiveFormsModule, DataTableModule,
        TypeaheadModule.forRoot(), AccordionModule.forRoot(), TabsModule],
    exports: [FormsModule, CommonModule, ReactiveFormsModule, DataTableModule,
        TypeaheadModule, AccordionModule, TabsModule]

})
export class SharedModule {

}
