import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { NoteIndexModel } from '../models/NoteIndexModel';
import { NoteModel } from '../models/NoteModel';
import { ResponseData } from '../models/ResponseData';
import { HttpService } from './http.service';

@Injectable({
  providedIn: 'root'
})
export class NoteService {

  constructor(private httpService: HttpService) { }
  update(model: NoteModel): Observable<ResponseData> {
    return this.httpService.doPost(environment.API_URL + '/note/Save', model);
  }
  getById(noteId: number, type: number) {
    return this.httpService.doPost(environment.API_URL + '/note/getbyid', null, { "noteId": noteId, "type": type });
  }
  getlist(index: NoteIndexModel) {
    return this.httpService.doPost(environment.API_URL + '/note/getlist', index);
  }
}
