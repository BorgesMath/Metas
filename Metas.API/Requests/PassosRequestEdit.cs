namespace Metas.API.Requests;

public record PassosRequestEdit(
    string? Name = null,  // O nome pode ser alterado
    bool? Continuo = null,  // Status contínuo pode ser editado
    int? Tempo = null,  // Tempo opcional para edição
    string? Descricao = null  // Descrição opcional
);