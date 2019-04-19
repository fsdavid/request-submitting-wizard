import {RequestModel} from '../models/request.model';
import * as storeActions from './store.actions';
import {CurrentUser} from '../models/currentUser';


export interface StoresState {
  globalStores: State;
}

export interface  State {
  ownRequests: RequestModel[];
  requestsToApprove: RequestModel[];
  currentUser: CurrentUser;
}

const initialState: State = {
  ownRequests: [new RequestModel(0, '', '', '', '', '', '', '', '', '', false, '', false, '', [], [])],
  requestsToApprove: [new RequestModel(0, '', '', '', '', '', '', '', '', '', false, '', false, '', [], [])],
  currentUser: new CurrentUser('Davit Gogshelidze', 'testemaillforuserinwizardapplication@gmail.com', 'user'),
};





export function storesReducer (state = initialState, action: storeActions.StoreActions) {
  switch (action.type) {
    case (storeActions.SET_REQUESTS) :
      return {
        ...state,
        ownRequests: action.payload['ownRequests'],
        requestsToApprove: action.payload['requestsToApprove'],
      };

    case (storeActions.ADD_REQUEST) :
      return {
        ...state,
        ownRequests: [...state.ownRequests, action.payload]
      };

    case (storeActions.REMOVE_REQUEST) :
      console.log('removing' + action.payload);
      const indexOfRequestToRemove = state.ownRequests.findIndex((obj => obj.requestNo === action.payload));
      const requestDeleting = [...state.ownRequests];
      requestDeleting.splice(indexOfRequestToRemove, 1);
      return {
        ...state,
        ownRequests: requestDeleting,
      };

    case (storeActions.SUBMIT_REQUEST) :
      const indexOfRequestToSubmit = state.requestsToApprove.findIndex((obj => obj.requestNo === action.payload));
      const requestSubmitting = [...state.requestsToApprove];
      requestSubmitting.splice(indexOfRequestToSubmit, 1);


      return {
        // after add authentication change first line with just "..state" (without brackets)
        ...state,
        requestsToApprove: requestSubmitting,
      };


    case (storeActions.SUBMIT_REQUEST_CREATOR) :
      const ownerIndexOfRequestToSubmit = state.ownRequests.findIndex((obj => obj.requestNo === action.payload));


      const requestToSubmit1_C = state.ownRequests[ownerIndexOfRequestToSubmit];
      const requestSubmitting1_C = requestToSubmit1_C;

      console.log(requestSubmitting1_C);

      requestSubmitting1_C.requestStatus = 'Creator Approved';


      const updatedRequest1_C = {
        ...requestToSubmit1_C,
        ...requestSubmitting1_C
      };
      const toUpdateRequest1_C = [...state.ownRequests];
      toUpdateRequest1_C[ownerIndexOfRequestToSubmit] = updatedRequest1_C;


      return {
        // after add authentication change first line with just "..state" (without brackets)
        ...state,
        ownRequests: toUpdateRequest1_C,
      };




    case (storeActions.DISAGREE_REQUEST) :
      const indexOfRequestToDisagree = state.requestsToApprove.findIndex((obj => obj.requestNo === action.payload));
      const requestDisagreeing = [...state.requestsToApprove];
      requestDisagreeing.splice(indexOfRequestToDisagree, 1);

      return {
        // after add authentication change first line with just "..state" (without brackets)
        ...state,
        requestsToApprove: requestDisagreeing,
      };


    case (storeActions.SET_USER) :
      const userToSet = new CurrentUser('', '', action.payload);
      switch (action.payload) {
        case ('user'):
          userToSet.name = 'Davit Gogshelidze';
          userToSet.email = 'testemaillforuser654@gmail.com';
          break;
        case ('supervisor'):
          userToSet.name = 'Supervisor';
          userToSet.email = 'testemaillforsupervisor654@gmail.com';
          break;
        case ('director'):
          userToSet.name = 'Director';
          userToSet.email = 'testemaillfordirector654@gmail.com';
          break;
      }

      return {
        ...state,
        currentUser: userToSet
      };


    default:
      return state;
  }
}
