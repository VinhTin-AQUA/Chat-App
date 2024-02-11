import { Component } from '@angular/core';
import { SidebarComponent } from '../components/sidebar/sidebar.component';
import { ConversationComponent } from './conversation/conversation.component';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [SidebarComponent, ConversationComponent, RouterOutlet],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.scss'
})
export class ChatComponent {

}
