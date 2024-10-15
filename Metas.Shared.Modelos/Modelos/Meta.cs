using Metas.Shared.Modelos.Validador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metas.Shared.Modelos.Modelos;

public class Meta(string nome) : Valida
{
    public int Id { get; set; }

    public virtual ICollection<Passos> Passos { get; set; } = [];

    public bool Status { get; set; } = false;

    public string Nome { get; set; } = nome;

    public int? Tempo { get; set; }

    public bool Continuo { get; set; }

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

