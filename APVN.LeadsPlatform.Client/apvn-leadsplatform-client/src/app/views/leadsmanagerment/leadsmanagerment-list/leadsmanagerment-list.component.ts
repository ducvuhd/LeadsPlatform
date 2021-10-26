import { Component, OnInit, ViewChild, NgModule, ElementRef, Renderer2 } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CustomModalComponent } from '../../modal/custom-modal.component';
import { GroupRole, ResponseData, SystemCode } from '../../../models/ResponseData';
import { ResponseCode } from 'src/app/utils/enums/responseCode.enum';
import { LeadsmanagermentService } from '../../../services/leadsmanagerment.service';
import { LeadsFilterModel } from 'src/app/models/leads-filter-model';
import { LeadsManagermentModel } from 'src/app/models/leadsmanagerment-model';
import { HelperFormat } from 'src/app/utils/helpers/helper';
import { FormControl, NgForm } from '@angular/forms';
import { CommonService } from '../../../services/common.service';
import { MatDialog } from '@angular/material/dialog';
import { AssigLeadToDealerModel } from 'src/app/models/AssigLeadToDealerModel';
import { DealerIndexModel } from 'src/app/models/DealderIndexModel';
import { ModalDirective, ModalOptions } from 'ngx-bootstrap/modal';
import { DealerFromOtoModel } from 'src/app/models/DealerFromOtoModel';
import { LeadAction } from 'src/app/models/leadsmanagerment-lead-action';
import { KeyValueObject } from 'src/app/utils/KeyValueObject';
import { DatePipe } from '@angular/common';
import { Constants } from '../../../utils/constants';
import { JwtHelper } from '../../../utils/helpers/jwt.helper';
// declare var $: any;
import * as $ from "jquery";
import { UtilityHelper } from 'src/app/utils/helpers/utility-helper';
import { PermissionHelper } from 'src/app/utils/helpers/permission-helper';
import { SystemRole } from 'src/app/utils/enums/system-role.enum';
import { NoteModel } from 'src/app/models/NoteModel';
import { NoteType } from 'src/app/utils/enums/note.enum';
@Component({
    selector: 'app-leadsmanagerment-list',
    templateUrl: './leadsmanagerment-list.component.html',
    styleUrls: ['./leadsmanagerment-list.component.css'],
    providers: [LeadsmanagermentService, DatePipe]
})

export class LeadsmanagermentListComponent implements OnInit {
    /** 
     * PROPERTY
     */
    @ViewChild('modalDialog') modalDialog: CustomModalComponent;
    list: LeadsManagermentModel[];
    filterModel = new LeadsFilterModel();
    dealerList = [];
    leadStatusList: [];
    makeList: [];
    modelList: any;
    cityList: [];
    secondHandList: [];
    userList: [];
    campaignList: [];
    sourceLeadsList: [];
    boxLeadsList: [];
    bankList: [];
    paymentStatusList: [];
    leadsIsActiveList: [];
    isErrFromDate = false;
    isErrToDate = false;
    IsSelectAll = false;
    TotalRecord = 0;
    dealderId = 0;
    saleSelected: number;
    leadIdSelected: number;
    assignLeadToDealder: AssigLeadToDealerModel
    dealerIndex: DealerIndexModel
    dealerIndexSearchForm: DealerIndexModel
    listDealerFromOto: DealerFromOtoModel[];
    listDealerSearchForm: DealerFromOtoModel[];
    isPermission: boolean;
    isAdmin: boolean;
    paymentStatusListForDealer = [];
    noteModel: NoteModel = new NoteModel();
    //Khai báo đo scroll top để load thêm item dealer
    X = 0;
    S = 0;
    XSearchForm = 0;
    SSearchForm = 0;

