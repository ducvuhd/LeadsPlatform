import { Component, OnInit, ViewChild, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CustomModalComponent } from '../../modal/custom-modal.component';
import { GroupRole, ResponseData, SystemCode } from '../../../models/ResponseData';
import { ResponseCode } from 'src/app/utils/enums/responseCode.enum';
import { LeadsmanagermentService } from '../../../services/leadsmanagerment.service';
import { LeadsFilterModel } from 'src/app/models/leads-filter-model';
import { LeadsManagermentModel, ListAction } from 'src/app/models/leadsmanagerment-model';
import { HelperFormat } from 'src/app/utils/helpers/helper';
import { FormControl, FormsModule, NgForm } from '@angular/forms';
import { NoteModel } from 'src/app/models/NoteModel';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { NoteService } from 'src/app/services/note.service';
import { NoteType } from 'src/app/utils/enums/note.enum';
import { KeyValueObject } from 'src/app/utils/KeyValueObject';
import { PermissionHelper } from 'src/app/utils/helpers/permission-helper';
import { SystemRole } from 'src/app/utils/enums/system-role.enum';
import { JwtHelper } from 'src/app/utils/helpers/jwt.helper';
import { Constants } from 'src/app/utils/constants';
import { HistoryService } from 'src/app/services/history.service';
import { HistoryIndexModel } from 'src/app/models/HistoryIndexModel';
import { NoteIndexModel } from 'src/app/models/NoteIndexModel';
import { HistoryType } from 'src/app/utils/enums/history.enum';

@Component({
    selector: 'app-leadsmanagerment-detail',
    templateUrl: './leadsmanagerment-detail.component.html',
    styleUrls: ['./leadsmanagerment-detail.component.css']
})
export class LeadsmanagermentDetailComponent implements OnInit {

    /** 
     * PROPERTY
     */
    @ViewChild('modalDialog') modalDialog: CustomModalComponent;
    list: LeadsManagermentModel[];
    filterModel = new LeadsFilterModel();
    leadsId = null;
    leadsDetail = null;
    leadsModel = null;
    leadsDetailExtendModel = null;
    leadNoteList = [];
    leadHistoryList = [];
    listAction = [];
    isErrFromDate = false;
    isErrToDate = false;
    IsSelectAll = false;
    TotalRecord = 0;
    noteModel: NoteModel = new NoteModel();
    isSubmitNote: boolean = false;
    @ViewChild("modalUpdateNote") public modalUpdateNote: ModalDirective
    isCollapsed1: boolean = false;
    listLeadActionStatus: KeyValueObject[] = [];
    historyIndexLead: HistoryIndexModel = new HistoryIndexModel();
    noteIndexLead: NoteIndexModel = new NoteIndexModel();
    permission: any;
    isSale: boolean = false;
    /**
     * Construct
     * 
     * @param leadsmanagermentService ts
     * @param toastr ts
     * @param router ts
     */
    constructor(private leadsmanagermentService: LeadsmanagermentService,
        private toastr: ToastrService,
        private route: ActivatedRoute,
        private router: Router,
        private noteService: NoteService,
        private historyService: HistoryService) {
        // this.filterModel = new LeadsFilterModel();
        // this.dealerList = [];
        // this.makeList = [];
        // this.modelList = [];
        // this.cityList = [];
        // this.secondHandList = [];
        this.leadNoteList = [];
        this.leadHistoryList = [];
        this.leadsModel = null;
        this.leadsDetailExtendModel = null;
    }

    ngOnInit(): void {
        this.route.params.subscribe(params => {
            this.leadsId = params['id'];
        });
        this.getDetail();
        this.permission = PermissionHelper;
    }

