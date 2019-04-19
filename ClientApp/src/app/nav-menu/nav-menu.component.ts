import {Component, OnInit} from '@angular/core';
import {Observable} from 'rxjs/index';
import {CurrentUser} from '../resources/models/currentUser';

import * as actions from '../resources/store/store.actions';
import * as reducer from '../resources/store/store.reducer';
import {Store} from '@ngrx/store';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;


  selectedRole: string;
  currentUser: CurrentUser;

  selectedRoleObservable: Observable<{currentUser: CurrentUser}>;

  constructor(private store: Store<reducer.StoresState>) {}

  ngOnInit() {
    this.selectedRoleObservable = this.store.select('stores');


    this.selectedRoleObservable.subscribe(s => {
      this.currentUser = s.currentUser;
      if (this.currentUser && this.currentUser.role) {
        this.selectedRole = this.currentUser.role;
        // this.getRequests();
      }
    });

    this.getRequests();
  }


  changeUser(event) {

    this.store.dispatch(new actions.SetUser(this.selectedRole));
    this.getRequests();

  }


  getRequests() {
    this.store.dispatch(new actions.GetRequests(this.currentUser.name));
  }


  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }



}
