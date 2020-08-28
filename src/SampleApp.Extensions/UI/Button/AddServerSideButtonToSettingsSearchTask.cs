using System;
using System.Linq;
using System.Web.UI.WebControls;
using Ucommerce;
using Ucommerce.Infrastructure.Globalization;
using Ucommerce.Pipelines;
using Ucommerce.Presentation;
using Ucommerce.Presentation.UI;
using Ucommerce.Search.Indexers;

namespace SampleApp.Extensions.UI.Button
{
	/// <summary>
	/// Adds a Server side button to the Settings search view. 
	/// </summary>
	/// <remarks>
	/// The button index everything from scratch. 
	/// </remarks>
	public class AddServerSideButtonToSettingsSearchTask : IPipelineTask<SectionGroup>
	{
		private readonly IScratchIndexer _scratchIndexer;
		private readonly IResourceManager _resourceManager;

		public AddServerSideButtonToSettingsSearchTask(IScratchIndexer scratchIndexer, IResourceManager resourceManager)
		{
			_scratchIndexer = scratchIndexer;
			_resourceManager = resourceManager;
		}

		public PipelineExecutionResult Execute(SectionGroup subject)
		{
			if (subject.GetViewName() != Constants.UI.Pages.Settings.Search) return PipelineExecutionResult.Success;
			
			//Finds the right section by filtering on OriginalName, using the Constants in uCommerce
			var section = subject.Sections.FirstOrDefault(s => s.OriginalName == Constants.UI.Sections.Settings.Search.Common);

			//If the view is not the one that we want to hook into, then do nothing
			if (section == null) return PipelineExecutionResult.Success;

			section.Menu.AddMenuButton(CreateServerSideButton());

			return PipelineExecutionResult.Success;
		}

		public ImageButton CreateServerSideButton()
		{
			var serverSideButton = new ImageButton();
			serverSideButton.ImageUrl = Ucommerce.Presentation.Resources.Images.Menu.Sort;
			serverSideButton.CausesValidation = false;

			//The client side command which executes on right click.
			var translatedConfirmText = _resourceManager.GetLocalizedText("SampleApp", "confirmScratchIndexing");
			serverSideButton.Attributes.Add("onclick", "if (confirm('" + translatedConfirmText + "')) { return true; } else return false;");

			//The server side command which executes on right click.
			serverSideButton.Click += IndexEverythingFromSratchMethod;
			return serverSideButton;
		}

		protected void IndexEverythingFromSratchMethod(object sender, EventArgs e)
		{
			_scratchIndexer.Index();		
		}
	}
}
