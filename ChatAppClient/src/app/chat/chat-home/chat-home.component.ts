import { CommonModule } from '@angular/common';
import { Component, ElementRef, ViewChild, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { UserStore } from '../../shared/stores/user.store';
import { GroupStore } from '../../shared/stores/group.store';
import { MessageService } from '../../services/message.service';
import { Message } from '../../shared/models/message';
import { Group } from '../../shared/models/group';
import { ChatService } from '../../services/chat.service';
import { UserService } from '../../services/user.service';
import { User } from '../../shared/models/user';
import { GroupService } from '../../services/group.service';

@Component({
	selector: 'app-chat-home',
	standalone: true,
	imports: [CommonModule, FormsModule],
	templateUrl: './chat-home.component.html',
	styleUrl: './chat-home.component.scss',
})
export class ChatHomeComponent {
	groupStore = inject(GroupStore);
	@ViewChild('messinput',{ static: false }) messInput!: ElementRef;
	@ViewChild('messageBox') private messageBox!: ElementRef;
	userStore = inject(UserStore);
	group: Group | null = null;
	content: string = '';
	userCode: string = '';
	members: string[] = [];

	isShowMessageMenu: boolean = false;
	isShowAddUser: boolean = false;
	isShowOnlineUser: boolean = false;
	isShowMembers: boolean = false;

	errorMessage: string[]=[]

	constructor(
		private messageService: MessageService,
		public chatService: ChatService,
		private userService: UserService,
		private groupService: GroupService
	) {}

	async ngOnInit() {
		await this.chatService.createChatConnection();
	}

	private messageBoxScrollToBottom() {
		this.messageBox.nativeElement.scrollTop = this.messageBox.nativeElement.scrollHeight;
	}

	ngOnDestroy() {
		if (this.group !== null) {
			this.chatService.stopConnection(this.group?.uniqueCodeGroup, this.userStore.fullName());
		}
	}

	onClickGroupItem(group: Group) {
		if (this.group?.uniqueCodeGroup === group.uniqueCodeGroup) {
			return;
		}

		if(this.group !== null) {
			this.chatService.disconnectGroup(this.group.uniqueCodeGroup, this.userStore.fullName())
		}
		

		this.group = group;
		this.messageService.getMessagesOfGroup(group.uniqueCodeGroup).subscribe((result: any) => {
			this.chatService.messages = [...result.data.messagesOfGroup];
		});
		this.chatService.joinGroup(group.uniqueCodeGroup, this.userStore.fullName());

		if(this.members.length === 0 && this.group !== null) {
			this.groupService.getMembersOfGroup(this.group?.uniqueCodeGroup)
			.subscribe((result: any) => {
				this.members = result.data.group
			})
		}
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

				// thêm tin nhắn vào client của người gửi
				this.chatService.messages.push(newMess);

				// thêm tin nhắn vào client của người nhận
				if (this.group !== null) {
					this.chatService.sendMessageToGroup(this.group?.uniqueCodeGroup, newMess);
				}
				this.content = '';
				this.messInput.nativeElement.focus();
				this.messageBoxScrollToBottom();
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

					if(result.data.addUserToGroup.success === false) {
						this.errorMessage = result.data.addUserToGroup.errorMessages;
						return;
					}
					this.errorMessage = [];
					this.isShowAddUser = false;
					this.members = [...this.members, result.data.addUserToGroup.data.fullName]
					this.userCode = '';
				});
		}
	}

	showOnlineUsers() {
		this.isShowOnlineUser = !this.isShowOnlineUser;
	}

	showMembers() {
		this.isShowMembers = !this.isShowMembers;
	}
}
