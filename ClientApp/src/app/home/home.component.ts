import {Component, OnInit, ViewChild} from '@angular/core';
import {CurrentUser} from '../resources/models/currentUser';
import {Observable} from 'rxjs/index';
import {RequestModel} from '../resources/models/request.model';
import {Store} from '@ngrx/store';
import * as actions from '../resources/store/store.actions';
import * as reducer from '../resources/store/store.reducer';
import {MatTable} from '@angular/material';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  ownRequests: RequestModel[];
  requestsToApprove: RequestModel[];
  currentUser: CurrentUser;
  selectedRoleObservable: Observable<{ownRequests: RequestModel[], requestsToApprove: RequestModel[], currentUser: CurrentUser}>;

  @ViewChild(MatTable) table: MatTable<any>;


  displayedColumns = ['requestNo', 'requestDate', 'requiredDeliveryDate', 'requestType', 'requestStatus', 'editRequest'];
  ownRequestsForTable;

  displayedColumnsApprove = ['requestNo', 'requester', 'requestDate', 'requiredDeliveryDate', 'requestType', 'requestStatus', 'editRequest'];
  approveRequestsForTable;

  constructor(private store: Store<reducer.StoresState>) {}

  ngOnInit() {
    this.selectedRoleObservable = this.store.select('stores');

    this.selectedRoleObservable.subscribe(s => {
      this.ownRequests = s.ownRequests;
      this.requestsToApprove = s.requestsToApprove;
      this.currentUser = s.currentUser;

      this.ownRequestsForTable = this.ownRequests.map(or => {
       return {requestNo: or.requestNo, requestDate: or.requestDate, requiredDeliveryDate: or.requiredDeliveryDate,
         requestType: or.requestType, requestStatus: or.requestStatus};
      });
      if (this.requestsToApprove) {
        this.approveRequestsForTable = this.requestsToApprove.map(or => {
          return {requestNo: or.requestNo, requester: or.requester, requestDate: or.requestDate, requiredDeliveryDate: or.requiredDeliveryDate,
            requestType: or.requestType, requestStatus: or.requestStatus};
        });
      } else {
        this.approveRequestsForTable = null;
      }
    });

  }

  deleteRequest(requestNo: number) {
    this.store.dispatch(new actions.RemoveRequestBase(requestNo));
    this.table.renderRows();
  }
  disagreeRequest(requestNo: number) {
    this.store.dispatch(new actions.DisagreeRequestBase(requestNo));
    this.table.renderRows();
  }
  submitRequest(requestNo: number, owner: boolean) {
    this.store.dispatch(new actions.SubmitRequestBase({payload: requestNo, owner: owner}));
    this.table.renderRows();
  }

  readDate(date) {
    const d = new Date(date);
    return d.getDate() + '/' + (d.getMonth() + 1) + '/' + d.getFullYear();
  }

}
