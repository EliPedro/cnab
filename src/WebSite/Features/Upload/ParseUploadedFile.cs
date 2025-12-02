using System.Globalization;

namespace WebSite.Features.Upload;

public readonly struct ParseUploadedFile(string line)
{
    public UploadCommand Parse()
    {
        if (string.IsNullOrWhiteSpace(line))
            throw new ArgumentException("Invalid CNAB file line.");

        int typeCode = int.Parse(line[..1]);
        string dateString = line.Substring(1, 8);
        string valueString = line.Substring(9, 10);
        string cpf = line.Substring(19, 11).Trim();
        string card = line.Substring(30, 12).Trim();
        string timeString = line.Substring(42, 6);
        string storeOwner = line.Substring(48, 14).Trim();
        string storeName = line.Substring(62, 18).Trim();

        var date = DateTime.ParseExact(dateString, "yyyyMMdd", CultureInfo.InvariantCulture);
        var time = DateTime.ParseExact(timeString, "HHmmss", CultureInfo.InvariantCulture);
        var value = decimal.Parse(valueString) / 100;

        return new UploadCommand
        {
            TypeCode = typeCode,
            Date = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second),
            Value = value,
            Cpf = cpf,
            Card = card,
            StoreOwner = storeOwner,
            StoreName = storeName
        };

    }
}
