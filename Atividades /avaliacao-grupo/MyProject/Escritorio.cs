using System;
using System.Collections.Generic;

namespace Namespace
{
    public class Escritorio
    {
        private List<Advogado> advogados;
        private List<Cliente> clientes;

        public List<Advogado> Advogados => advogados;
        public List<Cliente> Clientes => clientes;

        public Escritorio()
        {
            advogados = new List<Advogado>();
            clientes = new List<Cliente>();
        }

        public void AdicionarAdvogado(Advogado advogado)
        {
            try
            {
                if (!advogados.Exists(a => a.CPF == advogado.CPF) && !advogados.Exists(a => a.CNA == advogado.CNA))
                {
                    advogados.Add(advogado);
                    Console.WriteLine("Advogado adicionado com sucesso!");
                }
                else
                {
                    throw new RepeatedRegisterAttorneyException();
                }
            }
            catch (RepeatedRegisterAttorneyException ex)
            {
                Console.WriteLine($"Erro ao adicionar advogado: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
            }
        }

        public void AdicionarCliente(Cliente cliente)
        {
            try
            {
                if (!clientes.Exists(c => c.CPF == cliente.CPF))
                {
                    clientes.Add(cliente);
                    Console.WriteLine("Cliente adicionado com sucesso!");
                }
                else
                {
                    throw new RepeatedRegisterClientException();
                }
            }
            catch (RepeatedRegisterClientException ex)
            {
                Console.WriteLine($"Erro ao adicionar cliente: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
            }
        }

        public void ListarAdvogados()
        {
            Console.WriteLine("Lista de Advogados:");
            foreach (var advogado in advogados)
            {
                Console.WriteLine($"Nome: {advogado.Nome}, Data de Nascimento: {advogado.DataNascimento}, CPF: {advogado.CPF}, CNA: {advogado.CNA}");
            }
            Console.WriteLine();
        }

        public void ListarClientes()
        {
            Console.WriteLine("Lista de Clientes:");
            foreach (var cliente in clientes)
            {
                Console.WriteLine($"Nome: {cliente.Nome}, Data de Nascimento: {cliente.DataNascimento}, CPF: {cliente.CPF}, Estado Civíl: {cliente.EstadoCivil}, Profissão: {cliente.Profissao}");
            }
        }

        public void DeletarAdvogado(string cpf)
        {
            try
            {
                var cpfAdvogado = advogados.Find(a => a.CPF == cpf);

                if (cpfAdvogado != null)
                {
                    advogados.Remove(cpfAdvogado);
                    Console.WriteLine("Advogado deletado com sucesso!");
                }
                else
                {
                    throw new CpfNotFoundException();
                }
            }
            catch (CpfNotFoundException ex)
            {
                Console.WriteLine($"Erro ao deletar advogado: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado ao deletar advogado: {ex.Message}");
            }
        }

        public void DeletarCliente(string cpf)
        {
            try
            {
                var cpfCliente = clientes.Find(c => c.CPF == cpf);

                if (cpfCliente != null)
                {
                    clientes.Remove(cpfCliente);
                    Console.WriteLine("Cliente deletado com sucesso!");
                }
                else
                {
                    throw new CpfNotFoundException();
                }
            }
            catch (CpfNotFoundException ex)
            {
                Console.WriteLine($"Erro ao deletar cliente: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado ao deletar cliente: {ex.Message}");
            }
        }
    }
}
