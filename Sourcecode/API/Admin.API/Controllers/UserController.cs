using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessLogic;
using Strive.Common;
using System;
using System.Collections.Generic;
using Strive.BusinessEntities.Employee;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using MediatR;
using System.Net;
using System.Threading.Tasks;
using Strive.BusinessLogic.User;
using Strive.BusinessEntities.Auth;
using Strive.BusinessLogic.User.CreateUser;

namespace Admin.Api.Controllers
{
    [Authorize]
    [Route("Admin/[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        /// <summary>
        /// Create User.
        /// </summary>
        [Route("Create")]
        [HttpPost]
        [ProducesResponseType(typeof(UserDTO), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> RegisterCustomer([FromBody]CreateUserRequest request)
        {
            var user = await _mediator.Send(new CreateUserCommand(request.Email, request.Name));
            return Created(string.Empty, user);
        }

    }
}