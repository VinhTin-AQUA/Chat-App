import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, Query } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Apollo, ApolloModule } from 'apollo-angular';
import { CREATE_USER } from '../graphql/mutations/userMutation';
import { catchError, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { RegisterRequest } from '../shared/models/registerRequest';

@Component({
	selector: 'app-register',
	standalone: true,
	imports: [CommonModule, ReactiveFormsModule, HttpClientModule, ApolloModule],
	templateUrl: './register.component.html',
	styleUrl: './register.component.scss',
})
export class RegisterComponent {
	avatarFileName: string = 'No avatar';
	backgroundAvatar: string = "url('/assets/no-avatar.png')";
	avatarAdd: any;
	registerForm!: FormGroup;
	isSubmit: boolean = false;
	data: any;
	errorMessages: string[] = [];

	constructor(
		private formBuilder: FormBuilder,
		private http: HttpClient,
		private router: Router,
		private auth: AuthService
	) {}

	ngOnInit() {
		this.registerForm = this.formBuilder.group({
			fullName: ['', [Validators.required]],
			email: ['', [Validators.email, Validators.required]],
			password: ['', [Validators.required, Validators.minLength(8)]],
			reEnterPassword: ['', [Validators.required]],
		});

		const imageUrl = '/assets/no-avatar.png';
		// nếu người dùng không chọn avatar thì lấy avatar mặc định
		this.http.get(imageUrl, { responseType: 'blob' }).subscribe(data => {
			const file = new File(['blob'], 'no-avatar.png', { type: 'image/png' });
			this.avatarAdd = file;
		});
	}

	onSelectedAvatar(event: any) {
		const file = event.target.files[0];
		this.avatarAdd = file;
		if (file) {
			const reader = new FileReader();
			reader.onload = (e: any) => {
				this.backgroundAvatar = `url(${e.target.result})`;
			};
			reader.readAsDataURL(file);
		}
	}

	onRegister() {
		this.isSubmit = true;
		if (this.registerForm.invalid === true) {
			return;
		}

		if (this.onChangeReenterPassword() === false) {
			return;
		}

		const model: RegisterRequest = {
			email: this.registerForm.controls['email'].value,
			fullName: this.registerForm.controls['fullName'].value,
			password: this.registerForm.controls['password'].value,
			reEnterPassword: this.registerForm.controls['reEnterPassword'].value,
			avatarUrl: '',
		};

		this.auth.register(model).subscribe({
			next: (result: any) => {
				if (result.data.createUser.success === true) {
					this.router.navigateByUrl('/login');
				} else {
					this.errorMessages = result.data.createUser.errorMessages;
				}
			},
			error: err => {
				console.log(err.message);
			},
		});
	}

	private onChangeReenterPassword(): boolean {
		if (
			this.registerForm.controls['password'].value !==
			this.registerForm.controls['reEnterPassword'].value
		) {
			this.registerForm.controls['reEnterPassword'].markAsDirty({
				onlySelf: true,
			});
			return false;
		}
		this.registerForm.controls['reEnterPassword'].markAsPristine({
			onlySelf: true,
		});
		return true;
	}
}
