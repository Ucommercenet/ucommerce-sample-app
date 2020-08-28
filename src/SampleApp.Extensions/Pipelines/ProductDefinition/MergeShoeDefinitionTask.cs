using System.Linq;
using Ucommerce.EntitiesV2;
using Ucommerce.EntitiesV2.Definitions;
using Ucommerce.Pipelines;
using Ucommerce.Security;

namespace SampleApp.Extensions.Pipelines.ProductDefinition
{
	/// <summary>
	/// Merge the new ProductDefinition with existing ProductDefinition 
	/// if ProductDefinition.Name exist. 
	/// </summary>
	public class MergeShoeDefinitionTask : IPipelineTask<Ucommerce.EntitiesV2.ProductDefinition>
	{
		private readonly IRepository<Ucommerce.EntitiesV2.ProductDefinition> _productDefinitionRepository;
		private readonly IPipeline<IDefinition> _saveDefinitionPipeline;

		public MergeShoeDefinitionTask(IRepository<Ucommerce.EntitiesV2.ProductDefinition> productDefinitionRepository,
			IPipeline<IDefinition> saveDefinitionPipeline)
		{
			_productDefinitionRepository = productDefinitionRepository;
			_saveDefinitionPipeline = saveDefinitionPipeline;
		}

		public PipelineExecutionResult Execute(Ucommerce.EntitiesV2.ProductDefinition subject)
		{
			var existingShoeDefinition = _productDefinitionRepository.Select().FirstOrDefault(x => x.Name == subject.Name);

			if (existingShoeDefinition == null) return PipelineExecutionResult.Success;

			MergeProductDefinitionFields(existingShoeDefinition, subject);

			_saveDefinitionPipeline.Execute(existingShoeDefinition);

			return PipelineExecutionResult.Success;
		}

		/// <summary>
		/// It's responsible for merging the new productDefinitionFields into the existing ProductDefinition.
		/// If the new ProductDefinitionfield isn't already present in the existing ProductDefinition.  
		/// </summary>
		/// <param name="existingShoeDefinition"></param>
		/// <param name="newProductDefinition"></param>
		private void MergeProductDefinitionFields(Ucommerce.EntitiesV2.ProductDefinition existingShoeDefinition, Ucommerce.EntitiesV2.ProductDefinition newProductDefinition)
		{
			foreach (var productDefinitionField in newProductDefinition.ProductDefinitionFields)
			{
				if (ProductDefinitionfieldExist(existingShoeDefinition, productDefinitionField)) continue;

				EnsureFieldNameIsUnique(existingShoeDefinition, productDefinitionField);

				existingShoeDefinition.AddProductDefinitionField(productDefinitionField);
			}
		}

		/// <summary>
		/// Updates the name on the new ProductDefinitionField if a existing productDefintionField has the same name. 
		/// </summary>
		/// <param name="existingShoeDefinition"></param>
		/// <param name="productDefinitionField"></param>
		private void EnsureFieldNameIsUnique(Ucommerce.EntitiesV2.ProductDefinition existingShoeDefinition, ProductDefinitionField productDefinitionField)
		{
			if (existingShoeDefinition.ProductDefinitionFields.Any(x => x.Name == productDefinitionField.Name && !x.Deleted))
			{
				productDefinitionField.Name = string.Format("{0}_{1}", productDefinitionField.Name,
					productDefinitionField.DataType.DefinitionName);
			}
		}

		/// <summary>
		/// True if there is a productDefintionField with same Name and DateType which isn't deleted.
		/// </summary>
		/// <param name="existingShoeDefinition"></param>
		/// <param name="productDefinitionField"></param>
		/// <returns></returns>
		private static bool ProductDefinitionfieldExist(Ucommerce.EntitiesV2.ProductDefinition existingShoeDefinition, ProductDefinitionField productDefinitionField)
		{
			return existingShoeDefinition.ProductDefinitionFields.Any(x => x.Name == productDefinitionField.Name && x.DataType.DataTypeId == productDefinitionField.DataType.DataTypeId && !x.Deleted);
		}
	}
}
