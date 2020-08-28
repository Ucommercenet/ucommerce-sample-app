using Ucommerce.Infrastructure.Logging;
using Ucommerce.Pipelines;

namespace SampleApp.Extensions.Pipelines.ProductDefinition
{
	/// <summary>
	/// Pipeline responsible for tasks associated with maintaining data on specific definition during app start
	/// </summary>
	public class AddShoeDefinitionPipeline : Pipeline<Ucommerce.EntitiesV2.ProductDefinition>
	{
		public AddShoeDefinitionPipeline(IPipelineTask<Ucommerce.EntitiesV2.ProductDefinition>[] tasks, ILoggingService loggingService) : base(tasks, loggingService)
		{
		}
	}
}
