using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Restaurant.Abstractions.Api;
using Restaurant.Abstractions.DataTransferObjects;
using Restaurant.Core.ViewModels;

namespace Restaurant.Core.UnitTests.ViewModels
{
    public class OrdersViewModelTest : BaseAutoMockedTest<OrdersViewModel>
    {
        [Test, AutoDomainData]
        public async Task Load_should_load_items_and_set_to_orders(IEnumerable<OrderDto> orders)
        {
            var viewModel = ClassUnderTest;
            GetMock<IOrdersApi>().Setup(x => x.GetAll()).Returns(Task.FromResult(orders));

            await viewModel.LoadOrders();

            Assert.That(viewModel.Orders.Count, Is.EqualTo(orders.Count()));
        }
    }
}
