using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System.Threading;
using System.Threading.Tasks;

namespace Grocery.Data.Interfaces {
  public interface ICosmosDbClient {
    Task<Document> ReadDocumentAsync(string collection, string documentId, RequestOptions options = null, CancellationToken cancellationToken = default);
    Task<Document> CreateDocumentAsync(string collection, object document, RequestOptions options = null, bool disableAutomaticIdGeneration = false, CancellationToken cancellationToken = default);
    Task<Document> ReplaceDocumentAsync(string collection, string documentId, object document, RequestOptions options = null, CancellationToken cancellationToken = default);
    Task<Document> DeleteDocumentAsync(string collection, string documentId, RequestOptions options = null, CancellationToken cancellationToken = default);
  }
}
