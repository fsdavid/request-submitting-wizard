import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { AddRequestComponent } from './add-request/add-request.component';
import {ViewRequestComponent} from './view-request/view-request.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MaterialModule} from './resources/shared-modules/material.module';
import {StoreRouterConnectingModule} from '@ngrx/router-store';
import {EffectsModule} from '@ngrx/effects';
import {StoreModule} from '@ngrx/store';
import {storesReducer} from './resources/store/store.reducer';
import {StoreEffects} from './resources/store/store.effects';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    AddRequestComponent,
    ViewRequestComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    MaterialModule,

    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'add-request', component: AddRequestComponent },
      { path: 'view/:request', component: ViewRequestComponent },
      { path: '**', redirectTo: '' },
    ]),

    StoreModule.forRoot({stores: storesReducer}),
    EffectsModule.forRoot([StoreEffects]),
    StoreRouterConnectingModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
