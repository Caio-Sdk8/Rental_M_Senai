using Senai.Rental.WebApi.Domains;
using Senai.Rental.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Rental.WebApi.Interfaces
{
    interface IAluguelRepository
    {
        public List<AluguelDomain> listar();

        public AluguelDomain buscarPorId(int id);

        public void inserir(AluguelDomain aluguel);

        public void atualizar(int id, AluguelDomain aluguelAtualizado);

        public void deletar(int id);
    }
}
