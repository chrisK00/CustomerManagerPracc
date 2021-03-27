import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { CustomerListComponent } from './components/customers/customer-list/customer-list.component';
import { TestErrorsComponent } from './components/test-errors/test-errors.component';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: '', runGuardsAndResolvers: 'always', canActivate: [AuthGuard], children: [
      { path: 'customers', component: CustomerListComponent },
      { path: 'errors', component: TestErrorsComponent }
    ]
  },

  { path: '**', component: HomeComponent, pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
