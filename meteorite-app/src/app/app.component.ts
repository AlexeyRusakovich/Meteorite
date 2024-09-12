import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MeteoriteOverviewComponent } from './features/overview/components/meteorite-overview/meteorite-overview.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, MeteoriteOverviewComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'meteorite-app';
}
