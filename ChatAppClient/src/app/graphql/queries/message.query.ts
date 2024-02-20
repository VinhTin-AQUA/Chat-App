import { gql } from 'apollo-angular';

export const GET_MESSAGES_OF_GROUP = gql`
	query ($uniqueCodeGroup: String!) {
		messagesOfGroup(uniqueCodeGroup: $uniqueCodeGroup) {
			senderId
			senderName
			content
			avatarSender
			timeStamp
		}
	}
`;
