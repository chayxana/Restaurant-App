using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace Restaurant.Server.Api.UnitTests
{
	public class AutoDomainDataAttribute : AutoDataAttribute
	{
		public AutoDomainDataAttribute() : base(CustomFixture)
		{
		}

		private static IFixture CustomFixture()
		{
			var fixture = new Fixture();

			fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
				.ToList()
				.ForEach(b => fixture.Behaviors.Remove(b));

			fixture.Behaviors.Add(new OmitOnRecursionBehavior());

			return fixture;
		}
	}
}
