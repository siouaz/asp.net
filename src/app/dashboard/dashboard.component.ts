import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ProjectListComponent } from '../project-list/project-list.component';
import { NavbarComponent } from '../navbar/navbar.component';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [RouterOutlet, RouterLink,CommonModule,
    ProjectListComponent,NavbarComponent, FormsModule
     ],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent {
  showAddProjectForm = false;
  activeProjectsCount!: number;
  inactiveProjectsCount!: number;
  newProject={
    name: '',
    healthCheckUrl:'',
    siteUrl:'',
    checkFrequency: null,
    assignedTo: null
  };
  addProject(){

  }
  getProjectStats(){

  }
  
}


