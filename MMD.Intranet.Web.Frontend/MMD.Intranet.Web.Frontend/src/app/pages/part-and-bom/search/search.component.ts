import { Component, OnInit } from '@angular/core';
import { PartAndBomService } from '../services/part-and-bom.service';
import { HttpParams } from '@angular/common/http';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Events } from '../../../core/events';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent implements OnInit {
  searchForm: FormGroup;
  result = [];
  public sortBy = "parT1_PART";
  public sortOrder = "asc";
  public filterBycolumn = "parT1_PART";

  constructor(private partAndBomService: PartAndBomService,
    private fb: FormBuilder,
    private event: Events
  ) { }

  ngOnInit() {
    this.setupForm();
  }

  setupForm() {
    this.searchForm = this.fb.group({
      description: ['', Validators.required]
    });
  }

  submit() {
    if (this.searchForm.invalid) {
      return;
    }
    this.event.publish('loaderShow', true);
    var params: any = {};//new HttpParams();
    params.searchFor = this.searchForm.value.description;
    this.partAndBomService.search(params, true, null, false)
      .subscribe(this.handleSucess.bind(this), this.handleError.bind(this));
  }


  handleSucess(res) {
    this.event.publish('loaderShow', false);
    this.result = res.result;
  }

  handleError(error) {
    this.event.publish('loaderShow', false);

  }

}
