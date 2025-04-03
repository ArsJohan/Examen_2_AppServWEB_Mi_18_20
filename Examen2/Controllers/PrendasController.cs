using Examen2.Clases;
using Examen2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Examen2.Controllers
{
    [RoutePrefix("api/Prenda")]
    public class PrendasController : ApiController
    {

        [HttpPost]
        public string CargarArchivo(string idCliente, string nombre, string correo, string celular, Prenda prenda)
        {
            clsPrenda upload = new clsPrenda();
            upload.prenda = prenda;

            return upload.GrabarPrenda(idCliente, nombre, correo, celular);
        }

        [HttpGet]
        [Route("PrendasXCliente")]
        public IQueryable ObtenerPrendasXCliente(string idCliente )
        {
            clsPrenda upload = new clsPrenda();
            return upload.ObtenerPrendasXCliente(idCliente);
        }
    }
}