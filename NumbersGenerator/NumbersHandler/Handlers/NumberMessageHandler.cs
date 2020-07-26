using MessageBus.Messages;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace NumbersHandler.Handlers
{
    public class NumberMessageHandler : IMessageHandler<NumberMessage>
    {
        private readonly int _bufferThreshold;

        private readonly List<int> _buffer;

        public NumberMessageHandler(IOptions<NumbersConfiguration> options)
        {
            _bufferThreshold = options.Value.BufferThreshold;
            _buffer = new List<int>();
        }

        public async Task Handle(NumberMessage message)
        {
            _buffer.Add(message.Value);

            if (_buffer.Count == _bufferThreshold)
            {
                double average = _buffer.Sum() / _bufferThreshold;
                _buffer.Clear();
                Console.WriteLine($"{DateTime.Now.ToLongTimeString()} : {average}");
            }
        }
    }
}
