﻿
//---------------------------------------------------------------------------------------
//      This code was generated by a Jumper tool. 
//      Runtime version : 1.0
//      Generation Time : 24.10.2023 10:52
//---------------------------------------------------------------------------------------

using Core.ApiHelpers.JwtHelper.Models;
using Core.Persistence.Dynamic;
using Core.Persistence.Models.Responses;
using Core.WebHelper.NameValueCollectionHelpers;
using IdentityServer.Application.Features.Auth.Commands.Login;
using IdentityServer.Application.Features.Users.Commands.Create;
using IdentityServer.Application.Features.Users.Commands.DeleteById;
using IdentityServer.Application.Features.Users.Commands.Update;
using IdentityServer.Application.Features.Users.Commands.ValidateForgetCode;
using IdentityServer.Application.Features.Users.Queries.GetById;
using IdentityServer.Application.Features.Users.Queries.GetByUserName;
using IdentityServer.Application.Features.Users.Queries.ListDynamic;
using IdentityServer.Domain.Entities;
using IdentityServer.UI.Api.Controllers.Base;
using IdentityServer.UI.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification.Abstract;
using Notification.Entities.Concrete;
using System.Collections.Specialized;

namespace IdentityServer.UI.Api.Controllers
{
    [Route("Users")]
    public class UsersController : MediatrController
    {
        private readonly IMailingService _mailingService;
        private readonly TokenParameters _parameters;
        public UsersController(IMailingService mailingService, TokenParameters parameters)
        {
            _mailingService = mailingService;
            _parameters = parameters;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {
            var response = await base.Mediator.Send(new GetByIdUserQuery { Id = _parameters.UserId });
            return Ok(Response<GetByIdUserResponse>.Success(response, 200));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserCommand request)
        {
            var createRes = await base.Mediator.Send(request);
            var loginRes = await base.Mediator.Send(new LoginCommand { DeviceId = request.DeviceId, DeviceToken = request.DeviceToken, Password = request.Password, UserName = request.UserName });
            var mail = new SendMailBaseModel
            {
                Content = "",
                IsContactMail = false,
                MailModel = new PostContactForm { FullName = createRes.FirstName, Subject = "👋 NurayMind'a Hoş Geldin! 🚀✨", Message = $"Content" },
                Subject = request.Language == "tr" ? "👋 NurayMind'a Hoş Geldin! 🚀✨" : "👋 Welcome to NurayMind ! 🚀✨",
                TemplateName = $"_WelcomeMail_{request.Language}",
                To = new HashSet<string> { createRes.MailAddress }
            };
            await _mailingService.Send(mail);
            return Ok(Response<LoginResponse>.Success(loginRes, 200));
        }

        [HttpPost("sendforgotmail")]
        [AllowAnonymous]
        public async Task<IActionResult> SendForgotMail([FromBody] GetByMailAddressUserQuery query)
        {
            var response = await base.Mediator.Send(query);
            var code = response.CompanyName;

            var mail = new SendMailBaseModel
            {
                Content = "",
                IsContactMail = false,
                MailModel = new PostContactForm { FullName = response.UserName, Subject = "", Message = code! },
                Subject = query.Language == "tr" ? "NurayMind Şifre Sıfırlama Talebi" : "NurayMind Reset Password Request",
                TemplateName = $"_ResetPasswordMail_{query.Language}",
                To = new HashSet<string> { response.MailAddress }
            };
            await _mailingService.Send(mail);

            return Ok(Response<GetByMailAddressUserResponse>.Success(response, 200));
        }

        [HttpPost("validateforgotcode")]
        [AllowAnonymous]
        public async Task<IActionResult> ValidateForgotCode([FromBody] ValidateForgetCodeUserCommand command)
        {
            var response = await base.Mediator.Send(command);
            return Ok(Response<ValidateForgetCodeUserResponse>.Success(response, 200));
        }

        [HttpGet("getlist")]
        public async Task<IActionResult> ListAsync(NameValueCollection collection)
        {
            var query = new ListDynamicUserQuery
            {
                DynamicQuery = collection.ToDynamicFilter<User>(),
                PageRequest = collection.ToPageRequest()
            };
            var response = await base.Mediator.Send(query);

            return Ok(Response<ListModel<ListDynamicUserResponse>>.Success(response, 200));
        }

        [HttpPost("list")]
        public async Task<IActionResult> ListAsync([FromBody] ListDynamicUserQuery query)
        {
            var response = await base.Mediator.Send(query);
            return Ok(Response<ListModel<ListDynamicUserResponse>>.Success(response, 200));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateUserCommand request)
        {
            return Ok(Response<LoginResponse>.Success(await base.Mediator.Send(request), 200));
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            return Ok(Response<DeleteByIdUserResponse>.Success(await base.Mediator.Send(new DeleteByIdUserCommand { Id = id }), 200));
        }

        [HttpDelete("deleteprofile")]

        public async Task<IActionResult> DeleteProfile()
        {
            return Ok(Response<DeleteByIdUserResponse>.Success(await base.Mediator.Send(new DeleteByIdUserCommand { Id = _parameters.UserId }), 200));
        }

        [HttpGet("dropdown")]
        public async Task<IActionResult> Dropdown(string searchTerm)
        {
            var query = new ListDynamicUserQuery { DynamicQuery = new DynamicQuery { Filter = Filter.Create("PhoneNumber", FilterOperator.ContainsIgnoreCase, searchTerm) } };
            return Json((await base.Mediator.Send(query)).Items.Select(w => new { Id = w.Id, PhoneNumber = w.PhoneNumber }));
        }



        [HttpPost("getnames")]
        public async Task<IActionResult> GetNames(List<Guid> ids)
        {
            var dynamicQuery = new DynamicQuery()
            {
                Filter = Filter.Create("Id", FilterOperator.Equals, ids.First().ToString()),
            };
            if (ids.Count > 1)
            {
                dynamicQuery.Filter.Filters = new List<Filter>();
                dynamicQuery.Filter.Logic = Logic.Or;
                for (int i = 1; i < ids.Count; i++)
                {
                    dynamicQuery.Filter.Filters.Add(new Filter { Field = "Id", Logic = Logic.Or, Operator = FilterOperator.Equals, Value = ids[i].ToString() });
                }
            }
            var query = new ListDynamicUserQuery { DynamicQuery = dynamicQuery };
            var datas = await base.Mediator.Send(query);
            return Json(datas.Items.Select(w => new { Id = w.Id, PhoneNumber = w.PhoneNumber }));
        }

    }
}


