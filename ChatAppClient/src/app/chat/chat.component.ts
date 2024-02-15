import { Component } from '@angular/core';
import { ConversationComponent } from './conversation/conversation.component';
import { RouterOutlet } from '@angular/router';
import { SidebarComponent } from '../shared/components/sidebar/sidebar.component';

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [SidebarComponent, ConversationComponent, RouterOutlet],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.scss'
})
export class ChatComponent {

}
