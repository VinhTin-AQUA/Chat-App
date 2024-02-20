import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { UserStore } from '../../shared/stores/user.store';
import { GroupStore } from '../../shared/stores/group.store';
import { MessageService } from '../../services/message.service';
import { Message } from '../../shared/models/message';
import { Group } from '../../shared/models/group';


@Component({
	selector: 'app-chat-home',
	standalone: true,
	imports: [CommonModule, FormsModule],
	templateUrl: './chat-home.component.html',
	styleUrl: './chat-home.component.scss',
})
export class ChatHomeComponent {
	groupStore = inject(GroupStore);
	messages: Message[] = [];
	userStore = inject(UserStore)
	groupName: string  = '';

	constructor(private messageService: MessageService) {
	}

	onClickGroupItem(group: Group) {
		this.groupName = group.groupName
		this.messageService.getMessagesOfGroup(group.uniqueCodeGroup).subscribe((result: any) => {
			this.messages = result.data.messagesOfGroup
		})
	}
}
