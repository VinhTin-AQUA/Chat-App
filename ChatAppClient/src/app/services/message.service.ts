import { Injectable } from '@angular/core';
import { Apollo } from 'apollo-angular';
import { GET_MESSAGES_OF_GROUP } from '../graphql/queries/message.query';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  constructor(private apollo: Apollo) { }

  getMessagesOfGroup(uniqueCodeGroup: string) {
    return this.apollo.query({
      query: GET_MESSAGES_OF_GROUP,
      variables: {
        uniqueCodeGroup: uniqueCodeGroup
      }
    })

  }
}
