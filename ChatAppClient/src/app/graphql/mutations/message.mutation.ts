import { gql } from 'apollo-angular';

export const SEND_MESSAGE = gql`
	mutation ($content: String!, $email: String!, $uniqueCodeGroup: String!) {
		sendMessage(
			model: { content: $content, email: $email, uniqueCodeGroup: $uniqueCodeGroup }
		) {
      content
      avatarSender
      senderName
      timeStamp
      senderId
		}
	}
`;
