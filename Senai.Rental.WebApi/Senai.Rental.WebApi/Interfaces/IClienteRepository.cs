using Senai.Rental.WebApi.Domains;
using Senai.Rental.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Rental.WebApi.Interfaces
{
    interface IClienteRepository
    {
        public List<ClienteDomain> listar();

        public ClienteDomain buscarPorId(int id);

        public void inserir(ClienteDomain Cliente);

        public void atualizar(int id,ClienteDomain clienteAtualizado);

        public void deletar(int id);
    }
}
