//using System;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using WebSite.Database;
//using WebSite.Entities;

//namespace WebSite.Services;

//public class CnabImportService : ICnabImportService
//{
//    private readonly ApplicationDbContext _db;
//    private readonly ILogger<CnabImportService> _logger;

//    public CnabImportService(ApplicationDbContext db, ILogger<CnabImportService> logger)
//    {
//        _db = db ?? throw new ArgumentNullException(nameof(db));
//        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
//    }

//    public async Task<ImportResult> ImportFromStreamAsync(Stream stream)
//    {
//        if (stream == null) throw new ArgumentNullException(nameof(stream));

//        using var reader = new StreamReader(stream);
//        var total = 0;
//        var imported = 0;
//        var newStores = 0;
//        var parseErrors = 0;

//        // We'll buffer additions and SaveChanges in batches to reduce DB pressure.
//        const int batchSize = 500;
//        var pending = 0;

//        while (!reader.EndOfStream)
//        {
//            var line = await reader.ReadLineAsync();
//            total++;

//            if (string.IsNullOrWhiteSpace(line)) continue;
//            if (line.Length < 80)
//            {
//                parseErrors++;
//                _logger.LogWarning("Skipping short line {LineNumber}", total);
//                continue;
//            }

//            try
//            {
//                var typeCode = int.Parse(line.Substring(0, 1));
//                var datePart = line.Substring(1, 8);
//                var valuePart = line.Substring(9, 10);
//                var cpf = line.Substring(19, 11).Trim();
//                var card = line.Substring(30, 12).Trim();
//                var timePart = line.Substring(42, 6);
//                var owner = line.Substring(48, 14).Trim();
//                var storeName = line.Substring(62, 19).Trim();

//                var dateTime = DateTime.ParseExact(datePart + timePart, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
//                var cents = long.Parse(valuePart);
//                var amount = cents / 100m;

//                var store = await _db.Stores.SingleOrDefaultAsync(s => s.Name == storeName && s.Owner == owner);
//                if (store == null)
//                {
//                    store = new Store(Guid.NewGuid(), storeName, owner);
//                    _db.Stores.Add(store);
//                    newStores++;
//                }

//                var tx = new Transaction
//                {
//                    StoreId = store.Id,
//                    TypeCode = typeCode,
//                    Date = dateTime,
//                    Amount = amount,
//                    Cpf = cpf,
//                    Card = card
//                };

//                _db.Transactions.Add(tx);
//                pending++;
//                imported++;

//                if (pending >= batchSize)
//                {
//                    await _db.SaveChangesAsync();
//                    pending = 0;
//                }
//            }
//            catch (Exception ex)
//            {
//                parseErrors++;
//                _logger.LogWarning(ex, "Failed to parse line {LineNumber}", total);
//            }
//        }

//        if (pending > 0)
//            await _db.SaveChangesAsync();

//        return new ImportResult(total, imported, newStores, parseErrors);
//    }
//}
