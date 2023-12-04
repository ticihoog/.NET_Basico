using System;
using System.Collections.Generic;
using System.Linq;

namespace Namespace
{
    public class CasoJuridico
    {
        public DateTime Abertura { get; set; }
        public float ProbabilidadeSucesso { get; set; }
        public List<Documento>? Documentos { get; set; }
        public List<(float Custos, string Descricao)>? Custos { get; set; }
        public DateTime Encerramento { get; set; }
        public List<Advogado>? Advogados { get; set; }
        public Cliente? Cliente { get; set; }
        public string? Status { get; set; }

        public CasoJuridico(DateTime abertura, float probabilidadeSucesso, List<Documento>? documentos, List<(float Custos, string Descricao)>? custos, DateTime encerramento, List<Advogado>? advogados, Cliente? cliente, string? status)
        {
            Abertura = abertura;
            ProbabilidadeSucesso = probabilidadeSucesso;
            Documentos = documentos;
            Custos = custos;
            Encerramento = encerramento;
            Advogados = advogados;
            Cliente = cliente;
            Status = status;
        }

        public void AdicionarDocumento(Documento documento)
        {
            if (Documentos == null)
            {
                Documentos = new List<Documento>();
            }

            Documentos.Add(documento);
            Console.WriteLine("Documento adicionado ao caso.");
        }

        public float CalcularCustoTotal()
        {
            if (Custos == null)
            {
                return 0;
            }

            return Custos.Sum(custo => custo.Custos);
        }
    }
}
