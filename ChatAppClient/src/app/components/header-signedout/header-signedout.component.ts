import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-header-signedout',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './header-signedout.component.html',
  styleUrl: './header-signedout.component.scss'
})
export class HeaderSignedoutComponent {

}
