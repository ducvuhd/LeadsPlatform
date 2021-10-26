import { Component, OnInit, OnChanges, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators, NgForm } from '@angular/forms';
import { LeadsmanagermentService } from '../../../services/leadsmanagerment.service';
import { LeadsManagermentModel, ListAction } from '../../../models/leadsmanagerment-model';
import { LeadAction } from '../../../models/leadsmanagerment-lead-action';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { from } from 'rxjs';
import { ResponseData, SystemCode } from 'src/app/models/ResponseData';
import { UtilityHelper } from 'src/app/utils/helpers/utility-helper';
import { CommonService } from '../../../services/common.service';
import { LeadsActionExtends } from 'src/app/models/LeadsActionExtends';
import { max } from 'rxjs/operators';
import { JsonpClientBackend } from '@angular/common/http';
import { KeyValueObject } from 'src/app/utils/KeyValueObject';
import { Constants } from 'src/app/utils/constants';
import { JwtHelper } from 'src/app/utils/helpers/jwt.helper';
import { PermissionHelper } from 'src/app/utils/helpers/permission-helper';
import { SystemRole } from 'src/app/utils/enums/system-role.enum';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { NoteModel } from 'src/app/models/NoteModel';
import { NoteType } from 'src/app/utils/enums/note.enum';

@Component({
  selector: 'app-leadsmanagerment-add-lead',
  templateUrl: './leadsmanagerment-add-lead.component.html',
  styleUrls: ['./leadsmanagerment-add-lead.component.css']
})
export class LeadsmanagermentAddLeadComponent implements OnInit {

  form: FormGroup;
  formSubmitAttempt: boolean;
  selectChangeCar: boolean;
  modeInsert: boolean;
  leads: LeadsManagermentModel;
  listInfoCar: LeadAction[];
  sources: [];
  listUser: [];
  listCampaign: [];
  listPayment: [];
  cityList: [];

  lstmake: any[];
  lstmodel: any[];
  mustLoadModelEdit = false;
  lstSecondhand: any[];
  lstLeadActionType: any[];
  bankList: any[];
  isCollapsed1: boolean = false;
  isCollapsed2: boolean = false;
  isCollapsed3: boolean = true;
  isCollapsed4: boolean = true;
  isCollapsed5: boolean = true;
  isCollapsed6: boolean = true;
  isCollapsed7: boolean = true;
  lstLeadActionSaleSetType: any[] = [];
  isCollapsed8: boolean = true;
  listLeadActionStatus: KeyValueObject[] = [];
  index: number = 0;
  isSubmitBlock: boolean = false;
  noteModel: NoteModel = new NoteModel();
  @ViewChild("modalBlockLead") public modalBlockLead: ModalDirective
  constructor(
    private leadService: LeadsmanagermentService,
    private commonService: CommonService,
    private toastr: ToastrService,
    private activatedRoute: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    this.leads = new LeadsManagermentModel();
    const leadId = parseInt(this.activatedRoute.snapshot.params['leadId']);
    this.initValue();
    if (isNaN(leadId)) {
      this.modeInsert = true;
    } else {
      this.modeInsert = false;
      this.leadService.getToEdit(leadId).subscribe((res: any) => {
        if (res.Code === SystemCode.Success) {
          // let leadModel = UtilityHelper.toPascal(res.data) as LeadsManagermentModel;
          this.leads = res.Data;
          this.selectChangeCar = this.leads.IsChangeCar;
          this.resetForm(this.leads);
          this.mustLoadModelEdit = true;

          for (let i = 0; i < this.leads.ListAction.length; i++) {
            this.leads.ListAction[i].Index = i;
            for (let j = 0; j < this.leads.ListAction[i].ListLeadsActionExtends.length; j++) {
              this.leads.ListAction[i].ListLeadsActionExtends[j].Index = j;
            }
          }
        }
        else {
          this.toastr.error(res.Message);
        }
      })
    }
  }

