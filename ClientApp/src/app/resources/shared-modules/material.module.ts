import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  MatButtonModule, MatCheckboxModule, MatDatepickerModule, MatInputModule, MatNativeDateModule, MatSelectModule,
  MatTableModule,
  MatTabsModule
} from '@angular/material';

@NgModule({
  imports: [MatButtonModule, MatCheckboxModule, MatSelectModule, MatTabsModule, MatTableModule, MatInputModule, MatDatepickerModule, MatNativeDateModule],
  exports: [MatButtonModule, MatCheckboxModule, MatSelectModule, MatTabsModule, MatTableModule, MatInputModule, MatDatepickerModule, MatNativeDateModule],
})
export class MaterialModule { }
