import { gql } from 'apollo-angular';

export const ADDING_FRIEND_NOTICE_SUN = gql`
	subscription {
		addingFriendSent {
			id
			content
			type
			isReaded
			createdAt
			related {
				id
				type
			}
		}
	}
`;
