﻿using System.Net;
using FastEndpoints;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Endpoints.Orders.Delete;

public class DeleteOrderCommandValidator: Validator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(x=>x.Id)
            .NotEmpty()
            .MustAsync(IsOrderExist)
            .WithMessage("The order with the given ID does not exist.")
            .WithErrorCode(HttpStatusCode.BadRequest.ToString());
    }

    private async Task<bool> IsOrderExist(int id, CancellationToken ct)
    {
        using var scope = CreateScope();
        var dbContext = scope.Resolve<EcommerceDbContext>();

        return await dbContext.Orders.AnyAsync(x => x.Id == id, ct);
    }
}