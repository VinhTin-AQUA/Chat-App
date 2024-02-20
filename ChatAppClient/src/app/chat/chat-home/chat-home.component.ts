import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { UserStore } from '../../shared/stores/user.store';
import { GroupStore } from '../../shared/stores/group.store';
import { MessageService } from '../../services/message.service';
import { Message } from '../../shared/models/message';
import { Group } from '../../shared/models/group';
import { ChatService } from '../../services/chat.service';
import { UserService } from '../../services/user.service';

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
	userStore = inject(UserStore);
	group: Group | null = null;
	content: string = '';
	userCode: string = '';

	isShowMessageMenu: boolean = false;
	isShowAddUser: boolean = false;
	isShowOnlineUser: boolean = false;

	constructor(
		private messageService: MessageService,
		public chatService: ChatService,
		private userService: UserService
	) {}

	async ngOnInit() {
		await this.chatService.createChatConnection();
	}

	ngOnDestroy() {
		if (this.group !== null) {
			this.chatService.stopConnection(this.group?.groupName, this.userStore.fullName());
		}
	}

	onClickGroupItem(group: Group) {
		if (this.group?.uniqueCodeGroup === group.uniqueCodeGroup) {
			return;
		}

		this.group = group;
		this.messageService.getMessagesOfGroup(group.uniqueCodeGroup).subscribe((result: any) => {
			this.messages = [...result.data.messagesOfGroup];
		});
		this.chatService.joinGroup(group.groupName, this.userStore.fullName());
	}

	onSendMessage() {
		this.messageService
			.sendMessage(this.group!.uniqueCodeGroup, this.userStore.email(), this.content)
			.subscribe((result: any) => {
				let newMess: Message = {
					avatarSender: result.data.sendMessage.avatarSender,
					content: result.data.sendMessage.content,
					senderId: result.data.sendMessage.senderId,
					senderName: result.data.sendMessage.senderName,
					timeStamp: new Date(),
				};
				console.log(newMess);

				this.messages.push(newMess);
			});
	}

	showMessageMenu() {
		this.isShowMessageMenu = !this.isShowMessageMenu;
	}

	showAddUser(event: Event) {
		// event.stopPropagation();
		this.isShowAddUser = !this.isShowAddUser;
	}

	addUserToGroup() {
		if (this.group !== null) {
			this.userService
				.addUserToGroup(this.userCode, this.group?.uniqueCodeGroup)
				.subscribe((result: any) => {
					// console.log(result);
				});
		}
	}

	showOnlineUsers() {
		this.isShowOnlineUser = !this.isShowOnlineUser;
	}
}
