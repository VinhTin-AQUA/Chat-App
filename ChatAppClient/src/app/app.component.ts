import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterOutlet } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { AuthService } from './services/auth.service';
import { jwtDecode } from 'jwt-decode';
import { LoadingStore } from './shared/stores/loading.store';
import { patchState } from '@ngrx/signals';
import { LoggingInLoadingComponent } from './shared/components/logging-in-loading/logging-in-loading.component';
import { UserService } from './services/user.service';

@Component({
	selector: 'app-root',
	standalone: true,
	imports: [CommonModule, RouterOutlet, LoginComponent, LoggingInLoadingComponent],
	templateUrl: './app.component.html',
	styleUrl: './app.component.scss',
})
export class AppComponent {
	loadingStore = inject(LoadingStore);

	ngOnInit() {
		
	}
}
