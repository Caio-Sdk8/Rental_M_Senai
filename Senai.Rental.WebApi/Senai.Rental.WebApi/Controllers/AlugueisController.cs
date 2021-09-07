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
    public class AlugueisController : ControllerBase
    {
        private IAluguelRepository _AluguelRepository { get; set; }

        public AlugueisController() {

            _AluguelRepository = new AluguelRepository();
        }

        [HttpGet]
        public IActionResult get()
        {
            List<AluguelDomain> ListaAlugueis = _AluguelRepository.listar();

            return Ok(ListaAlugueis);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            AluguelDomain aluguelPesquisado = _AluguelRepository.buscarPorId(id);

            if (aluguelPesquisado == null)
            {
                return NotFound("Nenhum Aluguel encontrado!");
            }

            return Ok(aluguelPesquisado);
        }

        [HttpPost]
        public IActionResult Post(AluguelDomain novoAluguel)
        {
            _AluguelRepository.inserir(novoAluguel);

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public IActionResult PutUrl(int id, AluguelDomain generoAtualizado)
        {
            AluguelDomain aluguelBuscado = _AluguelRepository.buscarPorId(id);

            if (aluguelBuscado == null)
            {
                return NotFound
                    (new
                    {
                        mensagem = "Aluguel não encontrado!",
                        erro = true
                    });
            }

            try
            {
                _AluguelRepository.atualizar(id, generoAtualizado);

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
            _AluguelRepository.deletar(id);

            return StatusCode(204);
        }
    }


}
