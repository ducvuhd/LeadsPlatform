import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { LeadsQuantityForDealerIndexModel } from 'src/app/models/LeadsQuantityForDealerIndexModel';
import { LeadsQuantityForDealerModel } from 'src/app/models/LeadsQuantityForDealerModel';
import { ResponseData, SystemCode } from 'src/app/models/ResponseData';
import { LeadsmanagermentService } from 'src/app/services/leadsmanagerment.service';
import { ResponseCode } from 'src/app/utils/enums/responseCode.enum';

@Component({
  selector: 'app-dealergetlead',
  templateUrl: './dealergetlead.component.html',
  styleUrls: ['./dealergetlead.component.css']
})
export class DealergetleadComponent implements OnInit {
  indexModel: LeadsQuantityForDealerIndexModel = new LeadsQuantityForDealerIndexModel();
  model: LeadsQuantityForDealerModel = new LeadsQuantityForDealerModel();
  index: number = 0;
  @ViewChild('setSchedule') public setSchedule: ModalDirective;
  constructor(
    private leadsmanagermentService: LeadsmanagermentService,
    private toastr: ToastrService,

  ) { }

  ngOnInit(): void {
    this.getList(1);
  }
  getList(pageIndex: number) {
    this.indexModel.pageIndex = pageIndex;
    this.indexModel.listLeadsQuantityForDealer = [];
    this.leadsmanagermentService.leadsQuantityForDealerGetListPaging(this.indexModel)
      .subscribe((res: ResponseData) => {
        if (res.code == SystemCode.Success) {
          const indexModel = res.data as LeadsQuantityForDealerIndexModel;
          this.indexModel.listLeadsQuantityForDealer = indexModel.listLeadsQuantityForDealer;
          this.indexModel.pageIndex = indexModel.pageIndex;
          this.indexModel.totalPage = indexModel.totalPage;
          this.indexModel.totalRecord = indexModel.totalRecord;
        }
      });
  }
  reset() {
    this.model = new LeadsQuantityForDealerModel();
  }
  pageChangedDealer(pageIndex: number) {
    this.getList(pageIndex);
  }
  update(f: NgForm) {
    if (this.model.startDate > this.model.endDate) {
      this.toastr.warning("Ngày bắt đầu phải nhỏ hơn ngày kết thúc");
      return false;
    }
    if (this.model.assignQuantity <= 0 || this.model.assignQuantity % 1 != 0) {
      return false;
    }
    this.leadsmanagermentService.leadsQuantityForDealerInsert(this.model).subscribe((res: ResponseData) => {
      this.setSchedule.hide();
      if (res.code == ResponseCode.Success) {
        this.getList(this.indexModel.pageIndex);
        f.resetForm();
        this.toastr.success(res.message);
      } else {
        this.toastr.error(res.message);
      }
    });
  }
  showFormSetSchedule(dealerId: number, dealerName: string, dealerEmail: string) {
    this.reset();
    this.model.dealerId = dealerId;
    this.model.dealerName = dealerName;
    this.model.dealerEmail = dealerEmail;
    this.setSchedule.show();
  }
  cancelFormSetSchedule(id: number) {
    this.leadsmanagermentService.leadsQuantityCancelSetSchedule(id).subscribe((res: ResponseData) => {
      if (res.code == SystemCode.Success) {
        this.toastr.success(res.message);
        this.indexModel.listLeadsQuantityForDealer.filter(x => x.id == id)[0].isActive = false;
      } else {
        this.toastr.error(res.message);
      }
    });
  }
}
