import { Injectable } from '@angular/core';
import { Apollo } from 'apollo-angular';
import { GET_MESSAGES_OF_GROUP } from '../graphql/queries/message.query';
import { SEND_MESSAGE } from '../graphql/mutations/message.mutation';

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

  sendMessage(uniqueCodeGroup: string,email: string, content: string) {
    return this.apollo.mutate({
      mutation: SEND_MESSAGE,
      variables: {
        uniqueCodeGroup:uniqueCodeGroup,
        email: email,
        content: content
      }
    })
  }
}
