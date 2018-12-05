import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LunchMerchantComponent } from './lunch-merchant/lunch-merchant.component';
import { AddLunchComponent } from './add-lunch/add-lunch.component';
import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { EditLunchComponent } from './edit-lunch/edit-lunch.component';

@NgModule({
  imports: [
    CommonModule,
    AngularFontAwesomeModule
  ],
  declarations: [LunchMerchantComponent, AddLunchComponent, EditLunchComponent]
})
export class MerchantModule { }
