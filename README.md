# mutiversion_assemblies_in_functions
This code demonstrates how references to multiple versions of an assembly in an Azure Functions project can cause unexpected and irrelevant exceptions while executing a function. 

NOTE: This solution has been tested on a local machine only. A simillar behavior is expected when hosted in Azure. Please verify.

The solution contains two libraries: Lib55 and Lib56.
Lib55 refrences Microsoft.IdentityModel.Protocols.OpenIdConnect version 5.5, and Lib56 references Microsoft.IdentityModel.Protocols.OpenIdConnect version 5.6.

In order to reproduce the problem, start project FunctionTests and call any endpoint that makes a call to Lib55 first. There are two such endpoints: CallLib55 and MixedCalls1.
Once the exception is thrown, any subsequent calls to Lib55 from any anedpoint will throw the exception, but all calls to Lib56 will go through successfully.

The thrown exception is: System.MissingMethodException: Method not found: 'System.Collections.Generic.ICollection`1<Microsoft.IdentityModel.Tokens.SecurityKey> Microsoft.IdentityModel.Protocols.OpenIdConnect.OpenIdConnectConfiguration.get_SigningKeys()'.

In order to prevent the exception, right after starting project,  FunctionTests,call an endpoint which calls Lib56 first. Such endpoints are CallLib56 and MixedCalls2. Once any of these endpoints is called, all subsequent calls to both Lib55 and Lib56 will go through successfully.

It is important to note that the presence of the following line of code is necessary to always replicate the problem successfully:

var assembly = Assembly.GetAssembly(typeof(OpenIdConnectConfiguration));

The placement of this line of code in the source code of the function does not really mater, even if it appears after call to a method in Lib55, which causes the exception. (currently it is in the catch block).
However, there have been cases that even without that line of code, the exception has thrown, but this happens randomely. 



