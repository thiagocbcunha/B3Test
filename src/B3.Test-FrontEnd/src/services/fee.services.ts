import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { retry, catchError } from 'rxjs/operators';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { FeeModel }from '../models/fee-model';
import { BaseService } from './base-services';

@Injectable({ providedIn: 'root' })
export class FeeService extends BaseService
{
    constructor(private httpClient: HttpClient) 
    {
        super();
    }

    httpOptions = {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    }

    public get(): Observable<FeeModel>
    {
        return this.httpClient.get<FeeModel>(`${this.url}/Fee/0`)
            .pipe(retry(2), catchError(this.handleError));
    }

    public getConsolidated(): Observable<FeeModel>
    {
        return this.httpClient.get<FeeModel>(`${this.url}/Fee/0/consolidated`)
            .pipe(retry(2), catchError(this.handleError));
    }
}
