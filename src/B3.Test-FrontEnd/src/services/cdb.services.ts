import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { BaseService } from './base-services';
import { CDBModel } from '../models/cdb-model';
import { CDBInvSimulationModel }from '../models/cdb-inv-simulation-model';

@Injectable({ providedIn: 'root' })
export class CDBService extends BaseService
{
    constructor(private httpClient: HttpClient) 
    {
        super();
    }

    httpOptions = {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    }

    public getSimulation(cdbModel:CDBModel): Observable<CDBInvSimulationModel>
    {
        return this.httpClient.post<CDBInvSimulationModel>(`${this.url}/Simulation`, cdbModel)
            .pipe(retry(2), catchError(this.handleError));
    }
}
