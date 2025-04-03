using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Data.Entity.Infrastructure;

namespace Examen2.Clases
{
	public class clsUpload
	{

		public string Datos { get; set; }
		public string Proceso { get; set; }

		public HttpRequestMessage request { get; set; }

		private List<string> Archivos;

		public async Task<HttpResponseMessage> GrabarArchivo(bool actualizar)
		{
			bool existe = false; //variable para verificar si existe el archivo
            if (!request.Content.IsMimeMultipartContent())
            {
				return request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, "No se envió un archivo para procesar");
            }
            string root = HttpContext.Current.Server.MapPath("~/Archivos");
			var provider = new MultipartFormDataStreamProvider(root);
            try
            {
                await request.Content.ReadAsMultipartAsync(provider);
                if (provider.FileData.Count > 0) //verifica si hay archivos
                {
                    Archivos = new List<string>(); // inicializa la lista de archivos
                    // recorremos los archivos
                    foreach (MultipartFileData file in provider.FileData)
                    {
                        string fileName = file.Headers.ContentDisposition.FileName; //busca el nombre del archivo
                        if (fileName.StartsWith("\"") && fileName.EndsWith("\"")) //verifica si el nombre del archivo tiene comillas
                        {
                            fileName = fileName.Trim('"'); //elimina las comillas
                        }
                        if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                        {
                           fileName = Path.GetFileName(fileName); //elimina la ruta del archivo
                        }

                        if (File.Exists(Path.Combine(root, fileName))) //verifica si el archivo existe
                        {
                            if (actualizar) //verifica si se va a actualizar el archivo
                            {
                                File.Delete(Path.Combine(root, fileName)); //elimina el archivo local
                                File.Move(file.LocalFileName, Path.Combine(root, fileName)); //mueve el archivo a la ruta
                                existe = true; //cambia la variable a true
                            }
                            else
                            {
                                //El archivo ya existe en el servidor, no se va a cargar, se elimina el archivo temporal
                                File.Delete(Path.Combine(root, fileName)); 
                                existe = true; //si no se va a actualizar el archivo, cambia la variable a true
                            }
                        }
                        else
                        {
                            Archivos.Add(fileName); //agrega el nombre archivo a la lista
                            //Renombra el archivo temporal
                            File.Move(file.LocalFileName, Path.Combine(root, fileName)); //mueve el archivo a la ruta
                        }
                    }
                    if(!existe) //verifica si no existe el archivo
                    {
                        //Se genera el proceso de gestión en la base de datos
                        string RptaBD = ProcesarBD();
                        //Termina el ciclo, responde que se cargó el archivo correctamente
                        return request.CreateResponse(System.Net.HttpStatusCode.OK, "Se cargaron los archivos en el servidor, "+ RptaBD);
                    }
                    else
                    {
                        return request.CreateErrorResponse(System.Net.HttpStatusCode.Conflict, "Ya existen los archivos en el servidor" );   
                    }
                }
                else
                {
                    return request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, "No se envió un archivo para procesar");
                }
            }
            catch (Exception ex)
            {
                return request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }


        }


        public HttpResponseMessage EliminarArchivo(string archivo)
        {
            try
            {
                string Ruta = HttpContext.Current.Server.MapPath("~/Archivos");
                string archivoCompleto = Path.Combine(Ruta, archivo);
                if (File.Exists(archivoCompleto))
                {
                    File.Delete(archivoCompleto);
                    string resultado = ProcesarBD(archivo);
                    return request.CreateResponse(HttpStatusCode.OK, "Se eliminó el archivo del servidor, " + resultado);

                }
                else
                {
                    return request.CreateErrorResponse(HttpStatusCode.NotFound, "El archivo no se encuentra en el servidor");
                }
            }
            catch (Exception ex)
            {
                return request.CreateErrorResponse(HttpStatusCode.InternalServerError, "No se pudo eliminar el archivo. " + ex.Message);
            }
        }

        private string ProcesarBD(string nombreArchivo = "")
        {
            clsPrenda prenda = new clsPrenda();
            switch (Proceso.ToUpper())
            {
                case "PRODUCTO":

                    return prenda.GrabarImagenPrenda(Convert.ToInt32(Datos), Archivos);

                case "TRASH":
                    return prenda.EliminarImagenPrenda(nombreArchivo);
                default:
                    return "No se ha definido el proceso en la base de datos";
            }
        }
    }
}