    listLeadsAction: KeyValueObject[];
    listLeadActionForLeads = [];
    currUser: any;
    permission: any;
    isSubmitBlock: boolean = false;
    @ViewChild('assignDealer') public assignDealer: ModalDirective;
    @ViewChild('assignSale') public assignSale: ModalDirective;
    @ViewChild('searchDealerModal', { static: false }) public searchDealerModal: ElementRef;
    @ViewChild('searchDealerModalFormSearch', { static: false }) public searchDealerModalFormSearch: ElementRef;
    @ViewChild("modalBlockLead") public modalBlockLead: ModalDirective
    /**
     * Construct
     * 
     * @param leadsmanagermentService ts
     * @param toastr ts
     * @param router ts
     */
    constructor(private leadsmanagermentService: LeadsmanagermentService,
        private toastr: ToastrService,
        private router: Router,
        private commonService: CommonService,
        private datePipe: DatePipe,
        public dialog: MatDialog
    ) {
        // this.filterModel = new LeadsFilterModel();
        // this.dealerList = [];
        // this.makeList = [];
        // this.modelList = [];
        // this.cityList = [];
        // this.secondHandList = [];
        this.assignLeadToDealder = new AssigLeadToDealerModel();
        this.dealerIndex = new DealerIndexModel();
        this.dealerIndexSearchForm = new DealerIndexModel();
        this.listDealerFromOto = [];
        this.listDealerSearchForm = [];
        this.listLeadsAction = [];
        // this.isPermission = PermissionHelper.IsAdmin;
        // this.isAdmin = PermissionHelper.hasRole(SystemRole.Admin);
        this.currUser = JwtHelper.decodeToken(localStorage.getItem(Constants.TokenKey));
        this.permission = PermissionHelper;
    }

    ngOnInit(): void {
        this.getList(1);
        this.getFormFilter();
    }
    /**
     * GET LIST ALL
     * 
     * @param pageIndex number
     */
    getList(pageIndex: number): void {
        // if(this.filterModel.KeyWord == null) this.filterModel.KeyW        this.filterModel.pageIndex = pageIndex;
        this.filterModel.pageIndex = pageIndex;

        if (this.filterModel.CreatedDate && this.filterModel.CreatedDate.length >= 2) {
            this.filterModel.CreatedDateArr = [
                this.datePipe.transform(this.filterModel.CreatedDate[0], 'yyyy-MM-ddTHH:mm'),
                this.datePipe.transform(this.filterModel.CreatedDate[1], 'yyyy-MM-ddTHH:mm')
            ];
            // this.filterModel.CreatedDateArr[1] = this.datePipe.transform(this.filterModel.CreatedDate[1], 'yyyy-MM-ddTHH:mm')  ;
        }
        if (this.filterModel.StartCareDate && this.filterModel.StartCareDate.length >= 2) {
            this.filterModel.StartCareDateArr = [
                this.datePipe.transform(this.filterModel.StartCareDate[0], 'yyyy-MM-ddTHH:mm'),
                this.datePipe.transform(this.filterModel.StartCareDate[1], 'yyyy-MM-ddTHH:mm')
            ];
            // this.filterModel.StartCareDateArr[0] = this.datePipe.transform(this.filterModel.StartCareDate[0], 'yyyy-MM-ddTHH:mm')  ;
            // this.filterModel.StartCareDateArr[1] = this.datePipe.transform(this.filterModel.StartCareDate[1], 'yyyy-MM-ddTHH:mm')  ;
        }
        this.leadsmanagermentService.search(this.filterModel).subscribe(res => {
            if (res != null && res.code == 1) {
                this.list = res.data.list;
                // this.TotalRecord = res.data.Page.TotalRecord;
                this.filterModel.pageIndex = res.data.page.pageIndex;
                this.filterModel.pageSize = res.data.page.pageSize;
                this.filterModel.totalRecord = res.data.page.totalRecord;
            }
            else {
                this.toastr.error("Có lỗi khi thực thi");
            }
        });
    }

    getListNotFilter(pageIndex: number) {
        this.getList(pageIndex);
    }

    /**
     * GET LIST FORM FILTER
     * 
     * @param null 
     */
    getFormFilter() {
        this.leadsmanagermentService.getFormFilter().subscribe((res) => {
            if (res != null && res.code == 1) {
                this.dealerList = res.data.dealerList;
                this.makeList = res.data.makeList;
                // this.modelList = res.data.modelList;
                this.cityList = res.data.cityList;
                this.secondHandList = res.data.secondHandList;
                this.userList = res.data.userList;
                this.leadStatusList = res.data.leadStatusList;
                this.campaignList = res.data.campaignList;
                this.sourceLeadsList = res.data.sourceLeadsList;
                this.boxLeadsList = res.data.boxLeadsList;
                this.bankList = res.data.bankList;
                this.paymentStatusList = res.data.paymentStatusList;
                this.leadsIsActiveList = res.data.leadsIsActiveList;
            }
        });
    }

    /**
     * GET DETAIL
     * 
     * @param id Id leads
     */
    getById(id: number) {

    }

    /**
     * COMMON FUNCTION
     * 
     * @param event this
     */
    pageChange(event) {
        this.filterModel.pageIndex = event;
        this.getList(this.filterModel.pageIndex);
    }

