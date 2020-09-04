import { Component, OnInit, OnDestroy } from '@angular/core';
import { PartAndBomService } from '../services/part-and-bom.service';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { HttpParams } from '@angular/common/http';
import { PartDetailService } from '../../dashboard/services/part-detail.service';
import { Events } from '../../../core/events';
import { window } from 'rxjs/operator/window';
import 'rxjs/add/operator/switchMap';
import { SettingService } from '../../../core/services/setting.service';
import { StorageService } from '../../../core/services/storage.service';

@Component({
  selector: 'mmd-part-details',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})
export class DetailComponent implements OnInit, OnDestroy {

  data: any = {};
  searchOptions: any;
  formSubmitSubscribe: any;
  isTabLoaded: boolean = false;
  constructor(
    private partAndBomService: PartAndBomService,
    private activatedRoute: ActivatedRoute,
    private partDetailService: PartDetailService,
    private event: Events,
    private setting: SettingService,
    public storage: StorageService,
    private router: Router) {

    event.subscribe('tabLoaded', (value) => {
      this.isTabLoaded = value;
    });

    this.formSubmitSubscribe = this.partDetailService.hendleFormSubmit.subscribe(params => {
      this.getpartDetail(params, true, true);
    });
  }

  getpartDetail(parameter, force, isback) {
    this.event.publish('loaderShow', true);

    var parameters = this.partDetailService.params;
    if (parameters) {
      parameter = parameters;
    }
    parameter.searchBy = "1";
    localStorage.removeItem('IsFirstLoadPartDetail');
    var searchfors = parameter.searchfor;

    this.partAndBomService.search(parameter, force, searchfors, isback)
      .subscribe(result => {
        this.setting.emitChildRender();
        this.event.publish('loaderShow', false);
        if (result) {
          this.data = result.result[0];
          //  this.data.partdatE_CREATED = new Date(this.data.partdatE_CREATED)
        }
        if (isback && result.result.length > 0)
          this.router.navigate(['/extranet/partsearchdetail/' + searchfors + '/partDetail']);

      }, error => {
      });
  }

  ngOnInit() {
  }

  ngOnDestroy() {
    this.unSubscribeAll();
  }

  unSubscribeAll() {
    if (this.formSubmitSubscribe) {
      this.formSubmitSubscribe.unsubscribe();
      this.formSubmitSubscribe = null;
    }
  }

}
