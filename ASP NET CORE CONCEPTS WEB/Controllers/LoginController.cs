using ASP_NET_CORE_CONCEPTS_WEB.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetCoreConcepts.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_NET_CORE_CONCEPTS_WEB.Controllers
{
    public class LoginController : Controller
  
    {
        UsuarioDal dal = new UsuarioDal();
        // GET: LoginController
        public ActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await Task.Run(() => dal.CrearUsuario(model));

                if (result.Equals(1))
                {
               
                return RedirectToAction("index", "Home");
                }
                else {
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                }
            }
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                var result = await Task.Run(() => dal.ObtenerUsuario(user.usuario));

                if (result.contrasena.Equals(user.contrasena))
                {
                    ViewData["Usuario"] = user.usuario;
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(user);
        }
    }
    
}
