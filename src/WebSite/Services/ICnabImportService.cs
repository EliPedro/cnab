using System.IO;
using System.Threading.Tasks;

namespace WebSite.Services;

public interface ICnabImportService
{
    /// <summary>
    /// Imports CNAB content from the provided stream. The stream will not be disposed by this method.
    /// Returns a brief summary of the import.
    /// </summary>
    Task<ImportResult> ImportFromStreamAsync(Stream stream);
}

public record ImportResult(int TotalLines, int ImportedTransactions, int NewStores, int ParseErrors);