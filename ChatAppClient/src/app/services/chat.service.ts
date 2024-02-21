import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { environment } from '../../environments/environment.development';
import { Message } from '../shared/models/message';

@Injectable({
	providedIn: 'root',
})
export class ChatService {
	private chatConnection?: HubConnection;
	onlineUsers: string[] = [];
	messages: Message[] = [];

	constructor() {}

	createChatConnection() {
		return new Promise(async resolve => {
			if (this.chatConnection?.state !== 'Connected') {
				this.chatConnection = new HubConnectionBuilder()
					.withUrl(`${environment.hupUrl}/hubs/chat`)
					.withAutomaticReconnect()
					.build();

				await this.startConnection();
			}

			this.chatConnection.on('OtherUserConnected', async (userName: string) => {
				this.onlineUsers.push(userName);
			});

			this.chatConnection.on('UserConnected', async (onlineUsers: string[]) => {
				this.onlineUsers = onlineUsers;
			});

			this.chatConnection.on('AddedUserToGroup', userNameAdded => {
				console.log(userNameAdded);
			});

			this.chatConnection.on("UserLeft", (userName) => {
				const index = this.onlineUsers.indexOf(userName);
				this.onlineUsers.splice(index, 1);
			})

			this.chatConnection.on("OtherMessageSent", (message: Message) => {
				this.messages.push(message);
			})

			resolve(1);
		});
	}

	private startConnection() {
		return new Promise(resolve => {
			this.chatConnection?.start().catch(error => {
				console.log(error);
			});

			resolve(1);
		});
	}

	private disConnectGroup(uniqueCodeGroup: string, userName: string) {
		return new Promise(resolve => {
			this.chatConnection?.invoke('DisConnectGroup', uniqueCodeGroup);
			resolve(1);
		});
	}



	async stopConnection(uniqueCodeGroup: string, userName: string) {
		// disconnect group
		await this.disConnectGroup(uniqueCodeGroup, userName);
		// dis connect chat
		await this.chatConnection?.stop().catch(err => {
			console.log(err);
		});
	}

	joinGroup(uniqueCodeGroup: string, userName: string) {
		this.chatConnection
			?.invoke('ConnectToGroup', uniqueCodeGroup, userName)
			.then(() => {})
			.catch(err => {
				console.log(err);
			});
	}

	sendMessageToGroup(uniqueCodeGroup: string, message: Message) {
		this.chatConnection
			?.invoke('SendMessageToGroup', uniqueCodeGroup, message)
			.then(() => {

			})
			.catch(err => {
				console.log(err);
			});
	}
}
