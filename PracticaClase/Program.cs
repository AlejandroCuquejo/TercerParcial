using Infraestructura.Conexiones;
using Servicios.ContactosService;

CiudadService ciudadService = new CiudadService("Server=localhost;Port=5432;User Id=postgres;Password=6408;Database=API_NET_Core;");
/*/Agregar datos a la DB
ciudadService.insertarCiudad(new Infraestructura.Modelos.CiudadModel
{
    descripcion = "dsdadas",
    nombre_cort = "da",
    estado = "Inactivo" 
});/*/


/*/Mostrar datos en la DB
var ciudad = ciudadService.obtenerCiudad(1);
Console.WriteLine($"Descripcion: {ciudad.descripcion}, Nombre corto: {ciudad.nombre_cort}");
/*/

/*/Modificar datos en la DB
var ciudad = ciudadService.obtenerCiudad(1);
ciudad.descripcion = "Ciudad del este";
ciudad.nombre_cort = "CDE";
ciudad.estado = "Inactivo";
ciudadService.modificarCiudad(ciudad);
*/
//elimnar ciudad


Console.WriteLine("...");
