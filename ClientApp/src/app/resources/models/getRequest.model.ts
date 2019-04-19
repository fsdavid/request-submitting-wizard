import {RequestModel} from './request.model';

export class GetRequestModel {
  ownRequests: RequestModel[];
  requestsToApprove: RequestModel[];
  constructor(ownRequests: RequestModel[],
              requestsToApprove: RequestModel[]) {
    this.ownRequests = ownRequests;
    this.requestsToApprove = requestsToApprove;
  }
}