  get f() {
    return this.form.controls;
  }

  submit() {
    console.log("dataForm:", this.form.getRawValue());
    this.copyValueFromForm(this.leads, this.form.getRawValue());
    console.log("leadModelAction:", this.leads);
    console.log(this.form);
    this.leads = this.formatMoney(this.leads);
    if (this.form.valid && this.checkValidateLeadAction(this.leads.ListAction)) {
      this.leads.ListAction.forEach(x => {
        let type = 0;
        x.SaleSetType = x?.ListSaleSetType?.reduce((sum, curr) => sum + curr, 0);
        for (const item of x.ListLeadsActionExtends) {
          item.BankId = item.BankId ?? 0;
          item.CityId = item.CityId ?? 0;
          type = type + item.Type;
        }
        x.Type = type;
        x.ListLeadsActionExtendsStr = JSON.stringify(x.ListLeadsActionExtends);
      });
      this.leadService.addLead((this.leads)).subscribe(res => {
        console.log(res);
        if (res.code == SystemCode.Success) {
          this.toastr.success(res.message);
          if (!this.modeInsert) {
            this.router.navigate(['/leadsmanagerment/list']);
          }
          this.initValue();
        } else {
          this.toastr.error(res.message);
        }
      });
    }
  }
  reset() {
    this.initValue();
  }

  addHTMLInfoCar() {
    this.formSubmitAttempt = false;
    if (this.leads.ListAction.length > 1) {
      const maxIndex = Math.max.apply(Math, this.leads.ListAction.map(function (o) { return o.Index; }))
      const leadsAction = new LeadAction();
      leadsAction.Index = maxIndex + 1;
      this.leads.ListAction.push(leadsAction);
      leadsAction.ListLeadsActionExtends.push(new LeadsActionExtends());

    } else {
      if (this.leads.ListAction.length == 1) {
        const leadsAction = new LeadAction();
        leadsAction.Index = this.leads.ListAction[0].Index + 1;
        this.leads.ListAction.push(leadsAction);
        leadsAction.ListLeadsActionExtends.push(new LeadsActionExtends());

      } else {
        const leadsAction = new LeadAction();
        this.leads.ListAction.push(leadsAction);
        leadsAction.ListLeadsActionExtends.push(new LeadsActionExtends());
      }
    }
  }

  removeHTMLInfoCar(leadActionIndex: number) {
    const leadsAction = this.leads.ListAction.filter(x => x.Index == leadActionIndex)[0];
    if (leadsAction != null && leadsAction != undefined) {
      const index = this.leads.ListAction.indexOf(leadsAction);
      this.leads.ListAction.splice(index, 1);
    }
  }

  onChangeMake(event) {
    if (event?.makeID && event?.makeID != '') {
      this.commonService.handlerMakeChange(event.makeID).subscribe((res) => {
        if (res != null && res.code == SystemCode.Success) {
          this.lstmodel = res.data.modelList;
          this.mustLoadModelEdit = false;
        }
      });
    } else {
      this.lstmodel = [];
    }
  }

  changeFormatMoney(event) {
    const oldValue = event.target.value
    let newValue = this.formatValue(oldValue, ',');
    event.target.value = newValue + ' VNĐ';
  }

  clickInputMoney(event) {
    const oldValue = event.target.value;
    let newValue = this.formatValue(oldValue, '');
    if (isNaN(Number(newValue))) {
      event.target.value = 0;
    } else {
      event.target.value = newValue;
    }
  }

  mouseoutMoney(event) {
    this.changeFormatMoney(event);
  }

  formatValue(value, char) {
    if (value == null || value == undefined) {
      return 0;
    }
    value = String(value);
    return parseFloat(value.replace(/,/g, ""))
      .toFixed(0)
      .toString()
      .replace(/\B(?=(\d{3})+(?!\d))/g, char);
  }

