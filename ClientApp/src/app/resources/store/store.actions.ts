import {Action} from '@ngrx/store';
import {RequestModel} from '../models/request.model';
import {Actions} from '@ngrx/effects';

export const GET_REQUESTS = 'GET_REQUESTS';
export const SET_REQUESTS = 'SET_REQUESTS';
export const FETCH_REQUEST = 'FETCH_REQUEST';
export const ADD_REQUEST = 'ADD_REQUEST';
export const REMOVE_REQUEST_BASE = 'REMOVE_REQUEST_BASE';
export const REMOVE_REQUEST = 'REMOVE_REQUEST';
export const SUBMIT_REQUEST_BASE = 'SUBMIT_REQUEST_BASE';
export const SUBMIT_REQUEST = 'SUBMIT_REQUEST';
export const SUBMIT_REQUEST_CREATOR = 'SUBMIT_REQUEST_CREATOR';
export const DISAGREE_REQUEST_BASE = 'DISAGREE_REQUEST_BASE';
export const DISAGREE_REQUEST = 'DISAGREE_REQUEST';
export const SET_USER = 'SET_USER';


export class GetRequests implements Action {
  readonly type = GET_REQUESTS;
  constructor (public payload: string) {}
}

export class SetRequests implements Action {
  readonly type = SET_REQUESTS;
  constructor (public payload: RequestModel[]) {}
}

export class FetchRequest implements Action {
  readonly type = FETCH_REQUEST;
  constructor (public payload: RequestModel) {}
}

export class AddRequest implements Action {
  readonly type = ADD_REQUEST;
  constructor (public payload: RequestModel) {}
}

export class RemoveRequestBase implements Action {
  readonly type = REMOVE_REQUEST_BASE;
  constructor (public payload: number) {}
}

export class RemoveRequest implements Action {
  readonly type = REMOVE_REQUEST;
  constructor (public payload: number) {}
}

export class SubmitRequestBase implements Action {
  readonly type = SUBMIT_REQUEST_BASE;
  constructor (public payload: {payload: number, owner: boolean}) {}
}

export class SubmitRequest implements Action {
  readonly type = SUBMIT_REQUEST;
  constructor (public payload: number) {}
}

export class SubmitRequestCreator implements Action {
  readonly type = SUBMIT_REQUEST_CREATOR;
  constructor (public payload: number) {}
}

export class DisagreeRequestBase implements Action {
  readonly type = DISAGREE_REQUEST_BASE;
  constructor (public payload: number) {}
}

export class SetUser implements Action {
  readonly type = SET_USER;
  constructor (public payload: string) {}
}

export class DisagreeRequest implements Action {
  readonly type = DISAGREE_REQUEST;
  constructor (public payload: number) {}
}




export type StoreActions = GetRequests
  | SetRequests
  | FetchRequest
  | AddRequest
  | RemoveRequestBase
  | RemoveRequest
  | SubmitRequestBase
  | SubmitRequest
  | SubmitRequestCreator
  | DisagreeRequestBase
  | DisagreeRequest
  | SetUser;







