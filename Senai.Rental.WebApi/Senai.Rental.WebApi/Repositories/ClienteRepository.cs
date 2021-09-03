using Senai.Rental.WebApi.Domains;
using Senai.Rental.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Rental.WebApi.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private string stringConexao = @"Data Source=localhost\SQLEXPRESS; initial catalog=M_Rental; integrated security=true"; 

        public void atualizar(int id,ClienteDomain clienteAtualizado)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryAtualizar = @"UPDATE CLIENTE SET nomeCliente = '@nomeClienteAtualizado', sobrenomeCliente = '@sobrenomeClienteAtualizado', cpfCliente = '@cpfClienteAtualizado' WHERE idCliente = @id";

                using (SqlCommand cmd = new SqlCommand(queryAtualizar, con))
                {
                    cmd.Parameters.AddWithValue("@nomeClienteAtualizado", clienteAtualizado.nomeCliente);
                    cmd.Parameters.AddWithValue("@sobrenomeClienteAtualizado", clienteAtualizado.sobrenomeCliente);
                    cmd.Parameters.AddWithValue("@cpfClienteAtualizado", clienteAtualizado.cpfCliente);
                    cmd.Parameters.AddWithValue("@id", id);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public ClienteDomain buscarPorId(int id)
        {
            ClienteDomain cliente = new ClienteDomain();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryBuscar = @"SELECT idCliente, nomeCliente, sobrenomeCliente, cpfCliente FROM Cliente WHERE idCliente = @id";

                using (SqlCommand cmd = new SqlCommand(queryBuscar, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    con.Open();
                    SqlDataReader leitor;

                    leitor = cmd.ExecuteReader();

                    if (leitor.Read())
                    {

                        cliente.idCliente = Convert.ToInt32(leitor[0]);
                        cliente.nomeCliente = Convert.ToString(leitor[1]);
                        cliente.sobrenomeCliente = Convert.ToString(leitor[2]);
                        cliente.cpfCliente = Convert.ToString(leitor[3]);

                        return cliente;
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
                string queryDelete = @"DELETE FROM CLIENTE WHERE idCliente = @id";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void inserir(ClienteDomain cliente)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = @"INSERT INTO CLIENTE idCliente, nomeCliente, sobrenomeCliente, cpfCliente VALUES(@idCliente, '@nomeCliente', '@sobrenomeCliente', '@cpfCliente')";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@idCliente", cliente.idCliente);
                    cmd.Parameters.AddWithValue("@nomeCliente", cliente.nomeCliente);
                    cmd.Parameters.AddWithValue("@sobrenomeCliente", cliente.sobrenomeCliente);
                    cmd.Parameters.AddWithValue("@cpfCliente", cliente.cpfCliente);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<ClienteDomain> listar()
        {
            List<ClienteDomain> listaClientes = new List<ClienteDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = @"SELECT idCliente, nomeCliente 'NOME', sobrenomeCliente 'Sobrenome', cpfCliente 'CPF' FROM CLIENTE";

                con.Open();

                SqlDataReader leitor;
                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {

                    leitor = cmd.ExecuteReader();

                    while (leitor.Read())
                    {
                        ClienteDomain cliente = new ClienteDomain()
                        {
                            idCliente = Convert.ToInt32(leitor[0]),
                            nomeCliente = Convert.ToString(leitor[1]),
                            sobrenomeCliente = Convert.ToString(leitor[2]),
                            cpfCliente = Convert.ToString(leitor[3])
                        };

                        listaClientes.Add(cliente);
                    }
                };
            };

            return listaClientes;
        }
    }
}
