using WebShop13kVizsga.Dto;
using WebShop13kVizsga.Persistence;

namespace WebShop13kVizsga.Model
{
    public class OrderModel
    {
        private readonly DataDbContext _context;

        public OrderModel(DataDbContext context)
        {
            _context = context;
        }

        #region OrderList
        public IEnumerable<OrderDto> OrderList()
        {
            return _context.Orders
                .Select(z => new OrderDto
                {
                    OrderId = z.OrderId,
                    CartId = z.CartId,
                    UserId = z.UserId,
                    TotalPrice = z.TotalPrice,
                    TargetAddress = z.TargetAddress,
                    TargetPhone = z.TargetPhone,
                    Status = z.Status,
                    Date = z.Date
                })
                .ToList();
        }
        #endregion

        #region OrderById
        public OrderDto? GetOrder(int id)
        {
            return _context.Orders
                .Where(a => a.OrderId == id)
                .Select(a => new OrderDto
                {
                    OrderId = a.OrderId,
                    CartId = a.CartId,
                    UserId = a.UserId,
                    TotalPrice = a.TotalPrice,
                    TargetAddress = a.TargetAddress,
                    TargetPhone = a.TargetPhone,
                    Status = a.Status,
                    Date = a.Date
                })
                .FirstOrDefault();
        }
        #endregion

        #region NewOrder
        public void NewOrder(OrderDto dto)
        {
            if (_context.Orders.Any(x => x.OrderId == dto.OrderId && dto.OrderId != 0))
            {
                throw new InvalidOperationException("Ilyen rendelés már létezik.");
            }

            using var trx = _context.Database.BeginTransaction();
            try
            {
                var order = new Order
                {
                    CartId = dto.CartId,
                    UserId = dto.UserId,
                    TotalPrice = dto.TotalPrice,
                    TargetAddress = dto.TargetAddress,
                    TargetPhone = dto.TargetPhone,
                    Date = DateTimeOffset.UtcNow,
                    Status = OrderStatus.ItemQuantityOnHold
                };

                _context.Orders.Add(order);
                _context.SaveChanges();

                trx.Commit();
            }
            catch
            {
                trx.Rollback();
                throw;
            }
        }
        #endregion

        #region ConfirmData(phone+address)
        public void ConfirmData(int id, ConfirmOrderDataDto dto)
        {
            var order = _context.Orders.First(o => o.OrderId == id);

            order.TargetAddress = dto.TargetAddress;
            order.TargetPhone = dto.TargetPhone;
            order.Status = OrderStatus.DataConfirmed;

            _context.SaveChanges();
        }
        #endregion

        #region PaymentSuccess
        public void PaymentSuccess(int id)
        {
            var order = _context.Orders.First(o => o.OrderId == id);

            if (order.Status != OrderStatus.DataConfirmed)
                throw new InvalidOperationException("Rossz adatok");

            order.Status = OrderStatus.PaymentSuccess;
            _context.SaveChanges();
        }
        #endregion

        #region ConfirmOrder
        public void ConfirmOrder(int id)
        {
            var order = _context.Orders.First(o => o.OrderId == id);

            if (order.Status != OrderStatus.PaymentSuccess)
                throw new InvalidOperationException("Nincs még fizetve");

            order.Status = OrderStatus.OrderConfirmed;
            _context.SaveChanges();
        }
        #endregion

        #region DeleteOrder
        public void DeleteOrder(int id)
        {
            if (!_context.Orders.Any(x => x.OrderId == id))
            {
                throw new InvalidOperationException("Ilyen rendelés nem létezik.");
            }

            using var trx = _context.Database.BeginTransaction();
            try
            {
                var order = _context.Orders.First(o => o.OrderId == id);
                _context.Orders.Remove(order);
                _context.SaveChanges();

                trx.Commit();
            }
            catch
            {
                trx.Rollback();
                throw;
            }
        }
        #endregion
    }
}
