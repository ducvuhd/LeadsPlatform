import { Component, OnInit } from "@angular/core";
import { HttpService } from 'src/app/services/http.service';
import { environment } from 'src/environments/environment';

@Component({
    templateUrl: 'upload-file.component.html',
    styleUrls: ['upload-file.component.css']
})
export class UploadFileComponent implements OnInit {
    constructor(private httpService: HttpService) {}
    ngOnInit(): void {
    }
    uploadFile(files: any): void {
        if(files.length == 0) {
            return;
        }
        let fileUpload = files[0];
        this.httpService
            .doPostFile(environment.API_URL + '/shift-exchange-employee/upload-shift-schedule',fileUpload)
            .subscribe((res: any) => {
                console.log(res);
            });
    }
}