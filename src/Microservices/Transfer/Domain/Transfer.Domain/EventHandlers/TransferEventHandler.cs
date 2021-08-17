using Domain.Core.Bus;
using System.Threading.Tasks;
using Transfer.Domain.Events;
using Transfer.Domain.Interfaces;
using Transfer.Domain.Models;

namespace Transfer.Domain.EventHandlers
{
    public class TransferEventHandler : IEventHandler<TransferCreatedEvent>
    {
        private readonly ITransferRepository _transferRepository;

        public TransferEventHandler(ITransferRepository transferRepository)
        {
            _transferRepository = transferRepository;
        }

        public Task Handle(TransferCreatedEvent @event)
        {
            _transferRepository.Add(new TransferLog()
            {
                FromAccount = @event.From,
                ToAccount = @event.To,
                TransferAmount = @event.Amount
            });

            return Task.CompletedTask;
        }
    }
}
