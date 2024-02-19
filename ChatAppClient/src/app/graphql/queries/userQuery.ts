import { gql } from 'apollo-angular';

export const GET_USER_BY_EMAIL = gql`
	query ($email: String!) {
		userByEmail(email: $email) {
			email
			fullName
			avatarUrl
			phoneNumber
			uniqueCodeUser
		}
	}
`;

export const GET_ALL_USER = gql`
	query {
		allUsers {
			fullName
		}
	}
`;


