using ChatAppServer.Models;
using ChatAppServer.Repositories.IRepositories;
using ChatAppServer.Schema.Types;
using ChatAppServer.Schema.Types.DTOTypes;

namespace ChatAppServer.Schema.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class MessageMutation
    {
        public async Task<MessageType> SendMessage(MessageTypeInput model, [Service] IMessageRepository messageRepository, [Service] IAuthRepository authRepository)
        {
            var user = await authRepository.GetUserByEmail(model.Email);

            var message = new Message
            {
                SenderId = user.Email!,
                SenderName = user.FullName,
                Content = model.Content,
                AvatarSender = user.AvatarUrl,
                TimeStamp = DateTime.Now
            };

            await messageRepository.SendMessage(message, model.UniqueCodeGroup);

            return new MessageType
            {
                SenderId = user.Email!,
                SenderName = user.FullName,
                Content = model.Content,
                AvatarSender = user.AvatarUrl,
            };
        }
    }
}
