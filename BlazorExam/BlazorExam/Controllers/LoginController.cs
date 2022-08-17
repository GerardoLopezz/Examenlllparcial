using BlazorExam.Data;
using Datos.Interfaces;
using Datos.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Modelos;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BlazorExam.Controllers
{
    public class LoginController : Controller
    {
        private readonly MySQLConfiguracion _configuracion;
        private ILoginRepositorio _loginRepositorio;
        private IPasajeroRepositorio _paspajeroRepositorio;
        public LoginController(MySQLConfiguracion configuracion)
        {
            _configuracion = configuracion;
            _loginRepositorio = new LoginRepositorio(configuracion.CadenaConexion);
            _paspajeroRepositorio = new PasajeroRepositorio(configuracion.CadenaConexion);
        }

        [HttpPost("/account/login")]
        public async Task<IActionResult> Login(Login login)
        {
            string rol = string.Empty;
            try
            {
                bool pasajeroValido = await _loginRepositorio.ValidarUsuario(login);
                if (pasajeroValido)
                {
                    Pasajero usu = await _paspajeroRepositorio.GetPorCodigo(login.Codigo);
                    if (usu.EstaActivo)
                    {
                        rol = usu.Rol;

                        //Añadimos los claums del usuarios y su rol

                        var claims = new[]
                        {
                            new Claim(ClaimTypes.Name, usu.Codigo),
                            new Claim(ClaimTypes.Role, rol)
                        };

                        //Crear el claim principal
                        ClaimsIdentity clainsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(clainsIdentity);

                        //Generamos cookie

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal,
                                                     new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTime.UtcNow.AddHours(2) });

                    }
                    else
                    {
                        return LocalRedirect("/login/El usuario esta inactivo");
                    }
                }
                else
                {
                    return LocalRedirect("/loginDatos de usuario invalidos");
                }
            }
            catch (Exception ex)
            {

            }
            return LocalRedirect("/");

        }

        [HttpGet("/account/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/login");
        }

    }

        
}
