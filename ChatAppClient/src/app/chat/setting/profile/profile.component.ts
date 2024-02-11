import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, HttpClientModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent {
  avatarFileName: string = 'No avatar';
  backgroundAvatar: string = "url('/assets/no-avatar.png')";
  avatarAdd: any;
  registerForm!: FormGroup;
  isSubmit: boolean = false;

  constructor(private formBuilder: FormBuilder, private http: HttpClient) {}

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      fullName: ['', [Validators.required]],
      email: ['', [Validators.email, Validators.required]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      reEnterPassword: ['', [Validators.required]],
    });

    const imageUrl = '/assets/no-avatar.png';
    // nếu người dùng không chọn avatar thì lấy avatar mặc định
    this.http.get(imageUrl, { responseType: 'blob' }).subscribe((data) => {
      const file = new File(['blob'], 'no-avatar.png', { type: 'image/png' });
      this.avatarAdd = file;
    });
  }

  onSelectedAvatar(event: any) {
    const file = event.target.files[0];
    this.avatarAdd = file;

    console.log(this.avatarAdd);

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

    console.log(this.registerForm.value);
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
