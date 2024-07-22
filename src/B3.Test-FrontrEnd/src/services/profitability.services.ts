import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { retry, catchError } from 'rxjs/operators';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { BaseService } from './base-services';
import { ProfitabilityModel } from '../models/profitability-model';

@Injectable({ providedIn: 'root' })
export class ProfitabilityService extends BaseService
{
    constructor(private httpClient: HttpClient) 
    {
        super();
    }

    httpOptions = {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    }

    public getCDI(): Observable<ProfitabilityModel>
    {
        return this.httpClient.get<ProfitabilityModel>(`${this.url}/Profitability/cdi`)
            .pipe(retry(2), catchError(this.handleError));
    }
}
