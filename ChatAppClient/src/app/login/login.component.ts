import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { Apollo, ApolloModule } from 'apollo-angular';
import { AuthService } from '../services/auth.service';
import { LoginRequest } from '../shared/models/loginRequest';
import { UserService } from '../services/user.service';
import { UserStore } from '../shared/stores/user.store';

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
	private userStore = inject(UserStore);

	constructor(
		private formBuilder: FormBuilder,
		private authService: AuthService,
		private router: Router,
		private userService: UserService
	) {

	}

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

		this.authService.login(model).subscribe((result: any) => {
			console.log(result.data.login);
			this.errorMessages = result.data.login.errorMessages;

			if (result.data.login.success === true) {
				this.userService.setUser(result.data.login);
				this.authService.saveToken(result.data.login.data.token);
				this.router.navigateByUrl('/chat');
			}
		});
	}
}
