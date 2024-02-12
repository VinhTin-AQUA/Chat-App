import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component } from '@angular/core';
import {
	FormBuilder,
	FormControl,
	FormGroup,
	ReactiveFormsModule,
	Validators,
} from '@angular/forms';
import { AuthService } from '../shared/services/auth.service';


@Component({
	selector: 'app-register',
	standalone: true,
	imports: [CommonModule, ReactiveFormsModule, HttpClientModule],
	templateUrl: './register.component.html',
	styleUrl: './register.component.scss',
})
export class RegisterComponent {
	avatarFileName: string = 'No avatar';
	backgroundAvatar: string = "url('/assets/no-avatar.png')";
	avatarAdd: any;
	registerForm!: FormGroup;
	isSubmit: boolean = false;

	constructor(private formBuilder: FormBuilder, 
    private http: HttpClient,
    private authService: AuthService) {}

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

		if (this.registerForm.invalid) {
			return;
		}

		let form = new FormData();
		form.append('file', this.avatarAdd);
		form.append('fullName', this.registerForm.controls['fullName'].value);
		form.append('email', this.registerForm.controls['email'].value);
		form.append('password', this.registerForm.controls['password'].value);
		form.append('reEnterPassword', this.registerForm.controls['reEnterPassword'].value);

    this.authService.register(form).subscribe({
      next: (res) => {
        console.log(res);
        
      },
      error: (err) => {
        console.log(err);
        
      }
    })
	}

	onChangeReenterPassword() {
		if (
			this.registerForm.controls['password'].value !==
			this.registerForm.controls['reEnterPassword'].value
		) {
			this.registerForm.controls['reEnterPassword'].markAsDirty({
				onlySelf: true,
			});
			return;
		}

		this.registerForm.controls['reEnterPassword'].markAsPristine({
			onlySelf: true,
		});
	}
}
