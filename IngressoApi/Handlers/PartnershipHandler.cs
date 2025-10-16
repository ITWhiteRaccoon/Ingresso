using Microsoft.AspNetCore.WebUtilities;

namespace IngressoApi.Handlers
{
public class PartnershipHandler : DelegatingHandler
{
    private readonly string _partnership;

    public PartnershipHandler(string partnership)
    {
        _partnership = partnership;
        // Set the default inner handler
        InnerHandler = new HttpClientHandler(); 
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Use QueryHelpers to safely append the partnership parameter to the request's URI
        var uriWithParam = QueryHelpers.AddQueryString(request.RequestUri.ToString(), "partnership", _partnership);

        request.RequestUri = new Uri(uriWithParam);
            
        // Pass the modified request to the next handler in the chain
        return base.SendAsync(request, cancellationToken);
    }
}
}