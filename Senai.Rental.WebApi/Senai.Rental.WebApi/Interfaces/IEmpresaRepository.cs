using Senai.Rental.WebApi.Domains;
using Senai.Rental.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Rental.WebApi.Interfaces
{
    interface IEmpresaRepository
    {
        public List<EmpresaDomain> listar();

        public EmpresaDomain buscarPorId(int id);

        public void inserir(EmpresaDomain empresa);

        public void atualizar(int id, EmpresaDomain empresaAtualizada);

        public void deletar(int id);
    }
}
