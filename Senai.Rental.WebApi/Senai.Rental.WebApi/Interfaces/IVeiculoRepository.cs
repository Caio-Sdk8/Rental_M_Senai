using Senai.Rental.WebApi.Domains;
using Senai.Rental.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Rental.WebApi.Interfaces
{
    interface IVeiculoRepository
    {
        public List<VeiculoDomain> listar();

        public VeiculoDomain buscarPorId(int id);

        public void inserir(VeiculoDomain veiculo);

        public void atualizar(int id, VeiculoDomain veiculoAtualizado);

        public void deletar(int id);
    }
}
