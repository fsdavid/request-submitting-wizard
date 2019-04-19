import {Component, ElementRef, Inject, ViewChild} from '@angular/core';
import * as actions from '../resources/store/store.actions';
import * as reducer from '../resources/store/store.reducer';
import {animate, state, style, transition, trigger} from '@angular/animations';
import {ListItemModel} from '../resources/models/list-item.model';
import {RequestModel} from '../resources/models/request.model';
import {MatTable} from '@angular/material';
import {Store} from '@ngrx/store';

@Component({
  selector: 'app-add-request',
  templateUrl: './add-request.component.html',
  styleUrls: ['./add-request.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0', display: 'none'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class AddRequestComponent {

  request: RequestModel = new RequestModel(null, '', '', '', '', '', '', '', '', '', false, '', false, '', [], [{noteId: 0, requestId: 0, noteType: '', content: '', noteFiles: []}]);

  dataSource = this.request.lineItems;
  columnsToDisplay = ['description', 'quantity', 'unit', 'unitPrice', 'budgetObject', 'amount'];
  expandedElement: ListItemModel | null;

  addingLine = false;
  addingAccLine = false;

  @ViewChild(MatTable) table: MatTable<any>;
  @ViewChild('Description') description: ElementRef;
  @ViewChild('Quantity') quantity: ElementRef;
  @ViewChild('Unit') unit: ElementRef;
  @ViewChild('UnitPrice') unitPrice: ElementRef;
  @ViewChild('BudgetObject') budgetObject: ElementRef;
  @ViewChild('Amount') amount: ElementRef;
  @ViewChild('SubCenterNo') subCenterNo: ElementRef;
  @ViewChild('AmountAcc') amountAcc: ElementRef;

  constructor(private store: Store<reducer.StoresState>) {}



  addLineToRequest() {
    this.request.lineItems.push(
      new ListItemModel(
        this.request.lineItems.length, this.request.requestNo, this.description.nativeElement.value, +this.quantity.nativeElement.value,
        this.unit.nativeElement.value, +this.unitPrice.nativeElement.value, + this.budgetObject.nativeElement.value,
        +this.amount.nativeElement.value, []));
    console.log(this.request);
    this.table.renderRows();

    this.cancelAddLineToRequest();
  }

  cancelAddLineToRequest() {
    this.addingLine = false;
    this.description.nativeElement.value = null;
    this.quantity.nativeElement.value = null;
    this.unit.nativeElement.value = null;
    this.unitPrice.nativeElement.value = null;
    this.budgetObject.nativeElement.value = null;
    this.amount.nativeElement.value = null;
  }

  addAccLine(lineId: number) {
    const lineToAdd = this.request.lineItems.find(l => +l.lineId === +lineId).accLines;
    const acc = {itemId: lineToAdd.length, lineId: lineId, subCenterNo: +this.subCenterNo.nativeElement.value, amount: +this.amountAcc.nativeElement.value};
    lineToAdd.push(acc);
  }

  cancelAddAccLine() {
    this.addingAccLine = false;
    this.subCenterNo = null;
    this.amountAcc = null;
  }

  resetRequest() {
    this.request = new RequestModel(null, '', '', '', '', '', '', '', '', '', false, '', false, '', [], [{noteId: 0, requestId: 0, noteType: '', content: '', noteFiles: []}]);
    this.table.renderRows();
  }

  addRequest() {
    this.request.requestDate = new Date(this.request.requestDate).toISOString();
    this.request.requiredDeliveryDate = new Date(this.request.requiredDeliveryDate).toISOString();
    this.request.requestStatus = 'Request Generated';
    this.store.dispatch(new actions.FetchRequest(this.request));

  }


}
