import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { PartAndBomComponent } from './part-and-bom.component';
import { SearchComponent } from './search/search.component';
import { DetailComponent } from './detail/detail.component';
import { PartAndBomGuard } from './part-and-bom.guard';

const routes: Routes = [
    {
        path: '', component: PartAndBomComponent, children: [
            { path: 'search', component: SearchComponent },
            { path: 'partAndBom/:partNumber', component: DetailComponent }
        ],
        canActivate: [PartAndBomGuard]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
    declarations: [],

    providers: [PartAndBomGuard]
})
export class PartAndBomRoutingModule {
    static components = [PartAndBomComponent, SearchComponent];
}
