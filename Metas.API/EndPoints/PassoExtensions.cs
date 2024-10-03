using Metas.API.Response;
using Metas.shared.Dados.Banco;
using Metas.Shared.Modelos.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Metas.API.EndPoints;

public static class PassoExtensions
{

    public static void AddEndPointsPassos(this
        WebApplication app)
    {

        var groupBuilder = app.MapGroup("passos")
       .RequireAuthorization()
       .WithTags("PASSOS");



        groupBuilder.MapGet("", ([FromServices] DAL<Passos> dal) =>
        {

            var listaDePassos = dal.Listar();

            if (listaDePassos is null)
            {
                return Results.NotFound();
            }
            var listaDePassosResponse = EntityListToResponseList(listaDePassos);


            return Results.Ok(listaDePassosResponse);


        });

    }

    #region FUNCOES AUXILIARES

    private static List<PassoResponse> EntityListToResponseList(IEnumerable<Passos> listaDepassos)
    {
        return listaDepassos.Select(a => EntityToResponse(a)).ToList();
    }

    private static PassoResponse EntityToResponse(Passos passo)
    {
        return new PassoResponse( passo.Nome, passo.Tempo, passo.Status, passo.Descricao);
    }


    #endregion

}
