using SignalRProject.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRProject.Interfaces
{
    
    public interface IChatRoomService
    {
        Task<Guid> CreateChatRoom(string connectionId);
        Task<Guid> GetChatRoomFromConnection(string connectionId);
    }

    public class ChatRoomService : IChatRoomService
    {
        private readonly DatabaseContext _context;

        public ChatRoomService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateChatRoom(string connectionId)
        {
            var existsChatRoom = _context.ChatRooms.SingleOrDefault(c => c.ConnectionId == connectionId);
            if (existsChatRoom != null)
            {
                return await Task.FromResult(existsChatRoom.Id);
            }
            ChatRoom chatRoom = new ChatRoom()
            {
                Id = Guid.NewGuid(),
                ConnectionId = connectionId
            };
            _context.ChatRooms.Add(chatRoom);
            _context.SaveChanges();
            return await Task.FromResult(chatRoom.Id);
        }

        public async Task<Guid> GetChatRoomFromConnection(string connectionId)
        {
            var chatRoom = _context.ChatRooms.SingleOrDefault(c => c.ConnectionId == connectionId);
            return await Task.FromResult(chatRoom.Id);
        }
    }
}
