import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import Chart from 'chart.js/auto';


@Component({
  selector: 'app-my-chart',
  standalone: true,
  imports: [],
  template: '<canvas #myChart></canvas>',
  styleUrls: ['./my-chart.component.scss']
})
export class MyChartComponent 
 {
 
}