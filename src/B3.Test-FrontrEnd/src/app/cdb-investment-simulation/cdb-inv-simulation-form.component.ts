import { Component, OnInit } from '@angular/core';

import { CDBModel } from '../../models/cdb-model';
import { FeeModel } from '../../models/fee-model';
import { CDBService } from '../../services/cdb.services';
import { FeeService } from '../../services/fee.services';
import { ProfitabilityModel } from '../../models/profitability-model';
import { ProfitabilityService } from '../../services/profitability.services';
import { CDBInvSimulationModel } from '../../models/cdb-inv-simulation-model';

@Component({
    selector: 'cdb-inv-simulation-form',
    templateUrl: './cdb-inv-simulation-form.component.html',
    styleUrls: ['./cdb-inv-simulation-form.component.css']
})

export class CDBInvSimulationFormComponent implements OnInit
{
    submitted = false;
    model = new CDBModel();

    constructor(private feeService:FeeService, private profitabilityService:ProfitabilityService, private cDBService: CDBService){}

    ngOnInit() 
    {
        this.feeService.getConsolidated().subscribe((feeModel: FeeModel)=>{ this.model.CDIToday = feeModel.fee });
        this.profitabilityService.getCDI().subscribe((cdi: ProfitabilityModel)=>{ this.model.paidProfitability = cdi.paid });
    }

    onSubmit()
    {

    }

    getSimulation() 
    { 
        this.cDBService.getSimulation(this.model).subscribe((result:CDBInvSimulationModel) => 
        {
            this.submitted = true;
            this.model.resultSimulation = result;
            console.log(this.model.resultSimulation);
        });
    }
}