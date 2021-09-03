using Senai.Rental.WebApi.Domains;
using Senai.Rental.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Rental.WebApi.Repositories
{
    public class VeiculoRepository : IVeiculoRepository
    {
        private string stringConexao = @"Data Source=localhost\SQLEXPRESS; initial catalog=M_Rental; integrated security=true";

        public void atualizar(int id, VeiculoDomain veiculoAtualizado)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryAtualizar = @"UPDATE VEICULO SET placaVeiculo = '@placaVeiculoAtualizado', idModelo = @idModeloAtualizado WHERE idVeiculo = @id";

                using (SqlCommand cmd = new SqlCommand(queryAtualizar, con))
                {
                    cmd.Parameters.AddWithValue("@placaVeiculo", veiculoAtualizado.placaVeiculo);
                    cmd.Parameters.AddWithValue("@idModelo", veiculoAtualizado.idModelo);
                    cmd.Parameters.AddWithValue("@id", id);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public VeiculoDomain buscarPorId(int id)
        {
            VeiculoDomain veiculo = new VeiculoDomain();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryBuscar = @"SELECT idVeiculo, nomeEmpresa 'Empresa', nomeModelo 'Modelo Veiculo', nomeMarca 'Marca Veiculo', placaVeiculo 'Placa',FROM VEICULO
JOIN EMPRESA
ON EMPRESA.idEmpresa = VEICULO.idEmpresa
JOIN MODELO
ON MODELO.idModelo = VEICULO.idModelo
JOIN MARCA
ON MARCA.idMarca = MODELO.idModelo 
WHERE idVeiculo = @id";

                using (SqlCommand cmd = new SqlCommand(queryBuscar, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    con.Open();
                    SqlDataReader leitor;

                    leitor = cmd.ExecuteReader();

                    if (leitor.Read())
                    {

                        veiculo.idVeiculo = Convert.ToInt32(leitor[0]);
                        veiculo.idModelo = Convert.ToInt32(leitor[2]);
                        veiculo.idEmpresa = Convert.ToInt32(leitor[1]);
                        veiculo.placaVeiculo = Convert.ToString(leitor[4]);

                        return veiculo;
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
                string queryDelete = @"DELETE FROM VEICULO WHERE idVeiculo = @id";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void inserir(VeiculoDomain veiculo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = @"INSERT INTO VEICULO idModelo, idEmpresa, placaVeiculo VALUES(@idModelo, @idEmpresa, '@placaVeiculo')";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@idModelo", veiculo.idModelo);
                    cmd.Parameters.AddWithValue("@idEmpresa", veiculo.idEmpresa);
                    cmd.Parameters.AddWithValue("@placaVeiculo", veiculo.placaVeiculo);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<VeiculoDomain> listar()
        {
            List<VeiculoDomain> listaVeiculos = new List<VeiculoDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = @"SELECT idVeiculo, nomeEmpresa 'Empresa', nomeModelo 'Modelo Veiculo', nomeMarca 'Marca Veiculo', placaVeiculo 'Placa',FROM VEICULO
JOIN EMPRESA
ON EMPRESA.idEmpresa = VEICULO.idEmpresa
JOIN MODELO
ON MODELO.idModelo = VEICULO.idModelo
JOIN MARCA
ON MARCA.idMarca = MODELO.idModelo";

                con.Open();

                SqlDataReader leitor;
                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {

                    leitor = cmd.ExecuteReader();

                    while (leitor.Read())
                    {
                        VeiculoDomain veiculo = new VeiculoDomain()
                        {
                            idVeiculo = Convert.ToInt32(leitor[0]),
                            idModelo = Convert.ToInt32(leitor[2]),
                            idEmpresa = Convert.ToInt32(leitor[1]),
                            placaVeiculo = Convert.ToString(leitor[4])
                        };

                        listaVeiculos.Add(veiculo);
                    }
                };
            };

            return listaVeiculos;
        }
    }
}
