import { NgModule, LOCALE_ID } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule, registerLocaleData, DecimalPipe } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { CDBInvSimulationFormComponent } from './cdb-investment-simulation/cdb-inv-simulation-form.component';

import localePt from '@angular/common/locales/pt';

registerLocaleData(localePt);

@NgModule({
  imports: [
    BrowserModule,
    CommonModule,
    HttpClientModule,
    FormsModule
  ],
  declarations: [
    AppComponent,
    CDBInvSimulationFormComponent
  ],
  providers: [{ provide: LOCALE_ID, useValue: 'pt' }],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
