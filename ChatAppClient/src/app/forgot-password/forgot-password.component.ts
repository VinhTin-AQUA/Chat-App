import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { HeaderSignedoutComponent } from '../shared/components/header-signedout/header-signedout.component';

@Component({
	selector: 'app-forgot-password',
	standalone: true,
	imports: [HeaderSignedoutComponent, RouterLink],
	templateUrl: './forgot-password.component.html',
	styleUrl: './forgot-password.component.scss',
})
export class ForgotPasswordComponent {
  
}
