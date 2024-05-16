import { Routes } from '@angular/router';
import { LoginComponent } from './core/login/login.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ProjectdetailsComponent } from './projectdetails/projectdetails.component';
import { uptime } from 'os';
import { UpdateProjectComponent } from './update-project/update-project.component';


export const routes: Routes = [
    {
         path: '', redirectTo:'login', pathMatch:'full'
    },
    {
        path:'login',
        component:LoginComponent,
    },
   
    {
        path:'dashboard',
         component: DashboardComponent,data: {title:'Dashboard'}
    },
    
    
    {
        path: 'projectdetails/:id', 
        component:ProjectdetailsComponent,data: {title:'ProjectsDetails'}
    },
    {
        path:'UpdateProject/:id',
        component:UpdateProjectComponent,data: {title:'UpdateProject'}
    }
    
    
    

   
    
    

    
];
