using Senai.Rental.WebApi.Domains;
using Senai.Rental.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Rental.WebApi.Repositories
{
    public class AluguelRepository : IAluguelRepository
    {
        private string stringConexao = @"Data Source=localhost\SQLEXPRESS; initial catalog=M_Rental; integrated security=true";

        public void atualizar(int id, AluguelDomain aluguelAtualizado)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryAtualizar = @"UPDATE ALUGUEL SET dataAluguel = '@dataAluguelAtualizada', idVeiculo = @idVeiculoAtualizado WHERE idAluguel = @id";

                using (SqlCommand cmd = new SqlCommand(queryAtualizar, con))
                {
                    cmd.Parameters.AddWithValue("@idVeiculoAtualizado", aluguelAtualizado.idVeiculo);
                    cmd.Parameters.AddWithValue("@dataAluguelAtualizada", aluguelAtualizado.dataAluguel);
                    cmd.Parameters.AddWithValue("@id", id);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public AluguelDomain buscarPorId(int id)
        {
            AluguelDomain aluguel = new AluguelDomain();

           using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryBuscar = @"SELECT idAluguel, idVeiculo, idCliente, dataAluguel FROM ALUGUEL WHERE idAluguel = @id";

                using (SqlCommand cmd = new SqlCommand(queryBuscar, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    con.Open();
                    SqlDataReader leitor;

                        leitor = cmd.ExecuteReader();

                    if(leitor.Read())
                    {

                        aluguel.idAluguel = Convert.ToInt32(leitor[0]);
                        aluguel.idCliente = Convert.ToInt32(leitor[1]);
                        aluguel.idVeiculo = Convert.ToInt32(leitor[2]);
                        aluguel.dataAluguel = Convert.ToDateTime(leitor[3]);

                        return aluguel;
                    }else{
                     return null; }
                }
            }
        }

        public void deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = @"DELETE FROM ALUGUEL WHERE idAluguel = @id";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void inserir(AluguelDomain novoAluguel)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = @"INSERT INTO ALUGUEL dataAluguel VALUES('@dataAluguel')";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@dataAluguel", novoAluguel.dataAluguel);

                    cmd.ExecuteNonQuery();
                }
            }

            
        }

        public List<AluguelDomain> listar()
        {
            List<AluguelDomain> listaAlugueis = new List<AluguelDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = @"SELECT idAluguel, idVeiculo, idCliente, nomeEmpresa 'Empresa', nomeModelo 'Modelo Veiculo', nomeMarca 'Marca Veiculo', placaVeiculo 'Placa', dataAluguel 'Data', nomeCliente 'Cliente' FROM ALUGUEL
JOIN CLIENTE
ON CLIENTE.idCliente = ALUGUEL.idCliente
JOIN VEICULO
ON VEICULO.idVeiculo = ALUGUEL.idVeiculo
JOIN EMPRESA
ON EMPRESA.idEmpresa = VEICULO.idEmpresa
JOIN MODELO
ON MODELO.idModelo = Veiculo.idModelo
JOIN MARCA
ON MARCA.idMarca = MODELO.idModelo";

                con.Open();

                SqlDataReader leitor;
                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {

                    leitor = cmd.ExecuteReader();

                    while (leitor.Read())
                    {
                        AluguelDomain aluguel = new AluguelDomain()
                        {
                            idAluguel = Convert.ToInt32(leitor[0]),
                            idCliente = Convert.ToInt32(leitor[2]),
                            idVeiculo = Convert.ToInt32(leitor[1]),
                            dataAluguel = Convert.ToDateTime(leitor[7])
                        };

                        listaAlugueis.Add(aluguel);
                    }
                };
            };

            return listaAlugueis;
        }
    }
}
