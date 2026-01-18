

using API.Grpc.Protos;

namespace Basket.Application.Interfaces;

public interface IDiscountGrpcService
{
    Task<CouponModel> GetDiscountAsync(string productName);
}
