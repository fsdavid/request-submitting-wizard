import {ListItemModel} from './list-item.model';
import {NoteModel} from './note.model';

export class RequestModel {
  requestNo: number;
  requester: string;
  requestOffice: string;
  requestOffice2: string;
  requestDate: string;
  requestStatus: string;
  requestType: string;
  requiredDeliveryDate: string;
  technicalContact: string;
  phone: string;
  technicalApprovalRequired: boolean;
  description: string;
  renewalContract: boolean;
  supplier: string;
  lineItems: ListItemModel[];
  notes: NoteModel[];
  constructor(requestNo: number,
              requester: string,
              requestOffice: string,
              requestOffice2: string,
              requestDate: string,
              requestStatus: string,
              requestType: string,
              requiredDeliveryDate: string,
              technicalContact: string,
              phone: string,
              technicalApprovalRequired: boolean,
              description: string,
              renewalContract: boolean,
              supplier: string,
              lineItems: ListItemModel[],
              notes: NoteModel[]) {

    this.requestNo = requestNo;
    this.requester = requester;
    this.requestOffice = requestOffice;
    this.requestOffice2 = requestOffice2;
    this.requestDate = requestDate;
    this.requestStatus = requestStatus;
    this.requestType = requestType;
    this.requiredDeliveryDate = requiredDeliveryDate;
    this.technicalContact = technicalContact;
    this.phone = phone;
    this.technicalApprovalRequired = technicalApprovalRequired;
    this.description = description;
    this.renewalContract = renewalContract;
    this.supplier = supplier;
    this.lineItems = lineItems;
    this.notes = notes;
  }

}
