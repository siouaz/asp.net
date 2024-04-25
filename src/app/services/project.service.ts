import { Injectable } from '@angular/core';
import { Project } from '../projectdetails/projectdetails.component';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {
  private projects: Project[]  = [
    { id: 1, name: 'Project A', description: 'Description for Project A', developer: 'dev x ', status: '   active ', lastCheck: '20/04/2024',health:'Healthy',errorDetails:'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry' },
    { id: 2, name: 'Project B', description: 'Description for Project B', developer: 'dev y ', status: '   inactive ', lastCheck: '20/04/2024',health:'Unhealthy',errorDetails:'jvubgbeug' },
    { id: 3, name: 'Project C', description: 'Description for Project C', developer: 'dev Z ', status: '   maintenance  ', lastCheck: '20/04/2024',health:'Healthy',errorDetails:'jvubgbeug' },
    { id: 4, name: 'Project D', description: 'Description for Project D', developer: 'dev d ', status: '   active ', lastCheck: '20/04/2024',health:'Healthy',errorDetails:'jvubgbeug' },
    { id: 5, name: 'Project E', description: 'Description for Project E', developer: 'dev e', status: '   inactive ', lastCheck: '20/04/2024',health:'Healthy',errorDetails:'jvubgbeug' },
    { id: 6, name: 'Project F', description: 'Description for Project E', developer: 'dev e', status: '   inactive ', lastCheck: '20/04/2024',health:'Healthy',errorDetails:'jvubgbeug' }

    
  ];
  constructor(){}
  getProjectById(id: number): Project | undefined {
    return this.projects.find(project => project.id === id);
  }

  deleteProject(id: number): void {
    // Logic to delete the project from the projects array
    this.projects = this.projects.filter(project => project.id !== id);
  }
  addProject(project: Project) {
    
  }
}



