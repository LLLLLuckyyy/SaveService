using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaveService.Resources.Api.Repository;
using System;

namespace SaveService.Resources.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class FileController : ControllerBase
    {
        private readonly IFileRepo _context;

        public FileController(IFileRepo context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ObjectResult> Get(int IdOfFile)
        {
            if (IdOfFile > 0)
            {
                try
                {
                    var fileInBites = await _context.GetAsync(IdOfFile, User.Identity.Name);
                    return new ObjectResult(fileInBites);
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
        public async Task<HttpStatusCode> Save(IFormFile file)
        {
            if (file != null)
            {
                try
                {
                    await _context.SaveAsync(file, User.Identity.Name);
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
        public async Task<HttpStatusCode> Delete(int IdOfFile)
        {
            if (IdOfFile > 0)
            {
                try
                {
                    await _context.DeleteAsync(IdOfFile, User.Identity.Name);
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
        public async Task<HttpStatusCode> Edit(IFormFile file, int IdOfFileToChange)
        {
            if (file != null && IdOfFileToChange > 0)
            {
                try
                {
                    await _context.EditAsync(file, IdOfFileToChange, User.Identity.Name);
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
