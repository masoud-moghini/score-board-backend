using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using backend.Models;
using backend.persistance;
using System.Linq;

namespace backend.Controllers
{
    [Route("/api/messages")]
    public class message:Controller
    {
        ApiContext context;
        public message(ApiContext context)
        {
            this.context=context;
        }
        [HttpGet]
        public IEnumerable<Message> GetMessage()
        {
            return context.messages;
        }

        [HttpGet("{name}")]
        public IEnumerable<Message> GetMessage(string name)
        {
            return context.messages.Where(message =>
                message.Owner==name
            );

        
        }

        [HttpPost]
        public Message Post([FromBody]Message message)
        {
            var dbEntity=context.messages.Add(message).Entity;
            context.SaveChanges();
            return dbEntity;
        }
    }
}