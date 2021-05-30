using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BackAgain.Dto;
using BackAgain.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BackAgain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly WebSocketConnectionManager _webman;

        public ValuesController(Service.WebSocketConnectionManager webman)
        {
            _webman = webman;
        }
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            /*var socket = _webman.getAllSockets().FirstOrDefault();
            var response = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new WebSocketClientResponse { message = _webman.getAllSockets().Count.ToString(), type = (int) Model.WebSocketMessageType.ConnectionID}));

            await socket.Value.SendAsync(response , System.Net.WebSockets.WebSocketMessageType.Text, true,CancellationToken.None);*/
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
