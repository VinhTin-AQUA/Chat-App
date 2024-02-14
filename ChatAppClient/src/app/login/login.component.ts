import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { Apollo, ApolloModule } from 'apollo-angular';
import { LOGIN } from '../graphql/queries/userQuery';
import { AuthService } from '../services/auth.service';
import { LoginRequest } from '../shared/models/loginRequest';

@Component({
	selector: 'app-login',
	standalone: true,
	imports: [RouterLink, ReactiveFormsModule, ApolloModule],
	templateUrl: './login.component.html',
	styleUrl: './login.component.scss',
})
export class LoginComponent {
	loginGormGroup!: FormGroup;
	submitted: boolean = false;
	errorMessages: string[] = [];

	constructor(
		private formBuilder: FormBuilder,
		private auth: AuthService,
		private router: Router
	) {}

	ngOnInit() {
		this.loginGormGroup = this.formBuilder.group({
			email: ['', [Validators.required, Validators.email]],
			password: ['', [Validators.required]],
		});
	}

	onLogin() {
		this.submitted = true;

		if (this.loginGormGroup.invalid === true) {
			return;
		}
		const model: LoginRequest = {
			email: this.loginGormGroup.controls['email'].value,
			password: this.loginGormGroup.controls['password'].value,
		};
		this.auth.login(model).subscribe((result: any) => {
			console.log(result.data.login);
			this.errorMessages = result.data.login.errorMessages;

			if (result.data.login.success === true) {
				this.auth.saveToken(result.data.login.data.token);
				this.router.navigateByUrl('/chat')
			}
		});
	}
}
