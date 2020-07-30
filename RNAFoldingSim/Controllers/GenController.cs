using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RNAFoldingSim.Models;

namespace RNAFoldingSim.Controllers
{
    public class GenController : Controller
    {
        private readonly object _foldlock = new object();

    	[HttpPost]
        public IActionResult Index()
        {
            var an = new Animator();
            var s = HttpContext.Request.Form["baseString"];
            string validated = Fold.ValidateBaseString(s);
            if (validated == null)
            {
                return Json(new GenerateResponse { id=0, valid=false });
            }
            an.Animate(validated);
            var f = an.Fold();
            lock (_foldlock)
            {
                // f.id: Id of the animation/fold
                f.id = Fold.folds.Count;
                Fold.folds.Add(f);
            }
            // Call saving function here
            Save_Animation.Save(f);
            return Json(new GenerateResponse { id=f.id, valid=true });
        }
    }
}
