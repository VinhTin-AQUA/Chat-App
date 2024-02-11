import { Component } from '@angular/core';
import { HeaderSignedoutComponent } from '../components/header-signedout/header-signedout.component';
import { RouterLink } from '@angular/router';

@Component({
	selector: 'app-forgot-password',
	standalone: true,
	imports: [HeaderSignedoutComponent, RouterLink],
	templateUrl: './forgot-password.component.html',
	styleUrl: './forgot-password.component.scss',
})
export class ForgotPasswordComponent {
  
}
