namespace Namespace;
public class Pessoa
{
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }
    public string CPF { get; set; }

    public Pessoa(string nome, DateTime dataNascimento, string cpf)
    {
        Nome = nome;
        DataNascimento = dataNascimento;
        CPF = cpf;
    }
  public static Pessoa ObterPessoaPorCpf(List<Pessoa> pessoas, string cpf)
    {
        return pessoas.FirstOrDefault(p => p.CPF == cpf) ?? throw new CpfNotFoundException();
    }


}
