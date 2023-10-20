using System.Threading;
using System.Threading.Tasks;

namespace CompanyName.SampleApi.Application.Interfaces.Data;

public interface ISalesUnitOfWork
{
    IProductRepository ProductRepository { get; }
    IOrderRepository OrderRepository { get; }
    IShoppingCartRepository ShoppingCartRepository { get; }
    Task<int> SaveChangesAsync();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}