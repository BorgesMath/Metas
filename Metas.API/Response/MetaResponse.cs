using Metas.Shared.Modelos.Modelos;

namespace Metas.API.Response;

public record MetaResponse(string Nome, int? Tempo, string? Descricao,  bool Status, ICollection<PassoResponse> Passos);
