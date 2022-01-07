using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRProject.Entities
{
    public class ChatRoom
    {
        public Guid Id { get; set; }
        public string ConnectionId { get; set; }
    }
}
