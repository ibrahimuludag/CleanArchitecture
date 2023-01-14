using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace CleanArchitecture.Infrastructure.Controllers;

public static class ApiConventions {
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesDefaultResponseType(typeof(ProblemDetails))]
    public static void Create(
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
        [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object request)
    {
        // Method intentionally left empty.
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesDefaultResponseType(typeof(ProblemDetails))]
    public static void Delete(
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
        [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object id)
    {
        // Method intentionally left empty.
    }


    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesDefaultResponseType(typeof(ProblemDetails))]
    [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
    public static void Get([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
        [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        int id)
    {
        // Method intentionally left empty.
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesDefaultResponseType(typeof(ProblemDetails))]
    [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
    public static void List()
    {
        // Method intentionally left empty.
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesDefaultResponseType(typeof(ProblemDetails))]
    [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
    public static void Update(
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
        [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
        object request)
    {
        // Method intentionally left empty.
    }
}
