using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Ucommerce;
using Ucommerce.EntitiesV2.Queries.Orders;
using Ucommerce.Pipelines;
using Ucommerce.Presentation.UI;

namespace SampleApp.Extensions.UI.Button
{
	public class AddChangeOrderStatusButtonTask : IPipelineTask<SectionGroup>
	{
		private readonly IJavaScriptFactory _javaScriptFactory;

		public AddChangeOrderStatusButtonTask(IJavaScriptFactory javaScriptFactory)
		{
			_javaScriptFactory = javaScriptFactory;
		}

		public PipelineExecutionResult Execute(SectionGroup subject)
		{
			if (subject.GetViewName() != Constants.UI.Pages.Orders.OrderGroup) return PipelineExecutionResult.Success;

			//Finds the right section by filtering on OriginalName, using the Constants in uCommerce
			var section = subject.Sections.FirstOrDefault(s => s.OriginalName == Constants.UI.Sections.Orders.OrderGroup.Common);

			//If the view is not the one that we want to hook into, then do nothing
			if (section == null) return PipelineExecutionResult.Success;

			section.Menu.AddMenuButton(CreateServerSideButton(section));

			return PipelineExecutionResult.Success;
		}

		public ImageButton CreateServerSideButton(Section section)
		{
			var serverSideButton = new ImageButton();
			serverSideButton.ImageUrl = Ucommerce.Presentation.Resources.Images.Menu.Properties;
			serverSideButton.CausesValidation = false;

			//The server side command which executes when clicked.
			serverSideButton.Click += (sender, args) =>
			{
				var checkedPurchaseOrderIds = new StringBuilder();

				var repeater = (Repeater) section.Controls[0].FindControl("OrdersRepeater");
				var items = (IList<PurchaseOrderSummary>) repeater.DataSource;

				foreach (RepeaterItem item in repeater.Items)
				{
					var checkbox = item.FindControl("purchaseOrders") as CheckBox;
					if (checkbox.Checked)
					{
						PurchaseOrderSummary purchaseOrderSummary = items[item.ItemIndex];
						var id = purchaseOrderSummary.OrderId;
						checkedPurchaseOrderIds.Append(id);
						checkedPurchaseOrderIds.Append(",");
					}
				}

				if (checkedPurchaseOrderIds.Length > 0)
				{
					checkedPurchaseOrderIds.Remove(checkedPurchaseOrderIds.Length - 1, 1);

					// Registrerer JavaScript på web siden
					section.Page.ClientScript.RegisterClientScriptBlock(
						GetType(),
						"refresh",
						_javaScriptFactory.CreateJavascript(
							_javaScriptFactory.OpenModalFunction(string.Format("/orders/changeorderstatus.aspx?name={0}&parentUrl={1}",
								checkedPurchaseOrderIds, section.Page.Request.Url.PathAndQuery), "", 500, 275)));
				}
				else
				{
					// Show alert
					section.Page.ClientScript.RegisterClientScriptBlock(
						GetType(),
						"refresh",
						_javaScriptFactory.CreateJavascript(string.Format("alert('{0}');", "No order(s) selected")));
				}
			};

			return serverSideButton;
		}
	}
}