  getModelFromMakeId(makeID) {
    if (this.mustLoadModelEdit == true) {
      if (makeID && makeID != '') {
        this.commonService.handlerMakeChange(makeID).subscribe((res) => {
          if (res != null && res.code == 1) {
            this.lstmodel = res.data.modelList;
          }
        });
      }
      this.mustLoadModelEdit = false;
    }
  }


  onItemChange() {
    this.selectChangeCar = this.form.value.IsChangeCar;
  }
  removeLeadsAcctionExtends(leadsActionIndex: number, leadExtendIndex: number) {
    const leadsAction = this.leads.ListAction.filter(x => x.Index == leadsActionIndex)[0];
    if (leadsAction != null && leadsAction != undefined) {
      const leadsActionExtends = leadsAction.ListLeadsActionExtends.filter(x => x.Index == leadExtendIndex)[0];
      const index = leadsAction.ListLeadsActionExtends.indexOf(leadsActionExtends);
      leadsAction.ListLeadsActionExtends.splice(index, 1);
    }
  }
  AddLeadsAcctionExtends(leadsActionIndex: number) {
    this.formSubmitAttempt = false;
    const leadsAction = this.leads.ListAction.filter(x => x.Index == leadsActionIndex)[0];
    if (leadsAction != null && leadsAction != undefined) {
      if (leadsAction.ListLeadsActionExtends.length > 1) {
        const maxIndex = Math.max.apply(Math, leadsAction.ListLeadsActionExtends.map(function (o) { return o.Index; }))
        // const lastLeadsActionExtends = leadsAction.ListLeadsActionExtends.reduce((x, y) => x.Index > y.Index ? x : y)[0];
        const leadsActionExtends = new LeadsActionExtends();
        leadsActionExtends.Index = maxIndex + 1;
        leadsAction.ListLeadsActionExtends.push(leadsActionExtends);
      } else {
        if (leadsAction.ListLeadsActionExtends.length == 1) {
          const actionExtends = new LeadsActionExtends();
          actionExtends.Index = leadsAction.ListLeadsActionExtends[0].Index + 1;
          leadsAction.ListLeadsActionExtends.push(actionExtends);
        } else {
          leadsAction.ListLeadsActionExtends.push(new LeadsActionExtends());
        }
      }

    }
  }
  private initValue() {
    this.formSubmitAttempt = false;
    this.selectChangeCar = false;
    this.leads.ListAction = [new LeadAction()];
    this.leads.ListAction.forEach(x => {
      x.ListLeadsActionExtends = [new LeadsActionExtends()]
    });

    this.leadService.getFormCreate().subscribe((res) => {
      if (res != null) {
        const dataRes = res.data;
        this.cityList = dataRes.cityList;
        this.listPayment = dataRes.lstPaymentStatus;
        this.sources = dataRes.lstSource;
        this.listCampaign = dataRes.lstCampaign.result;
        this.listUser = dataRes.lstUser;
        this.lstmake = dataRes.lstmake;
        this.lstmodel = this.modeInsert ? [] : dataRes.lstmodel;
        this.lstSecondhand = dataRes.lstSecondhand;
        this.lstLeadActionType = dataRes.lstLeadActionType;
        this.lstLeadActionSaleSetType = this.lstLeadActionType.filter(x => x.Key != 32 && x.Key != 64 && x.Key != 128);
        this.bankList = dataRes.bankList;
        this.listLeadActionStatus = dataRes.lstActionStatus;
      }
    });
    this.initForm();
  }

