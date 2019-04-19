import {Actions, Effect, ofType} from '@ngrx/effects';
import {Observable, throwError} from 'rxjs/index';
import {Action} from '@ngrx/store';
import {
  AddRequest, DisagreeRequestBase, FetchRequest, GetRequests, RemoveRequestBase,
  SubmitRequestBase
} from './store.actions';
import {Injectable} from '@angular/core';
import { map, mergeMap} from 'rxjs/internal/operators';
import {HttpClient} from '@angular/common/http';
import {GetRequestModel} from '../models/getRequest.model';

@Injectable()
export class StoreEffects {

  @Effect()
  getRequests: Observable<Action> = this.actions$.pipe(
    ofType<GetRequests>('GET_REQUESTS'),
    mergeMap((action) => {
        return this.httpClient.get<GetRequestModel>(
          'api/GetRequests/' + action.payload,
          {
            observe: 'body',
            responseType: 'json'
          }).pipe(
          map ((data) => {
            return {
              type: 'SET_REQUESTS',
              payload: data
            };
          })
        );
      }
    )
  );

  @Effect()
  addRequest: Observable<Action> = this.actions$.pipe(
    ofType<FetchRequest>('FETCH_REQUEST'),
    mergeMap((action) => {
        return this.httpClient.post(
          'api/AddRequest', action.payload,
          {
            observe: 'body',
            responseType: 'text'
          }).pipe(
          map ((data) => {
            return {
              type: 'ADD_REQUEST',
              payload: action.payload
            };
          })
        );
      }
    )
  );

  @Effect()
  RemoveRequestBase: Observable<Action> = this.actions$.pipe(
    ofType<RemoveRequestBase>('REMOVE_REQUEST_BASE'),
    mergeMap((action) => {
        return this.httpClient.post(
          'api/RemoveRequest/' + action.payload,
          {
            observe: 'body',
            responseType: 'json'
          }).pipe(
          map ((data) => {
            return {
              type: 'REMOVE_REQUEST',
              payload: action.payload
            };
          })
        );
      }
    )
  );


  @Effect()
  disagreeRequestBase: Observable<Action> = this.actions$.pipe(
    ofType<DisagreeRequestBase>('DISAGREE_REQUEST_BASE'),
    mergeMap((action) => {
        return this.httpClient.post(
          'api/DisagreeRequest/' + action.payload,
          {
            observe: 'body',
            responseType: 'json'
          }).pipe(
          map ((data) => {
            return {
              type: 'DISAGREE_REQUEST',
              payload: action.payload
            };
          })
        );
      }
    )
  );

  @Effect()
  submitRequestBase: Observable<Action> = this.actions$.pipe(
    ofType<SubmitRequestBase>('SUBMIT_REQUEST_BASE'),
    mergeMap((action) => {
        return this.httpClient.post(
          'api/SubmitRequest/' + action.payload['payload'],
          {
            observe: 'body',
            responseType: 'json'
          }).pipe(
          map ((data) => {
            if (action.payload['owner']) {
              return {
                type: 'SUBMIT_REQUEST_CREATOR',
                payload: +action.payload['payload']
              };
            } else {
              return {
                type: 'SUBMIT_REQUEST',
                payload: +action.payload['payload']
              };
            }
          })
        );
      }
    )
  );



  constructor(private actions$: Actions,
              private httpClient: HttpClient) {}
}

