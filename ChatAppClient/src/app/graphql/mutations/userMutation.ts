import { gql } from 'apollo-angular';

export const CREATE_USER = gql`
	mutation (
		$email: String!
		$fullName: String!
		$avatarUrl: String!
		$password: String!
		$reEnterPassword: String!
	) {
		createUser(
			model: {
				email: $email
				fullName: $fullName
				avatarUrl: $avatarUrl
				password: $password
				reEnterPassword: $reEnterPassword
			}
		) {
			success
			errorMessages
			data {
				... on UserType {
					fullName
				}
			}
		}
	}
`;

export const ADD_USER_TO_GROUP = gql`
	mutation ($uniqueCodeGroup: String!, $uniqueCodeUser: String!) {
		addUserToGroup(uniqueCodeGroup: $uniqueCodeGroup, uniqueCodeUser: $uniqueCodeUser) {
			success
		}
	}
`;
