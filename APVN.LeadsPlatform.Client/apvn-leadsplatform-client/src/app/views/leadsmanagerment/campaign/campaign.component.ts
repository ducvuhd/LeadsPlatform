import { ElementRef, ViewChild } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { FormGroup, NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ThemeService } from 'ng2-charts';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Campaign } from 'src/app/models/Campaign';
import { ResponseData, SystemCode } from 'src/app/models/ResponseData';
import { LeadsmanagermentService } from 'src/app/services/leadsmanagerment.service';
import { ResponseCode } from 'src/app/utils/enums/responseCode.enum';
import { UtilityHelper } from 'src/app/utils/helpers/utility-helper';
import { KeyValueObject } from 'src/app/utils/KeyValueObject';

@Component({
  selector: 'app-campaign',
  templateUrl: './campaign.component.html',
  styleUrls: ['./campaign.component.css']
})
export class CampaignComponent implements OnInit {
  campaign: Campaign;
  campaignForImport: Campaign;
  lstCampaign: Campaign[];
  index: number;
  showDivError: string;
  @ViewChild('updatecampaign') public updatecampaign: ModalDirective;
  @ViewChild('file') file: ElementRef;
  constructor(
    private leadsService: LeadsmanagermentService,
    private toastr: ToastrService,
    private router: Router
  ) {
  }

  ngOnInit(): void {
    this.index = 0;
    this.campaign = new Campaign();
    this.campaignForImport = new Campaign();
    this.lstCampaign = [];
    this.getAllCampaign();
    this.showDivError = "";
  }
  getAllCampaign() {
    this.leadsService.getAllCampaign().subscribe((res: ResponseData) => {
      if (res.code == ResponseCode.Success) {
        this.lstCampaign = res.data as Campaign[];
      }
    });
  }
  downloadTemplate() {
    this.leadsService.downloadTemplateImportLead().subscribe((res: ResponseData) => {
      if (res.code == ResponseCode.Success) {
        const blob = new Blob([UtilityHelper.s2ab(atob(res.data))], {
          type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,'
        });
        const url = window.URL.createObjectURL(blob);
        window.open(url);
      } else if (res.code === ResponseCode.Error) {
        this.toastr.error(res.message);
      } else {
        this.toastr.warning(res.message);
      }
    });
  }
  resetCampaign() {
    this.campaign = new Campaign();
  }
  update(f: NgForm) {
    if (isNaN(this.campaign.id)) {
      this.campaign.id = 0;
    }
    this.leadsService.saveCampaign(this.campaign).subscribe((res: ResponseData) => {
      this.updatecampaign.hide();
      if (res.code == ResponseCode.Success) {
        this.campaign.id = +res.data;
        this.lstCampaign = [this.campaign, ...this.lstCampaign];
        this.resetCampaign();
        f.resetForm();
        this.toastr.success(res.message);
      } else {
        this.toastr.error(res.message);
      }
    });
  }
  uploadFile(files: any): void {
    if (files.length == 0) {
      this.toastr.warning('Bạn chưa chọn file upload.');
      return;
    }
    if (isNaN(this.campaignForImport.id) || this.campaignForImport.id == 0) {
      this.toastr.warning('Bạn chưa chọn campaign');
      return;
    }
    let fileUpload = files[0];
    var lstKeyValue = [] as KeyValueObject[];
    var keyValue = new KeyValueObject();
    keyValue.Key = "CampaignId";
    keyValue.Value = this.campaignForImport.id;
    lstKeyValue.push(keyValue);
    this.leadsService.importLead(fileUpload, lstKeyValue).subscribe((res: ResponseData) => {
      if (res.code === ResponseCode.Success) {
        this.toastr.success("Import thành công");
        this.showDivError = "";
        this.file.nativeElement.value = null;
      } else {
        this.showDivError = res.message;
      }
    });
  }
}
