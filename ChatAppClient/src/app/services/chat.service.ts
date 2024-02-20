import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { environment } from '../../environments/environment.development';

@Injectable({
	providedIn: 'root',
})
export class ChatService {
	private chatConnection?: HubConnection;
  onlineUsers: string[] = [];

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

			this.chatConnection.on('UserConnected', async (userName: string) => {
        this.onlineUsers.push(userName);
			});

			this.chatConnection.on('AddedUserToGroup', userNameAdded => {
				console.log(userNameAdded);
			});

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

	private disConnectGroup(groupName: string, userName: string) {
		return new Promise(resolve => {
			this.chatConnection?.invoke('DisConnectGroup', groupName);
			resolve(1);
		});
	}

	async stopConnection(groupName: string, userName: string) {
		// disconnect group
    await this.disConnectGroup(groupName,userName)
		// dis connect chat
		await this.chatConnection?.stop().catch(err => {
			console.log(err);
		});
	}

	joinGroup(groupName: string, userName: string) {
		this.chatConnection
			?.invoke('ConnectToGroup', groupName, userName)
			.then(() => {})
			.catch(err => {
				console.log(err);
			});
	}
}
