using Xunit;
using Xunit.Abstractions;

namespace Csv2Sql.Tests;

public class CsvShould
{
    private readonly ITestOutputHelper _output;

    public CsvShould(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void ParseLine_1()
    {
        var line = "\" oj \",1234,\"Hejsan, hoppsan\",,filliflopp,\"hello\"";
        var actual = Csv.ParseLine(line, ',');
        var expected = new[] {"oj", "1234", "Hejsan, hoppsan", "", "filliflopp", "hello"};

        Assert.Equal(expected.Length, actual.Length);
        Assert.Equal(expected[0], actual[0]);
        Assert.Equal(expected[1], actual[1]);
        Assert.Equal(expected[2], actual[2]);
        Assert.Equal(expected[3], actual[3]);
        Assert.Equal(expected[4], actual[4]);
        Assert.Equal(expected[5], actual[5]);
    }

    [Fact]
    public void ParseLine_2()
    {
        var line = "1111111,2222222,01234567-8,Testtest,Testtest AB - Sthlm,33333,,44444,test@test.com,1";
        var actual = Csv.ParseLine(line, ',');
        var expected = new[]
        {
            "1111111", "2222222", "01234567-8", "Testtest", "Testtest AB - Sthlm", "33333", "", "44444",
            "test@test.com", "1"
        };

        Assert.Equal(expected.Length, actual.Length);
        Assert.Equal(expected[0], actual[0]);
        Assert.Equal(expected[1], actual[1]);
        Assert.Equal(expected[2], actual[2]);
        Assert.Equal(expected[3], actual[3]);
        Assert.Equal(expected[4], actual[4]);
        Assert.Equal(expected[5], actual[5]);
        Assert.Equal(expected[6], actual[6]);
        Assert.Equal(expected[7], actual[7]);
        Assert.Equal(expected[8], actual[8]);
        Assert.Equal(expected[9], actual[9]);
    }

    [Fact]
    public void ParseLine_3()
    {
        var line = "123123123,232323232,3232323-5,Hej Svej Finland Oy,,22111233,\"\",2233111,,2";
        var actual = Csv.ParseLine(line, ',');
        var expected = new[]
        {
            "123123123", "232323232", "3232323-5", "Hej Svej Finland Oy", "", "22111233", "", "2233111",
            "", "2"
        };

        Assert.Equal(expected.Length, actual.Length);
        Assert.Equal(expected[0], actual[0]);
        Assert.Equal(expected[1], actual[1]);
        Assert.Equal(expected[2], actual[2]);
        Assert.Equal(expected[3], actual[3]);
        Assert.Equal(expected[4], actual[4]);
        Assert.Equal(expected[5], actual[5]);
        Assert.Equal(expected[6], actual[6]);
        Assert.Equal(expected[7], actual[7]);
        Assert.Equal(expected[8], actual[8]);
        Assert.Equal(expected[9], actual[9]);
    }

    [Fact(Skip = "TODO")]
    public void ParseLine_4()
    {
        var line = ",\"\",\",\",";
        var actual = Csv.ParseLine(line, ',');
        var expected = new[]
        {
            "", "", ",", ""
        };
        
        //_output.WriteLine($"[{string.Join(" | ", expected)}]");
        //_output.WriteLine($"[{string.Join(" | ", actual)}]");

        Assert.Equal(expected.Length, actual.Length);
        Assert.Equal(expected[0], actual[0]);
        Assert.Equal(expected[1], actual[1]);
        Assert.Equal(expected[2], actual[2]);
        Assert.Equal(expected[3], actual[3]);
    }
}