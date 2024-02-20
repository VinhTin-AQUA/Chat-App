import { Injectable } from '@angular/core';
import { Apollo } from 'apollo-angular';
import { GroupTypeInput } from '../shared/models/groupInputType';
import { CREATE_GROUP_TO_USER } from '../graphql/mutations/groupMutation';
import { GET_GROUPS_OF_USER } from '../graphql/queries/group.query';

@Injectable({
	providedIn: 'root',
})
export class GroupService {
	constructor(private apollo: Apollo) {}

	createGroup(model: GroupTypeInput, email: string) {
		return this.apollo.mutate({
			mutation: CREATE_GROUP_TO_USER,
			variables: {
				email: email,
				groupName: model.groupName,
				password: model.password,
			},
		});
	}

	getGroupsOfUser(ownerId: string) {
		return this.apollo.query({
			query: GET_GROUPS_OF_USER,
			variables: {
				ownerId: ownerId,
			},
		});
	}
}
