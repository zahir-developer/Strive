using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Strive.BusinessEntities.Model;
using Strive.BusinessLogic;
using Strive.Common;
using System;
using System.Collections.Generic;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Admin.API.Controllers
{
    [Authorize]
    //[AutoValidateAntiforgeryToken]

    [Route("Admin/[Controller]")]
    public class WhiteLabellingController : StriveControllerBase<IWhiteLabelBpl>
    {
        public WhiteLabellingController(IWhiteLabelBpl whiteLabelBpl) : base(whiteLabelBpl) { }

        [HttpPost]
        [Route("Add")]
        public Result AddWhiteLabelling([FromBody] WhiteLabelModel whiteLabel) => _bplManager.AddWhiteLabelling(whiteLabel);

        [HttpPost]
        [Route("Update")]
        public Result UpdateWhiteLabelling([FromBody] WhiteLabelModel whiteLabel) => _bplManager.UpdateWhiteLabelling(whiteLabel);

        [HttpGet]
        [Route("GetAll")]
        public Result GetAll() => _bplManager.GetAll();

        [HttpPost]
        [Route("Save")]
        public Result SaveTheme([FromBody] ThemeModel themes) => _bplManager.SaveTheme(themes);
    }
}
