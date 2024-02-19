import { Routes } from '@angular/router';
import { ChatComponent } from './chat.component';
import { ChatHomeComponent } from './chat-home/chat-home.component';

export const routes: Routes = [
	{
		path: '',
		component: ChatComponent,
		children: [
			{path:'', component: ChatHomeComponent, title: 'IGroupCord'}
		],
	},
];