    onChangeCreateToDate(event) {
        if (event.target.value && event.target.value != '' && !HelperFormat.checkformatdatehmm(event.target.value)) {
            this.isErrToDate = true;
        } else {
            this.isErrToDate = false;
        }
    }

    onChangeCreateFromDate(event) {
        if (event.target.value && event.target.value != '' && !HelperFormat.checkformatdatehmm(event.target.value)) {
            this.isErrFromDate = true;
        } else {
            this.isErrFromDate = false;
        }
    }

    onChangeMake(event) {
        if (!event) {
            this.filterModel.ModelId = 0;
            this.modelList = [];
        }
        if (event.makeID && event.makeID != '') {
            this.commonService.handlerMakeChange(event.makeID).subscribe((res) => {
                if (res != null && res.code == 1) {
                    this.filterModel.ModelId = 0;
                    this.modelList = res.data.modelList;
                }
            });
        }
        else {
            this.modelList = [];
        }
    }

    selectAll(event) {

    }

    changeIsActiveLead(leadId: number, isActive: boolean): void {
        this.leadsmanagermentService.changeisactivelead(leadId, isActive).subscribe((res) => {
            if (res.code === SystemCode.Success) {
                this.toastr.success(res.message);
                let lead = this.list.find(x => {
                    x = UtilityHelper.toPascal(x) as LeadsManagermentModel;
                    return x.Id === leadId;
                }) as any;
                lead.isActive = isActive;
            } else {
                this.toastr.error(res.message);
            }
        });
    }

