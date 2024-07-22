import { CDBInvPerformaceByMonthModel } from "./cdb-inv-performanceByMonth-model";

export class CDBInvSimulationModel 
{
    public performance: number = 0;
    public taxExemptProfit: number = 0; 
    public performanceByMonth?: CDBInvPerformaceByMonthModel[];
}
  