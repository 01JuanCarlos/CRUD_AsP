using Microsoft.AspNetCore.Mvc;
using POOII_CL2_TorresMirandaJuanCarlos.Datos;
using POOII_CL2_TorresMirandaJuanCarlos.Models;


namespace POOII_CL2_TorresMirandaJuanCarlos.Controllers
{
    public class MantenedorController : Controller
    {
        
        Producto Producto=new Producto();
        public IActionResult Listar()
        {
            var oLista = Producto.Listar();
            return View(oLista);
        }

        public IActionResult Agregar()
        {
            return View();

        }
        [HttpPost]
        public IActionResult Agregar(ProductoModel oProducto , IFormFile foto)
        {

            if (!ModelState.IsValid)
                return View();

            var respuesta = Producto.Agregar(oProducto,foto);
            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();

        }



        public IActionResult Actualizar(int IdProducto)
        {
            var oproducto = Producto.Obtener(IdProducto);

            return View(oproducto);
        }

        [HttpPost]
        public IActionResult Actualizar(ProductoModel oProducto)
        {
            if (!ModelState.IsValid)
                return View();

            var respuesta = Producto.Actualizar(oProducto);
            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

        /// <summary>
        /// ///////////////
        /// </summary>
        /// <p/param>
        /// <returns></returns>
        public IActionResult Eliminar(int IdProducto)
        {
            var oproducto = Producto.Obtener(IdProducto);

            return View(oproducto);
        }

        [HttpPost]
        public IActionResult Eliminar(ProductoModel oProducto)
        {
            var respuesta = Producto.Eliminar(oProducto.id);
            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }


        public IActionResult Buscar()
        {
            
            return View();
        }


        //[HttpPost]
        //public IActionResult BuscarFecha(ProductoModel oProducto)
        //{
        //    if (!ModelState.IsValid)
        //        return View();

        //    var respuesta = Producto.BuscarFecha(oProducto);
        //    if (respuesta)
        //        return RedirectToAction("Listar");
        //    else
        //        return View();
        //}








    }
}
