using System;
using System.Collections.Generic;

class TLV
{
    public string Id { get; set; }
    public string Length { get; set; }
    public string Value { get; set; }
    public string Name { get; set; }
}

class Program
{
    static void Main()
    {
        List<TLV> emvTags = new List<TLV>();

        emvTags.Add(new TLV { Id = "9F06", Length = "7", Value = "A0000000032010", Name = "Application Identifier (AID)" });
        emvTags.Add(new TLV { Id = "5F34", Length = "1", Value = "01", Name = "Application Transaction Counter (ATC)" });
        emvTags.Add(new TLV { Id = "9F27", Length = "1", Value = "80", Name = "Cryptogram Information Data" });
        emvTags.Add(new TLV { Id = "9F26", Length = "8", Value = "4657B7AAD0991B60", Name = "Application Cryptogram" });
        emvTags.Add(new TLV { Id = "9F10", Length = "7", Value = "06010A03A00800", Name = "Issuer Application Data" });
        emvTags.Add(new TLV { Id = "9F36", Length = "2", Value = "0007", Name = "Application Transaction Counter (ATC) (Extended)" });
        emvTags.Add(new TLV { Id = "9F02", Length = "6", Value = "000000000000", Name = "Amount, Authorized (Numeric)" });
        emvTags.Add(new TLV { Id = "9F03", Length = "6", Value = "000000000000", Name = "Amount, Other (Numeric)" });
        emvTags.Add(new TLV { Id = "9F1A", Length = "2", Value = "0414", Name = "Terminal Country Code" });
        emvTags.Add(new TLV { Id = "5F2A", Length = "2", Value = "0414", Name = "Transaction Currency Code" });
        emvTags.Add(new TLV { Id = "9F37", Length = "4", Value = "E44839E4", Name = "Unpredictable Number" });
        emvTags.Add(new TLV { Id = "9F33", Length = "3", Value = "604020", Name = "Terminal Capabilities" });
        emvTags.Add(new TLV { Id = "9F1E", Length = "8", Value = "3535353335323138", Name = "Interface Device (IFD) Serial Number" });

        // Specs Tags
        emvTags.Add(new TLV { Id = "57", Length = "11", Value = "4507788153899851D2411226100006740F", Name = "Track Data" });
        emvTags.Add(new TLV { Id = "5A", Length = "8" , Value = "4507788153899851", Name = "Application PAN" });
        emvTags.Add(new TLV { Id = "82", Length = "2" , Value = "3C00", Name = "Application Interchange Profile" });
        emvTags.Add(new TLV { Id = "8C", Length = "15", Value = "9F02069F03069F1A0295055F2A029A039C019F3704", Name = "Card Risk Management Data" });
        emvTags.Add(new TLV { Id = "95", Length = "5" , Value = "8000048000", Name = "Terminal Verification Results (TVR)" });
        emvTags.Add(new TLV { Id = "9A", Length = "3" , Value = "210524", Name = "Transaction Date" });
        emvTags.Add(new TLV { Id = "9C", Length = "1" , Value = "01", Name = "Transaction Type" });
        emvTags.Add(new TLV { Id = "9B", Length = "2" , Value = "6800", Name = "Transaction Status Information" });
        emvTags.Add(new TLV { Id = "50", Length = "10", Value = "5669736120456C656374726F6E204449", Name = "Application Label" });


        // Print the list of TLV objects
        foreach (var tag in emvTags)
        {
            Console.WriteLine($"Tag ID: {tag.Id}");
            Console.WriteLine($"Tag Length: {tag.Length}");
            Console.WriteLine($"Tag Value: {tag.Value}");
            Console.WriteLine($"Tag Name: {tag.Name}\n");
        }
        Console.ReadKey();
    }
}
