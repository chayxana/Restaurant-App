using Autofac;
using Moq;
using NUnit.Framework;
using ReactiveUI;
using Restaurant.Abstractions.Facades;
using Restaurant.Abstractions.Services;
using Restaurant.Services;
using Restaurant.ViewModels;

namespace Restaurant.UnitTests.Services
{
    [TestFixture]
    public class NavigationServiceTest
    {
        private INavigationService _navigationService;
        private Mock<IViewFor<WelcomeViewModel>> _welcoleStartPageMock;
        private Mock<INavigationFacade> _navigationFacade;

        [OneTimeSetUp]
        public void Init()
        {
            _welcoleStartPageMock = new Mock<IViewFor<WelcomeViewModel>>();
            _navigationFacade = new Mock<INavigationFacade>();

            var builder = new ContainerBuilder();
            builder.Register(x => _welcoleStartPageMock.Object).As<IViewFor<WelcomeViewModel>>();
            _navigationService = new NavigationService(builder.Build(), _navigationFacade.Object);
        }

        [Test]
        public void Navigate_async_with_valid_vm_shlould_navigate_page()
        {
            //var vm = new WelcomeViewModel();
            //_navigationService.NavigateAsync(vm);
            //_navigationService.CurrentPage.Should().Be(_welcoleStartPageMock.Object);
        }

        [Test]
        public void Navigate_modal_async_with_valid_vm_should_popup_page()
        {
            //var vm = new WelcomeViewModel();
            //_navigationService.NavigateModalAsync(vm);
            //_navigationService.CurrentPage.Should().Be(_welcoleStartPageMock.Object);
        }
    }
}
