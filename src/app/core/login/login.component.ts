declare var google: any;
import { CommonModule } from "@angular/common";
import { Component, OnInit } from "@angular/core";


@Component({
    selector: 'app-login',
    standalone: true,
    imports:[CommonModule],
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit{
    ngOnInit(): void {
        google.accounts.id.initialize({
        client_id:'853623842014-8bguanugf7812jlfj9a5d2cacfo8t1te.apps.googleusercontent.com',
        callback:(resp: any)=>{

        }
      });
      google.accounts.id.renderButton(document.getElementById("google-btn"),
      {
        theme: 'filled_blue',
        size: 'small',
        shape:'rectangle',
        width: 300,
      })
    }

}