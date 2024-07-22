import { CDBInvSimulationModel } from "./cdb-inv-simulation-model";

export class CDBModel
{
    public CDIToday: number = 0;
    public paidProfitability: number = 0;
    public resultSimulation:CDBInvSimulationModel = new CDBInvSimulationModel();

    constructor(
        public TimeInvestmentInMonth?: number,
        public InitialInvestment?: number,
        public InvestmentEnum: number = 0
    ) {
      
    }
}
