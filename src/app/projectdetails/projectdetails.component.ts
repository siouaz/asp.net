import { Component, Injectable, OnInit, viewChild } from '@angular/core';
import { NavbarComponent } from '../navbar/navbar.component';
import{MatTableDataSource} from '@angular/material/table';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { ProjectService } from '../services/project.service';
import { ActivatedRoute,Router } from '@angular/router';
import{MatCardModule} from '@angular/material/card'
import { MatSort, MatSortModule } from '@angular/material/sort';
import { FormsModule } from '@angular/forms';
import { UpdateProjectComponent } from '../update-project/update-project.component';



export interface Project{
  id: number;
  name:string;
  description:string;
  developer: string;
  status: string;
  lastCheck: string;
  health: string;
  errorDetails:string;
  responseTime?: number;
  environment?:string;//l'environnement mta3 dep
}

@Component({
  selector: 'app-projectdetails',
  standalone: true,
  imports: [NavbarComponent,MatTableModule,MatIconModule,CommonModule,MatCardModule,MatSort,
    MatSortModule,FormsModule,UpdateProjectComponent
  ],
  templateUrl: './projectdetails.component.html',
  styleUrls: ['./projectdetails.component.scss']
})


export class ProjectdetailsComponent implements OnInit{
 

  project?: Project |null=null;
  filter:string='';
  dataSource:
   MatTableDataSource<Project>=new
   MatTableDataSource();

   aplyFilter(event:Event){
    const inputElement = event.target as HTMLInputElement;
    const filterValue = inputElement.value;
    this.filter= filterValue.trim().toLowerCase();
  }
  matchesFilter(project: Project):
  boolean{
    const searchValue = this.filter;
    return Object.values(project).some(value=> value.toString().toLowerCase().includes(searchValue));
  }
   
  constructor(
    private route: ActivatedRoute,
    private projectservice:
    ProjectService,
    private router: Router,
    
  
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => 
      {
      const projectId =
       params.get('id');
      if(projectId !== null){
        const project=
        this.projectservice.getProjectById(+projectId);
         if(project) {
          this.project = project;
          this.dataSource.data = [this.project];

          
           
        }else{
          console.error('project not found');

        }
         
        }
         

      }

    )};
    updateProject(projectId: number): void {
      
      this.router.navigate(['/updateProject', projectId]);
    }
  
    deleteProject(project: Project): void {
      
      this.projectservice.deleteProject(project.id);
      this.router.navigate(['/projects']);
    }
    restartProject(projectId: number): void {
      //  redémarrer le projet
    }
  
    viewLogs(projectId: number): void {
      //  afficher les logs
    }
  }
  
  
  
  

      
    
  


  
  
  

  


