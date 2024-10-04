namespace Metas.API.Requests;

public record PassosRequest(
    string Name,  // Nome do passo
    bool Continuo,  // Se o passo é contínuo ou não
    int Tempo,  // Tempo dedicado ao passo
    string? Descricao,  // Descrição do passo (opcional)
    int MetaID
);