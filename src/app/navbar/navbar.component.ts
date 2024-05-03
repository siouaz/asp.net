import { Component } from '@angular/core';
import{MatToolbarModule} from '@angular/material/toolbar';
import{MatButtonModule} from '@angular/material/button';
import{MatIconModule} from '@angular/material/icon';
import{MatMenuModule} from '@angular/material/menu';
import { RouterModule,Router, NavigationEnd,ActivatedRoute} from '@angular/router';

import { filter, map } from 'rxjs/operators';
import { title } from 'process';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [MatToolbarModule, MatButtonModule,MatIconModule,MatMenuModule,RouterModule,

  ],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {
  currentPageTitle: string=
  'Dashboard';
  constructor(private router: Router, private activatedRoute:ActivatedRoute){
    this.router.events.pipe(
      filter(event=> event instanceof NavigationEnd),
    
    map(()=>{
      let route = this.activatedRoute;
                while(route.firstChild){
        route= route.firstChild;
      }
        return route;
      
    }),
    filter(route=> route.outlet === 
      'primary'),
            map(route=> route.snapshot.data['title']||'Dashboard')
    ).subscribe((title:string)=> this.currentPageTitle = title);
    
      
    }
  }


