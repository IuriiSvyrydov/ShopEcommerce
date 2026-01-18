using Basket.Application.Features.Responses;
using MediatR;

namespace Application.Features.Queries;

public record GetBasketByUserNameQuery(string UserName) : IRequest<ShoppingCartResponse>;

