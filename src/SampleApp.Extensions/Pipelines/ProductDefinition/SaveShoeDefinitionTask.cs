using System.Linq;
using Ucommerce.EntitiesV2;
using Ucommerce.EntitiesV2.Definitions;
using Ucommerce.Pipelines;

namespace SampleApp.Extensions.Pipelines.ProductDefinition
{
	/// <summary>
	/// Save the new ProductDefintion 
	/// if ProductDefinition.Name dosn't exist. 
	/// </summary>
	public class SaveShoeDefinitionTask : IPipelineTask<Ucommerce.EntitiesV2.ProductDefinition>
	{
		private readonly IRepository<Ucommerce.EntitiesV2.ProductDefinition> _productDefinitionRepository;
		private readonly IPipeline<IDefinition> _saveDefinitionPipeline;

		public SaveShoeDefinitionTask(IRepository<Ucommerce.EntitiesV2.ProductDefinition> productDefinitionRepository,
			IPipeline<IDefinition> saveDefinitionPipeline)
		{
			_productDefinitionRepository = productDefinitionRepository;
			_saveDefinitionPipeline = saveDefinitionPipeline;
		}

		public PipelineExecutionResult Execute(Ucommerce.EntitiesV2.ProductDefinition subject)
		{
			if (_productDefinitionRepository.Select().Any(x => x.Name == subject.Name)) return PipelineExecutionResult.Success;
						
			_saveDefinitionPipeline.Execute(subject);

			return PipelineExecutionResult.Success;
		}
	}
}
