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
    public class VeiculosController : ControllerBase
    {
        private IVeiculoRepository _VeiculoRepository { get; set; }

        public VeiculosController()
        {
            _VeiculoRepository = new VeiculoRepository();
        }

        [HttpGet]
        public IActionResult get()
        {
            List<VeiculoDomain> ListaVeiculos = _VeiculoRepository.listar();

            return Ok(ListaVeiculos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            VeiculoDomain veiculoPesquisado = _VeiculoRepository.buscarPorId(id);

            if (veiculoPesquisado == null)
            {
                return NotFound("Nenhum Veiculo encontrado!");
            }

            return Ok(veiculoPesquisado);
        }

        [HttpPost]
        public IActionResult Post(VeiculoDomain novoVeiculo)
        {
            _VeiculoRepository.inserir(novoVeiculo);

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public IActionResult PutUrl(int id, VeiculoDomain veiculoAtualizado)
        {
            VeiculoDomain veiculoBuscado = _VeiculoRepository.buscarPorId(id);

            if (veiculoBuscado == null)
            {
                return NotFound
                    (new
                    {
                        mensagem = "Veiculo não encontrado!",
                        erro = true
                    });
            }

            try
            {
                _VeiculoRepository.atualizar(id, veiculoAtualizado);

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
            _VeiculoRepository.deletar(id);

            return StatusCode(204);
        }
    }
}
