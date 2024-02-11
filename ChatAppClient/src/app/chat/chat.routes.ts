import { Routes } from '@angular/router';
import { ChatComponent } from './chat.component';
import { ConversationComponent } from './conversation/conversation.component';
import { GroupComponent } from './group/group.component';
import { SettingComponent } from './setting/setting.component';

export const routes: Routes = [
	{
		path: '',
		component: ChatComponent,
		children: [
			{ path: '', redirectTo: 'conversations', pathMatch: 'full' },
			{ path: 'conversations', component: ConversationComponent, title: 'Conversations' },
			{ path: 'groups', component: GroupComponent, title: 'Groups' },
			{ path: 'settings', component: SettingComponent, title: 'Settings' },
		],
	},
];
