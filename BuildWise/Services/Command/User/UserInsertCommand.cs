using BuildWise.Payload.User;
using MediatR;

namespace BuildWise.Services.Command.User
{
    public class UserInsertCommand : IRequest<int>
    {
        public UserInsertCommand(UserInsertPayload payload)
        {

            Payload = payload;
        }
        public UserInsertPayload Payload { get; set; }
    }
    internal class UserInsertCommandHandler : IRequestHandler<UserInsertCommand, int> 
    {
        public UserInsertCommandHandler()
        {
            
        }
        public async Task<int> Handle(UserInsertCommand command, CancellationToken cancellationToken)
        {
            return 0;
        }
    }
}
