using System;
using System.Collections.Generic;
using System.Text;
using WebSite.Entities;
using Xunit;

namespace UnitTest;

public class CnabParseTests
{

    [Fact(DisplayName = "Invalid CNAB file")]
    public void InvalidParse()
    {
        // Arrange
        string cnabData = "Sample CNAB data";
        var parser = new ParseUploadedFile(cnabData);
        // Act
        var result = parser.Parse();
        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsFailure);
        Assert.Equal("Invalid CNAB file line.", result.Error.Message);
    }

    [Theory(DisplayName = "Successful CNAB file parse")]
    [InlineData("3201903010000014200096206760174753****3153153453JOÃO MACEDO   BAR DO JOÃO  ", "JOÃO MACEDO")]
    [InlineData("5201903010000013200556418150633123****7687145607MARIA JOSEFINALOJA DO Ó - MATRIZ", "MARIA JOSEFINA")]
    [InlineData("3201903010000012200845152540736777****1313172712MARCOS PEREIRAMERCADO DA AVENIDA", "MARCOS PEREIRA")]
    public void SuccessfulParse(string line, string owner)
    {
        var parser = new ParseUploadedFile(line);
        var result = parser.Parse();

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Equal(owner, result.Value.StoreOwner);
    }
}
