using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

public class InternetNetwork
{
    [Key]
    public int ID { get; set; }

    [AllowNull]
    public string Code1 { get; set; }
    [AllowNull]
    public string NetworkName1 { get; set; }
    [AllowNull]
    public string Code2 { get; set; }
    [AllowNull]
    public string NetworkName2 { get; set; }
    [AllowNull]
    public string Code3 { get; set; }
    [AllowNull]
    public string NetworkName3 { get; set; }
    [AllowNull]
    public string Code4 { get; set; }
    [AllowNull]
    public string NetworkName4 { get; set; }
    [AllowNull]
    public string Code5 { get; set; }
    [AllowNull]
    public string NetworkName5 { get; set; }
}