  private initForm() {
    this.form = new FormGroup({
      FullName: new FormControl('', [Validators.required]),
      Mobile: new FormControl('', [Validators.required]),
      Email: new FormControl(),
      IsChangeCar: new FormControl(false),
      ChangeCarTime: new FormControl(),
      CurrentCar: new FormControl(),
      HasWife: new FormControl(false),
      HasChild: new FormControl(false),
      SourceLead: new FormControl(0, [Validators.required, Validators.pattern("^[0-9]*$")]),
      Description: new FormControl(),
      Address: new FormControl(),
      ExpectedDescription: new FormControl(),
      PriceRage: new FormControl(),
      // LoanMoney: new FormControl(0, [Validators.pattern("^[0-9]*$")]),
      // LoanTime: new FormControl(null, [Validators.pattern("^[0-9]*$")]),
      StableIncome: new FormControl(false),
      Job: new FormControl(),
      Income: new FormControl(0, Validators.pattern("^[0-9]+((\,[0-9]{1,3})*)+( VNĐ)?$")),
      ReasonChangeCar: new FormControl(),
      FamilyIncome: new FormControl(0, Validators.pattern("^[0-9]+((\,[0-9]{1,3})*)+( VNĐ)?$")),
      CreatedDate: new FormControl('', [Validators.required]),
      CityId: new FormControl(null, [Validators.required]),
      AssignToId: new FormControl(),
      GoodAddress: new FormControl(false),
      GoodActionDealer: new FormControl(false),
      LookingOtherCar: new FormControl(false),
      OtherCar: new FormControl(),
      IsSoldCar: new FormControl(false),
      DealerConsider: new FormControl(),
      Note: new FormControl(),
      Version: new FormControl(),
      Year: new FormControl(),
      Color: new FormControl(),
      // BankId: new FormControl(),
      IsActive: new FormControl(),
      CityTestDrive: new FormControl(),
    });
  }

  private resetForm(value: LeadsManagermentModel) {
    this.form.reset({
      FullName: value.FullName,
      Mobile: value.Mobile,
      Email: value.Email,
      IsChangeCar: value.IsChangeCar,
      ChangeCarTime: value.ChangeCarTime,
      CurrentCar: value.CurrentCar,
      HasWife: value.HasWife,
      HasChild: value.HasChild,
      SourceLead: value.SourceLead,
      Description: value.Description,
      Address: value.Address,
      ExpectedDescription: value.ExpectedDescription,
      PriceRage: value.PriceRage,
      // LoanMoney: value.LoanMoney,
      // LoanTime: value.LoanTime,
      StableIncome: value.StableIncome,
      Job: value.Job,
      Income: this.formatValue(value.Income, ',') + ' VNĐ',
      ReasonChangeCar: value.ReasonChangeCar,
      FamilyIncome: this.formatValue(value.FamilyIncome, ',') + ' VNĐ',
      StartCareDate: new Date(value.StartCareDate).toLocaleDateString('en-ZA').replaceAll('/', '-'),
      CityId: value.CityId,
      AssignToId: value.AssignToId,
      GoodAddress: value.GoodAddress,
      GoodActionDealer: value.GoodActionDealer,
      LookingOtherCar: value.LookingOtherCar,
      OtherCar: value.OtherCar,
      IsSoldCar: value.IsSoldCar,
      DealerConsider: value.DealerConsider,
      Note: value.Note,
      IsActive: value.IsActive,
      CreatedDate: new Date(value.CreatedDate).toLocaleDateString('en-ZA').replaceAll('/', '-'),
      Version: value.Version,
      Year: value.Year,
      Color: value.Color,
      BankId: value.BankId,
      CityTestDrive: value.CityTestDrive,
    })
  }
  private checkValidateLeadAction(lstLeadAction: LeadAction[]): boolean {

    let result: boolean = true; let resultExtends: boolean = true;
    for (const item of lstLeadAction) {
      if (Number(item.MakeId) == 0 || isNaN(Number(item.MakeId))) {
        result = false;
        break;
      }
      if (Number(item.ModelId) == 0 || isNaN(Number(item.ModelId))) {
        result = false;
        break;
      }
      // if (Number(item.SecondHand) == 0 || isNaN(Number(item.SecondHand))) {
      //   result = false;
      //   break;
      // }
      if (Number(item.CampaignId) == 0 || isNaN(Number(item.CampaignId))) {
        result = false;
        break;
      }
      if (Number(item.PaymentStatus) == 0 || isNaN(Number(item.PaymentStatus))) {
        result = false;
        break;
      }
      for (const extendsLeadsAction of item.ListLeadsActionExtends) {
        if (Number(extendsLeadsAction.SecondHand) == 0 || isNaN(Number(extendsLeadsAction.SecondHand))) {
          resultExtends = false;
          break;
        }
        if (Number(extendsLeadsAction.Type) == 0 || isNaN(Number(extendsLeadsAction.Type))) {
          resultExtends = false;
          break;
        }
      }
      if (!resultExtends) {
        result = false;
        break;
      }
    }
    return result;
  }
  private copyValueFromForm(ojbNeedAppend, objAppend) {
    var origKey
    for (origKey in objAppend) {
      if (objAppend.hasOwnProperty(origKey)) {
        if (objAppend[origKey] != null) {
          ojbNeedAppend[origKey] = objAppend[origKey]
        }
      }
    }
    return ojbNeedAppend;
  }

