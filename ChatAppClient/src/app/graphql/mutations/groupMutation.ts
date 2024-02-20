import { gql } from 'apollo-angular';

export const CREATE_GROUP_TO_USER = gql`
	mutation ($email: String!, $password: String!, $groupName: String!) {
		createGroupToUser(model: { groupName: $groupName, password: $password }, email: $email) {
			groupName
			uniqueCodeGroup
			id
		}
	}
`;
