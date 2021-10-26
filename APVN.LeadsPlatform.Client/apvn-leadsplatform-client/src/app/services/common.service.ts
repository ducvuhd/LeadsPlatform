import { HttpService } from './http.service';
import { Observable } from 'rxjs';
import { ResponseData } from '../models/ResponseData';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})
export class CommonService {
    constructor(private httpService: HttpService) {}

    /**
     * Handler change MAKE
     * 
     */
    handlerMakeChange(makeId){
        return this.httpService.doPost(
            environment.API_URL + '/common/handler-make-change', 
            null,
            {
                makeId: makeId
            }
        );
    }
}
