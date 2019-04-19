export class AccLineModel {
  itemId: number;
  lineId: number;
  subCenterNo: number;
  amount: number;
  constructor (itemId: number,
               lineId: number,
               subCenterNo: number,
               amount: number) {

    this.itemId = itemId;
    this.lineId = lineId;
    this.subCenterNo = subCenterNo;
    this.amount = amount;
  }
}
