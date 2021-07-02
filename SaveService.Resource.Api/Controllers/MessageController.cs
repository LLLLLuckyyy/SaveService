using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaveService.Resources.Api.Repository;

namespace SaveService.Resources.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class MessageController : ControllerBase
    {
        private readonly IMessageRepo _context;

        public MessageController(IMessageRepo context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ObjectResult> Get(int IdOfMessage)
        {
            if (IdOfMessage > 0)
            {
                try
                {
                    var messageText = await _context.GetAsync(IdOfMessage, User.Identity.Name);
                    return new ObjectResult(messageText);
                }
                catch (NullReferenceException)
                {
                    return new ObjectResult(HttpStatusCode.NotFound);
                }
                catch (Exception)
                {
                    return new ObjectResult(HttpStatusCode.InternalServerError);
                }
            }
            else
            {
                return new ObjectResult(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<HttpStatusCode> Save(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                try
                {
                    await _context.SaveAsync(text, User.Identity.Name);
                    return HttpStatusCode.OK;
                }
                catch (NullReferenceException)
                {
                    return HttpStatusCode.NotFound;
                }
                catch (Exception)
                {
                    return HttpStatusCode.InternalServerError;
                }
            }
            else
            {
                return HttpStatusCode.BadRequest;
            }
        }


        [HttpDelete]
        [Route("[action]")]
        public async Task<HttpStatusCode> Delete(int IdOfMessage)
        {
            if (IdOfMessage > 0)
            {
                try
                {
                    await _context.DeleteAsync(IdOfMessage, User.Identity.Name);
                    return HttpStatusCode.OK;
                }
                catch (NullReferenceException)
                {
                    return HttpStatusCode.NotFound;
                }
                catch (MemberAccessException)
                {
                    return HttpStatusCode.Locked;
                }
                catch (Exception)
                {
                    return HttpStatusCode.InternalServerError;
                }
            }
            else
            {
                return HttpStatusCode.BadRequest;
            }
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<HttpStatusCode> Edit(string text, int IdOfMessageToChange)
        {
            if (!string.IsNullOrWhiteSpace(text) && IdOfMessageToChange > 0)
            {
                try
                {
                    await _context.EditAsync(text, IdOfMessageToChange, User.Identity.Name);
                    return HttpStatusCode.OK;
                }
                catch (NullReferenceException)
                {
                    return HttpStatusCode.NotFound;
                }
                catch (MemberAccessException)
                {
                    return HttpStatusCode.Locked;
                }
                catch (Exception)
                {
                    return HttpStatusCode.InternalServerError;
                }
            }
            else
            {
                return HttpStatusCode.BadRequest;
            }
        }

    }
}
