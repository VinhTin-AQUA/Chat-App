import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { Friend } from '../../shared/models/friend';

@Component({
	selector: 'app-search-friend',
	standalone: true,
	imports: [FormsModule],
	templateUrl: './search-friend.component.html',
	styleUrl: './search-friend.component.scss',
})
export class SearchFriendComponent {
	searchString: string = '';
  friends: Friend[] = [];

	constructor(private userService: UserService) {}

	onEnter() {
		console.log(this.searchString);

		this.userService.searchFriends(this.searchString).subscribe((result: any) => {
      this.friends = result.data.searchUserByName;
    });
	}
}
