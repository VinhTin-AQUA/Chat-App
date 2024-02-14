import { gql } from 'apollo-angular';

export const GET_USER_BY_EMAIL = gql`
	query ($email: String!) {
		userByEmail(email: $email) {
			fullName
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

export const LOGIN = gql`
	query ($email: String!, $password: String!) {
		login(model: { email: $email, password: $password }) {
			success
			errorMessages
			data
		}
	}
`;
