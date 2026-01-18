
using API.Grpc.Protos;
using Basket.Application.Interfaces;

namespace Basket.Infrastructure.Services;

public sealed class DiscountGrpcService : IDiscountGrpcService
{
    private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoServiceClient;
    public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
    {
        _discountProtoServiceClient = discountProtoServiceClient;
    }
    public async Task<CouponModel> GetDiscountAsync(string productName)
    {
        var request = new GetDiscountRequest { ProductName = productName };
        var response = await _discountProtoServiceClient.GetDiscountAsync(request);
        return response;

    }
}