  private formatMoney(leads: LeadsManagermentModel) {
    let newValue = Number(this.formatValue(leads.LoanMoney, ''));
    if (isNaN(newValue) == false) {
      leads.LoanMoney = newValue;
    } else {
      leads.LoanMoney = 0;
    }

    let newValue1 = Number(this.formatValue(leads.Income, ''));
    if (isNaN(newValue) == false) {
      leads.Income = newValue1;
    } else {
      leads.Income = 0;
    }

    let newValue2 = Number(this.formatValue(leads.FamilyIncome, ''));
    if (isNaN(newValue) == false) {
      leads.FamilyIncome = newValue2;
    } else {
      leads.FamilyIncome = 0;
    }

    return leads;
  }
  saveLeadActionStatus(status: number, leadActionId: number) {
    const currUser = JwtHelper.decodeToken(localStorage.getItem(Constants.TokenKey));
    if (PermissionHelper.hasRole(SystemRole.Admin, SystemRole.Lead) || +currUser.certserialnumber == this.leads.AssignToId) {
      this.leadService.updateLeadActionStatus(status, leadActionId).subscribe((res: ResponseData) => {
        if (res.code == SystemCode.Success) {
          this.toastr.success(res.message);
        } else {
          this.toastr.error(res.message);
        }
      });
    } else {
      this.toastr.warning('Bạn không có quyền thao tác');
    }
  }
  checkBlockLead(f: NgForm) {
    this.copyValueFromForm(this.leads, this.form.getRawValue());
    if (!this.leads.IsActive) {
      this.resetFormBlock(f);
      this.showFormBlock(this.leads.Id, this.leads.Status);
    } else {
      this.leadService.changeisactivelead(this.leads.Id, this.leads.IsActive).subscribe((res) => {
        if (res.code === SystemCode.Success) {
          this.toastr.success(res.message);
        } else {
          this.toastr.error(res.message);
        }
      });
    }
  }
  resetFormBlock(f: NgForm) {
    this.isSubmitBlock = false;
    f.reset();
  }
  showFormBlock(leadId: number, status: number) {
    this.noteModel = new NoteModel();
    this.noteModel.relationId = leadId;
    this.noteModel.currentRelationStatus = status;
    this.modalBlockLead.show();
  }
  blockLead(f: NgForm) {
    this.isSubmitBlock = true;
    if (!f.form.valid) {
      return;
    }
    this.noteModel.type = NoteType.Leads;
    this.leadService.blockLead(this.noteModel).subscribe((res: any) => {
      if (res.code == SystemCode.Success) {
        this.modalBlockLead.hide();
        this.toastr.success(res.message);
      } else {
        this.toastr.error(res.message);
      }
    });
  }
  hideModelBloclLead(){
    this.copyValueFromForm(this.leads, this.form.getRawValue());
    this.leads.IsActive = !this.leads.IsActive;
    this.form.patchValue({
      IsActive: this.leads.IsActive
    });
    this.modalBlockLead.hide();
  }
}
