using System.Linq;
using AutoFixture;
using AutoFixture.NUnit3;

namespace Restaurant.Client.UnitTests
{
	public class AutoDomainDataAttribute : AutoDataAttribute
	{
		public AutoDomainDataAttribute() : base(CustomFixture())
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
