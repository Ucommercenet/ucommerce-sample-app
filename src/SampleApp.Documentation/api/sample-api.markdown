# Sample API

The Sample app comes with simple API called ``SampleApp.Extensions.Api.SampleApi``, the sample api has two methods. 

The first is ``SampleApi.uCommerceVersion()`` and it returns the the version of uCommerce the application uses as a string, as you can see below.

{CODE-START:csharp /}
public static string uCommerceVersion()
{
	string binPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
	var assemblyName = AssemblyName.GetAssemblyName(string.Format("{0}\\uCommerce.dll", binPath));
	return assemblyName.Version.ToString();
}
{CODE-END /}

The second method is ``SampleApi.SchemaVersion()`` which executes a query to get schema version of the the database and the method returns the schema version as a string, as you can see below.

{CODE-START:csharp /}
public static string SchemaVersion()
{
	var sessionProvider = ObjectFactory.Instance.Resolve<ISessionProvider>();
			
	//Don't use the using(var session = _sessionProvider.GetSession()) pattern as the session will be disposed at the end of the http request AND
	//Disposing the session here will cause trouble as lazy loading for all entities will then be broken.
	var session = sessionProvider.GetSession();

	return new SchemaVersionQuery().Execute(session).FirstOrDefault().ToString();
}
{CODE-END /}