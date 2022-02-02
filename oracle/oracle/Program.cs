// This is an automatically generated code sample. 
// To make this code sample work in your Oracle Cloud tenancy, 
// please replace the values for any parameters whose current values do not fit
// your use case (such as resource IDs, strings containing ‘EXAMPLE’ or ‘unique_id’, and 
// boolean, number, and enum parameters with values not fitting your use case).

using System;
using System.Threading.Tasks;
using Oci.IdentityService;
using Oci.Common;
using Oci.Common.Auth;

namespace Oci.Sdk.DotNet.Example.Identity
{
    public class ListAvailabilityDomainsExample
    {
        public static async Task Main()
        {
            // Create a request and dependent object(s).
            var listAvailabilityDomainsRequest = new Oci.IdentityService.Requests.ListAvailabilityDomainsRequest
            {
                CompartmentId = "ocid1.test.oc1..<unique_ID>EXAMPLE-compartmentId-Value"
            };

            // Create a default authentication provider that uses the DEFAULT
            // profile in the configuration file.
            // Refer to <see href="https://docs.cloud.oracle.com/en-us/iaas/Content/API/Concepts/sdkconfig.htm#SDK_and_CLI_Configuration_File>the public documentation</see> on how to prepare a configuration file. 
            var provider = new ConfigFileAuthenticationDetailsProvider("DEFAULT");
            try
            {
                // Create a service client and send the request.
                using (var client = new IdentityClient(provider, new ClientConfiguration()))
                {
                    var response = await client.ListAvailabilityDomains(listAvailabilityDomainsRequest);
                    // Retrieve value from the response.
                    var availabilityDomainValueCount = response.Items.Count;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"ListAvailabilityDomains Failed with {e.Message}");
                throw e;
            }
        }

    }
}