    receivelead(leadId: number): void {
        this.leadsmanagermentService.receivelead(leadId,).subscribe((res) => {
            if (res.code === SystemCode.Success) {
                const currUser = JwtHelper.decodeToken(localStorage.getItem(Constants.TokenKey));
                let lead = this.list.find((t: any) => t.id == leadId) as any;
                lead.userName = currUser.unique_name;
                lead.startCareDateName = new Date().toLocaleDateString('en-GB');
                this.toastr.success('Nhận lead thành công!!');
            } else {
                this.toastr.error(res.message);
            }
        });
    }
    showAssignToDealer() {
        this.leadsmanagermentService.getLeadActionByLeadsId(this.assignLeadToDealder.leadsId).subscribe((res: ResponseData) => {
            this.listLeadsAction = [];
            this.listLeadActionForLeads = res.data as LeadAction[];
            this.listLeadActionForLeads.forEach(element => {
                let obj = new KeyValueObject();
                obj.Value = 'Hãng: ' + element.makeName + ', Dòng: ' + element.modelName + ', Tỉnh/TP: ' + element.cityName;
                obj.Key = element.id;
                this.listLeadsAction = [...this.listLeadsAction, obj];
            });
            // this.assignLeadToDealder.dealerId = 0;
            // this.assignLeadToDealder.dealerName = '';
            // this.assignLeadToDealder.leadsActionId = null;
            this.assignLeadToDealder.leadsActionId = null;
            this.assignLeadToDealder.leadActionPaymentStatus = null;
            this.assignLeadToDealder.dealerNote = '';
            this.assignDealer.show();
        });
    }
    showAssignToSale(leadId: number) {
        this.leadIdSelected = leadId;
        this.assignSale.show();
    }
    searchDearlerFocusIn() {
        this.searchDealerModal.nativeElement.classList.add('show');
    }
    searchDealerFocusOut() {
        $(document).mouseup(function (e) {
            var container = $("#box-list-dealer");
            var input = $("#dealer-filter-input");
            // if the target of the click isn't the container nor a descendant of the container
            if (!container.is(e.target) && container.has(e.target).length === 0
                && !input.is(e.target) && input.has(e.target).length === 0) {
                container.removeClass('show');
            }
        });
        // this.searchDealerModal.nativeElement.classList.remove('show');
    }
    deleteUserNameFilter() {
        this.dealerIndex.userNameFilter = '';
        this.assignLeadToDealder.dealerId = 0;
        this.listDealerFromOto = [];
    }
    setDealerId(userName: string, userId: number) {
        if (this.assignLeadToDealder.lstDealerId.length < 3) {
            this.assignLeadToDealder.dealerName = userName;
            this.assignLeadToDealder.lstDealerName.push(userName);
            this.assignLeadToDealder.dealerId = userId;
            this.assignLeadToDealder.lstDealerId.push(userId);
            this.dealerIndex.userNameFilter = userName;
            const dealer = this.listDealerFromOto.filter(x => x.userId == userId)[0];
            const index = this.listDealerFromOto.indexOf(dealer);
            if (index > -1) {
                this.listDealerFromOto.splice(index, 1);
            }
            if (this.assignLeadToDealder.lstDealerId.length == 3) {
                this.searchDealerModal.nativeElement.classList.remove('show');
            }
        }else{
            this.toastr.warning("Chỉ được chọn tối đa 3 lead");
        }
    }
    deleteUserDearlerChosen() {
        this.assignLeadToDealder.lstDealerName = [];
        this.assignLeadToDealder.lstDealerId = [];
    }
    searchDealer() {
        this.S = 0;
        this.X = 0;
        this.dealerIndex.pageIndex = 1;
        this.dealerIndex.listDealer = [];
        if ((this.dealerIndex.userNameFilter == "") || (this.dealerIndex.userNameFilter == null)
            || this.dealerIndex.userNameFilter.length < 3) {
            return;
        }
        this.leadsmanagermentService.searchDealerFromOto(this.dealerIndex).subscribe((res: ResponseData) => {
            this.dealerIndex = res.data as DealerIndexModel;
            this.listDealerFromOto = this.dealerIndex.listDealer;
            $("#box-list-dealer").scrollTop(0);
        });
    }
    loadmoreDealer() {
        //xn = (Sn-Xn-1)/3 + Xn-1
        //S là chiều cao scrollHeight; 
        //X chiều cao của scrollTop khi scroll đến sẽ load tiếp dữ liệu
        //currentScrollTOp chiều cao hiện tại của scrollTop
        //currentScrollTOp >= Xn thì sẽ load dữ liệu

        this.S = $("#box-list-dealer").get(0).scrollHeight;//(S(n))
        var currentScrollTOp = $("#box-list-dealer").scrollTop();
        const isLoadMoreDealer = currentScrollTOp >= ((this.S - this.X) / 3 + this.X);
        if (isLoadMoreDealer) {
            this.X = $("#box-list-dealer").scrollTop();//X(n-1)
            this.dealerIndex.listDealer = [];
            if (this.dealerIndex.totalPage >= this.dealerIndex.pageIndex) {
                this.dealerIndex.pageIndex++;
                this.leadsmanagermentService.searchDealerFromOto(this.dealerIndex).subscribe((res: ResponseData) => {
                    var lst = res.data.listDealer;
                    lst.forEach(element => {
                        this.listDealerFromOto.push(element);
                    });
                });
            }
        }
    }
    saveAssignToDealer() {
        if (this.assignLeadToDealder.lstDealerId.length == 0) {
            this.toastr.warning("Bạn chưa chọn Dealer");
            return; 
        }
        if (this.assignLeadToDealder.leadsActionId == 0 || this.assignLeadToDealder.leadsActionId == undefined) {
            this.toastr.warning("Bạn chưa chọn box hãng dòng");
            return;
        }
        if (this.assignLeadToDealder.leadActionPaymentStatus == 0 || this.assignLeadToDealder.leadActionPaymentStatus == undefined) {
            this.toastr.warning("Bạn chưa chọn trạng thái thanh toán");
            return;
        }
        this.leadsmanagermentService.assigLeadsToDealer(this.assignLeadToDealder).subscribe((res: ResponseData) => {
            if (res.code == ResponseCode.Success) {
                this.toastr.success(res.message);
                this.assignDealer.hide();
            } else {
                this.toastr.error(res.message);
            }
        });
    }

