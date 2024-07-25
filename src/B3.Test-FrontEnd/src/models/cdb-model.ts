import { CDBInvSimulationModel } from "./cdb-inv-simulation-model";

export class CDBModel
{
    public Fee: number = 0;
    public CDIToday: number = 0;
    public paidProfitability: number = 0;
    public resultSimulation: CDBInvSimulationModel = new CDBInvSimulationModel();
    public fixedResultSimulation: CDBInvSimulationModel = new CDBInvSimulationModel();

    constructor(
        public TimeInvestmentInMonth?: number,
        public InitialInvestment?: number,
        public InvestmentType: number = 0
    ) {
      
    }
}
