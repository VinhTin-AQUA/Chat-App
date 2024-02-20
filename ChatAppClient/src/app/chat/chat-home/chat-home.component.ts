import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { UserStore } from '../../shared/stores/user.store';
import { GroupStore } from '../../shared/stores/group.store';

@Component({
	selector: 'app-chat-home',
	standalone: true,
	imports: [CommonModule, FormsModule],
	templateUrl: './chat-home.component.html',
	styleUrl: './chat-home.component.scss',
})
export class ChatHomeComponent {
	groupStore = inject(GroupStore);

	ngOnInit() {
		
	}

	private loadMessagesOfFirstGroup() {}
}
