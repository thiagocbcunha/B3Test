import { FormsModule } from '@angular/forms';
import { NgModule, LOCALE_ID } from '@angular/core';
import { MatTabsModule } from '@angular/material/tabs';
import { provideHttpClient } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule, registerLocaleData } from '@angular/common';

import { AppComponent } from './app.component';
import { CDBInvSimulationFormComponent } from './cdb-investment-simulation/cdb-inv-simulation-form.component';

import localePt from '@angular/common/locales/pt';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';

registerLocaleData(localePt);

@NgModule({
  imports: [
    BrowserModule,
    CommonModule,
    FormsModule,
    MatTabsModule
  ],
  declarations: [
    AppComponent,
    CDBInvSimulationFormComponent
  ],
  providers: [{ provide: LOCALE_ID, useValue: 'pt' }, provideHttpClient(), provideAnimationsAsync()],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
