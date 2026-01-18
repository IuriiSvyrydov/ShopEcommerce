

using MediatR;
using Ordering.Application.DTO;

namespace Ordering.Application.Features.Queries;

public record GetOrderListQuery(string UserName): IRequest<List<OrderDTO>>;

