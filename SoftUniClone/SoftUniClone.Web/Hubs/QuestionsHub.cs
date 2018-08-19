using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftUniClone.Web.Hubs
{
    public class QuestionsHub : Hub
    {
        //public async Task PostQuestion(string user, string question)
        //{


        //    return;
        //}

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
    }
}
