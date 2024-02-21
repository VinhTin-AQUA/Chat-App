import { gql } from 'apollo-angular';

export const GET_GROUPS_OF_USER = gql`
	query ($ownerId: String!) {
		groupsOfUser(ownerId: $ownerId) {
			id
			ownerId
			groupName
			uniqueCodeGroup
		}
	}
`;

export const GET_MEMBERS_OF_GROUP = gql`
	query ($uniqueCodeGroup: String!) {
		group(uniqueCodeGroup: $uniqueCodeGroup)
	}
`;
