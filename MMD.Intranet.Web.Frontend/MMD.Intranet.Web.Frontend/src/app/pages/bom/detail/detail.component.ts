import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { HttpParams } from '@angular/common/http';
import { BomService } from '../services/bom.service';
import { SearchService } from '../../dashboard/services/search-service';
import { TypeaheadMatch } from 'ngx-bootstrap/typeahead';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import { PartAndBomService } from '../../part-and-bom/services/part-and-bom.service';
@Component({
    selector: 'app-detail',
    templateUrl: './detail.component.html',
    styleUrls: ['./detail.component.scss']
})
export class BomDetailComponent implements OnInit {

    data: any = {};
    partDetailModel: any = {};
    singleLevelBomModel: any = {};
    bomOperationDetail: any = [];
    searchOptions: any;
    apiLink: any;

    public asyncSelected: string;
    public typeaheadLoading: boolean;
    public typeaheadNoResults: boolean;
    public dataSource: Observable<any>;

    constructor(private bomService: BomService, private partService: PartAndBomService, private searchService: SearchService, private activatedRoute: ActivatedRoute) {

    }

    ngOnInit() {

        this.activatedRoute.params.subscribe((params: Params) => {

            let key = params['key'];
            if (key) {
                this._getSerachOption(key);
            }
        });

    }


    _getSerachOption(key) {
        // this.searchService.getSearchOptions('bomEnqueries', key).subscribe(result => {
        //     // this.searchOptions = result.result.options;
        //     console.log(result);
        // }, error => {

        // })
    }
    getbomDetail(parameter) {
        this.bomService.bomDetail(parameter)
            .subscribe(result => {
                this.partDetailModel = result.partDetailModel;
                this.singleLevelBomModel = result.singleLevelBomModel;
                this.bomOperationDetail = result.bomOperationDetail;
            }, error => {

            });
    }

    handleFormSubmit(params) {
        this.getbomDetail(params);
    }


}