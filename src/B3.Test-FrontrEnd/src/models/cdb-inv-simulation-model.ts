import { CDBInvPerformaceByMonthModel } from "./cdb-inv-performanceByMonth-model";

export class CDBInvSimulationModel 
{
    public performance: number = 0;
    public profitFreeIR: number = 0; 
    public performanceByMonth?: CDBInvPerformaceByMonthModel[];
}
