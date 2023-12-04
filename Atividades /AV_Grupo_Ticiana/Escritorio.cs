namespace EscritorioJuridico;
public class Escritorio
{
    private List<Advogado> advogados;
    public List<Advogado> Advogados => advogados;
    private List<Cliente> clientes;
    public List<Cliente> Clientes => clientes;
    private List<Documento> documentos;
    public List<Documento> Documentos => documentos;
    
    // --------------------
    private List<CasoJuridico> casosJuridicos;
    public List<CasoJuridico> CasosJuridicos => casosJuridicos;
    // --------------------

    public Escritorio()
    {
        advogados = new List<Advogado>();
        clientes = new List<Cliente>();
        documentos = new List<Documento>();
    // --------------------
        casosJuridicos = new List<CasoJuridico>();
    // --------------------
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

    // --------------------
    public void AdicionarCasoJuridico(CasoJuridico casoJuridico)
    {
        casosJuridicos.Add(casoJuridico);
        Console.WriteLine("Caso Jurídico adicionado com sucesso!");
    }


    public void ListarCasosJuridicos()
    {
        Console.WriteLine("Lista de Casos Jurídicos:");
        foreach (var casoJuridico in casosJuridicos)
        {
            ExibirInformacoesCasoJuridico(casoJuridico);
            Console.WriteLine();
        }
    }

    public void DeletarCasoJuridico(DateTime abertura)
    {
        var casoJuridico = casosJuridicos.Find(c => c.Abertura == abertura);

        if (casoJuridico != null)
        {
            casosJuridicos.Remove(casoJuridico);
            Console.WriteLine("Caso Jurídico deletado com sucesso!");
        }
        else
        {
            Console.WriteLine("Caso Jurídico não encontrado.");
        }
    }

    private void ExibirInformacoesCasoJuridico(CasoJuridico casoJuridico)
    {
        Console.WriteLine($"Abertura: {casoJuridico.Abertura:dd/MM/yyyy}");
        Console.WriteLine($"Probabilidade de Sucesso: {casoJuridico.ProbabilidadeSucesso}%");
        Console.WriteLine($"Encerramento: {casoJuridico.Encerramento:dd/MM/yyyy}");
        Console.WriteLine($"Status: {casoJuridico.Status ?? "N/A"}");

        if (casoJuridico.Cliente != null)
        {
            Console.WriteLine("Informações do Cliente:");
            Console.WriteLine($"Nome: {casoJuridico.Cliente.Nome}");
            Console.WriteLine($"CPF: {casoJuridico.Cliente.CPF}");
        }

        if (casoJuridico.Advogados != null && casoJuridico.Advogados.Count > 0)
        {
            Console.WriteLine("Advogados Envolvidos:");
            foreach (var advogado in casoJuridico.Advogados)
            {
                Console.WriteLine($"Nome: {advogado.Nome}, CPF: {advogado.CPF}");
            }
        }

        if (casoJuridico.Documentos != null && casoJuridico.Documentos.Count > 0)
        {
            Console.WriteLine("Documentos Associados:");
            foreach (var documento in casoJuridico.Documentos)
            {
                casoJuridico.ExibirInformacoesDocumento(documento);
                Console.WriteLine();
            }
        }

        if (casoJuridico.Custos != null && casoJuridico.Custos.Count > 0)
        {
            Console.WriteLine("Custos Associados:");
            foreach (var custo in casoJuridico.Custos)
            {
                Console.WriteLine($"Valor: {custo.Custos}, Descrição: {custo.Descricao}");
            }
        }
    }
    // --------------------
}

