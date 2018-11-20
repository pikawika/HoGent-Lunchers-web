import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LunchMerchantComponent } from './lunch-merchant/lunch-merchant.component';
import { AddLunchComponent } from './add-lunch/add-lunch.component';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [LunchMerchantComponent, AddLunchComponent]
})
export class MerchantModule { }
