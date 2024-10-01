namespace Metas.API.Requests;

public record MetaRequest(
    string Name,  // Nome da Meta
    bool Continuo,  // Se a meta é contínua ou não
    int? Tempo,  // Tempo dedicado (opcional)
    string? Descricao  // Descrição da meta
);