import { Component,OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Project } from '../projectdetails/projectdetails.component';
import { ProjectService } from '../services/project.service';
import { NavbarComponent } from '../navbar/navbar.component';


@Component({
  selector: 'app-update-project',
  standalone: true,
  imports: [NavbarComponent],
  templateUrl: './update-project.component.html',
  styleUrl: './update-project.component.scss'
})
export class UpdateProjectComponent implements OnInit {
  projectId:number |null= null;
  project :Project|undefined;
  constructor(private route:ActivatedRoute,
    private projectService:
    ProjectService
  ){}
  ngOnInit(): void {
    const projectId = 
    this.route.snapshot.params['id'];
    if(projectId){
      this.project=
      this.projectService.getProjectById(+projectId);
    }
      
      
       
      
  }

}
