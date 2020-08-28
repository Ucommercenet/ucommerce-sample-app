using System;
using Ucommerce.Pipelines;
using Ucommerce.Pipelines.Initialization;

namespace SampleApp.Extensions.Pipelines.Initialize
{
	/// <summary>
	/// Responsible for executing the addShoeDefinitionPipeline in the Initialize pipeline.
	/// </summary>
	public class ExecuteAddShoeDefinitionPipelineTask : IPipelineTask<InitializeArgs>
	{
		private readonly Pipeline<Ucommerce.EntitiesV2.ProductDefinition> _addShoeDefinitionPipeline;

		public ExecuteAddShoeDefinitionPipelineTask(Pipeline<Ucommerce.EntitiesV2.ProductDefinition> addShoeDefinitionPipeline)
		{
			_addShoeDefinitionPipeline = addShoeDefinitionPipeline;
		}

		public PipelineExecutionResult Execute(InitializeArgs subject)
		{
			//Run pipeline as multiple steps are needed. See 'ShoeDefinitionPipeline.config' for steps included.
			return _addShoeDefinitionPipeline.Execute(new Ucommerce.EntitiesV2.ProductDefinition());
		}
	}
}
