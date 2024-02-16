import { Routes } from '@angular/router';
import { ChatComponent } from './chat.component';
import { ConversationComponent } from './conversation/conversation.component';
import { GroupComponent } from './group/group.component';
import { SettingComponent } from './setting/setting.component';
import { SearchFriendComponent } from './search-friend/search-friend.component';
import { NoticeComponent } from './notice/notice.component';

export const routes: Routes = [
	{
		path: '',
		component: ChatComponent,
		children: [
			{ path: '', redirectTo: 'conversations', pathMatch: 'full' },
			{ path: 'conversations', component: ConversationComponent, title: 'Conversations' },
			{ path: 'groups', component: GroupComponent, title: 'Groups' },
			{ path: 'search', component: SearchFriendComponent, title: 'Search friends' },
			{ path: 'notices', component: NoticeComponent, title: 'Notices' },
			{ path: 'settings', loadChildren: () => import('./setting/setting.routes').then(r => r.routes) },
		],
	},
];
