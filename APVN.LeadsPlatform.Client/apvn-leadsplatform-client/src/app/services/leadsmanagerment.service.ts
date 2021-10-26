import { HttpService } from './http.service';
import { Observable } from 'rxjs';
import { ResponseData } from '../models/ResponseData';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { LeadsManagermentModel } from '../models/leadsmanagerment-model';
import { Campaign } from '../models/Campaign';
import { KeyValueObject } from '../utils/KeyValueObject';
import { DealerIndexModel } from '../models/DealderIndexModel';
import { AssigLeadToDealerModel } from '../models/AssigLeadToDealerModel';
import { NoteModel } from '../models/NoteModel';
import { LeadsQuantityForDealerIndexModel } from '../models/LeadsQuantityForDealerIndexModel';
import { LeadsQuantityForDealerModel } from '../models/LeadsQuantityForDealerModel';

@Injectable({
    providedIn: 'root'
})
export class LeadsmanagermentService {

    constructor(private httpService: HttpService) {
    }

    addLead(lead: LeadsManagermentModel): Observable<ResponseData> {
        return this.httpService.doPost(environment.API_URL + '/LeadsManagerment/save', lead);
    }
    addLeadJson(lead: string): Observable<ResponseData> {
        return this.httpService.doPost(environment.API_URL + '/LeadsManagerment/save', lead);
    }
    getToEdit(leadId: number): Observable<ResponseData> {
        return this.httpService.doGet(environment.API_URL + '/LeadsManagerment/getbyid/' + leadId);
    }

    update(leadModel: LeadsManagermentModel) {
        return this.httpService.doPost(environment.API_URL + '/LeadsManagerment/edit', leadModel);
    }

    assignleadtosale(leadsId: number, assignToId: number): Observable<ResponseData> {
        return this.httpService.doPost(environment.API_URL + '/LeadsManagerment/assignleadtosale', null, { 'leadsId': leadsId, 'assignToId': assignToId });
    }

    receivelead(leadId: number): Observable<ResponseData> {
        return this.httpService.doPost(environment.API_URL + '/LeadsManagerment/getleadforsale/' + leadId);
    }

    changeisactivelead(leadsId: number, isActive: boolean): Observable<ResponseData> {
        return this.httpService.doPost(environment.API_URL + '/LeadsManagerment/changeisactivelead', null, { 'leadId': leadsId, 'isActive': isActive });
    }
    /**
     * Get form filter
     * 
     */
    getFormFilter() {
        return this.httpService.doPost(
            environment.API_URL + '/leadsmanagerment/get-form-filter'
        );
    }

    /**
     * Get search main list
     * 
     */
    search(model) {
        return this.httpService.doPost(
            environment.API_URL + '/leadsmanagerment/get-main-list',
            model
        );
    }

    /**
    * Get form create
    * 
    */
    getFormCreate(): Observable<ResponseData> {
        return this.httpService.doPost(environment.API_URL + '/leadsmanagerment/get-form-create');
    }

    /**
     * GET DETAIL
     * 
     */
    getDetail(id) {
        return this.httpService.doPost(
            environment.API_URL + '/leadsmanagerment/get-detail',
            null,
            { id: id }
        );
    }
    initDetail() {
        return this.httpService.doPost(environment.API_URL + '/leadsmanagerment/init-detail');
    }
    //Campaign
    getAllCampaign(): Observable<ResponseData> {
        return this.httpService.doGet(environment.API_URL + '/leadsmanagerment/get-all-campaign');
    }
    saveCampaign(campaign: Campaign) {
        return this.httpService.doPost(environment.API_URL + '/leadsmanagerment/save-campaign', campaign);
    }
    downloadTemplateImportLead() {
        return this.httpService.doPost(environment.API_URL + '/leadsmanagerment/downloadTemplate');
    }
    importLead(file: File, lstKeyValue = [] as KeyValueObject[]): Observable<ResponseData> {
        return this.httpService.doPostFile(environment.API_URL + '/leadsmanagerment/importleads', file, lstKeyValue);
    }
    searchDealerFromOto(indexModel: DealerIndexModel): Observable<ResponseData> {
        return this.httpService.doPost(environment.API_URL + '/leadsmanagerment/get-dealer-from-oto', indexModel);
    }
    getLeadActionByLeadsId(leadsId: number): Observable<ResponseData> {
        return this.httpService.doPost(environment.API_URL + '/leadsmanagerment/get-leadaction-by-leadsId/' + leadsId);
    }
    assigLeadsToDealer(model: AssigLeadToDealerModel): Observable<ResponseData> {
        return this.httpService.doPost(environment.API_URL + '/leadsmanagerment/assigleadtodealer', model);

    }
    updateLeadActionStatus(status: number, leadActionId: number): Observable<ResponseData> {
        return this.httpService.doPost(environment.API_URL + '/leadsmanagerment/update-leadactionstatus', null, { 'status': status, 'leadActionId': leadActionId });
    }
    blockLead(noteModel: NoteModel): Observable<ResponseData> {
        return this.httpService.doPost(environment.API_URL + '/leadsmanagerment/block-lead', noteModel);
    }
    leadsQuantityForDealerGetListPaging(model: LeadsQuantityForDealerIndexModel): Observable<ResponseData> {
        return this.httpService.doPost(environment.API_URL + '/leadsmanagerment/leadquantity-getlistpaging', model);
    }
    
    leadsQuantityForDealerInsert(model: LeadsQuantityForDealerModel): Observable<ResponseData> {
        return this.httpService.doPost(environment.API_URL + '/leadsmanagerment/leadquantity-insert', model);
    }
    leadsQuantityCancelSetSchedule(id: number): Observable<ResponseData> {
        return this.httpService.doPost(environment.API_URL + '/leadsmanagerment/leadquantity-cancel-schedule', null, { 'id': id, });
    }
}
