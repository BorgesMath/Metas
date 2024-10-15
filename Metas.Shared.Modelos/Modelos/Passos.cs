using Metas.Shared.Modelos.Validador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Metas.Shared.Modelos.Modelos;

public class Passos(string nome) : Valida
{
    public int Id { get; set; }  

    public int MetaID { get; set; }

    [JsonIgnore]
    public virtual Meta? Meta { get; set; }  

    public string Nome { get; set; } = nome;  

    public bool Continuo { get; set; }  

    public int Tempo { get; set; }  

    public bool Status { get; set; }  

    public string? Descricao { get; set; }

    protected override void Validar()
    {
        if (string.IsNullOrWhiteSpace(Nome))
        {
            Erros.RegistrarErro("O nome do passo é obrigatório.");
        }

        if (Tempo <= 0)
        {
            Erros.RegistrarErro("O tempo deve ser maior que zero.");
        }

        // Adicione mais validações conforme necessário
    }
}
