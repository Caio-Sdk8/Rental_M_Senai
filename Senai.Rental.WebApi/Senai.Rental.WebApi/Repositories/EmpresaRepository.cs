using Senai.Rental.WebApi.Domains;
using Senai.Rental.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Rental.WebApi.Repositories
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private string stringConexao = @"Data Source=localhost\SQLEXPRESS; initial catalog=M_Rental; integrated security=true";

        public void atualizar(int id, EmpresaDomain empresaAtualizada)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryAtualizar = @"UPDATE EMPRESA SET nomeEmpresa = '@nomeEmpresaAtualizada' WHERE @id";

                using (SqlCommand cmd = new SqlCommand(queryAtualizar, con))
                {
                    cmd.Parameters.AddWithValue("@nomeEmpresaAtualizada", empresaAtualizada.nomeEmpresa);
                    cmd.Parameters.AddWithValue("@id", id);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public EmpresaDomain buscarPorId(int id)
        {
            EmpresaDomain empresa = new EmpresaDomain();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryBuscar = @"SELECT idEmpresa, nomeEmpresa 'Empresa', FROM EMPRESA
                WHERE idEmpresa = @id";

                using (SqlCommand cmd = new SqlCommand(queryBuscar, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    con.Open();
                    SqlDataReader leitor;

                    leitor = cmd.ExecuteReader();

                    if (leitor.Read())
                    {

                        empresa.idEmpresa = Convert.ToInt32(leitor[0]);
                        empresa.nomeEmpresa = Convert.ToString(leitor[1]);

                        return empresa;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public void deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = @"DELETE FROM EMPRESA WHERE idEmpresa = @id";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void inserir(EmpresaDomain empresa)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = @"INSERT INTO EMPRESA idEmpresa, nomeEmpresa VALUES(@idEmpresa, '@nomeEmpresa')";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@idEmpresa", empresa.idEmpresa);
                    cmd.Parameters.AddWithValue("@nomeEmpresa", empresa.nomeEmpresa);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<EmpresaDomain> listar()
        {
            List<EmpresaDomain> listaEmpresas = new List<EmpresaDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = @"SELECT idEmpresa, nomeEmpresa 'Empresa' FROM EMPRESA";

                con.Open();

                SqlDataReader leitor;
                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {

                    leitor = cmd.ExecuteReader();

                    while (leitor.Read())
                    {
                        EmpresaDomain empresa = new EmpresaDomain()
                        {
                            idEmpresa = Convert.ToInt32(leitor[0]),
                            nomeEmpresa = Convert.ToString(leitor[1]),
                        };

                        listaEmpresas.Add(empresa);
                    }
                };
            };

            return listaEmpresas;
        }
    }
}
