import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {  RouterModule } from '@angular/router';


@Component({
  selector: 'app-project-list',
  standalone: true,
  imports: [RouterModule,CommonModule],
  templateUrl: './project-list.component.html',
  styleUrl: './project-list.component.scss',
  
})
export class ProjectListComponent  {
  trackById(index: number, project:any):number{
    return project.id;
  }
  
  projects: any[]=[
    {name: "project 1",id:1},
    {name: "project 2",id:2},
    {name: "project 3",id:3},
    {name: "project 4",id:4},
    {name: "project 5",id:5},
    {name: "project 6",id:6},
    
    
    
];
currentPage: number = 1;
  itemsPerPage: number = 10;

  get paginatedProjects() {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    return this.projects.slice(startIndex, startIndex + this.itemsPerPage);
  }

  nextPage() {
    this.currentPage++;
  }

  previousPage() {
    if (this.currentPage > 1) this.currentPage--;




}
}
