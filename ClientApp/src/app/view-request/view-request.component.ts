import {Component, OnDestroy, OnInit} from '@angular/core';
import * as actions from '../resources/store/store.actions';
import * as reducer from '../resources/store/store.reducer';
import {Store} from '@ngrx/store';
import {ActivatedRoute} from '@angular/router';
import {RequestModel} from '../resources/models/request.model';
import {Observable} from 'rxjs/index';
import {animate, state, style, transition, trigger} from '@angular/animations';
import {ListItemModel} from '../resources/models/list-item.model';

@Component({
  selector: 'app-view-request',
  templateUrl: './view-request.component.html',
  styleUrls: ['./view-request.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0', display: 'none'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class ViewRequestComponent implements OnInit, OnDestroy {

  requestNo: number;
  private sub: any;




  request: RequestModel = new RequestModel(0, '', '', '', '', '', '', '', '', '', false, '', false, '', [], []);
  requestObservable: Observable<{ownRequests: RequestModel[], requestsToApprove: RequestModel[]}>;


  dataSource = this.request.lineItems;
  columnsToDisplay = ['description', 'quantity', 'unit', 'unitPrice', 'budgetObject', 'amount'];
  expandedElement: ListItemModel | null;



  constructor(private store: Store<reducer.StoresState>, private route: ActivatedRoute) { }

  ngOnInit() {
    this.sub = this.route.params.subscribe(params => {
        this.requestNo = +params['request'];
    });

    this.requestObservable = this.store.select('stores');

    this.requestObservable.subscribe(s => {
      if (s.ownRequests && +s.ownRequests.findIndex(r => +r.requestNo === +this.requestNo) >= 0) {
        this.request = s.ownRequests.find(r => +r.requestNo === +this.requestNo);
      } else if (s.requestsToApprove && +s.requestsToApprove.findIndex(r => +r.requestNo === +this.requestNo) >= 0) {
        this.request = s.requestsToApprove.find(r => +r.requestNo === +this.requestNo);
      }
      this.dataSource = this.request.lineItems;
    });



  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

  readDate(date) {
    const d = new Date(date);
    return d.getDate() + '/' + (d.getMonth() + 1) + '/' + d.getFullYear();
  }

}
