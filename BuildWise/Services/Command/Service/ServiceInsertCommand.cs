using BuildWise.Payload.Service;
using MediatR;

namespace BuildWise.Services.Command.Service
{
    public class ServiceInsertCommand : IRequest<int>
    {
        public ServiceInsertCommand(ServiceInsertPayload payload)
        {
            Payload = payload;
        }
        public ServiceInsertPayload Payload { get; set; }
    }
    internal class ServiceInsertCommandHandler : IRequestHandler<ServiceInsertCommand, int>
    {
        public ServiceInsertCommandHandler()
        {
            
        }

        public async Task<int> Handle(ServiceInsertCommand request, CancellationToken cancellationToken)
        {

        }
    }

}
