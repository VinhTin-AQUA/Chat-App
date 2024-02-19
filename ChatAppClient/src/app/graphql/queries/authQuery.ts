import { gql } from 'apollo-angular';

export const REFRESH_TOKEN = gql`
	query ($email: String!) {
		refreshRoken(email: $email) {
			data {
				... on UserType {
					token
				}
			}
		}
	}
`;

export const LOGIN = gql`
	query ($email: String!, $password: String!) {
		login(model: { email: $email, password: $password }) {
			success
			errorMessages
			data {
				... on UserType {
					email
					fullName
					avatarUrl
					phoneNumber
					token
					uniqueCodeUser
				}
			}
		}
	}
`;