    getDetail() {
        this.leadsmanagermentService.initDetail().subscribe(response => {
            if (response != null && response.code == SystemCode.Success) {
                this.listLeadActionStatus = response.data as KeyValueObject[];
                this.leadsmanagermentService.getDetail(this.leadsId).subscribe(res => {
                    if (res != null && res.Code == 1) {
                        this.leadsModel = res.Data.LeadsModel;
                        this.listAction = res.Data.LeadsModel.ListAction;
                        if (this.listAction.length > 1) {
                            this.isCollapsed1 = true;
                        }
                        const currUser = JwtHelper.decodeToken(localStorage.getItem(Constants.TokenKey));
                        if (PermissionHelper.IsSale() && (+currUser.certserialnumber) == this.leadsModel.AssignToId) {
                            this.isSale = true;
                        }
                        this.noteIndexLead.RelationId = this.leadsModel.Id;
                        this.noteIndexLead.Type = NoteType.Leads;
                        this.getListNote(this.noteIndexLead);
                        this.historyIndexLead.RelationId = this.leadsModel.Id;
                        this.historyIndexLead.Type = HistoryType.Leads;
                        this.getListHistory(this.historyIndexLead);
                    }
                    else {
                        this.toastr.error("Có lỗi khi thực thi");
                    }
                });
            }
        });
    }
    getListNote(index: NoteIndexModel) {
        this.noteService.getlist(index).subscribe((res) => {
            if (res.Code == SystemCode.Success) {
                this.leadNoteList = res.Data.ListNote as NoteModel[];
            }
        });
    }
    pageChangedNoteLead(pageIndex: number) {
        this.noteIndexLead.pageIndex = pageIndex;
        this.getListNote(this.noteIndexLead);
    }
    getListHistory(index: HistoryIndexModel) {
        this.historyService.getlist(index).subscribe((res) => {
            if (res.Code == SystemCode.Success) {
                this.leadHistoryList = res.Data.ListHistory;
            }
        });
    }
    pageChangedHistoryLead(pageIndex: number) {
        this.historyIndexLead.pageIndex = pageIndex;
        this.getListHistory(this.historyIndexLead);
    }
    showFormAddNote() {
        this.noteModel = new NoteModel();
        this.modalUpdateNote.show();
    }
    showFormEditNote(noteId: number) {
        this.noteService.getById(noteId, NoteType.Leads).subscribe((res: ResponseData) => {
            if (res.code == SystemCode.Success) {
                this.noteModel = res.data as NoteModel;
                this.modalUpdateNote.show();
            } else {
                this.toastr.error(res.message);
            }
        });
    }
    updateNote(f: NgForm) {
        this.isSubmitNote = true;
        if (!f.form.valid) {
            return;
        }
        this.noteModel.type = NoteType.Leads;
        this.noteModel.relationId = this.leadsModel.Id;
        this.noteService.update(this.noteModel).subscribe((res: any) => {
            if (res.Code == SystemCode.Success) {
                if (this.noteModel.id == 0) {
                    this.noteModel = res.Data as NoteModel;
                    this.leadNoteList = [this.noteModel, ...this.leadNoteList];
                } else {
                    var rowNoteEdit = this.leadNoteList.filter(x => x.Id == this.noteModel.id)[0];
                    let index = this.leadNoteList.indexOf(rowNoteEdit);
                    if (index > -1) {
                        this.leadNoteList[index] = res.Data as NoteModel;
                    }
                }
                this.modalUpdateNote.hide();
                this.toastr.success("Thao tác thành công");
            } else {
                this.toastr.error(res.Message);
            }
        });
    }
    resetFormNote(f: NgForm) {
        this.isSubmitNote = false;
        f.reset();
    }
    saveLeadActionStatus(status: number, leadActionId: number) {
        const currUser = JwtHelper.decodeToken(localStorage.getItem(Constants.TokenKey));
        if (PermissionHelper.hasRole(SystemRole.Admin, SystemRole.Lead) || +currUser.certserialnumber == this.leadsModel.AssignToId) {
            this.leadsmanagermentService.updateLeadActionStatus(status, leadActionId).subscribe((res: ResponseData) => {
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
}
