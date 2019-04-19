import {AccLineModel} from './acc-line.model';

export class ListItemModel {
  lineId: number;
  requestId: number;
  description: string;
  quantity: number;
  unit: string;
  unitPrice: number;
  budgetObject: number;
  amount: number;
  accLines: AccLineModel[];
  constructor(
    lineId: number,
    requestId: number,
    description: string,
    quantity: number,
    unit: string,
    unitPrice: number,
    budgetObject: number,
    amount: number,
    accLines: AccLineModel[],
  ) {
    this.lineId = lineId;
    this.requestId = requestId;
    this.description = description;
    this.quantity = quantity;
    this.unit = unit;
    this.unitPrice = unitPrice;
    this.budgetObject = budgetObject;
    this.accLines = accLines;
    this.amount = amount;

  }
}
