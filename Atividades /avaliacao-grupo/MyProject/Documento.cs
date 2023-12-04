using System;
using System.Collections.Generic;
using System.Linq;

namespace Namespace
{
    public class Documento
    {
        public DateTime DataDeModificacao { get; set; }
        public int Codigo { get; set; }
        public string? Tipo { get; set; }
        public string? Descricao { get; set; }

        public Documento(DateTime dataDeModificacao, int codigo, string? tipo, string? descricao)
        {
            DataDeModificacao = dataDeModificacao;
            Codigo = codigo;
            Tipo = tipo;
            Descricao = descricao;
        }

        public static List<string> ObterTop10TiposDocumentos(List<Documento> documentos)
        {
            var top10Tipos = documentos.GroupBy(d => d.Tipo)
                                       .OrderByDescending(g => g.Count())
                                       .Take(10)
                                       .Select(g => g.Key)
                                       .ToList();

            return top10Tipos;
        }
    }
}

