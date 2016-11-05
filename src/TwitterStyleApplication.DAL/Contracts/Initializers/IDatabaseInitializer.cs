using System.Threading.Tasks;

namespace TwitterStyleApplication.DAL.Contracts.Initializers
{
	public interface IDatabaseInitializer
	{
		Task Seed();
	}
}
