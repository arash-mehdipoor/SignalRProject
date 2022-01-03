using Microsoft.AspNetCore.SignalR;
using SignalRProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRProject.Hubs
{
    public class SiteChatHub : Hub
    {
        private readonly IChatRoomService _chatRoom;

        public SiteChatHub(IChatRoomService chatRoom)
        {
            _chatRoom = chatRoom;
        }

        public async Task SendMessage(string sender, string message)
        {
            var roomId = await _chatRoom.GetChatRoomFromConnection(Context.ConnectionId);
            await Clients.Groups(roomId.ToString()).SendAsync("GetNewMessage", sender, message, DateTime.Now.ToShortDateString());
        }

        public override async Task OnConnectedAsync()
        {
            var roomId = await _chatRoom.CreateChatRoom(Context.ConnectionId);
            await Clients.Caller.SendAsync("GetNewMessage", "پشتیبانی", "خوش آمدید");
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());
            await base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
