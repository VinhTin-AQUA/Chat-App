import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
	selector: 'app-chat-home',
	standalone: true,
	imports: [CommonModule, FormsModule],
	templateUrl: './chat-home.component.html',
	styleUrl: './chat-home.component.scss',
})
export class ChatHomeComponent {
	isShowCreateRoom: boolean = false;
  roomCode: string = '';

	showCreateRoom() {
		this.isShowCreateRoom = !this.isShowCreateRoom;
	}


}
