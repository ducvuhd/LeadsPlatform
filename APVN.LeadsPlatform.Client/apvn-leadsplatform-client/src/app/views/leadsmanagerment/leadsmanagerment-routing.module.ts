import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LeadsmanagermentListComponent } from './leadsmanagerment-list/leadsmanagerment-list.component';
import { LeadsmanagermentDetailComponent } from './leadsmanagerment-detail/leadsmanagerment-detail.component';
import { LeadsmanagermentAddLeadComponent } from './leadsmanagerment-add-lead/leadsmanagerment-add-lead.component';
import { CampaignComponent } from './campaign/campaign.component';
import { DealergetleadComponent } from './dealergetlead/dealergetlead.component';

const routes: Routes = [
    {
        path: 'list',
        component: LeadsmanagermentListComponent,
        data: {
            title: 'Danh sách Leads'
        }
    },
    {
        path: 'addLead',
        component: LeadsmanagermentAddLeadComponent,
        data: {
            title: 'Thêm Leads'
        }
    },
    {
        path: 'updateLead/:leadId',
        component: LeadsmanagermentAddLeadComponent,
        data: {
            title: 'Update Leads'
        }
    },
    {
        path: 'get-detail/:id',
        component: LeadsmanagermentDetailComponent,
        data: {
            title: 'Chi tiết Leads'
        }
    },
    {
        path: 'campaign',
        component: CampaignComponent,
        data: {
            title: 'Quản lý campaign'
        }
    },
    {
        path: 'dealergetlead',
        component: DealergetleadComponent,
        data: {
            title: 'Quản lý dealer lấy lead'
        }
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class LeadsmanagermentRoutingModule {

}
