using Examen2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Examen2.Clases
{
    public class clsPrenda
    {
        private DBExamen2Entities dbExamen= new DBExamen2Entities(); //Es el atributo para gestionar la conexion a la base de datos
        public Prenda prenda { get; set; }

        public string Datos {  get; set; }

        public string EliminarImagenPrenda(string fotoPrenda)
        {
            try
            {
                var imagen = dbExamen.FotoPrendas.FirstOrDefault(i => i.FotoPrenda1 == fotoPrenda);
                if (imagen != null)
                {
                    dbExamen.FotoPrendas.Remove(imagen);
                    dbExamen.SaveChanges();
                    return "Se eliminó la referencia en la base de datos";
                }
                return "No se encontró la imagen en la base de datos";
            }
            catch (Exception ex)
            {
                return "Error al eliminar la imagen de la base de datos: " + ex.Message;
            }
        }
        public string GrabarPrenda(string idCliente, string nombre, string correo, string celular)
        {
            try
            {
                var ultimaPrenda = dbExamen.Prendas.OrderByDescending(p => p.IdPrenda).FirstOrDefault();

                int nuevoCodigo = (ultimaPrenda != null) ? ultimaPrenda.IdPrenda + 1 : 1;
                Cliente cliente = dbExamen.Clientes.FirstOrDefault(c => c.Documento == idCliente);

                // Si el cliente no existe, se crea y guarda en la base de datos
                if (cliente == null)
                {
                    cliente = new Cliente
                    {
                        Documento = idCliente,
                        Nombre = nombre,
                        Email = correo,
                        Celular = celular,
                    };
                    dbExamen.Clientes.Add(cliente);
                    dbExamen.SaveChanges();
                }

                // Se asocia la prenda al cliente y se guarda
                prenda.IdPrenda = nuevoCodigo;
                prenda.Cliente = cliente.Documento;
                dbExamen.Prendas.Add(prenda);
                dbExamen.SaveChanges();

                return "Se grabó la información en la base de datos";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string GrabarImagenPrenda(int idPrenda, List<string> fotoPrenda)
        {
            try
            {
                int nuevoId = dbExamen.FotoPrendas.Any()? dbExamen.FotoPrendas.Max(f => f.idFoto) + 1: 1; // Si no hay registros, empezamos desde 1

                foreach (string imagen in fotoPrenda)
                {
      
                    FotoPrenda imagenP= new FotoPrenda();
                    imagenP.idPrenda = idPrenda;
                    imagenP.FotoPrenda1 = imagen;
                    imagenP.idFoto = nuevoId++;
                    dbExamen.FotoPrendas.Add(imagenP);
                    dbExamen.SaveChanges();
                }
                return "Se grabo la informacion en la base de datos";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }


        public IQueryable ObtenerPrendasXCliente(string idCliente)
        {

            return from C in dbExamen.Set<Cliente>()
                   join P in dbExamen.Set<Prenda>()
                   on C.Documento equals P.Cliente
                   join F in dbExamen.Set<FotoPrenda>()
                   on P.IdPrenda equals F.idPrenda into imagenesPrenda
                   where C.Documento == idCliente
                   select new
                   {
                       Cliente = new
                       {
                           C.Documento,
                           C.Nombre,
                           C.Email,
                           C.Celular,
                       },
                       Prendas = new
                       {
                           P.IdPrenda,
                           P.Descripcion,
                           P.TipoPrenda,
                           P.Valor,
                          
                           Imagenes = imagenesPrenda.Select(img => new
                           {
                               img.idFoto,
                               img.FotoPrenda1,
                           }).ToList()
                       }
                   };
        }
    }
}