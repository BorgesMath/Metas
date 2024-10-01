namespace Metas.API.Requests;

public record MetaRequestEdit(
    string? Name = null,  // O nome pode ser alterado, mas é opcional
    bool? Continuo = null,  // O status contínuo pode ser editado
    int? Tempo = null,  // O tempo também é opcional durante a edição
    string? Descricao = null  // A descrição pode ser alterada ou removida
);