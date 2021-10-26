import {LeadAction} from '../models/leadsmanagerment-lead-action'
export class LeadsManagermentModel {
    constructor() {
        this.IsChangeCar = false;
        this.StableIncome = false;
        this.HasWife = false;
        this.HasChild = false;
        this.GoodAddress = false;
        this.LookingOtherCar = false;
        this.GoodActionDealer = false;
        this.IsSoldCar = false;
        this.IsActive = true;
        this.ListAction = [];     
    }
    
    Id: number;
    FullName: string;
    Mobile: string;
    Email: string;
    CityId: number;
    SourceLead: number;
    StartCareDate: Date;
    Description: string;
    Address: string;
    ExpectedDescription: string;
    IsChangeCar: boolean;
    ChangeCarTime: number;
    PriceRage: string;
    LoanMoney: number;
    LoanTime: number;
    Job: string;
    Income: number;
    StableIncome: boolean;
    CurrentCar: string;
    ReasonChangeCar: string;
    HasWife: boolean;
    HasChild: boolean;
    FamilyIncome: number;
    AssignToId: number;
    GoodAddress: boolean;
    LookingOtherCar: boolean;
    OtherCar: string;
    GoodActionDealer: boolean;
    DealerConsider: string;
    IsSoldCar: boolean;
    Note: string;
    CityTestDrive: string;

    ListAction: LeadAction[];
    //
    DealerId: number;
    DealerRecievedDate: Date;
    IsActive: boolean;
    Status: number;
    CreatedDate: Date;
    CreatedBy: string;
    LastModifiedDate: Date;
    LastModifiedBy: string;
    Version: string;
    Year: string;
    Color: string;
    BankId: number;
}

export class ListAction {
    
}

