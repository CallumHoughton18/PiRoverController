using Moq;
using Ninject;
using NUnit.Framework;
using PiRoverController.Factories;
using PiRoverController.PresentationLogic.Interfaces;
using PiRoverController.Tests.Stubs;
using System;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PiRoverController.Tests
{

    //It would probably be better to wrap ninject in my own IIoCContainer interface, so i can swap out the DI framework at ease. But as that's already more overkill
    //in an already over engineered solution I've opted to skip that, YAGNI and all.
    [TestFixture]
    [Category("unittests")]
    public class ViewFactoryTests
    {
        IKernel _container;
        Mock<INavigator> _mockNav = new Mock<INavigator>();

        [SetUp]
        public void SetupContainer()
        {
            var settings = new NinjectSettings() { LoadExtensions = false };
            _container = new StandardKernel(settings);
            _container.Bind<INavigator>().ToConstant(_mockNav.Object);
        }

        [Test]
        public void Register_Valid()
        {
            var sut = GenerateBasicViewFactory(_container);
            sut.Register<TestViewModel, TestPage>();

            Assert.That(sut.Map.Count, Is.EqualTo(1));
            Assert.That(sut.Map[typeof(TestViewModel)], Is.EqualTo(typeof(TestPage)));
        }

        [Test]
        public void RegisterAndCache_Valid()
        {
            var sut = GenerateBasicViewFactory(_container);
            sut.RegisterAndCache<TestViewModel, TestPage>();

            Assert.That(sut.Map.Count, Is.EqualTo(1));
            Assert.That(sut.Map[typeof(TestViewModel)], Is.EqualTo(typeof(TestPage)));

            Assert.That(sut.CachedPages.Count, Is.EqualTo(1));
            Assert.That(sut.CachedPages[typeof(TestViewModel)], Is.TypeOf(typeof(TestPage)));
        }

        [Test]
        public void Resolve_Valid()
        {
            var sut = GenerateBasicViewFactory(_container);
            sut.Register<TestViewModel, TestPage>();

            var page = sut.Resolve<TestViewModel>();

            Assert.That(page, Is.TypeOf(typeof(TestPage)));
            Assert.That(page.BindingContext, Is.TypeOf(typeof(TestViewModel)));


        }

        [Test]
        public void Resolve_HasCachedPage_Valid()
        {
            var mockVM = new Mock<TestViewModel>(_mockNav.Object);
            _container.Bind<TestViewModel>().ToConstant(mockVM.Object);

            var sut = GenerateBasicViewFactory(_container);

            sut.RegisterAndCache<TestViewModel, TestPage>();
            var page = sut.Resolve<TestViewModel>();
            var page2 = sut.Resolve<TestViewModel>();

            mockVM.Verify(x => x.InitialLoad(), Times.Once);

        }

        [Test]
        public void Resolve_NoCachedPage_Valid()
        {
            var mockVM = new Mock<TestViewModel>(_mockNav.Object);
            _container.Bind<TestViewModel>().ToConstant(mockVM.Object);

            var sut = GenerateBasicViewFactory(_container);

            sut.Register<TestViewModel, TestPage>();
            var page = sut.Resolve<TestViewModel>();
            var page2 = sut.Resolve<TestViewModel>();

            mockVM.Verify(x => x.InitialLoad(), Times.Exactly(2));
        }

        [Test]
        public void Resolve_NoPageFound()
        {
            var sut = GenerateBasicViewFactory(_container);

            Assert.That(() => sut.Resolve<TestViewModel>(), Throws.InstanceOf<InvalidOperationException>());
        }

        private ViewFactory GenerateBasicViewFactory(IKernel container)
        {
            return new ViewFactory(container);
        }
    }
}