    saveAssignToSale() {
        if (this.saleSelected == undefined || this.saleSelected == 0) {
            this.toastr.warning("Bạn chưa chọn Saler");
            return
        }
        this.leadsmanagermentService.assignleadtosale(this.leadIdSelected, this.saleSelected).subscribe((res) => {
            if (res.code == SystemCode.Success) {
                let userName = (this.userList.find((u: any) => u.userId == this.saleSelected) as any).userName;
                let lead = this.list.find((t: any) => t.id == this.leadIdSelected) as any;
                lead.userName = userName;
                lead.startCareDateName = new Date().toLocaleDateString('en-GB');
                this.assignSale.hide();
                this.toastr.success(res.message);
            } else {
                this.toastr.error(res.message);
            }
        })
    }
    dealerFocusInSearchForm() {
        this.searchDealerModalFormSearch.nativeElement.classList.add('show');
    }
    dealerFocusOutSearchForm() {
        $(document).mouseup(function (e) {
            var container = $("#box-list-dealer-search-form");
            var input = $("#dealer-filter-input-search-form");
            // if the target of the click isn't the container nor a descendant of the container
            if (!container.is(e.target) && container.has(e.target).length === 0
                && !input.is(e.target) && input.has(e.target).length === 0) {
                container.removeClass('show');
            }
        });
        // this.searchDealerModal.nativeElement.classList.remove('show');
    }
    deleteDealerNameSearchForm() {
        this.filterModel.DealerName = '';
        this.listDealerSearchForm = [];
    }
    setDealerIdSearchForm(userName: string, userId: number) {
        this.filterModel.DealerName = userName;
        this.searchDealerModalFormSearch.nativeElement.classList.remove('show');
    }
    dealerSearchForm() {
        this.SSearchForm = 0;
        this.XSearchForm = 0;
        this.dealerIndexSearchForm.pageIndex = 1;
        this.dealerIndexSearchForm.listDealer = [];
        this.dealerIndexSearchForm.userNameFilter = this.filterModel.DealerName;
        if ((this.dealerIndexSearchForm.userNameFilter == "") || (this.dealerIndexSearchForm.userNameFilter == null)
            || this.dealerIndexSearchForm.userNameFilter.length < 3) {
            return;
        }
        this.leadsmanagermentService.searchDealerFromOto(this.dealerIndexSearchForm).subscribe((res: ResponseData) => {
            this.dealerIndexSearchForm = res.data as DealerIndexModel;
            this.listDealerSearchForm = this.dealerIndexSearchForm.listDealer;
            $("#box-list-dealer-search-form").scrollTop(0);
        });
    }
    loadmoreDealerSearForm() {
        //xn = (Sn-Xn-1)/3 + Xn-1
        //S là chiều cao scrollHeight; 
        //X chiều cao của scrollTop khi scroll đến sẽ load tiếp dữ liệu
        //currentScrollTOp chiều cao hiện tại của scrollTop
        //currentScrollTOp >= Xn thì sẽ load dữ liệu

        this.SSearchForm = $("#box-list-dealer-search-form").get(0).scrollHeight;//(S(n))
        var currentScrollTOp = $("#box-list-dealer-search-form").scrollTop();
        const isLoadMoreDealer = currentScrollTOp >= ((this.SSearchForm - this.XSearchForm) / 2 + this.XSearchForm);
        if (isLoadMoreDealer) {
            this.X = $("#box-list-dealer-search-form").scrollTop();//X(n-1)
            this.dealerIndexSearchForm.listDealer = [];
            if (this.dealerIndexSearchForm.totalPage >= this.dealerIndexSearchForm.pageIndex) {
                this.dealerIndexSearchForm.pageIndex++;
                this.leadsmanagermentService.searchDealerFromOto(this.dealerIndexSearchForm).subscribe((res: ResponseData) => {
                    var lst = res.data.listDealer;
                    lst.forEach(element => {
                        this.listDealerSearchForm.push(element);
                    });
                });
            }
        }
    }
    setPaymentSatatus() {
        const leadAction = this.listLeadActionForLeads.filter(x => x.id == this.assignLeadToDealder.leadsActionId)[0];
        if (leadAction != null && leadAction.id > 0) {
            this.paymentStatusListForDealer = [];
            const lstKeyValue = this.paymentStatusList as KeyValueObject[];
            if (leadAction.paymentStatus > 0) {
                const lstStatusCheck = lstKeyValue.filter(x => x.Key == leadAction.paymentStatus)[0];
                if (lstStatusCheck != null && lstStatusCheck != undefined) {
                    this.paymentStatusListForDealer = [...this.paymentStatusListForDealer, lstStatusCheck];
                }
            } else {
                this.paymentStatusListForDealer = lstKeyValue as KeyValueObject[];
            }
            this.assignLeadToDealder.leadActionPaymentStatus = this.paymentStatusListForDealer[0].Key;
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
        this.leadsmanagermentService.blockLead(this.noteModel).subscribe((res: any) => {
            if (res.code == SystemCode.Success) {
                this.modalBlockLead.hide();
                this.toastr.success(res.message);
                let lead = this.list.find(x => {
                    x = UtilityHelper.toPascal(x) as LeadsManagermentModel;
                    return x.Id === this.noteModel.relationId;
                }) as any;
                lead.isActive = false;
            } else {
                this.toastr.error(res.message);
            }
        });
    }
   
}
