import { Routes } from '@angular/router';
import { SalesListComponent } from './components/sales-list/sales-list.component';
import { SaleFormComponent } from './components/sale-form/sale-form.component';
import { SaleDetailComponent } from './components/sale-detail/sale-detail.component';
import { SaleCancelComponent } from './components/sale-cancel/sale-cancel.component';
import { CancelItemComponent } from './components/cancel-item/cancel-item.component';

export const routes: Routes = [
    { path: '', component: SalesListComponent},
    { path: 'sales/new', component: SaleFormComponent },
    { path: 'sales/edit/:id', component: SaleFormComponent },
    { path: 'sales/:id', component: SaleDetailComponent },
    { path: 'sales/cancel/:id', component: SaleCancelComponent },
    { path: 'sales/cancel-item/:saleId/:itemId', component: CancelItemComponent },
  ];
