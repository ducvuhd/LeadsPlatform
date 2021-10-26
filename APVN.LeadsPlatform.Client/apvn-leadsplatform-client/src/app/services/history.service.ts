import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HistoryIndexModel } from '../models/HistoryIndexModel';
import { ResponseData } from '../models/ResponseData';
import { HttpService } from './http.service';

@Injectable({
    providedIn: 'root'
})
export class HistoryService {

    constructor(private httpService: HttpService) { }
    getlist(index: HistoryIndexModel) {
        return this.httpService.doPost(environment.API_URL + '/history/getlist', index);
    }
}
