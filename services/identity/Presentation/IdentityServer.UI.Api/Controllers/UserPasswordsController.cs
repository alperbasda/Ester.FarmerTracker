
//---------------------------------------------------------------------------------------
//      This code was generated by a Jumper tool. 
//      Runtime version : 1.0
//      Generation Time : 24.10.2023 10:52
//---------------------------------------------------------------------------------------

using IdentityServer.Application.Features.UserPasswords.Commands.Create;
using IdentityServer.Application.Features.UserPasswords.Commands.DeleteById;
using IdentityServer.Application.Features.UserPasswords.Commands.Update;
using IdentityServer.Application.Features.UserPasswords.Queries.GetById;
using IdentityServer.Application.Features.UserPasswords.Queries.ListDynamic;
using IdentityServer.UI.Api.Controllers.Base;
using Core.Persistence.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using System.Web;
using Core.WebHelper.NameValueCollectionHelpers;
using IdentityServer.Domain.Entities;
using IdentityServer.Application.Features.UserPasswords.Commands.UpdateForUser;

namespace IdentityServer.UI.Api.Controllers
{
    [Route("UserPasswords")]
    public class UserPasswordsController : MediatrController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var response = await base.Mediator.Send(new GetByIdUserPasswordQuery { Id = id });
            return Ok(Response<GetByIdUserPasswordResponse>.Success(response,200));
        }

        [HttpGet("getlist")]
        public async Task<IActionResult> ListAsync(NameValueCollection collection)
        {
            var query = new ListDynamicUserPasswordQuery
            {
                DynamicQuery = collection.ToDynamicFilter<UserPassword>(),
                PageRequest = collection.ToPageRequest()
            };
            var response = await base.Mediator.Send(query);

            return Ok(Response<ListModel<ListDynamicUserPasswordResponse>>.Success(response,200));
        }

        [HttpPost("list")]
        public async Task<IActionResult> ListAsync(ListDynamicUserPasswordQuery query)
        {
            var response = await base.Mediator.Send(query);
            return Ok(Response<ListModel<ListDynamicUserPasswordResponse>>.Success(response,200));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateUserPasswordCommand request)
        {
            return Ok(Response<CreateUserPasswordResponse>.Success(await base.Mediator.Send(request), 200));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody]UpdateForUserUserPasswordCommand request)
        {
            return Ok(Response<UpdateForUserUserPasswordResponse>.Success(await base.Mediator.Send(request), 200));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            return Ok(Response<DeleteByIdUserPasswordResponse>.Success(await base.Mediator.Send(new DeleteByIdUserPasswordCommand { Id = id }), 200));
        }

    }
}


