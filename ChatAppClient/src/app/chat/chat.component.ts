import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MenuChatComponent } from '../shared/components/menu-chat/menu-chat.component';

@Component({
  selector: 'app-chat',
  imports:[RouterOutlet, MenuChatComponent],
  standalone: true,
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.scss'
})
export class ChatComponent {

}
