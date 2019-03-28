using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiFiliado.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiFiliado.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FiliadosController : ControllerBase
    {
        private readonly IRepositorioFiliado _repo;

        public FiliadosController(IRepositorioFiliado repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var filiados = _repo.Filiados.ToList();

            return Ok(filiados);
        }


    }
}