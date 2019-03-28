using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiFiliado.Models;
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

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(string id)
        {
            var filiado = _repo.Filiados
                .Where(p => p.Id == new Guid(id))
                .FirstOrDefault();

            if (filiado == null) return NotFound();

            return Ok(filiado);
        }

        [HttpPost]
        public IActionResult Post(Filiado model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _repo.Add(model);
                    return Created(string.Empty, result);
                }
                catch (Exception ex)
                {
                    Response.StatusCode = 500;
                    return new JsonResult(new
                    {
                        erro = ex.Message
                    });
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        public IActionResult Put(string id, Filiado model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var filiado = _repo.Filiados.FirstOrDefault(p => p.Id == new Guid(id));

                    var result = _repo.Add(model);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    Response.StatusCode = 500;
                    return new JsonResult(new
                    {
                        erro = ex.Message
                    });
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            var filiado = _repo.Filiados.FirstOrDefault(p => p.Id == new Guid(id));
            _repo.Delete(filiado);

            return Ok();
        }

    }
}