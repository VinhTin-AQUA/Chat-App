import { Component, inject } from '@angular/core';
import { UserService } from '../../../services/user.service';
import { AuthService } from '../../../services/auth.service';
import { Router } from '@angular/router';
import { UserStore } from '../../stores/user.store';
import { GroupTypeInput } from '../../models/groupInputType';
import { GroupService } from '../../../services/group.service';
import { FormsModule } from '@angular/forms';
import { GroupStore } from '../../stores/group.store';
import { patchState } from '@ngrx/signals';
import { Group } from '../../models/group';

@Component({
	selector: 'app-menu-chat',
	standalone: true,
	imports: [FormsModule],
	templateUrl: './menu-chat.component.html',
	styleUrl: './menu-chat.component.scss',
})
export class MenuChatComponent {
	isShowMenuProfile: boolean = false;
	private userStore = inject(UserStore);
	private groupStore = inject(GroupStore);
	isShowCreateRoom: boolean = false;
	isShowJoinGroup: boolean = false;
	groupName: string = '';
	password: string = '';
	roomCode: string = '';
	errorMessage: string = '';

	constructor(
		private userService: UserService,
		private router: Router,
		private groupService: GroupService
	) {}

	showMenuProfile() {
		this.isShowMenuProfile = !this.isShowMenuProfile;
	}

	onLogout(event: Event) {
		event.stopPropagation();
		this.userService.logout();
		this.router.navigateByUrl('/login');
	}

	onNavigateToProfile(event: Event) {
		event.stopPropagation();
	}

	showCreateRoom() {
		this.isShowCreateRoom = !this.isShowCreateRoom;
	}

	showJoinGroup() {
		this.isShowJoinGroup = !this.isShowJoinGroup;
	}

	onCreateGroup() {
		if (this.groupName === '' || this.password === '') {
			this.errorMessage = 'Group name or password is required';
			return;
		}

		const group: GroupTypeInput = {
			groupName: this.groupName,
			password: this.password,
		};
		const email = this.userStore.email();

		this.groupService.createGroup(group, email).subscribe((result: any) => {
			this.isShowCreateRoom = false;
			const newGroup: Group = {
				groupName: result.data.createGroupToUser.groupName,
				id: result.data.createGroupToUser.id,
				uniqueCodeGroup: result.data.createGroupToUser.uniqueCodeGroup,
				ownerId: '',
				password: '',
			};
			patchState(this.groupStore, {
				length: this.groupStore.length() + 1,
				groups: [...this.groupStore.groups(), newGroup],
			});
		});
	}

	onInputGroupId(event: any) {
		event.stopPropagation();
		console.log(123);
	}

	onJoinGroup(event: any) {
		event.stopPropagation();
	}
}
