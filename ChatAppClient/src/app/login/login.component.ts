import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { Apollo, ApolloModule } from 'apollo-angular';
import { AuthService } from '../services/auth.service';
import { LoginRequest } from '../shared/models/loginRequest';
import { UserService } from '../services/user.service';
import { jwtDecode } from 'jwt-decode';
import { GET_USER_BY_EMAIL } from '../graphql/queries/userQuery';

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
		private authService: AuthService,
		private router: Router,
		private userService: UserService,
		private apollo: Apollo,
	) {

	}

	ngOnInit() {
		this.loginGormGroup = this.formBuilder.group({
			email: ['', [Validators.required, Validators.email]],
			password: ['', [Validators.required]],
		});

		const token = this.authService.getToken();
		if (token === null || this.userService.isLoggedIn() === false) {
			this.userService.logout();
			this.router.navigateByUrl('/login');
			return;
		}

		var decoded: any = jwtDecode(token);
		this.apollo
			.query({
				query: GET_USER_BY_EMAIL,
				variables: {
					email: decoded.email,
				},
			})
			.subscribe((result: any) => {
				if (result.data.userByEmail === null) {
					console.log(result.data.userByEmail === null);
					this.userService.logout();
					this.router.navigateByUrl('/login');
					return;
				}
				
				this.userService.setUser(result.data.userByEmail);
				this.router.navigateByUrl('/chat');
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
			this.errorMessages = result.data.login.errorMessages;
			
			if (result.data.login.success === true) {
				
				this.userService.setUser(result.data.login.data);
				this.authService.saveToken(result.data.login.data.token);
				this.router.navigateByUrl('/chat');
			}
		});
	}
}
