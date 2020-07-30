using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RNAFoldingSim.Models;

namespace RNAFoldingSim.Controllers
{
    [Route("/animation")]
    public class AnimationController : Controller
    {
        [Route("{id}")]
        public IActionResult CreateAnimation(int id)
        {
            return Json(Fold.folds[id]);
        }
    }
}
