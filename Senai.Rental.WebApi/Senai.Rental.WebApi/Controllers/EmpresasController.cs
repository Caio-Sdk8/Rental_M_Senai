using Microsoft.AspNetCore.Mvc;
using Senai.Rental.WebApi.Domains;
using Senai.Rental.WebApi.Interfaces;
using Senai.Rental.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Rental.WebApi.Controllers
{
    [Produces("application/json")]

    [Route("api/[controller]")]

    [ApiController]
    public class EmpresasController : ControllerBase
    {
        private IEmpresaRepository _EmpresaRepository { get; set; }

        public EmpresasController()
        {

            _EmpresaRepository = new EmpresaRepository();
        }

        [HttpGet]
        public IActionResult get()
        {
            List<EmpresaDomain> ListaEmpresas = _EmpresaRepository.listar();

            return Ok(ListaEmpresas);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            EmpresaDomain empresaPesquisada = _EmpresaRepository.buscarPorId(id);

            if (empresaPesquisada == null)
            {
                return NotFound("Nenhuma Empresa encontrada!");
            }

            return Ok(empresaPesquisada);
        }

        [HttpPost]
        public IActionResult Post(EmpresaDomain novaEmpresa)
        {
            _EmpresaRepository.inserir(novaEmpresa);

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public IActionResult PutUrl(int id, EmpresaDomain empresaAtualizada)
        {
            EmpresaDomain empresaBuscada = _EmpresaRepository.buscarPorId(id);

            if (empresaBuscada == null)
            {
                return NotFound
                    (new
                    {
                        mensagem = "Empresa não encontrada!",
                        erro = true
                    });
            }

            try
            {
                _EmpresaRepository.atualizar(id, empresaAtualizada);

                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        [HttpDelete("excluir/{id}")]
        public IActionResult Delete(int id)
        {
            _EmpresaRepository.deletar(id);

            return StatusCode(204);
        }
    }
}
